﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            // LINQ syntaxt
            var query = from c in context.Courses
                        where c.Name.Contains("c#")
                        orderby c.Name
                        select c;            

            //foreach (var c in query)
            //    Console.WriteLine(c.Name);

            // Extension Methods
            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);

            //foreach (var c in courses)
            //    Console.WriteLine(c.Name);

            #region Order by/Group by

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


            #region Joins LINQ syntax/extension methods

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


            #region Join with Group By

            var query5 = from a in context.Authors
                         join c in context.Courses on a.Id equals c.AuthorId into g
                         select new { AuthorName = a.Name, Courses = g. Count() };

            foreach (var item in query5)            
                Console.WriteLine("{0} ({1})", item.AuthorName, item.Courses);

            #endregion

            Console.ReadKey();
        }
    }
}
