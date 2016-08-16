using ESN.Domain.Concrete;
using ESN.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var temp = new EFDbContext(string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Downloads\bp10\ESN.WebUI\App_Data\ESN3.mdf;Integrated Security=True"));

            //var a = int.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
            //var b = DateTime.Parse(a.ToString());


            //foreach (Profile item in temp.Profiles)
            //{
            //    Console.WriteLine(item.ProfileId.ToString());
            //}

            //foreach (User item in temp.Users)
            //{
            //    Console.WriteLine(item.UserId.ToString());
            //}

            //Console.WriteLine("stop execute data profiles");

            //Console.ReadLine();

            var List1 = new List<Type1>();
            var List2 = new List<Type2>();

            Type1 t1ex1 = new Type1 { id = 1, Field1 = 10 };
            Type1 t1ex2 = new Type1 { id = 2, Field1 = 20 };
            Type1 t1ex3 = new Type1 { id = 3, Field1 = 30 };

            Type2 t2ex1 = new Type2 { id = 1, Field2 = 111 };
            Type2 t2ex2 = new Type2 { id = 2, Field2 = 222 };

            List1.Add(t1ex1);
            List1.Add(t1ex2);
            List1.Add(t1ex3);

            List2.Add(t2ex1);
            List2.Add(t2ex2);



            var result1 = from l1 in List1
                         from l2 in List2
                         where l1.id == l2.id
                         where l2.Field2 == 111
                         select l1;

            var result2 = from u in temp.Users
                          from p in temp.Profiles
                          from f in temp.Friends
                          from ph in temp.Photobooks
                          where u.login == "login1"
                          where p.ProfileId == u.UserId
                          where f.ProfileId == u.UserId
                          where ph.ProfileId == f.subscriberId
                          select ph;



            Console.WriteLine("start foreach");
            foreach (var item in result2)
            {
                Console.WriteLine(item.Title);
            }
            Console.WriteLine("end foreach");
            Console.ReadLine();

        }

        public class Type1
        {
            public int id { get; set; }
            public int Field1 { get; set; }
        }

        public class Type2
        {
            public int id { get; set; }
            public int Field2 { get; set; }
        }
    }
}
