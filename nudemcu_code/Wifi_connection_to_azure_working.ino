    /*
     *  Simple HTTP get webclient test
     */
     
    #include <ESP8266WiFi.h>
    #include <SoftwareSerial.h>
    SoftwareSerial Myserial(11,10);
    const char* ssid     = "16";
    const char* password = "0546313491";
     
    const char* host = "shelfservice.azure-mobile.net";
     
    void setup() {
      Serial.begin(115200);
      delay(400);
      Myserial.begin(115200);
     
      // We start by connecting to a WiFi network
     
      Serial.println();
      Serial.println();
    //  Serial.print("Connecting to ");
    //  Serial.println(ssid);
      
      WiFi.begin(ssid, password);
      
      while (WiFi.status() != WL_CONNECTED) {
        delay(500);
     //   Serial.print(".");
      }
     
    /*  Serial.println("");
      Serial.println("WiFi connected");  
      Serial.println("IP address: ");
      Serial.println(WiFi.localIP());
      */
    }
     
    int value = 0;
     
    void loop() {
      delay(300);
      ++value;
     
    //  Serial.print("connecting to ");
   //   Serial.println(host);
      
      // Use WiFiClient class to create TCP connections
      WiFiClient client;
      const int httpPort = 80;
      if (!client.connect(host, httpPort)) {
     //   Serial.println("connection failed");
        return;
      }
      
      // We now create a URI for the request
      String url = "/api/shelf/GetSelected";//"/testwifi/index.html";
    //  Serial.print("Requesting URL: ");
     // Serial.println(url);
      
      // This will send the request to the server
      client.print(String("GET ") + url + " HTTP/1.1\r\n" +
                   "Host: " + host + "\r\n" + 
                   "X-ZUMO-APPLICATION: GGieGqyJRflxjyRIgSGxVrYBXIQPpn56\r\n" +
                   "Connection: close\r\n\r\n");
      delay(500);
      
      // Read all the lines of the reply from server and print them to Serial
      while(client.available()){
        
        String line = client.readStringUntil('\r');
        int firstClosingBracket = line.indexOf("<ROMAN");
        if(firstClosingBracket!=-1){
          //"<ROMAN
          //Alexey>"
          String ResultString=line.substring(8,line.length()-8);
          int Result= ResultString.toInt();
           Serial.println(Result);
           Myserial.println(Result);
          }
        
       // Serial.print(line);
      }
      
     // Serial.println();
    //  Serial.println("closing connection");
    }
