using System;
using System.Collections.Generic;
using System.Linq;
using TemperatureArduino.Domain.Entities;

namespace TemperatureArduino.Domain.Repositories.Abstract
{
    public interface ITemperatureRepository
    {
        //IQueryable<Temperature> GetTemperature();
        List<Temperature> GetTemperature();
        Temperature GetTemperatureById(int id);
        void SaveTemperature(Temperature entity);
        void DeleteTemperature(int id);
    }
}
