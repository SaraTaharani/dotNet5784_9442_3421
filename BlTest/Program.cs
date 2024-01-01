//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");



namespace BlTest
{
    internal class Program
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public static void CRUDEngineer()
        {
            char choose;
            do
            {
                choose = submenu("Engineer");
                switch (choose)
                {
                    case 'a'://ADD
                        Console.WriteLine("enter id for new engineer");
                        int id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter a name");
                        string name = Console.ReadLine()!;
                        Console.WriteLine("enter an email");
                        string email = Console.ReadLine()!;
                        Console.WriteLine("enter the cost");
                        double cost = double.Parse(Console.ReadLine()!);

                        BO.Engineer newEngineer = new BO.Engineer()
                        { id, name, email, cost };
                        try
                        {
                            int ans = s_bl!.Engineer!.Create(newEngineer);
                            Console.WriteLine("the engineer was addad");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                    case 'b'://read
                        Console.WriteLine("enter id of engineer to read");
                        int idRead = int.Parse(Console.ReadLine()!);
                        try
                        {
                            Console.WriteLine(s_dal!.Engineer!.Read(idRead));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                    case 'c'://readAll
                        Console.WriteLine("the list of the engineers");
                        IEnumerable<Engineer?> listEngineers = s_dal!.Engineer!.ReadAll(); ;
                        foreach (Engineer? engineer in listEngineers) { Console.WriteLine(engineer); }
                        break;
                    case 'd'://update
                        Console.WriteLine("enter an id of engineer to update");
                        int idUpdate = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter a name of engineer to update");
                        string nameUpdate = Console.ReadLine()!;
                        Console.WriteLine("enter a email of engineer to update");
                        string emailUpdate = Console.ReadLine()!;
                        try
                        {
                            Console.WriteLine("enter an engineer level between 0-4");
                            int? level = int.Parse(Console.ReadLine()!);
                            EngineerExperience levelUpdate;
                            bool b = Enum.TryParse<EngineerExperience>(level.ToString(), out levelUpdate);
                            if (!b)
                                throw new LogicException("enter an engineer level between 0 - 4");
                            EngineerExperience leveluUpdate = (EngineerExperience)level;
                            Console.WriteLine("enter cost of engineer to update");
                            int cost = int.Parse(Console.ReadLine()!);
                            Engineer updateEngineer = new(idUpdate, nameUpdate, emailUpdate, cost, levelUpdate);
                            s_dal!.Engineer!.Update(updateEngineer);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                        break;
                    case 'e'://delete
                        Console.WriteLine("enter id of en engineer to delete");
                        int idDelete = int.Parse(Console.ReadLine()!);
                        try
                        {
                            s_dal!.Engineer!.Delete(idDelete);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                        break;
                }
            }
            while (choose != 'f');
        }
        public static char submenu(string type)
        {
            Console.WriteLine("for add a " + type + " press a");
            Console.WriteLine("for read a " + type + " press b");
            Console.WriteLine("for read all " + type + " press c");
            Console.WriteLine("for update a " + type + " press d");
            Console.WriteLine("for delete a " + type + " press e");
            Console.WriteLine("for exit press f");
            char choose = char.Parse(Console.ReadLine()!);
            return choose;
        }





        static void Main(string[] args) {
            int choose;
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();
            do
            {
                Console.WriteLine("For engineer press 1");
                Console.WriteLine("For task press 2");
                Console.WriteLine("For Milestone press 3");
                Console.WriteLine("For exit press 0");
                choose = int.Parse(Console.ReadLine()!);
                switch (choose)
                {
                    case 0:
                        break;
                    case 1:
                        ();
                        break;
                    case 2:
                        ();
                        break;
                    case 3:
                        CRUDDepndency();
                        break;
                    default:
                        break;
                }
            }
            while (choose != 0);



        }

}