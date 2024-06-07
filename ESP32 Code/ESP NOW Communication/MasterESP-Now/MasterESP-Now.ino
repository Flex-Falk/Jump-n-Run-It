#include <esp_now.h>
#include <WiFi.h>

#include <Wire.h>
 
 
// REPLACE WITH THE MAC Address of your receiver 
//uint8_t broadcastAddress[] = {0x10, 0x97, 0xBD, 0xD2, 0xD4, 0xEC}; // send to Master
uint8_t broadcastAddress[] = {0x58, 0xBF, 0x25, 0x93, 0x39, 0xBC}; // send to IMU-ESP

typedef enum msg_type_t {
  MSG_TYPE_ACCEL_GYRO         = (1),   /**< Gravity + linear acceleration */
  MSG_TYPE_TEXT               = (2),
};

typedef struct sensor_imu_t{
  union {
    /* Accel sensors */
    struct {
        float x;
        float y;
        float z;
    };
    /* Orientation sensors */
    struct {
        float yaw;
        float pitch;
        float roll;
    };
  };
};

typedef struct msg_data_t {
  union {
    sensor_imu_t accel;
    sensor_imu_t gyro;
  };
  struct {
    char* msg;
    int length;
  };
};

typedef struct msg_any_t {
  msg_type_t msgType;
  msg_data_t data;
};

// msgs between imus
msg_any_t send_msg = {};
msg_any_t receive_msg = {};

esp_now_peer_info_t peerInfo;

// Callback when data is sent
void OnDataSent(const uint8_t *mac_addr, esp_now_send_status_t status) {
  Serial.println(status == ESP_NOW_SEND_SUCCESS ? "Delivery Success" : "Delivery Fail");
}

// Callback when data is received
void OnDataRecv(const esp_now_recv_info* info, const uint8_t *incomingData, int len) {
  memcpy(&receive_msg, incomingData, sizeof(receive_msg));
  switch(receive_msg.msgType){
    case(MSG_TYPE_TEXT): {
      Serial.println(receive_msg.data.msg);
      break;
    }
    case(MSG_TYPE_ACCEL_GYRO): { 
      Serial.printf("Accel: x:%f, y:%f, z:%f", receive_msg.data.accel.x, receive_msg.data.accel.x, receive_msg.data.accel.x);
      Serial.printf("Gyro: yaw:%f, pitch:%f, roll:%f", receive_msg.data.gyro.yaw, receive_msg.data.gyro.pitch, receive_msg.data.gyro.roll);
      break;
    }
  }
}

void setup(){
  // Init Serial Monitor
  Serial.begin(115200); 
 
  // Set device as a Wi-Fi Station
  WiFi.mode(WIFI_STA);

  // Init ESP-NOW
  if (esp_now_init() != ESP_OK) {
    Serial.println("Error initializing ESP-NOW");
    return;
  }

  // Once ESPNow is successfully Init, we will register for Send CB to
  // get the status of Trasnmitted packet
  esp_now_register_send_cb(OnDataSent);
  
  // Register peer
  memcpy(peerInfo.peer_addr, broadcastAddress, 6);
  peerInfo.channel = 0;  
  peerInfo.encrypt = false;
  
  // Add peer        
  if (esp_now_add_peer(&peerInfo) != ESP_OK){
    Serial.println("Failed to add peer");
    return;
  }
  // Register for a callback function that will be called when data is received
  esp_now_register_recv_cb(OnDataRecv);

}

void send_to_imu(struct msg_any_t _send_msg){
  esp_err_t result = esp_now_send(broadcastAddress, (uint8_t *) &_send_msg, sizeof(_send_msg)); 
  if (result == ESP_OK) {
    Serial.println("Sent with success");
  }
  else {
    Serial.println("Error sending the data");
  }
}
 
void loop(){

}




