import pandas as pd
import glob
import seaborn as sns
import matplotlib.pyplot as plt
import matplotlib as mpl
import os
import fnmatch

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
def plot_imu_data(df, title):
    fig, axes = plt.subplots(2, 3, figsize=(15, 10), picker=True)

    y_vars = df.columns.tolist()[1:]

    for ax, y_var in zip(axes.flatten(), y_vars):
        sns.scatterplot(data=df, x="Time", y=y_var, ax=ax, hue="Action")
        ax.set_title(f'delta vs {y_var}')

    plt.tight_layout()
    plt.title(label=title)
    plt.show()

def plot_each_seq():
    files = list_paths(pattern='*.csv')
    for file in files:
        print(file)
        pdFrame = pd.read_csv(file, sep=",");
        groupedDf = pdFrame.groupby(pdFrame.Type)
        df_gyro = groupedDf.get_group("Gyro")
        df_accel = groupedDf.get_group("Accel")
        print(pdFrame)
        print(df_gyro)
        print(df_accel)
        plot_imu_data(df_gyro, file + " - gyro")
        plot_imu_data(df_accel, file + " - accel")

plot_each_seq()