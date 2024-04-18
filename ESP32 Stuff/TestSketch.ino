#include <Arduino.h>

const int touchPin = 34; // Replace 34 with your analog input pin
const int threshold = 1000; // Adjust based on your setup (higher for less sensitivity)
int baseline = 0; // Stores the baseline capacitance reading

void setup() {
  Serial.begin(115200);
    Serial.println("Starting to scan...");

  pinMode(touchPin, INPUT);
  baseline = analogRead(touchPin); // Read initial baseline capacitance
      Serial.println("Impulse detected!");
}

void loop() {
  int reading = analogRead(touchPin);
  Serial.println(analogRead(touchPin));
  int difference = abs(reading - baseline); // Calculate absolute difference

  if (difference > threshold) {
    Serial.println("Impulse detected!");
  }

  delay(100); // Adjust delay based on desired detection rate
}