#include <esp_now.h>
#include <WiFi.h>
#include <Wire.h>

#include "I2Cdev.h"

#include "MPU6050_6Axis_MotionApps612.h"

#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
#include "Wire.h"
#endif
// TODO Remove Serial prints and convert it to send to master TEXT
//

// ------------- IMU PART ------------- 

MPU6050 mpu;

#define INTERRUPT_PIN 2  // use pin 2 on Arduino Uno & most boards
#define LED_PIN 13 // (Arduino is 13, Teensy is 11, Teensy++ is 6)
// MPU control/status vars
bool dmpReady = false;  // set true if DMP init was successful
uint8_t mpuIntStatus;   // holds actual interrupt status byte from MPU
uint8_t devStatus;      // return status after each device operation (0 = success, !0 = error)
uint16_t packetSize;    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

// orientation/motion vars
Quaternion q;           // [w, x, y, z]         quaternion container
VectorInt16 aa;         // [x, y, z]            accel sensor measurements
VectorInt16 gy;         // [x, y, z]            gyro sensor measurements
VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
VectorFloat gravity;    // [x, y, z]            gravity vector
float euler[3];         // [psi, theta, phi]    Euler angle container
float ypr[3];           // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector

// packet structure for InvenSense teapot demo
uint8_t teapotPacket[14] = { '$', 0x02, 0, 0, 0, 0, 0, 0, 0, 0, 0x00, 0x00, '\r', '\n' };

// INTERRUPT DETECTION ROUTINE
volatile bool mpuInterrupt = false;     // indicates whether MPU interrupt pin has gone high
void dmpDataReady() {
  mpuInterrupt = true;
}

void setupIMU() {
  // join I2C bus (I2Cdev library doesn't do this automatically)
  #if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
    Wire.begin();
    Wire.setClock(400000); // 400kHz I2C clock. Comment this line if having compilation difficulties
  #elif I2CDEV_IMPLEMENTATION == I2CDEV_BUILTIN_FASTWIRE
    Fastwire::setup(400, true);
  #endif

  Serial.println(F("Initializing I2C devices..."));
  mpu.initialize();
  pinMode(INTERRUPT_PIN, INPUT);

  // verify connection
  Serial.println(F("Testing device connections..."));
  Serial.println(mpu.testConnection() ? F("MPU6050 connection successful") : F("MPU6050 connection failed"));

  // wait for ready
  //Serial.println(F("\nSend any character to begin DMP programming and demo: "));
  //while (Serial.available() && Serial.read()); // empty buffer
  //while (!Serial.available());                 // wait for data
  //while (Serial.available() && Serial.read()); // empty buffer again

  // load and configure the DMP
  Serial.println(F("Initializing DMP..."));
  devStatus = mpu.dmpInitialize();

  // supply your own gyro offsets here, scaled for min sensitivity
  mpu.setXGyroOffset(107);
  mpu.setYGyroOffset(65);
  mpu.setZGyroOffset(14);
  mpu.setXAccelOffset(-4294);
  mpu.setYAccelOffset(706);
  mpu.setZAccelOffset(427);
  // make sure it worked (returns 0 if so)
  if (devStatus == 0) {
    // Calibration Time: generate offsets and calibrate our MPU6050
    mpu.CalibrateAccel(6);
    mpu.CalibrateGyro(6);
    Serial.println();
    mpu.PrintActiveOffsets();
    // turn on the DMP, now that it's ready
    Serial.println(F("Enabling DMP..."));
    mpu.setDMPEnabled(true);

    // enable Arduino interrupt detection
    Serial.print(F("Enabling interrupt detection (Arduino external interrupt "));
    Serial.print(digitalPinToInterrupt(INTERRUPT_PIN));
    Serial.println(F(")..."));
    attachInterrupt(digitalPinToInterrupt(INTERRUPT_PIN), dmpDataReady, RISING);
    mpuIntStatus = mpu.getIntStatus();

    // set our DMP Ready flag so the main loop() function knows it's okay to use it
    Serial.println(F("DMP ready! Waiting for first interrupt..."));
    dmpReady = true;

    // get expected DMP packet size for later comparison
    packetSize = mpu.dmpGetFIFOPacketSize();
  } else {
    // ERROR!
    // 1 = initial memory load failed
    // 2 = DMP configuration updates failed
    // (if it's going to break, usually the code will be 1)
    Serial.print(F("DMP Initialization failed (code "));
    Serial.print(devStatus);
    Serial.println(F(")"));
  }
}


// ------------- ESP NOW PART ------------- 
 
// REPLACE WITH THE MAC Address of your receiver 
uint8_t broadcastAddress[] = {0x10, 0x97, 0xBD, 0xD2, 0xD4, 0xEC}; // send to Master
//uint8_t broadcastAddress[] = {0x58, 0xBF, 0x25, 0x93, 0x39, 0xBC}; // send to IMU-ESP

typedef enum msg_type_t {
  MSG_TYPE_ACCEL_GYRO         = (1),   /**< Gravity + linear acceleration */
  MSG_TYPE_TEXT                  = (2),
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

//peer esp's
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
      dmpReady = !dmpReady;
      break;
    }
    case(MSG_TYPE_ACCEL_GYRO): { 
      break;
    }
  }
}

void send_to_master(struct msg_any_t _send_msg){
  esp_err_t result = esp_now_send(broadcastAddress, (uint8_t *) &_send_msg, sizeof(_send_msg)); 
  if (result == ESP_OK) {
    Serial.println("Sent with success");
  }
  else {
    Serial.println("Error sending the data");
  }
}

void send_imu_data() {
  mpu.dmpGetGravity(&gravity, &q);

  // display Euler angles in degrees
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);
  send_msg.data.gyro.yaw = ypr[0] * 180 / M_PI;
  send_msg.data.gyro.pitch = ypr[1] * 180 / M_PI;
  send_msg.data.gyro.roll = ypr[2] * 180 / M_PI;

  // display real acceleration, adjusted to remove gravity
  mpu.dmpGetAccel(&aa, fifoBuffer);
  mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);
  send_msg.data.accel.x = aaReal.x;
  send_msg.data.accel.y = aaReal.y;
  send_msg.data.accel.z = aaReal.z;
  send_msg.msgType = MSG_TYPE_ACCEL_GYRO;

  send_to_master(send_msg);
}

void setupESPNOW(){
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

void setup(){
  setupESPNOW();
  setupIMU();
}


void loop(){
  // if programming failed, don't try to do anything
  if (!dmpReady) return;
  // read a packet from FIFO
  if (mpu.dmpGetCurrentFIFOPacket(fifoBuffer)) { // Get the Latest packet
    send_imu_data();
  }
  
  delay(15);
}





