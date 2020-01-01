#define trigPin 8
#define echoPin 9

// Echo ve Trigger uçları sırasıyla 8 ve 9 pinine bağlanır. (Trigger 8 Pinine , Echo 9 Pinine)
// GND GND'ye bağlanır , Vcc 5Volt a bağlanır.
long duration;
float distanceCM;
float distanceInch;


void setup()
{
  pinMode(trigPin,OUTPUT);
  pinMode(echoPin,INPUT);
  Serial.begin(9600);
}

void loop()
{
digitalWrite(trigPin,LOW);
delayMicroseconds(2);

digitalWrite(trigPin,HIGH);
delayMicroseconds(10);
digitalWrite(trigPin,LOW);

duration = pulseIn(echoPin,HIGH);

distanceCM = duration * 0.034 / 2;
//distanceInch = duration * 0.0133 / 2;

//Serial.print("-----Uzaklık Mesafesi------");
//Serial.println();
Serial.print(distanceCM);
//Serial.print(" Santimetre");
//Serial.println();

//Serial.print(distanceInch);
//Serial.println("  İnç");
//Serial.println("---------------------------");
Serial.println(" ");



delay(1000);
}
