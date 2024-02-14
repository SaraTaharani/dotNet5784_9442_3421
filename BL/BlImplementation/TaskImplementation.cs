namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

/*מה עוד נשאר?
*לעשות את השורה הבאה בקריאייט:הוספת משימות קודמות מתוך רשימת המשימות הקיימת
*/
internal class TaskImplementation : ITask
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    //Help functions
    private IEnumerable<BO.TaskInList>? CalculationOfDependencies(int id)
    {
        IEnumerable<DO.Dependency?> allDependencies = _dal.Dependency.ReadAll(dependency => dependency?.DependentTask == id).ToList();//create a list of all the dependencies of the task with the id that the function get 
        IEnumerable<DO.Task>? allTasks = allDependencies.Select(dependency=> _dal.Task.Read((int)dependency?.DependentTask!)!);//לבדוק שזה עובד
        IEnumerable<BO.TaskInList> listOfTasksInList = //create a list of all the Tasks with linqToObject
         from DO.Task doTask in allTasks!//For each task in the task list we will create an object
         select new BO.TaskInList()//create the objects in the list
            {
                Id = doTask.Id,
                Description = doTask.Description!,
                Alias = doTask.Alias!,
                Status = CalculationOfStatus(doTask)
            };
        return listOfTasksInList;//Returns the list of all tasks 
    }
    private BO.EngineerInTask? CalculateEngineer(DO.Task doTask)
    {
        int? engineerId = doTask.EngineerId;
        if (engineerId == null)
            return null;
        return new BO.EngineerInTask()
        {
            Id = (int)engineerId,
            Name = _dal.Engineer.Read((int)engineerId)!.Name,
        };
    }
    private BO.MilestoneInTask? CalculationOfMilestone(DO.Task doTask)
    {
        // Retrieve all dependencies for the given task ID
        IEnumerable<DO.Dependency?> allDependencies = _dal.Dependency.ReadAll(
            dependency => dependency?.DependsOnTask == doTask.Id).ToList();//create a list of all the dependencies of the task with the id that the function get 
        
        var myMilestone =    // Query the dependent tasks and select those that are milestones
             (from dependency in allDependencies
             let dependentTask = _dal.Task.Read((int)dependency?.DependentTask!)
             where dependentTask.IsMilestone
             select new BO.MilestoneInTask() { Id=dependentTask.Id, Alias=dependentTask.Alias }).FirstOrDefault();

         // Convert the result to a   BO.MilestoneInTask object'
        return myMilestone;
    }
    private BO.Status CalculationOfStatus(DO.Task task)
    {
        if (task.StartDate == null || task.ScheduledDate==null)//case that the task doesn't have a start date
            return (BO.Status)0;
        else if(task.ScheduledDate!=null && task.StartDate==null)//case that the task has a date of start in  the Scheduled but still doesn't have a start date
            return (BO.Status)1;
        else if (task.ScheduledDate != null && task.CompleteDate != null)//case that the task started but doesn't completed
            return (BO.Status)2;
        else if (task.CompleteDate==null && task.DeadlineDate < DateTime.Now)//case that the task need to be completed but didn't finish
            return (BO.Status)3;
        else 
            return (BO.Status)4;//case that the task completed
    }
    //CRUD functions
    public int Create(BO.Task boTask)
    {
        if (boTask.Alias == "")//check if the user insert an alias
            throw new BO.BlNotValidInputException("The Alias must contain atleast one letter");
        DO.Task doTask =//Creating a task in the structure of DO
 new DO.Task()
 {
     Id = 0,
     Description = boTask.Description,
     Alias = boTask.Alias,
     EngineerId = boTask.Engineer!.Id,
     Complexity = (DO.EngineerExperience)boTask.CopmlexityLevel!,
     CeratedAtDate = boTask.CreatedAtDate,
     IsMilestone = false,
     Active = true,
     RequiredEffortTime = (TimeSpan)(boTask.CompleteDate - boTask.StartDate)!,
     StartDate = boTask.StartDate,
     ScheduledDate = boTask.ScheduledStartDate,
     DeadlineDate = boTask.DeadlineDate,
     CompleteDate = boTask.CompleteDate,
     Deliverables = boTask.Deliverables,
     Remarks = boTask.Remarks
 };
        IEnumerable<DO.Dependency>? allDependencies =//Create a list of the dependencies of this task
            boTask.DependenciesList?.Select(dependency => new Dependency()
            { DependentTask = boTask.Id, DependsOnTask = dependency.Id });

        var help = from dependency in allDependencies//create a dependency in the DB for every dependency in the dependenciesList of this task
                   select _dal.Dependency.Create(dependency);
        int idTsk = _dal.Task.Create(doTask);
        return idTsk;
    }
    public BO.Task? Read(int id)
    {
        DO.Task? doTask;
        try
        {
            doTask = _dal.Task.Read(id);//create a dal engineer object
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistExeption(ex.Message);
        }
        return new BO.Task()
        {
            Id = id,
            Description = doTask!.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CeratedAtDate,
            Status =CalculationOfStatus(doTask), //Calculation Of Status by a function
            DependenciesList = CalculationOfDependencies(id),//A function that return a list of the dependencies
            Milestone = CalculationOfMilestone(doTask),//פונקציה שיוצרת אבן דרך מתאימה למשימה
            BaselineStartDate = doTask.ScheduledDate,
            StartDate = doTask.StartDate,
            ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = CalculateEngineer(doTask),//A function that will prepare an engineer object in the task 
            CopmlexityLevel = (BO.EngineerExperience)doTask.Complexity!
        };
    }
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<BO.Task?> allTasks =//create a list of all the engineers with linqToObject
            from DO.Task doTask in _dal.Task.ReadAll()
            select new BO.Task()//create the objects in the list
            {
                Id =doTask.Id,
                Description = doTask!.Description,
                Alias = doTask.Alias,
                CreatedAtDate = doTask.CeratedAtDate,
                Status = CalculationOfStatus(doTask), //Calculation Of Status by a function
                DependenciesList = CalculationOfDependencies(doTask.Id),//A function that return a list of the dependencies
                Milestone = CalculationOfMilestone(doTask),//A function that creates an appropriate milestone for the task
                BaselineStartDate = doTask.ScheduledDate,
                StartDate = doTask.StartDate,
                ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
                DeadlineDate = doTask.DeadlineDate,
                CompleteDate = doTask.CompleteDate,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Engineer = CalculateEngineer(doTask),//A function that will prepare an engineer object in the task 
                CopmlexityLevel = (BO.EngineerExperience)doTask.Complexity!
            };
        if (filter == null)
            return allTasks!;
        return allTasks.Where(filter!)!;//Filter by function
    }
    public void Update(BO.Task boTask)
    {
        if (boTask.Id <= 0)//check if the id is positive
            throw new BO.BlNotValidInputException("The id must be positive");
        //if (boTask.Alias == "")//check if the user insert an alias
        //    throw new BO.BlNotValidInputException("The Alias must contain atleast one letter");
        DO.Task doTask = new DO.Task
        {
            Id = boTask.Id,
            Description = boTask.Description,
            Alias = boTask.Alias,
            EngineerId = boTask.Engineer!.Id,
            Complexity = (DO.EngineerExperience)boTask.CopmlexityLevel!,
            CeratedAtDate = boTask.CreatedAtDate,
            IsMilestone = false,
            Active = true,
            RequiredEffortTime = (TimeSpan)(boTask.CompleteDate - boTask.StartDate)!,
            StartDate = boTask.StartDate,
            ScheduledDate = boTask.ScheduledStartDate,
            DeadlineDate = boTask.DeadlineDate,
            CompleteDate = boTask.CompleteDate,
            Deliverables = boTask.Deliverables,
            Remarks = boTask.Remarks
        };//Creating a task in the structure of DO
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (Exception ex)
        {
            throw new BO.BlDoesNotExistExeption(ex.Message);
        }
    }
    public void Delete(int id)
    {

        //לעשות לאחר שלב 5 על פי הוראת המורה
        //מה המשמעות המעשית של השורה הבאה: - - -שימו לב: אי אפשר למחוק משימות לאחר יצירת לו"ז הפרויקט.
        throw new NotImplementedException();
    }
}

