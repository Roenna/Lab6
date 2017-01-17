using System.ComponentModel.DataAnnotations;

namespace DawidKobierskiLab5.Models
{
    public class Grade : ModelBase
    {
        [Required]
        public string CourseName { get; set; }

        [Required]
        [Range(2.0, 5.5)]
        public decimal Value { get; set; }

        [Required]
        public int Ects { get; set; }
    }
}