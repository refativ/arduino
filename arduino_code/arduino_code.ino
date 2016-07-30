#include <SoftwareSerial.h>
#define stp 13 
#define dir 12
#define stp1 8 
#define dir1 7
#define dir2 4
#define stp2 5
//#define EN 9

SoftwareSerial mySerial(10, 11); // RX, TX



static  char c;
String ResultString="";


void MoveDown(int pos){
  digitalWrite(dir,LOW);
  digitalWrite(dir1,LOW);
  for(int x=0 ; x < pos; x++){
    digitalWrite(stp,HIGH);
    digitalWrite(stp1,HIGH);
    delay(1);
    digitalWrite(stp,LOW);
    digitalWrite(stp1,LOW);
    delay(1);
  }
}
void slideShelf(int direction){
 if( direction==1){
    digitalWrite(dir2,LOW);
    for(int i=0;i<1170;i++){
       digitalWrite(stp2,HIGH);
         delay(1);
        digitalWrite(stp2,LOW);
        delay(1);
    }
 }
    else{
      digitalWrite(dir2,HIGH);
      for(int i=0;i<1170;i++){
         digitalWrite(stp2,HIGH);
         delay(1);
        digitalWrite(stp2,LOW);
        delay(1);
      }
      
    }

  
 }

  


void MoveUp(int pos){
  digitalWrite(dir1,HIGH);
  digitalWrite(dir,HIGH);
  for(int x=0; x < pos; x++){
    digitalWrite(stp1,HIGH);
    digitalWrite(stp,HIGH);
    delay(1);
    digitalWrite(stp1,LOW);
    digitalWrite(stp,LOW);
    delay(1);
  }
}
int oldRes=0;

void TakeShelfDown(int pos){
slideShelf(1);
delay(300);
MoveUp(pos);
delay(300);
slideShelf(-1);
delay(300);
MoveUp(400);
delay(300);
slideShelf(1);
delay(300);
MoveDown(pos+400);
}


void ReturnShelf(int pos){
MoveUp(pos+400);
delay(300);
slideShelf(-1);
delay(300);
MoveDown(400);
delay(300);
slideShelf(1);
delay(300);
MoveDown(pos);
delay(300);
slideShelf(-1);

}

void setup() {
  // Open serial communications and wait for port to open:
  Serial.begin(115200);

  pinMode(stp,OUTPUT);
  pinMode(dir,OUTPUT);
  pinMode(stp1,OUTPUT);
  pinMode(dir1,OUTPUT);
  pinMode(stp2,OUTPUT);
  pinMode(dir2,OUTPUT);
 // pinMode(EN,OUTPUT);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }
  // set the data rate for the SoftwareSerial port
  mySerial.begin(115200);
  oldRes=0;
  c='\0';
  ResultString="";
}
void loop() { // run over and over
 // digitalWrite(EN,LOW);
  while(!mySerial.available()){
    
  }
  while (mySerial.available()) {
    c = mySerial.read();
    ResultString+=c;
    delay(200);
  }
  
  int Result= ResultString.toInt();
 Serial.println(Result);
  if (Result == 2000 || Result == 5050 || Result == 8100   || Result == 10 ) {
    
  if(Result!=oldRes){
    if((Result!=0)&&(Result!=10)){
      TakeShelfDown(Result);
      oldRes=Result;
    }
     Serial.println(oldRes);
     if(oldRes != 0 ) {
    if((Result!=0)&&(Result==10)){
      ReturnShelf(oldRes);
      oldRes=Result;
     }
    }  
  }
 } 
  delay(500);
  ResultString="";
}


