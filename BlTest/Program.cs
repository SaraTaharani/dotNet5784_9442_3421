using DalApi;
using DO;

namespace BlTest
{//חסר: מחיקה והדפסה עבור כל ישות
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public static void BOEngineer()
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
                        try
                        {
                            Console.WriteLine("enter an engineer level between 0-4");
                            int? level = int.Parse(Console.ReadLine()!);
                            BO.EngineerExperience levelEngineer;
                            bool b = Enum.TryParse<BO.EngineerExperience>(level.ToString(), out levelEngineer);
                            if (!b)
                                throw new BO.BlLogicException("enter an engineer level between 0 - 4");
                            BO.EngineerExperience leveluUpdate = (BO.EngineerExperience)level;
                            Console.WriteLine("enter cost of engineer to update");
                            int cost = int.Parse(Console.ReadLine()!);
                            BO.Engineer newEngineer = new BO.Engineer()
                            {
                                Id = id,
                                Name = name,
                                Email = email,
                                Level = levelEngineer,
                                Cost = cost
                            };
                            int ans = s_bl!.Engineer!.Create(newEngineer);
                            Console.WriteLine("the engineer was added");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                    case 'b'://Read
                        Console.WriteLine("enter id of engineer to read");
                        int idRead = int.Parse(Console.ReadLine()!);
                        try
                        {
                            Console.WriteLine(s_bl!.Engineer!.Read(idRead));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                    case 'c'://readAll
                        Console.WriteLine("the list of the engineers");
                        IEnumerable<BO.Engineer?> listEngineers = s_bl!.Engineer!.ReadAll(); ;
                        foreach(   BO.Engineer? engineer in listEngineers) 
                        { Console.WriteLine(engineer); }
                        break;
                    case 'd'://update
                        Console.WriteLine("enter id for new engineer");
                        int idtoUpdate = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter a name");
                        string nametoUpdate = Console.ReadLine()!;
                        Console.WriteLine("enter an email");
                        string emailtoUpdate = Console.ReadLine()!;

                        try
                        {
                            Console.WriteLine("enter an engineer level between 0-4");
                            int? level = int.Parse(Console.ReadLine()!);
                           BO.EngineerExperience levelUpdate;
                            bool b = Enum.TryParse<BO.EngineerExperience>(level.ToString(), out levelUpdate);
                            if (!b)
                                throw new BO.BlLogicException("enter an engineer level between 0 - 4");
                          levelUpdate = (BO. EngineerExperience)level;
                            Console.WriteLine("enter cost of engineer to update");
                            int cost = int.Parse(Console.ReadLine()!);
                            BO.Engineer updateEngineer = new BO.Engineer()
                            {
                                Id = idtoUpdate,
                                Name = nametoUpdate,
                                Email = emailtoUpdate,
                                Level = levelUpdate,
                                Cost = cost
                            };
                            s_bl!.Engineer!.Update(updateEngineer);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                        break;
                    case 'e'://delete
                        Console.WriteLine("enter id of en engineer to delete");
                        int idDelete = int.Parse(Console.ReadLine()!);
                        //try
                        //{
                        //    s_bl!.Engineer!.Delete(idDelete);
                        //}
                       // catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                        break;
                }
            }
            while (choose != 'f');
        }
        public static void BOTask()
        {
            char choose;
            do
            {
                choose = submenu("Task");
                switch (choose)
                {
                    case 'a'://add a task
                        Console.WriteLine("enter alias task");
                        string alias = Console.ReadLine()!;
                        Console.WriteLine("enter task description ");
                        string description = Console.ReadLine()!;
                        DO.Task task = new(0, description, alias);
                        try
                        {
                            int result = s_dal!.Task!.Create(task);
                            Console.WriteLine("the task was added");
                        }
                        catch (DalAlreadyExistsException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                    case 'b'://read a task by id
                        Console.WriteLine("enter an id number for read");
                        int id = int.Parse(Console.ReadLine()!);
                        try
                        {
                            Console.WriteLine(s_bl!.Task?.Read(id));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                    case 'c'://read all tasks
                        Console.WriteLine("all  tasks:");
                        IEnumerable<DO.Task?> arryOfAllTask = s_dal!.Task!.ReadAll();
                        foreach (var item in arryOfAllTask)
                            Console.WriteLine(item);
                        break;
                    case 'd'://update the task
                        Console.WriteLine("enter id of task to update");
                        int idUpdate = int.Parse(Console.ReadLine()!);
                        try
                        {
                            Console.WriteLine("enter task description ");
                            string updescription = Console.ReadLine()!;
                            Console.WriteLine("enter alias task");
                            string upalias = Console.ReadLine()!;

                            DO.Task upTask = new DO.Task() { Id = }(idUpdate, updescription, upalias);
                            s_dal!.Task!.Update(upTask);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }

                        break;
                    case 'e':
                        Console.WriteLine("enter id of task to delete");
                        int idForDelete = int.Parse(Console.ReadLine()!);
                        try
                        {
                            s_dal!.Task!.Delete(idForDelete);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                }
            } 

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

        static void Main(string[] args)
        {
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
                        BOEngineer();
                        break;
                    case 2:
                        BOTask();
                        break;
                    case 3:
                        BODepndency();
                        break;
                    default:
                        break;
                }
            }
            while (choose != 0);



        }

    }
}