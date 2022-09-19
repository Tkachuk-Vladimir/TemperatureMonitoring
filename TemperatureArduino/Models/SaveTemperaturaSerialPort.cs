using System;
using TemperatureArduino.Domain;
using TemperatureArduino.Domain.Repositories.Abstract;
using TemperatureArduino.Domain.Entities;
using System.IO.Ports;
using System.IO;

namespace TemperatureArduino.Models
{
    public class SaveTemperaturaSerialPort
    {
        private ITemperatureRepository temperatureRepository;

        public SaveTemperaturaSerialPort(ITemperatureRepository temperatureRepository)
        {
            this.temperatureRepository = temperatureRepository;
        }

        void Save()
        {
            SerialPort serialPort = new SerialPort("/dev/cu.wchusbserialfd120", 9600);
            int temperatureArduino;
            //EFTemperatureRepository repository = new EFTemperatureRepository(AppDbContext context);

            try
            {
                serialPort.Open();

                while (true)
                {
                    if (serialPort.BytesToRead > 0)
                    {
                        temperatureArduino = (int)serialPort.ReadByte();

                        Temperature temperature = new Temperature();
                        temperature.Id = default;
                        temperature.Temperatura = temperatureArduino;
                        temperature.DateAdded = DateTime.Now;
                        //repository.SaveTemperature(temperature);
                        //temperatureRepository.SaveTemperature(temperature);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
