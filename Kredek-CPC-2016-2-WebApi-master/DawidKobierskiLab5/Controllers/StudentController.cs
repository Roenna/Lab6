using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DawidKobierskiLab5.Models;
using System.Web.Http.Cors;

namespace DawidKobierskiLab5.Controllers
{
    [RoutePrefix("students")]
    public class StudentController : ApiController
    {
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<Student>))]
        public IHttpActionResult GetAll()
        {
            IEnumerable<Student> students;
            using (var ctx = new EfDbContext())
            {
                students = ctx.Students.Include(s => s.Grades).ToList();
            }

            return Ok(students);
        }

        [HttpGet, Route("{id:int}", Name = "GetStudent")]
        [ResponseType(typeof(Student))]
        public IHttpActionResult Get(int id)
        {
            Student student;
            using (var ctx = new EfDbContext())
            {
                student = ctx.Students.Include(s => s.Grades)
                    .SingleOrDefault(s => s.Id == id);
            }

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Add([FromBody]Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var ctx = new EfDbContext())
            {
                ctx.Students.Add(student);
                ctx.SaveChanges();
            }

            return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody]Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var ctx = new EfDbContext())
            {
                student.Id = id;
                ctx.Students.Attach(student);
                var dbEntry = ctx.Entry(student);
                dbEntry.State = EntityState.Modified;
                if (student.Grades != null)
                {
                    foreach (var grade in student.Grades)
                    {
                        ctx.Grades.Attach(grade);
                        var entry = ctx.Entry(grade);
                        entry.State = grade.Id == 0 ? EntityState.Added :
                            EntityState.Modified;
                    }
                }

                ctx.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            using (var ctx = new EfDbContext())
            {
                // var dbEntry = ctx.Students.Find(id);

                var dbEntry = ctx.Students.Include(s => s.Grades).FirstOrDefault(x => x.Id == id);

                if (dbEntry == null)
                {
                    return NotFound();
                }
                var gradesForDelete = dbEntry.Grades.ToList();
                foreach (var grade in gradesForDelete)
                {
                    ctx.Grades.Remove(grade);
                }

                ctx.Students.Remove(dbEntry);
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
