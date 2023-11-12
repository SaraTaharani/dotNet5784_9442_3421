using Dal;
using DalApi;
using DO;
using System.ComponentModel;
using System.Diagnostics.Metrics;


namespace DalTest
{
    internal class Program
    {
        private static IEngineer? s_dalEngineer = new EngineerImplementation();
        private static ITask? s_dalTask = new TaskImplementation();
        private static IDependency? s_dalDependency = new DependencyImplementation();


        public static void CRUDEngineer()
        {
            char choose=submenu<Engineer>();
            switch (choose)
            {
                case 'a'://ADD
                    Console.WriteLine("enter id for new engineer");
                    int id = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter a name");
                    string name = Console.ReadLine()!;
                    Console.WriteLine("entwr an email");
                    string email= Console.ReadLine()!;
                    Engineer newEngineer = new(id, name, email);
                    try
                    {
                        int ans = s_dalEngineer!.Create(newEngineer);
                        Console.WriteLine("the engineer was addad");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    break;
                case 'b'://read
                    Console.WriteLine("enter id of engineer to read");
                    int idRead=int.Parse(Console.ReadLine()!);
                    try
                    {
                        Console.WriteLine(s_dalEngineer!.Read(idRead));
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    break;
                case 'c'://readAll
                    Console.WriteLine("the list of the engineers");
                    List<Engineer> listEngineers = s_dalEngineer!.ReadAll(); ;
                    foreach (Engineer engineer in listEngineers) { Console.WriteLine(engineer);}
                    break;
                case 'd'://update
                    Console.WriteLine("enter an id of engineer to update");
                    int idUpdate=int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter a name of engineer to update");
                    string nameUpdate= Console.ReadLine()!;
                    Console.WriteLine("enter a email of engineer to update");
                    string emailUpdate = Console.ReadLine()!;
                    try
                    {
                        Console.WriteLine("enter an engineer level between 0-2");
                        int? level = int.Parse(Console.ReadLine()!);
                        EngineerExperience levelUpdate;
                        bool b = Enum.TryParse<EngineerExperience>(level.ToString(), out levelUpdate);
                        if (!b)
                            throw new Exception("enter an engineer level between 0 - 2");
                        EngineerExperience leveluUpdate = (EngineerExperience)level;
                        Console.WriteLine("enter cost of engineer to update");
                        int cost = int.Parse(Console.ReadLine()!);
                        Engineer updateEngineer = new(idUpdate, nameUpdate, emailUpdate, cost, levelUpdate);
                        s_dalEngineer!.Update(updateEngineer);
                    }
                    catch(Exception ex) { Console.WriteLine(ex.ToString()); }
                    break;
                case 'e'://delete
                    Console.WriteLine("enter id of en engineer to delete");
                    int idDelete=int.Parse(Console.ReadLine()!);
                    try
                    {
                        s_dalEngineer!.Delete(idDelete);
                    }
                    catch(Exception ex) { Console.WriteLine(ex.ToString()); }
                    break;
            }
        }
        public static void CRUDTask(char choose)
        {
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
                        int result = s_dalTask!.Create(task);
                        Console.WriteLine("the task was added");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'b'://read a task by id
                    Console.WriteLine("enter an id number for read");
                    int id = int.Parse(Console.ReadLine()!);
                    try
                    {
                        Console.WriteLine(s_dalTask?.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    break;
                case 'c'://read all tasks
                    Console.WriteLine("all  tasks:");
                    List<DO.Task> arryOfAllTask = s_dalTask!.ReadAll();
                    foreach (var item in arryOfAllTask)
                        Console.WriteLine(item);
                    break;
                case 'd'://update the task
                    Console.WriteLine("enter id of tasky to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);
                    try
                    {
                        Console.WriteLine("enter alias task");
                        string upalias = Console.ReadLine()!;
                        Console.WriteLine("enter task description ");
                        string updescription = Console.ReadLine()!;
                        DO.Task upTask = new(0, updescription, upalias);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                   
                    break;
                case 'e':
                    Console.WriteLine("enter id of task to delete");
                    int idForDelete = int.Parse(Console.ReadLine()!);
                    try
                    {
                        s_dalTask.Delete(idForDelete);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
            }
        }
        public static void CRUDDependency(char choose)
        {
            switch (choose)
            {

                case 'a'://add                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            t = new Task();
                    Console.WriteLine("enter a dependent task id");
                    int id = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter the  task befor ");
                    int idBefor = int.Parse(Console.ReadLine()!);
                    DO.Dependency d = new(4, id, idBefor);
                    try
                    {

                        int result = s_dalDependency!.Create(d);
                        Console.WriteLine("the dependency was added");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'b'://read by id
                    Console.WriteLine("enter dependency id to read");
                    int myId = int.Parse(Console.ReadLine()!);

                    try
                    {
                        Console.WriteLine(s_dalDependency?.Read(myId));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c'://read all
                    Console.WriteLine("all  dependencies:");
                    List<DO.Dependency> arryOfAllDepdndencies = s_dalDependency!.ReadAll();
                    foreach (var dep in arryOfAllDepdndencies)
                        Console.WriteLine(dep);
                    break;
                case 'd'://update
                    Console.WriteLine("enter id of dependency to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                    try
                    {
                        Console.WriteLine("enter  dependent task id");
                        int depId = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter task befor ");
                        int depIdBefor = int.Parse(Console.ReadLine()!);
                        DO.Dependency upDepend = new(idUpdate, depId, depIdBefor);

                        s_dalDependency?.Update(upDepend);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'e'://delete a dependency
                    Console.WriteLine("enter id of dependency to delete");
                    int idForDelete = int.Parse(Console.ReadLine()!);
                    try
                    {
                        s_dalDependency?.Delete(idForDelete);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default:
                    break;
            }

        }

        public static char submenu<T>()
        {
            Console.WriteLine("for add a" + typeof(T) + " press a");
            Console.WriteLine("for read a" + typeof(T) + " press b");
            Console.WriteLine("for read all " + typeof(T) + " press c");
            Console.WriteLine("for update a" + typeof(T) + " press d");
            Console.WriteLine("for delete a " + typeof(T) + " press e");
            char choose=char.Parse(Console.ReadLine()!);
            return choose;
          /* switch(typeof(T)) 
            {
                case Engineer:CRUDEngineer(choose); break;
                case DO.Task: CRUDTask(choose); break;
                case Dependency: CRUDDependency(choose); break;
            }*/
        }

        static void Main(string[] args)
        {
            Initialization.DO(s_dalEngineer, s_dalTask, s_dalDependency);
           // Console.WriteLine("For engineer press 1");
           // Console.WriteLine("For task press 2");
           // Console.WriteLine("For dependency press 3");
           // Console.WriteLine("For exit press 0");
           // int choose = int.Parse(Console.ReadLine()!);
           //switch(choose)
           // {
           //     case 0: 
           //         break;
           //     case 1: CRUDEngineer();
           //         break;
           //     case 2: submenu<DO.Task>();
           //         break;
           //     case 3: submenu<Dependency>();
           //         break;
           //     default:
           //         break;
           // }
        }
            
    }
}