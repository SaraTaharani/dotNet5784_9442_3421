﻿namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Threading.Tasks;

/*מה עוד נשאר?
*פונקציה שמחשבת אבן דרך בשביל אובייקט BOTask
*
*/
internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    //Help functions
    private IEnumerable<BO.TaskInList>? CalculationOfDependencies(int id)
    {
        IEnumerable<DO.Dependency?> allDependencies = _dal.Dependency.ReadAll(dependency => dependency?.DependentTask == id);//create a list of all the dependencies of the task with the id that the function get 
        IEnumerable<DO.Task?> allTasks;
        foreach (DO.Dependency? dependency in allDependencies )
        {
            allTask.Add( _dal.Task.Read((int)dependency?.DependentTask!));//אני רוצה ליצור פה רשימה של משימות שהמשימה הזו תלויה בהם
        }

        //from DO.Task doTask in _dal.Task.ReadAll(task=> task?.Id == allDependencies.FirstOrDefault())
        IEnumerable<BO.TaskInList> listOfTasksInList = //create a list of all the Tasks with linqToObject
         from DO.Task doTask in allTasks//עבור כל משימה ברשימת המשימות ניצור אוביקט 
        select new BO.TaskInList()//create the objects in the list
            {
                Id = doTask.Id,
                Description = doTask.Description!,
                Alias = doTask.Alias!,
                Status = CalculationOfStatus(doTask)
            };
        return listOfTasksInList;//מחזיר את רשימת כל המשימות 
    }
    private BO.EngineerInTask? CalculateEngineer(DO.Task doTask)
    {
        int engineerId = doTask.EngineerId;
        return new BO.EngineerInTask()
        {
            Id = engineerId,
            Name = _dal.Engineer.Read(engineerId)!.Name,
        };
    }
    private BO.MilestoneInTask? CalculationOfMilestone(DO.Task doTask)
    {
        return null;
    }
    private BO.Status CalculationOfStatus(DO.Task task)
    {
        if (task.StartDate == null || task.ScheduledDate==null)//case that the task doesnt have a start date
            return (BO.Status)0;
        else if(task.ScheduledDate!=null && task.StartDate==null)//case that the task has a date of start in  the scedualed but still doesnt have a start date
            return (BO.Status)1;
        else if (task.ScheduledDate != null && task.CompleteDate != null)//case that the task started but doesnt completed
            return (BO.Status)2;
        else if (task.CompleteDate==null && task.DeadlineDate < DateTime.Now)//case that the task need to be completed but didnt finish
            return (BO.Status)3;
        else 
            return (BO.Status)4;//case that the task completed
    }
    //CRUD functions
    public int Create(BO.Task boTask)
    {
        if (boTask.Id <= 0)//check if the id is positive
            throw new BO.BlNotValidInputException("The id must be positive");
        if (boTask.Alias == "")//check if the user insert an alias
            throw new BO.BlNotValidInputException("The Alias must contain atleast one letter");
        DO.Task doTask =//Creating a task in the structure of DO
 new DO.Task()
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
 };
        try
        {
            int idTsk = _dal.Task.Create(doTask);
            return idTsk;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
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
            CopmlexityLevel = (BO.EngineerExperience)doTask.Complexity
        };

    }
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<BO.Task?> allTasks =//create a list of all the engineers with linqToObject
            from DO.Task doTask in _dal.Task.ReadAll()
            select new BO.Task()//create the objects in the list
            {
                Id = doTask.Id,
                Description = doTask.Description!,
                Alias = doTask.Alias!,
                CreatedAtDate = doTask.CeratedAtDate,
            };
        if (filter == null)
            return allTasks!;
        return allTasks.Where(filter!)!;//Filter by function
    }
    public void Update(BO.Task boTask)
    {
        if (boTask.Id <= 0)//check if the id is positive
            throw new BO.BlNotValidInputException("The id must be positive");
        if (boTask.Alias == "")//check if the user insert an alias
            throw new BO.BlNotValidInputException("The Alias must contain atleast one letter");
        DO.Task doTask = new DO.Task
        {
            Id = boTask.Id,
            Description = boTask.Description,
            Alias = boTask.Alias,

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
/*קוד 
 
internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;

    private BO.Status calculateStatus(DO.Task? doTask)
    {
        BO.Status status;
        if (doTask?.ScheduledDate == null || doTask.StartDate == null)//עדיין לא התחיל
            status = 0;
        else if ((doTask.StartDate != null && doTask.StartDate > DateTime.Now) || doTask.CompleteDate == null)//מתוכנן כבר, עדין לא התחיל
            status = (BO.Status)2;
        else if ((doTask.CompleteDate != null && doTask.CompleteDate > DateTime.Now) || doTask.DeadlineDate == null)//התחיל, עדין לא נגמר
            status = (BO.Status)3;
        else if (doTask.DeadlineDate != null && doTask.DeadlineDate < DateTime.Now)//עבר את תאריך הסיום המתוכנן
            status = (BO.Status)4;
        else
            status = (BO.Status)0;

        return status;
    }

    private BO.MilestoneInTask? calculateMilestone(List<BO.TaskInList?>? tasksInList)
    {
        if (tasksInList == null) return null;
        BO.TaskInList? task = tasksInList.Where(task => task != null && _dal.Task.Read(task.Id)!.IsMilestone == true).FirstOrDefault();
        if (task == null) return null;
        return new BO.MilestoneInTask() { Id = task.Id, Alias = task.Alias };
    }

    private BO.EngineerInTask? calculateEngineer(DO.Task doTask)
    {
        BO.EngineerInTask engineer;
        if (doTask?.Engineerid == null)
        {
            engineer = new BO.EngineerInTask()
            {
                Id = doTask!.Engineerid,
                Name = _dal.Engineer.Read(doTask.Id)?.Name ?? string.Empty
            };
        }
        else
        {
            engineer = null!;
        }
        return engineer;
    }

    private List<BO.TaskInList?>? calculateTaskInList(int id)
    {
        List<DO.Dependency?>? dependencyList = new List<DO.Dependency?>(_dal.Dependency.ReadAll(dependency => dependency.DependentTask == id));

        List<BO.TaskInList?>? tasksInList = new List<BO.TaskInList?>(dependencyList.Select(dependency =>
        {
            if (dependency?.DependsOnTask != null)
            {
                var task = _dal.Task.Read(dependency.DependsOnTask);
                if (task != null)
                {
                    return new BO.TaskInList
                    {
                        Id = dependency.DependsOnTask,
                        Description = task.Description,
                        Alias = task.Alias,
                        Status = calculateStatus(task)
                    };
                }
            }
            return null;
        })).Where(dependency => dependency != null).ToList();
        return tasksInList;
    }

    public int Create(BO.Task task)
    {
        if (task.Id <= 0)
            throw new BO.BlInValidDataException("In valid value of id");
        if (task.Alias.Length < 0)
            throw new BO.BlInValidDataException("In valid length of alias");

        DO.Task doTask = new DO.Task(task.Id,
            task.Description,
            task.Alias,
            task.Engineer!.Id,
            (DO.EngineerExperience)task.ComplexityLevel!,
            task.CreatedAtDate,
            false,
            true,
            (TimeSpan)(task.CompleteDate - task.StartDate)!,
            task.StartDate,
            task.BaselineStartDate,
            task.DeadlineDate,
            task.CompleteDate,
            task.Deliverables,
            task.Remarks);
        try
        {
            if (task.DependenciesList != null)
                foreach (var dependency in task.DependenciesList)
                {
                    if (dependency != null)
                        _dal.Dependency.Create(new DO.Dependency(0, task.Id, dependency.Id));
                }

            int id = _dal.Task.Create(doTask);
            return id;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message, ex);
        }
    }

    public BO.Task Read(int id)
    {
        DO.Task doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"task with id: {id} does not exist");



        BO.Status status = calculateStatus(doTask);

        BO.Task boTask = new BO.Task
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CeratedAtDate,
            Status = status,
            DependenciesList = calculateTaskInList(id),
            Milestone = calculateMilestone(calculateTaskInList(id)),
            BaselineStartDate = doTask.ScheduledDate,
            StartDate = doTask.StartDate,
            ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = calculateEngineer(doTask),
            ComplexityLevel = (BO.EngineerExperience)doTask.Complexity
        };
        return boTask;
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<BO.Task?> tasks = _dal.Task.ReadAll().Select(doTask =>
        {
            if (doTask == null)
                return null;


            return new BO.Task
            {
                Id = doTask.Id,
                Description = doTask.Description,
                Alias = doTask.Alias,
                CreatedAtDate = doTask.CeratedAtDate,
                Status = calculateStatus(doTask),
                DependenciesList = calculateTaskInList(doTask.Id),
                Milestone = calculateMilestone(calculateTaskInList(doTask.Id)),
                BaselineStartDate = doTask.ScheduledDate,
                StartDate = doTask.StartDate,
                ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
                DeadlineDate = doTask.DeadlineDate,
                CompleteDate = doTask.CompleteDate,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Engineer = calculateEngineer(doTask),
                ComplexityLevel = (BO.EngineerExperience)doTask.Complexity
            };
        });
        if (filter == null)
            return tasks;
        return tasks.Where(filter!);
    }


    public void Update(BO.Task task)
    {

        if (Read(task.Id) is null)
        {
            throw new BO.BlDoesNotExistException($"There is no task with id:{task.Id}");
        }
        if (task.Alias.Length < 0)
            throw new BO.BlInValidDataException("In valid length of alias");

        DO.Task doTask = new DO.Task(task.Id,
                   task.Description,
                   task.Alias,
                   task.Engineer!.Id,
                   (DO.EngineerExperience)task.ComplexityLevel!,
                   task.CreatedAtDate,
                   false,
                   true,
                   (TimeSpan)(task.CompleteDate - task.StartDate)!,
                   task.StartDate,
                   task.BaselineStartDate,
                   task.DeadlineDate,
                   task.CompleteDate,
                   task.Deliverables,
                   task.Remarks);
        try
        {
            if (task.DependenciesList != null)
                foreach (var dependency in task.DependenciesList!)
                {
                    if (dependency != null)
                        _dal.Dependency.Create(new DO.Dependency(0, task.Id, dependency.Id));
                }
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex);
        }
    }
}*/
