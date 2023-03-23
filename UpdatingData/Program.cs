using Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatingData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            //var authors = context.Authors.ToList();
            //var author = authors.Single(a => a.Id == 1);

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

            context.SaveChanges();
        }
    }
}
