#include <Arduino.h>

//Pins for the different boards
#define LEFT_BOARD       39                   // left board (aka VN pin)
#define MIDDLE_BOARD     34                   // middle board
#define RIGHT_BOARD      35                   // right board

#define DEBOUNCE_TIME  50                     // the debounce time in millisecond, increase this time if it still chatters

#define PRESS_THRESHOLD 4                     // Threshold for number of state changes to confirm a press; used to finetune
                                              // higher: lower sensitivity aka more force for an input
                                              // lower: higher sensitivity aka less force for an input

// Variables for the Left Board:
int lastSteadyState_Left = LOW;               // the previous steady state from the input pin
int lastFlickerableState_Left = LOW;          // the previous flickerable state from the input pin
int currentState_Left;                        // the current reading from the input pin
unsigned long lastDebounceTime_Left = 0;      // the last time the output pin was toggled
int pressCounter_Left = 0;                    // variable for press counter

// Variables for the Middle Board:
int lastSteadyState_Middle = LOW;             // the previous steady state from the input pin
int lastFlickerableState_Middle  = LOW;       // the previous flickerable state from the input pin
int currentState_Middle ;                     // the current reading from the input pin
unsigned long lastDebounceTime_Middle  = 0;   // the last time the output pin was toggled
int pressCounter_Middle = 0;                  // variable for press counter

// Variables for the Right Board:
int lastSteadyState_Right = LOW;              // the previous steady state from the input pin
int lastFlickerableState_Right  = LOW;        // the previous flickerable state from the input pin
int currentState_Right ;                      // the current reading from the input pin
unsigned long lastDebounceTime_Right  = 0;    // the last time the output pin was toggled
int pressCounter_Right = 0;                   // variable for press counter


void setup() {
  Serial.begin(115200);                       // initialize serial communication at 115200 bits per second:

  // initialize the Board pins as pull-up inputs; the pull-up input pin will be HIGH when the switch is open and LOW when the switch is closed.
  pinMode(LEFT_BOARD, INPUT_PULLUP);
  pinMode(MIDDLE_BOARD, INPUT_PULLUP);
  pinMode(RIGHT_BOARD, INPUT_PULLUP);

}

void loop() {
  // read the state of the boards
  currentState_Left = digitalRead(LEFT_BOARD);
  currentState_Middle = digitalRead(MIDDLE_BOARD);
  currentState_Right = digitalRead(RIGHT_BOARD);


  // check to see if a board was just pressed (i.e. the input went from LOW to HIGH), and you've waited long enough since the last press to ignore any noise:

  if (currentState_Left != lastFlickerableState_Left) {       // If the switch/button changed, due to noise or pressing:
    lastDebounceTime_Left = millis();                         // reset the debouncing timer
    lastFlickerableState_Left = currentState_Left;            // save the the last flickerable state
    pressCounter_Left++;                                      // Increment counter for Left board  
  }

  if (currentState_Middle != lastFlickerableState_Middle) {   // If the switch/button changed, due to noise or pressing:
    lastDebounceTime_Middle = millis();                       // reset the debouncing timer
    lastFlickerableState_Middle = currentState_Middle;        // save the the last flickerable state
    pressCounter_Middle++;                                    // Increment counter for Middle board  
  }

  if (currentState_Right != lastFlickerableState_Right) {     // If the switch/button changed, due to noise or pressing:
    lastDebounceTime_Right = millis();                        // reset the debouncing timer
    lastFlickerableState_Right = currentState_Right;          // save the the last flickerable state
    pressCounter_Right++;                                     // Increment counter for Right board
  }

  if ((millis() - lastDebounceTime_Left) > DEBOUNCE_TIME) {     // whatever the reading is at, it's been there for longer than the debounce delay, so take it as the actual current state:
    if(pressCounter_Left >= PRESS_THRESHOLD){
      lastSteadyState_Left = currentState_Left;
      pressCounter_Left = 0;                                    // Reset counter after confirboardion
      Serial.print("Left:1");
    } else{
      pressCounter_Left = 0;                                    // Reset counter after confirboardion
    }
  }


  if ((millis() - lastDebounceTime_Middle) > DEBOUNCE_TIME) {   // whatever the reading is at, it's been there for longer than the debounce delay, so take it as the actual current state:
    if(pressCounter_Middle >= PRESS_THRESHOLD){
      lastSteadyState_Middle = currentState_Middle;
      pressCounter_Middle = 0;                                  // Reset counter after confirboardion
      Serial.print("Middle:1");
    } else{
      pressCounter_Middle = 0;                                  // Reset counter after confirboardion
    }
  }

  if ((millis() - lastDebounceTime_Right) > DEBOUNCE_TIME) {    // whatever the reading is at, it's been there for longer than the debounce delay, so take it as the actual current state:
    if(pressCounter_Right >= PRESS_THRESHOLD){
      lastSteadyState_Right = currentState_Right;
      pressCounter_Right = 0;                                   // Reset counter after confirboardion
      Serial.print("Right:1");
    } else{
      pressCounter_Right = 0;                                   // Reset counter after confirboardion
    }
  }
}