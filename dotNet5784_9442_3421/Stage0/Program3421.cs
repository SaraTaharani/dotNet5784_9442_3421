// See https://aka.ms/new-console-template for more information
namespace Targil0
{
  partial  class program
    { static void Main(string[] args)
        {
            Welcome3421();
            Welcome9442();
            Console.ReadKey();

        }
        static partial void Welcome9442();

        private static void Welcome3421()
        {
            Console.WriteLine("Enter your name: ");
            string name;
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
       
        }
    }
}

