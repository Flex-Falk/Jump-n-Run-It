import random

from UdpComms import UdpComms
import time
import random
import pandas
import torch
#import scikit-learn
from LSTM import load_lstm_from_file


# Serial.printf("IMU:%f,%f,%f,%f,%f,%f;",
#   receive_msg.data.accel.x, receive_msg.data.accel.y, receive_msg.data.accel.z,
#   receive_msg.data.gyro.yaw, receive_msg.data.gyro.pitch, receive_msg.data.gyro.roll);
def parse_imu_data(string):
    return list(map(lambda x: float(x), string.split(","))) # to use in LSTM we give it a batch_size of 1, so wrap with array

def run_server():
    # load model
    do_predict = load_lstm_from_file("gesture_model.pth", "label_encoder.pkl", "scaler.pkl") # set correct files later

    # https://github.com/Siliconifier/Python-Unity-Socket-Communication
    # Create UDP socket to use for sending (and receiving)
    print("Server is running\n", "Wait for client...\n")
    sock = UdpComms(udpIP="127.0.0.1", portTX=8000, portRX=8001, enableRX=True, suppressWarnings=True)

    while True:
        data = sock.ReadReceivedData() # read data

        if data != None: # if NEW data has been received since last ReadReceivedData function call
            # print(data) # print new received data -> here we need to parse and eval and send it back
            prediction = do_predict([parse_imu_data(data)])
            sock.SendData(prediction[0])  # Send prediction to unity

        time.sleep(0.00075) # 7.5ms sample rate 15ms/2


run_server()