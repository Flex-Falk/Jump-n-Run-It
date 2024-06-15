import random
import math
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt

def generate(_range, func, length):
    return [ func(i) + random.random() * _range[0] + random.random() * (_range[1] - 1) for i in range(length) ]


def fakeImuData(size):
    labels = ["nothing", "jump", "crouch", "punch"]
    cols = [
        ([-5, 6], lambda x: x, size), # delta time
        ([-0.3, 0.3], lambda x: math.sin(x), size), # yaw
        ([-0.5, 0.2], lambda x: math.sin(x), size), # pitch
        ([-0.1, 0.5], lambda x: math.sin(x), size), # roll
        ([-5, 5], lambda x: x, size), # x
        ([-5, 5], lambda x: x**2 , size), # y
        ([-5, 5], lambda x: (0.3 * x ** 3 + 0.5 * x ** 2 - 3.5 * x), size), # z
    ]
    header_row = ["delta", "yaw", "pitch", "roll", "accel_x", "accel_y", "accel_z"]
    data = {}
    for header, col in zip(header_row, cols) :
        data[header] = generate(*(col))
    data["label"] = [ labels[random.randint(0, len(labels) -1)] for i in range(size)]

    return pd.DataFrame(data)



