#include <esp_now.h>
#include <WiFi.h>

#include <Wire.h>
 
 
// REPLACE WITH THE MAC Address of your receiver 
uint8_t broadcastAddress[] = {0x10, 0x97, 0xBD, 0xD2, 0xD4, 0xEC}; // send to Master
//uint8_t broadcastAddress[] = {0x58, 0xBF, 0x25, 0x93, 0x39, 0xBC}; // send to IMU-ESP

typedef struct acceleration_msg {
  float acclx;
  float accly;
  float acclz;
} acceleration_msg;

acceleration_msg send_accl = {0.0, -9.84, 0.0};

acceleration_msg received_accl = {};

String success;

esp_now_peer_info_t peerInfo;

// Callback when data is sent
void OnDataSent(const uint8_t *mac_addr, esp_now_send_status_t status) {
  Serial.print("\r\nLast Packet Send Status:\t");
  Serial.println(status == ESP_NOW_SEND_SUCCESS ? "Delivery Success" : "Delivery Fail");
  if (status == 0){
    success = "Delivery Success :)";
  }
  else{
    success = "Delivery Fail :(";
  }
}

// Callback when data is received
void OnDataRecv(const uint8_t * mac, const uint8_t *incomingData, int len) {
  memcpy(&received_accl, incomingData, sizeof(received_accl));
  Serial.print("Bytes received: ");
  Serial.println(len);
  Serial.printf("x:%f, y:%f, z:%f", received_accl.acclx, received_accl.accly, received_accl.acclz);
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
 
void loop(){
  
  esp_err_t result = esp_now_send(broadcastAddress, (uint8_t *) &send_accl, sizeof(send_accl)); 
  if (result == ESP_OK) {
    Serial.println("Sent with success");
  }
  else {
    Serial.println("Error sending the data");
  }
  delay(1000);
}