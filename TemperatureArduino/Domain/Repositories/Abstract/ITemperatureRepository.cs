using System;
using System.Linq;
using TemperatureArduino.Domain.Entities;

namespace TemperatureArduino.Domain.Repositories.Abstract
{
    public interface ITemperatureRepository
    {
        IQueryable<Temperature> GetTemperature();
        Temperature GetTemperatureById(Guid id);
        void SaveTemperature(Temperature entity);
        void DeleteTemperature(Guid id);
    }
}
