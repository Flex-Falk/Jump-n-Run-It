#include <Arduino.h>

void setup() {
  Serial.begin(115200);
}

void loop() {
  
  generateRandomKey("jump", 0, 1);
  generateRandomKey("left", 0, 1);
  generateRandomKey("right", 0, 1);
  //delay(3000);
}

void generateRandomKey(String key, int min, int max){
  Serial.printf("%s:%d;", key, random(min, max));
  delay(random(10, 30));
}


