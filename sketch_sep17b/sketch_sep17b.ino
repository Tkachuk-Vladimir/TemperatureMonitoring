
void setup() 
{
  Serial.begin(9600);
}

void loop() 
{
  Serial.write(15);
  delay(5000);
}
