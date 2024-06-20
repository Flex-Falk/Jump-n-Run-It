import pandas as pd
import glob
import seaborn as sns
import matplotlib.pyplot as plt
import matplotlib as mpl
import os
import fnmatch
import mplcursors
from fakeData import fakeImuData


def list_paths(folder='.', pattern='*', case_sensitive=False, subfolders=False):
    """Return a list of the file paths matching the pattern in the specified
    folder, optionally including files inside subfolders.
    """
    match = fnmatch.fnmatchcase if case_sensitive else fnmatch.fnmatch
    walked = os.walk(folder) if subfolders else [next(os.walk(folder))]
    return [os.path.join(root, f)
            for root, dirnames, filenames in walked
            for f in filenames if match(f, pattern)]

def save(df, index_collection, title):
    for index in index_collection:
        df.loc[index, "Action"] = "Crouch"

    output_csv_file_path = f'./relabeled/{title}.csv'
    df.to_csv(output_csv_file_path, index=False)

def plot_imu_data(df, df_root, title):
    df = df.copy()
    fig, axes = plt.subplots(2, 3, figsize=(15, 10), picker=True)
    fig.suptitle(title, fontsize=12)
    y_vars = df.columns.tolist()

    index_collection = []
    for ax, y_var in zip(axes.flatten(), y_vars):
        sc = sns.scatterplot(data=df, x=range(0, df.shape[0]), y=y_var, ax=ax, hue="action")
        ax.set_title(f'index vs {y_var}')
        cursor = mplcursors.cursor(sc)

        @cursor.connect("add")
        def on_click(sel):
            index = sel.index # something about the index does not work
            index_collection.append(index)
            print(f'Selected: Index {index}, Value {df.iloc[index].tolist()}')

    plt.tight_layout()
    #plt.title(label=title)
    plt.show()

    #save(df_root, index_collection, title)

def clear_action(df):
    df["Action"] = "Neutral"

def plot_each_seq():
    files = list_paths(folder=".", pattern='*.csv')
    for file in files:
        pdFrame = pd.read_csv(file, sep=",");
        groupedDf = pdFrame.groupby(pdFrame.Type)
        df_gyro = groupedDf.get_group("Gyro")
        #clear_action(df_gyro)
        df_accel = groupedDf.get_group("Accel")
        #clear_action(df_accel)
        plot_imu_data(df_gyro, pdFrame, file + " - gyro")
        plot_imu_data(df_accel, pdFrame, file + " - accel")

def plot_gyro_accel():
    files = list_paths(folder="./relabeled/", pattern='*.csv')
    for file in files:
        print(file)
        pdFrame = pd.read_csv(file, sep=",");
        groupedDf = pdFrame.groupby(pdFrame.Type)
        try:
            df_gyro = groupedDf.get_group("Gyro")
            plot_imu_data(df_gyro, pdFrame, file + " - gyro")
        except:
            df_accel = groupedDf.get_group("Accel")
            plot_imu_data(df_accel, pdFrame, file + " - accel")

#plot_each_seq()
#plot_gyro_accel()

def convert_pdrames(file):
    pdFrame = pd.read_csv(file, sep=",");
    mergePd = pd.DataFrame(columns=["x", "y", "z", "yaw", "pitch", "roll", "action"])
    print("Merged size expected to be:", pdFrame.shape[0]/2)
    for i in range(0, pdFrame.shape[0], 2):
        if(i > pdFrame.shape[0] or i+1 > pdFrame.shape[0]):
            break
        accel_index = -1
        gyro_index = -1
        action = "Neutral"
        try:
            if pdFrame.loc[i]["Type"] == "Accel":
                accel_index = i
            elif pdFrame.loc[i+1]["Type"] == "Accel":
                accel_index = i+1
            if pdFrame.loc[i]["Type"] == "Gyro":
                gyro_index = i
            elif pdFrame.loc[i+1]["Type"] == "Gyro":
                gyro_index = i+1
        except:
            pass
        if accel_index == -1 or gyro_index == -1:
            print("continue")
            continue
        if pdFrame.loc[i]["Action"] == pdFrame.loc[i+1]["Action"]:
            action = pdFrame.loc[i]["Action"]

        row = [
            pdFrame.loc[accel_index]["x"],
            pdFrame.loc[accel_index]["y"],
            pdFrame.loc[accel_index]["z"],
            pdFrame.loc[gyro_index]["x"],
            pdFrame.loc[gyro_index]["y"],
            pdFrame.loc[gyro_index]["z"],
            action
        ]
        #print(row)
        mergePd.loc[int(i/2)] = row

    print(mergePd)
    return mergePd

def convertFiles(folder):
    filesToConvert = list_paths(folder, "*.csv")
    print(filesToConvert)
    for file in filesToConvert:
        print(file)
        convert_pdrames(file).to_csv("converted_" + file.split(".\\").pop(), index=False)

#convertFiles(".")

def plot_converted(folder):
    filesToConvert = list_paths(folder, "*.csv")
    print(filesToConvert)
    for file in filesToConvert:
        print(file)
        plot_imu_data(pd.read_csv(file), None, file)

plot_converted(".")