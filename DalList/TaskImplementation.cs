namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    //Creates new entity object in DAL
    public int Create(Task item)
    {
        //for entities with auto id
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    //Reads entity object by its ID 
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(x => x?.Id == id);
    }

    //Reads all entity objects
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }
    //Updates entity object
    public void Update(Task item)
    {
        Task? task = Read(item.Id);
        if (task is null)
            throw new Exception($"Task with ID={item.Id} is not exists");
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(item);
    }

    //Deletes an object by its Id
    public void Delete(int id)
    {
        Task? task = Read(id);
        if (task is null)
            throw new Exception($"Task with ID={id} is not exists");
        if (DataSource.Dependencies.Find(x => x?.DependsOnTask == id) is not null)///if the task cant be delete
            throw new Exception($"Task with ID={id} cant be deleted");
        Task newTask = new Task(DataSource.Config.NextTaskId, task.Description, task.Alias, task.Milestone, task.CreatedAt, task.Start, task.ForecastDate, task.Deadline, task.Complete, task.Deliverables, task.Remarks, task.EngineerId, task.ComlexityLevel, false);
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(newTask);
    }
}
