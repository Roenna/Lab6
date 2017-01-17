using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DawidKobierskiLab5.Models
{
    public class Student : ModelBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Index { get; set; }

        public IList<Grade> Grades { get; set; }
    }
}