using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Queries
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();            

            #region LINQ syntaxt

            var query = from c in context.Courses
                        where c.Name.Contains("c#")
                        orderby c.Name
                        select c;

            //foreach (var c in query)
            //    Console.WriteLine(c.Name);

            #endregion


            #region Extension Methods

            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);

            //foreach (var c in courses)
            //    Console.WriteLine(c.Name);

            #endregion


            #region Order by/Group by LINQ syntax

            var query1 = from c in context.Courses
                         where c.Author.Id == 1
                         orderby c.Level descending, c.Name
                         select new { Name = c.Name, Author = c.Author.Name };

            var query2 = from c in context.Courses
                         group c by c.Level 
                         into g
                         select g;

            //foreach (var group in query2)
            //{
            //    Console.WriteLine(group.Key);

            //    foreach (var course in group)
            //    {
            //        Console.WriteLine("\t{0}", course.Name);
            //    }
            //}

            //foreach (var group in query2)
            //{
            //    Console.WriteLine("{0} ({1})", group.Key, group.Count());
            //}

            #endregion


            #region Inner Joins LINQ syntax/extension methods

            var query3 = from c in context.Courses
                         join a in context.Authors
                         on c.AuthorId equals a.Id
                         select new { CourseName = c.Name, AuthorName = a.Name };

            var query4 = context.Courses.Join(context.Authors, 
                c => c.AuthorId, 
                a => a.Id, 
                (course, author) => 
                    new 
                    { 
                        CourseName = course.Name,
                        AuthorName = author.Name 
                    });

            //foreach ( var c in query4)            
            //    Console.WriteLine(c.CourseName + " " + c.AuthorName);            

            #endregion


            #region Group Join - LINQ syntax

            var query5 = from a in context.Authors
                         join c in context.Courses on a.Id equals c.AuthorId into g
                         select new { AuthorName = a.Name, Courses = g. Count() };

            //foreach (var item in query5)            
            //    Console.WriteLine("{0} ({1})", item.AuthorName, item.Courses);

            #endregion


            #region Cross join - LINQ syntax

            var query6 = from a in context.Authors
                         from c in context.Courses
                         select new { AuthorName = a.Name, CourseName = c.Name };

            #endregion


            #region Group Join extension methods

            var queryGroupJoin = context.Authors.GroupJoin(context.Courses,
                a => a.Id,
                c => c.AuthorId,
                (author, coursess) => new
                {
                    AuthorName = author.Name,
                    CoursesNo = coursess.Count()
                });

            foreach (var c in queryGroupJoin)
                Console.WriteLine("Author Name: " + c.AuthorName + " No of Courses: " + c.CoursesNo);

            #endregion


            #region Eager Loading /If no virtual keyword and Include it throws an exception

            var courses1 = context.Courses.Include(x => x.Author).ToList();

            foreach (var c in courses1)
            {
                Console.WriteLine("{0} {1}", c.Name, c.Author.Name);
            }

            #endregion


            #region Explicit loading

            var mosh = context.Authors.Single(a => a.Id == 1);

            context.Courses.Where(c => c.AuthorId == mosh.Id).Load();

            //How to use
            var moshCourses = mosh.Courses;

            #endregion

            Console.ReadKey();
        }
    }
}
