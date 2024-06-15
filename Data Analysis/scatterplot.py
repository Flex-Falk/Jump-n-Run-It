import pandas as pd
import glob
import seaborn as sns
import matplotlib.pyplot as plt
import matplotlib as mpl

from fakeData import fakeImuData

files = ["imu_jump.csv"]
template_path = r"..\Unity Code\{file}"

value = []
def plot_imu_data(df):
    fig, axes = plt.subplots(2, 3, figsize=(15, 10), picker=True)

    y_vars = df.columns.tolist()[1:]

    for ax, y_var in zip(axes.flatten(), y_vars):
        sns.scatterplot(data=df, x="delta", y=y_var, ax=ax, hue="label")
        ax.set_title(f'delta vs {y_var}')

    def onPick(e):
        print("pickevent scatter", e)
    fig.canvas.mpl_connect('pick_event', onPick)
    plt.tight_layout()
    plt.show()

def plot_each_seq():
    paths = [template_path.format(file=f) for f in files]
    for path in paths:
        pdFrame = pd.read_csv(path, sep=",");

plot_imu_data(fakeImuData(20))
print(value)