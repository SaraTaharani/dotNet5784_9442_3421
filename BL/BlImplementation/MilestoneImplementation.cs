namespace BlImplementation;
using BlApi;



internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private BO.Status CalculationOfStatus(DO.Task task)
    {
        if (task.StartDate == null || task.ScheduledDate == null)//case that the task doesn't have a start date
            return (BO.Status)0;
        else if (task.ScheduledDate != null && task.StartDate == null)//case that the task has a date of start in  the Scheduled but still doesn't have a start date
            return (BO.Status)1;
        else if (task.ScheduledDate != null && task.CompleteDate != null)//case that the task started but doesn't completed
            return (BO.Status)2;
        else if (task.CompleteDate == null && task.DeadlineDate < DateTime.Now)//case that the task need to be completed but didn't finish
            return (BO.Status)3;
        else
            return (BO.Status)4;//case that the task completed
    }
    public int Create(BO.Milestone item)
    {


        throw new NotImplementedException();
    }

    public BO.Milestone? Read(int id)
    {
        DO.Task? doTask;
        try
        {
            doTask = _dal.Task.Read(id);//tring tom get the milestone
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistExeption(ex.Message);
        }
        if (!doTask!.IsMilestone)//if its not a milstone
            throw new BO.BlLogicException($"Milestome with id: {id} does no exist");
        List<DO.Dependency> dependencyList = new List<DO.Dependency>(_dal.Dependency.ReadAll(dependency => dependency.DependentTask == id)!);//create a list of dependencies that the mailstone depend on them
        if (dependencyList.Count == 0)
        {
            throw new BO.BlLogicException($"The Milestome with id: {id} doesnt depend");
        }
       List<BO.TaskInList> listOfTasksInList = ( from  dependency in dependencyList!//For each Dependency in the Dependency list we will create an object
        select new BO.TaskInList()//create the objects in the list
        {
            Id = (int)dependency.DependsOnTask!,
            Description = _dal.Task.Read((int)dependency.DependsOnTask!)!.Description,
            Alias = _dal.Task.Read((int)dependency.DependsOnTask!)!.Alias,
            Status = CalculationOfStatus(_dal.Task.Read((int)dependency.DependsOnTask)!)
        }).ToList();

        BO.Milestone boMilestone = new BO.Milestone()//create the milstone object
        {
            Id = id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CeratedAtDate,
            Status = CalculationOfStatus(doTask),
            StartDate = doTask.StartDate,
            ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
            DeadLineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            CompletionPercentage = (listOfTasksInList.Count(t => t.Status == BO.Status.OnTrack) / (double)listOfTasksInList.Count) * 100,//checks how many tasks were done already
            Remarks = doTask.Remarks,
            Dependencies = listOfTasksInList,

        };
        return boMilestone;
    }

    public void Update(BO.Milestone updateMilestone)//updating the milstone
    {
        DO.Task prevmilestone = _dal.Task.Read(updateMilestone.Id)!;//tring tom get the milestone


        DO.Task doTask = new DO.Task()
        {
            Id = prevmilestone.Id,
            Description = updateMilestone.Description,
            Alias = updateMilestone.Alias,
            EngineerId = prevmilestone.EngineerId,
            Complexity = prevmilestone.Complexity,
            CeratedAtDate = prevmilestone.CeratedAtDate,
            IsMilestone = prevmilestone.IsMilestone,
            Active = prevmilestone.Active,
            RequiredEffortTime = prevmilestone.RequiredEffortTime,
            StartDate = prevmilestone.StartDate,
            ScheduledDate = prevmilestone.ScheduledDate,
            DeadlineDate = prevmilestone.DeadlineDate,
            CompleteDate = prevmilestone.CompleteDate,
            Deliverables = prevmilestone.Deliverables,
            Remarks = updateMilestone.Remarks

        };
        _dal.Task.Update(doTask);



        throw new NotImplementedException();
    }
}

