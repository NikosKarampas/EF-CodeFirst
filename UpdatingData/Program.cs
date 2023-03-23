using Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace UpdatingData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            #region Add new object to database

            var author = context.Authors.Single(a => a.Id == 1);

            var course = new Course
            {
                Name = "New Course",
                Description = "New Description",
                FullPrice = 19.95f,
                Level = 1,
                AuthorId = 1
                //Author = author
            };

            context.Courses.Add(course);

            #endregion


            #region Update object

            var courseToUpdate = context.Courses.Find(4);
            courseToUpdate.Name = "New Name";
            courseToUpdate.AuthorId = 2;

            #endregion


            #region Remove object

            //With Cascade Delete
            var courseToDelete = context.Courses.Find(6);
            context.Courses.Remove(courseToDelete);

            //Without Cascade Delete
            //Cannot delete author that has courses assigned to them
            //We have to first delete the courses

            var authorToDelete2 = context.Authors.Include(a => a.Courses).Single(a => a.Id == 2);

            context.Courses.RemoveRange(authorToDelete2.Courses);
            
            context.Authors.Remove(authorToDelete2);            

            #endregion

            context.SaveChanges();
        }
    }
}
