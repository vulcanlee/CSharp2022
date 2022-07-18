//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MultipleThreadAccessCollection
//{
//    internal class Program
//    {
//        static List<string> list = new List<string>();
//        static void Main(string[] args)
//        {

//            new Thread(() =>
//            {
//                Run1();
//            })
//            { IsBackground = true }.Start();
//            new Thread(() =>
//            {
//                Run2();
//                //Run3();
//            })
//            { IsBackground = true }.Start();

//            Console.ReadKey();
//        }


//        static void Run1()
//        {
//            while (true)
//            {
//                list.Add("A");
//            }
//        }
//        static void Run2()
//        {
//            while (true)
//            {
//                foreach (var item in list)
//                {

//                }
//                list.Clear();
//            }
//        }

//        static void Run3()
//        {
//            while (true)
//            {
//                for (int i = 0; i < list.Count; i++)
//                {
//                    var b = list[i];
//                }
//                list.Clear();
//            }
//        }
//    }
//}
