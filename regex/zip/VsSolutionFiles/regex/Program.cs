using System;

namespace regex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine(Parser.Instance(args[0]).Matches(args[1]));
        }
    }
}
