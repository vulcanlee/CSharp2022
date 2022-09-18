using System.Net.Http;
using System;
namespace csTryUsing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(var client = new HttpClient())
            {
                throw new NotImplementedException();
            }
            Console.WriteLine("Hello, World!");
        }
    }
}