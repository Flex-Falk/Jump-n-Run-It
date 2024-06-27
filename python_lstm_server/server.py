import random

from UdpComms import UdpComms
import time
import random
from LSTM import load_lstm_from_file


def parse_imu_data(string):
    return list(map(lambda x: float(x), string.split(","))) # to use in LSTM we give it a batch_size of 1, so wrap with array

def run_server():
    # load model
    do_predict = load_lstm_from_file("weight_file.pth", "label_encode.pkl", "scaler.pkl") # set correct files later

    # https://github.com/Siliconifier/Python-Unity-Socket-Communication
    # Create UDP socket to use for sending (and receiving)
    print("Server is running\n", "Wait for client...\n")
    sock = UdpComms(udpIP="127.0.0.1", portTX=8000, portRX=8001, enableRX=True, suppressWarnings=True)
    print("Client connected")

    while True:
        data = sock.ReadReceivedData() # read data

        if data != None: # if NEW data has been received since last ReadReceivedData function call
            print(data) # print new received data -> here we need to parse and eval and send it back
            prediction = do_predict([parse_imu_data(data)])
            sock.SendData(prediction[0])  # Send prediction to unity

        time.sleep(0.00075) # 7.5ms sample rate 15ms/2


print(parse_imu_data("-898.0,-12807.0,2671.0,-5.888142,-3.370853,-4.061652"))
#run_server()