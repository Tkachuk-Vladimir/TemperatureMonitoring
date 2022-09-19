using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TemperatureArduino.Domain.Entities;
using TemperatureArduino.Domain.Repositories.Abstract;


namespace TemperatureArduino.Domain.Repositories.EntityFramework
{
    public class EFTemperatureRepository : ITemperatureRepository
    {
        private readonly AppDbContext context;

        public EFTemperatureRepository(AppDbContext context)
        {
            this.context = context;
        }
        //public IQueryable<Temperature> GetTemperature()
        //{
        //  return context.Temperatures;
        //}

        public List<Temperature> GetTemperature()
        {
           return context.Temperatures.ToList();
        }

        public Temperature GetTemperatureById(int id)
        {
            return context.Temperatures.FirstOrDefault(x => x.Id == id);
        }

        public void SaveTemperature(Temperature entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
                //context.Temperatures.Add(entity);
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteTemperature(int id)
        {
            context.Temperatures.Remove(new Temperature() { Id = id });
            context.SaveChanges();
        }
    }
}
