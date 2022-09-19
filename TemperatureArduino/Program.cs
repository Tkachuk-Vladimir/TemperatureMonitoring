using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO.Ports;
using System.Threading;
using TemperatureArduino.Domain;
using TemperatureArduino.Domain.Repositories.Abstract;
using TemperatureArduino.Domain.Entities;
using TemperatureArduino.Models;
using System.IO;
using TemperatureArduino.Domain.Repositories.EntityFramework;
using TemperatureArduino.Service;
using Microsoft.EntityFrameworkCore;

namespace TemperatureArduino
{
    public class Program
    {
        static SerialPort serialPort;

        public static void Main(string[] args)
        {
            serialPort = new SerialPort("/dev/cu.wchusbserialfd120", 9600);
            var portNames = SerialPort.GetPortNames();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

            try
            {
                foreach (var port in portNames)
                {
                    //Try for every portName and break on the first working
                    if (!serialPort.IsOpen && port == serialPort.PortName)
                    {
                        serialPort.Open();
                        serialPort.DiscardInBuffer();
                    }
                }
                
            }
            catch(IOException ex)
            {
                Console.WriteLine(ex);
            }

            
            /*
            try
             {
                 serialPort.Open();

                 while (true)
                 {
                     if (serialPort.BytesToRead > 0)
                     {
                        temperatureArduino = (int)serialPort.ReadByte();

                        Temperature temperature = new Temperature()
                        {
                            Id = default,
                            Temperatura = temperatureArduino,
                            DateAdded = DateTime.Now
                        };

                        var repository = new EFTemperatureRepository(context);

                        repository.SaveTemperature(temperature);
                    }
                 }
             }
             catch(IOException ex)
             {
                 Console.WriteLine(ex);
             }
            */

            CreateHostBuilder(args).Build().Run();
        }
        public static void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int temperatureArduino;

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=Temperature.db;");
            AppDbContext context = new AppDbContext(optionsBuilder.Options);

            try
            {
                if (serialPort.BytesToRead > 0)
                {
                    temperatureArduino = (int)serialPort.ReadByte();

                    Temperature temperature = new Temperature()
                    {
                        Id = default,
                        Temperatura = temperatureArduino,
                        DateAdded = DateTime.Now
                    };

                    var repository = new EFTemperatureRepository(context);

                    repository.SaveTemperature(temperature);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
