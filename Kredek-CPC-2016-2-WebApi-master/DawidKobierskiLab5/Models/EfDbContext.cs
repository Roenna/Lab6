using System.Data.Entity;

namespace DawidKobierskiLab5.Models
{
    public class EfDbContext : DbContext
    {
        public virtual IDbSet<Student> Students { get; set; }
        public virtual IDbSet<Grade> Grades { get; set; }
    }
}