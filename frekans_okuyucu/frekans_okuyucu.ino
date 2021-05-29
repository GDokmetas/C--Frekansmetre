
#include <FreqCounter.h>


unsigned long frq;
int pinLed=13;

void setup() {
  pinMode(pinLed, OUTPUT);

  Serial.begin(9600);       

}



void loop() {

  FreqCounter::f_comp=10;  
  FreqCounter::start(1000); 

  while (FreqCounter::f_ready == 0) 

  frq=FreqCounter::f_freq;
  Serial.println(frq);
  delay(20);
  digitalWrite(pinLed,!digitalRead(pinLed)); 

}  
