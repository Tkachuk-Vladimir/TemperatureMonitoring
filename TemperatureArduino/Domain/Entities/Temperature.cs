using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemperatureArduino.Domain.Entities
{
    public class Temperature
    {
        public Temperature() => DateAdded = DateTime.UtcNow;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Temperature")]
        public int Temperatura { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
