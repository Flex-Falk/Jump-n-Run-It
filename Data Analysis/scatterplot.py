import pandas as pd
import glob
import seaborn as sns
import matplotlib.pyplot as plt
import matplotlib as mpl
import os
import fnmatch
import mplcursors
from fakeData import fakeImuData

files = []
file = "export_2.csv"

value = []

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
    y_vars = df.columns.tolist()[1:]

    index_collection = []
    for ax, y_var in zip(axes.flatten(), y_vars):
        sc = sns.scatterplot(data=df, x="Time", y=y_var, ax=ax, hue="Action")
        ax.set_title(f'delta vs {y_var}')
        cursor = mplcursors.cursor(sc)

        @cursor.connect("add")
        def on_click(sel):
            index = sel.index # something about the index does not work
            index_collection.append(index)
            print(f'Selected: Index {index}, Value {df.iloc[index].tolist()}')

    plt.tight_layout()
    plt.title(label=title)
    plt.show()

    #save(df_root, index_collection, title)

def clear_action(df):
    df["Action"] = "Neutral"

def plot_each_seq():
    files = list_paths(pattern='*.csv')
    for file in files:
        print(file)
        pdFrame = pd.read_csv(file, sep=",");
        groupedDf = pdFrame.groupby(pdFrame.Type)
        df_gyro = groupedDf.get_group("Gyro")
        #clear_action(df_gyro)
        df_accel = groupedDf.get_group("Accel")
        #clear_action(df_accel)
        print(pdFrame)
        print(df_gyro)
        print(df_accel)
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

plot_each_seq()
#plot_gyro_accel()