using System;
using System.ComponentModel.DataAnnotations;

namespace TemperatureArduino.Domain.Entities
{
    public class Temperature
    {
        public Temperature() => DateAdded = DateTime.UtcNow;

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Temperature")]
        public int Temperatura { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
