using System;
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

            foreach (var c in courses)
                Console.WriteLine(c.Name);
            
            Console.ReadKey();
        }
    }
}
