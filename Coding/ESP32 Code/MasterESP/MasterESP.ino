#include <esp_now.h>
#include <WiFi.h>

#include <Wire.h>

#include <Arduino.h>

//Pins for the different boards
#define LEFT_BOARD       39                                   // left board (aka VN pin)
#define MIDDLE_BOARD     34                                   // middle board
#define RIGHT_BOARD      35                                   // right board

#define DEBOUNCE_TIME    50                                   // the debounce time in millisecond, increase this time if it still chatters

#define PRESS_THRESHOLD   1                                  // Threshold for number of state changes to confirm a press;
                                                              // finetuning:
                                                              // higher: lower sensitivity aka more force for an input
                                                              // lower: higher sensitivity aka less force for an input
// Variables for the Left Board:
int lastSteadyState_Left = LOW;                               // the previous steady state from the input pin
int lastFlickerableState_Left = LOW;                          // the previous flickerable state from the input pin
int currentState_Left;                                        // the current reading from the input pin
unsigned long lastDebounceTime_Left = 0;                      // the last time the output pin was toggled
int pressCounter_Left = 0;                                    // how often a press was registered during the 

// Variables for the Middle Board:
int lastSteadyState_Middle = LOW;                             // the previous steady state from the input pin
int lastFlickerableState_Middle  = LOW;                       // the previous flickerable state from the input pin
int currentState_Middle ;                                     // the current reading from the input pin
unsigned long lastDebounceTime_Middle  = 0;                   // the last time the output pin was toggled
int pressCounter_Middle = 0;                                  // variable for press counter

// Variables for the Right Board:
int lastSteadyState_Right = LOW;                              // the previous steady state from the input pin
int lastFlickerableState_Right  = LOW;                        // the previous flickerable state from the input pin
int currentState_Right ;                                      // the current reading from the input pin
unsigned long lastDebounceTime_Right  = 0;                    // the last time the output pin was toggled
int pressCounter_Right = 0;                                   // variable for press counter
 
// REPLACE WITH THE MAC Address of your receiver 
//uint8_t broadcastAddress[] = {0x10, 0x97, 0xBD, 0xD2, 0xD4, 0xEC}; // send to Master
uint8_t broadcastAddress[] = {0x58, 0xBF, 0x25, 0x93, 0x06, 0x04}; // send to IMU-ESP
// // 58:bf:25:93:06:04

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
    struct {
      sensor_imu_t accel;
      sensor_imu_t gyro;
    };
    struct {
      char* msg;
      int length;
    };
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
void OnDataRecv(const esp_now_recv_info * info, const uint8_t *incomingData, int len) {
  memcpy(&receive_msg, incomingData, sizeof(receive_msg));
  switch(receive_msg.msgType){
    case(MSG_TYPE_TEXT): {
      Serial.println(receive_msg.data.msg);
      break;
    }
    case(MSG_TYPE_ACCEL_GYRO): { //note: We did not include the y-value in the print, since it is probably rather irrelevant for the machine learning approach we use.
      Serial.printf("IMU:%f,%f,%f,%f,%f;", receive_msg.data.accel.x, receive_msg.data.accel.z, receive_msg.data.gyro.yaw, receive_msg.data.gyro.pitch, receive_msg.data.gyro.roll);
      //Serial.printf("Accel:%f,%f,%f;", receive_msg.data.accel.x, receive_msg.data.accel.y, receive_msg.data.accel.z);
      //Serial.printf("Gyro:%f,%f,%f;", receive_msg.data.gyro.yaw, receive_msg.data.gyro.pitch, receive_msg.data.gyro.roll);
      break;
    }
  }
}

void setup(){
  Serial.begin(115200);                                       // initialize serial communication at 115200 bits per second

  // initialize the Board pins as pull-up inputs; the pull-up input pin will be HIGH when the switch is open and LOW when the switch is closed.
  pinMode(LEFT_BOARD, INPUT_PULLUP);
  pinMode(MIDDLE_BOARD, INPUT_PULLUP);
  pinMode(RIGHT_BOARD, INPUT_PULLUP); 
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
 
void loop() {
  // read the state of the single boards
  currentState_Left = digitalRead(LEFT_BOARD);
  currentState_Middle = digitalRead(MIDDLE_BOARD);
  currentState_Right = digitalRead(RIGHT_BOARD);

  // check to see if a board was just pressed (i.e. the input went from LOW to HIGH), and you've waited long enough since the last press to ignore any noise:
  if (currentState_Left != lastFlickerableState_Left) {         // If the switch/button changed, due to noise or pressing:
    lastDebounceTime_Left = millis();                           // reset the debouncing timer
    lastFlickerableState_Left = currentState_Left;              // save the the last flickerable state
    pressCounter_Left++;                                        // Increment counter for Left board  
  }

  if (currentState_Middle != lastFlickerableState_Middle) {     // If the switch/button changed, due to noise or pressing:
    lastDebounceTime_Middle = millis();                         // reset the debouncing timer
    lastFlickerableState_Middle = currentState_Middle;          // save the the last flickerable state
    pressCounter_Middle++;                                      // Increment counter for Middle board  
  }

  if (currentState_Right != lastFlickerableState_Right) {       // If the switch/button changed, due to noise or pressing:
    lastDebounceTime_Right = millis();                          // reset the debouncing timer
    lastFlickerableState_Right = currentState_Right;            // save the the last flickerable state
    pressCounter_Right++;                                       // Increment counter for Right board
  }

  if ((millis() - lastDebounceTime_Left) > DEBOUNCE_TIME) {     // whatever the reading is at, it's been there for longer than the debounce delay, so take it as the actual current state:
    if(pressCounter_Left >= PRESS_THRESHOLD){                   // If enough presses have been registered during the debouncetime:
      lastSteadyState_Left = currentState_Left;
      pressCounter_Left = 0;                                    // Reset counter after confirmation
      Serial.print("Left:1;");                                   // Register a step
    } else{                                                     // Else:
      pressCounter_Left = 0;                                    // Reset without classifying an input
  }


  if ((millis() - lastDebounceTime_Middle) > DEBOUNCE_TIME) {   // whatever the reading is at, it's been there for longer than the debounce delay, so take it as the actual current state:
    if(pressCounter_Middle >= PRESS_THRESHOLD){                 // If enough presses have been registered during the debouncetime:
      lastSteadyState_Middle = currentState_Middle;
      pressCounter_Middle = 0;                                  // Reset counter after confirmation
      Serial.print("Middle:1;");                                 // Register a step
    } else{                                                     // Else:
      pressCounter_Middle = 0;                                  // Reset without classifying an input
    }
  }

  if ((millis() - lastDebounceTime_Right) > DEBOUNCE_TIME) {    // whatever the reading is at, it's been there for longer than the debounce delay, so take it as the actual current state:
    if(pressCounter_Right >= PRESS_THRESHOLD){                  // If enough presses have been registered during the debouncetime:
      lastSteadyState_Right = currentState_Right;
      pressCounter_Right = 0;                                   // Reset counter after confirmation
      Serial.print("Right:1;");                                  // Register a step
    } else{                                                     // Else:
      pressCounter_Right = 0;                                   // Reset without classifying an input
    }
  }
}}