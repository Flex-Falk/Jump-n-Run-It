#include <Arduino.h>

//Pins for the different mats
#define LEFT_MAT       39                     // left mat (aka VN pin)
#define MIDDLE_MAT     34                     // middle mat
#define RIGHT_MAT      35                     // right mat

#define DEBOUNCE_TIME  50                     // the debounce time in millisecond, increase this time if it still chatters

// Variables for the Left Mat:
int lastSteadyState_Left = LOW;               // the previous steady state from the input pin
int lastFlickerableState_Left = LOW;          // the previous flickerable state from the input pin
int currentState_Left;                        // the current reading from the input pin
unsigned long lastDebounceTime_Left = 0;      // the last time the output pin was toggled

// Variables for the Middle Mat:
int lastSteadyState_Middle = LOW;             // the previous steady state from the input pin
int lastFlickerableState_Middle  = LOW;       // the previous flickerable state from the input pin
int currentState_Middle ;                     // the current reading from the input pin
unsigned long lastDebounceTime_Middle  = 0;   // the last time the output pin was toggled

// Variables for the Right Mat:
int lastSteadyState_Right = LOW;              // the previous steady state from the input pin
int lastFlickerableState_Right  = LOW;        // the previous flickerable state from the input pin
int currentState_Right ;                      // the current reading from the input pin
unsigned long lastDebounceTime_Right  = 0;    // the last time the output pin was toggled


void setup() {
  Serial.begin(115200);                       // initialize serial communication at 115200 bits per second:

  // initialize the Mat pins as pull-up inputs; the pull-up input pin will be HIGH when the switch is open and LOW when the switch is closed.
  pinMode(LEFT_MAT, INPUT_PULLUP);
  pinMode(MIDDLE_MAT, INPUT_PULLUP);
  pinMode(RIGHT_MAT, INPUT_PULLUP);

}

void loop() {
  // read the state of the mats
  currentState_Left = digitalRead(LEFT_MAT);
  currentState_Middle = digitalRead(MIDDLE_MAT);
  currentState_Right = digitalRead(RIGHT_MAT);


  // check to see if a mat was just pressed (i.e. the input went from LOW to HIGH), and you've waited long enough since the last press to ignore any noise:

  if (currentState_Left != lastFlickerableState_Left) {       // If the switch/button changed, due to noise or pressing:
    lastDebounceTime_Left = millis();                         // reset the debouncing timer
    lastFlickerableState_Left = currentState_Left;            // save the the last flickerable state
  }

  if (currentState_Middle != lastFlickerableState_Middle) {   // If the switch/button changed, due to noise or pressing:
    lastDebounceTime_Middle = millis();                       // reset the debouncing timer
    lastFlickerableState_Middle = currentState_Middle;        // save the the last flickerable state
  }

  if (currentState_Right != lastFlickerableState_Right) {     // If the switch/button changed, due to noise or pressing:
    lastDebounceTime_Right = millis();                        // reset the debouncing timer
    lastFlickerableState_Right = currentState_Right;          // save the the last flickerable state
  }

  if ((millis() - lastDebounceTime_Left) > DEBOUNCE_TIME) {           // whatever the reading is at, it's been there for longer than the debounce delay, so take it as the actual current state:
    if(lastSteadyState_Left == HIGH && currentState_Left == LOW)      // if the mat state has changed:
      Serial.print("Left:1");
    else if(lastSteadyState_Left == LOW && currentState_Left == HIGH)
      Serial.print("Left:0");

    lastSteadyState_Left = currentState_Left;                         // save the the last steady state
  }

  if ((millis() - lastDebounceTime_Middle) > DEBOUNCE_TIME) {         // whatever the reading is at, it's been there for longer than the debounce delay, so take it as the actual current state:
    if(lastSteadyState_Middle == HIGH && currentState_Middle == LOW)  // if the mat state has changed:
      Serial.print("Middle:1");
    else if(lastSteadyState_Middle == LOW && currentState_Middle == HIGH)
      Serial.print("Middle:0");

    lastSteadyState_Middle = currentState_Middle;                     // save the the last steady state
  }

  if ((millis() - lastDebounceTime_Right) > DEBOUNCE_TIME) {          // whatever the reading is at, it's been there for longer than the debounce delay, so take it as the actual current state:
    if(lastSteadyState_Right == HIGH && currentState_Right == LOW)    // if the mat state has changed:
      Serial.print("Right:1");
    else if(lastSteadyState_Right == LOW && currentState_Right == HIGH)
      Serial.print("Right:0");

    lastSteadyState_Right = currentState_Right;                       // save the the last steady state
  }
}