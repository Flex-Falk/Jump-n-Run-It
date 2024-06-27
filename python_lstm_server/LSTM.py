import torch
import torch.nn as nn
import torch.optim as optim
import pandas as pd
from sklearn.preprocessing import StandardScaler, LabelEncoder
import joblib
import numpy
import pickle


class LSTMClassifier(nn.Module):
    def __init__(self, input_size, hidden_size, output_size, num_layers):
        super(LSTMClassifier, self).__init__()
        self.hidden_size = hidden_size
        self.num_layers = num_layers
        self.lstm = nn.LSTM(input_size, hidden_size, num_layers, batch_first=True)
        self.fc = nn.Linear(hidden_size, output_size)

    def forward(self, x, h0=None, c0=None):
        if h0 is None or c0 is None:
          h0 = torch.zeros(self.num_layers, x.size(0), self.hidden_size).to(x.device)
          c0 = torch.zeros(self.num_layers, x.size(0), self.hidden_size).to(x.device)

        out, _ = self.lstm(x, (h0, c0))
        out = out[:, -1, :]
        out = self.fc(out)
        return out

"""
joblib.dump(scaler, 'scaler.pkl')
joblib.dump(label_encoder, 'label_encoder.pkl')

https://pytorch.org/tutorials/beginner/basics/saveloadrun_tutorial.html
https://stackoverflow.com/questions/28656736/using-scikits-labelencoder-correctly-across-multiple-programs
https://stackoverflow.com/questions/53152627/saving-standardscaler-model-for-use-on-new-datasets
"""


def load_lstm_from_file(model_weights_file, label_encoder_pickle, scalar_pickle):
    label_encoder = LabelEncoder()

    label_encoder = pickle.load(open(label_encoder_pickle, 'rb'))  # LabelEncoder()
    scaler = pickle.load(open(scalar_pickle, 'rb'))  # StandardScaler()

    # Hyperparameter
    input_size = 6  # Anzahl der Features
    hidden_size = 128
    output_size = len(label_encoder.classes_)  # Crouch, Jump, Shoot, Neutral
    num_layers = 2

    model = LSTMClassifier(input_size, hidden_size, output_size, num_layers)
    model.load_state_dict(
        torch.load(model_weights_file)  # corresponds to torch.save(model.state_dict(), 'model_weights.pth')
    )

    # important, to set the dropout and batch normalization layers to evaluation mode.
    # Failing to do this will yield inconsistent inference results.
    model.eval()

    def predict_new_data(new_data):
        new_data_scaled = scaler.transform(new_data)
        new_data_tensor = torch.tensor(new_data_scaled, dtype=torch.float32).unsqueeze(1)

        with torch.no_grad():
            outputs = model(new_data_tensor)
            _, predicted = torch.max(outputs.data, 1)

        predicted_labels = label_encoder.inverse_transform(predicted.cpu().numpy())
        return predicted_labels

    return predict_new_data
