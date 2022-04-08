#include <SPI.h>
#include <MFRC522.h>
#include<PubSubClient.h>

#include <ESP8266WiFi.h>
#include <ESPAsyncTCP.h>
#include <ESPAsyncWebServer.h>
#include <ESPDash.h>

#define BAUDRATE 115200
#define MAIN_LOOP_DELAY 2000
#define SS_PIN D8
#define RST_PIN D0
#define LM35pin A0
MFRC522 mfrc522(SS_PIN, RST_PIN);   //new MFRC522 instance.

AsyncWebServer server(80);
ESPDash dashboard(&server);

Card title_card(&dashboard, GENERIC_CARD, "Title");
Card temperature_card(&dashboard, TEMPERATURE_CARD, "Temperature", "°C");

double temperature;

const char* ssid = "wifi_ssid";
const char* pass = "wifi_password";
const char* mqttServer = "pc_ip";
const int mqttPort = 1883;
const char* topic = "meteo";

WiFiClient esp_board;
PubSubClient mqtt_client(esp_board);

void setup() {
  //  setup wifi
  WiFi.begin(ssid, pass);
  Serial.begin(BAUDRATE);
  Serial.print("Connecting to network ");
 
  while(WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(500);
  }
  
  Serial.println(" done");
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP()); 
  server.begin();
  delay(500);

//  setup website
  String s = "Web Thermometer";
  title_card.update(s);
  dashboard.sendUpdates();

  mqtt_client.setServer(mqttServer,mqttPort);
  mqtt_client.setCallback(callback);
  Serial.println("Connecting to MQTT server/broker ");

  while(!mqtt_client.connected()) {
    String client_id = "plytka";
  
    WiFi.mode(WIFI_STA);
    if(mqtt_client.connect(client_id.c_str())) {
      Serial.println("... connected");  
    } else {
      Serial.print("... failed ");
      Serial.println(mqtt_client.state());
      delay(1000);
    }
  }
 
  // setup rfid
  SPI.begin();             
  mfrc522.PCD_Init();
  Serial.println("Close a RFID tag/card to the reader");
  Serial.println(" - - - - - - - - - - - -");

  // subscribe to topic
  mqtt_client.subscribe(topic);
}

String tag_uid = "";
String message = "";
byte number;
long p_millis = 0;
long counter = 1;
bool isEmployee = 0;

void loop() {
  mqtt_client.loop();
  
  if(!mfrc522.PICC_IsNewCardPresent()) 
    return;
  
  if(!mfrc522.PICC_ReadCardSerial()) 
    return;

  for(int i = 0; i < (int)mfrc522.uid.size; i++) {
    tag_uid.concat(String(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " "));
    tag_uid.concat(String(mfrc522.uid.uidByte[i], HEX));
  }
  
  if (millis()-p_millis > MAIN_LOOP_DELAY) {
    if (tag_uid == " 37 31 A8 C9" || tag_uid == " 37 31 a8 c9") {
      isEmployee = 1;
      
      //  update website
      temperature_card.update(String(temperature));
      dashboard.sendUpdates();
    }
    else {
      isEmployee = 0;
      Serial.print("Unauthorized access: ");
      Serial.println(tag_uid);
    }
    temperature = ((analogRead(LM35pin)/1024.0)*3300-500)/10;  //TMP36
    message = String(counter++) + ";" + String(temperature) + ";" + String(tag_uid.substring(1)) + ";" + isEmployee;
    
    //  publish to mqtt
    mqtt_client.publish(topic, message.c_str());
    p_millis = millis();
  }
  tag_uid = "";
  message = "";
}

void callback(char *topic, byte *payload, unsigned int length) {
  Serial.print("Message arrived in topic: ");
  Serial.println(topic);
  Serial.print("> ");
  
  for (int i = 0; i < length; i++) {
    Serial.print((char) payload[i]);
  }
  
  Serial.println();
  Serial.println(" - - - - - - - - - - - -");
}
