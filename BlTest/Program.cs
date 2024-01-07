using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Reflection.Emit;

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
                    case 'f':
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
                        Console.WriteLine("enter task description");
                        string description = Console.ReadLine()!;
                        Console.WriteLine("enter alias task");
                        string alias = Console.ReadLine()!;
                        //create the dependencies list
                        IEnumerable<BO.TaskInList>? dependenciesList = new List<BO.TaskInList>();
                        Console.WriteLine("enter the id of the dependen task. to end enter 0");
                        int idDependsOnTask = int.Parse(Console.ReadLine()!);
                        BO.Task? dependsOnTask = null;
                        while (idDependsOnTask != 0)
                        {
                            try
                            {
                                dependsOnTask = s_bl.Task!.Read(idDependsOnTask);//read the task that the new task depends on
                            }
                            catch (Exception ex)//check if there is a task with this id
                            {
                                Console.WriteLine(ex.ToString());
                            }
                            //Creating a task in the list with the data of the task we read
                            dependenciesList.ToList().Add(new BO.TaskInList()
                            {
                                Id = idDependsOnTask,
                                Alias = dependsOnTask!.Alias,
                                Description = dependsOnTask!.Description,
                                Status = dependsOnTask!.Status
                            });//add this task to the dependencies list
                            idDependsOnTask = int.Parse(Console.ReadLine()!);
                        }
                        Console.WriteLine("enter the deliverables of the task");
                        string deliverables = Console.ReadLine()!;
                        Console.WriteLine("enter the remarks of the task");
                        string remarks = Console.ReadLine()!;
                        Console.WriteLine("enter the id of the engineer engineering the task");
                        int engineerId = int.Parse(Console.ReadLine()!);
                        BO.Engineer? engineer = null;
                        try
                        {
                            engineer = s_bl.Engineer.Read(engineerId);//read the id of the engineer that engineering this task
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        BO.Task newTask = new BO.Task()
                        {
                            Id = 0,
                            Description = description,
                            Alias = alias,
                            DependenciesList = dependenciesList,
                            CreatedAtDate = DateTime.Now,
                            Status = (BO.Status)0,
                            Milestone = null,
                            BaselineStartDate = null,
                            StartDate = null,
                            ScheduledStartDate = null,
                            ForecastDate = null,
                            DeadlineDate = null,
                            CompleteDate = null,
                            Deliverables = deliverables,
                            Remarks = remarks,
                            Engineer = new BO.EngineerInTask() { Id = engineer!.Id, Name = engineer.Name },
                            CopmlexityLevel = (BO.EngineerExperience)engineer.Level!
                        };
                        int result = s_bl!.Task!.Create(newTask);
                        Console.WriteLine("the task was added");
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
                        Console.WriteLine("all tasks:");
                        IEnumerable<BO.Task?> arryOfAllTask = s_bl!.Task!.ReadAll();
                        foreach (var item in arryOfAllTask)
                            Console.WriteLine(item);
                        break;
                    case 'd'://update the task
                        Console.WriteLine("enter id of task to update");
                        int updateId = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter task description");
                        string updateDescription = Console.ReadLine()!;
                        Console.WriteLine("enter alias task");
                        string updateAlias = Console.ReadLine()!;
                        //update the dependencies list
                        IEnumerable<BO.TaskInList>? updateDependenciesList = new List<BO.TaskInList>();
                        Console.WriteLine("enter the id of the dependen task. to end enter 0");
                        int updateIdDependsOnTask = int.Parse(Console.ReadLine()!);
                        BO.Task? updateDependsOnTask = null;
                        while (updateIdDependsOnTask != 0)
                        {
                            try
                            {
                                dependsOnTask = s_bl.Task!.Read(updateIdDependsOnTask);//read the task that the new task depends on
                            }
                            catch (Exception ex)//check if there is a task with this id
                            {
                                Console.WriteLine(ex.ToString());
                            }
                            //Creating a task in the list with the data of the task we read
                            updateDependenciesList.ToList().Add(new BO.TaskInList()
                            {
                                Id = updateIdDependsOnTask,
                                Alias = updateDependsOnTask!.Alias,
                                Description = updateDependsOnTask!.Description,
                                Status = updateDependsOnTask!.Status
                            });//add this task to the dependencies list
                            updateIdDependsOnTask = int.Parse(Console.ReadLine()!);
                        }
                        Console.WriteLine("enter the task creation date ");
                        DateTime updateCreatedAtDate = DateTime.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter the baseline start date of the task");
                        DateTime updateBaselineStartDate = DateTime.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter the start date of the task");
                        DateTime updateStartDate = DateTime.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter the scheduled start date of the task");
                        DateTime updateScheduledStartDate = DateTime.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter the forecast date of the task");
                        DateTime updateForecastDate = DateTime.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter the deadline date of the task");
                        DateTime updateDeadlineDate = DateTime.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter the complete date of the task");
                        DateTime updateCompleteDate = DateTime.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter the deliverables of the task");
                        string updateDeliverables = Console.ReadLine()!;
                        Console.WriteLine("enter the remarks of the task");
                        string updateRemarks = Console.ReadLine()!;
                        Console.WriteLine("enter the id of the engineer engineering the task");
                        int updateEengineerId = int.Parse(Console.ReadLine()!);
                        BO.Engineer? updateEngineer = null;
                        try
                        {
                            updateEngineer = s_bl.Engineer.Read(updateEengineerId);//read the id of the engineer that engineering this task
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        //create the updated task
                        try
                        {
                            s_bl!.Task!.Update(new BO.Task()
                            {
                                Id = updateId,
                                Alias = updateDependsOnTask!.Alias,
                                Description = updateDependsOnTask!.Description,
                                DependenciesList = updateDependenciesList,
                                CreatedAtDate = updateCreatedAtDate,
                                Status = (BO.Status)0,//לשאול תא המורה מה להציב פה
                                Milestone = null,//לתקן את זה אחרי שעושים את אבן דרך
                                BaselineStartDate = updateBaselineStartDate,
                                StartDate = updateBaselineStartDate,
                                ScheduledStartDate = updateScheduledStartDate,
                                ForecastDate = updateForecastDate,
                                DeadlineDate = updateDeadlineDate,
                                CompleteDate = updateCompleteDate,
                                Deliverables = updateDeliverables,
                                Remarks = updateRemarks,
                                Engineer = new BO.EngineerInTask() { Id = updateEngineer!.Id, Name = updateEngineer.Name },
                                CopmlexityLevel = (BO.EngineerExperience)updateEngineer.Level!
                            });
                            Console.WriteLine("The update was successful");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                    case 'e':
                        Console.WriteLine("enter id of task to delete");
                        int idForDelete = int.Parse(Console.ReadLine()!);
                        //try
                        //{
                        //    s_bl!.Task!.Delete(idForDelete);
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine(ex);
                        //}
                        break;
                    case 'f':
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
                      //  BOMilestone();
                        break;
                    default:
                        break;
                }
            }
            while (choose != 0);



        }

    }
}