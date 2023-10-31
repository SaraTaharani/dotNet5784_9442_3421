

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    //Creates new entity object in DAL
    public int Create(Engineer item)
    {
        //for entities with normal id (not auto id)
        if (Read(item.Id) is not null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    //Reads entity object by its ID 
    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(x => x?.Id == id);
    }

    //Reads all entity objects
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    //Updates entity object
    public void Update(Engineer item)
    {
        Engineer? engineer = Read(item.Id);
        if (engineer is null)
            throw new Exception($"Engineer with ID={item.Id} is not exists");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(item);
    }

    //Deletes an object by its Id
    public void Delete(int id)
    {
        Engineer? engineer = Read(id);
        if (engineer is null)
            throw new Exception($"Engineer with ID={id} is not exists");
        if (DataSource.Tasks.Find(x => x?.EngineerId == id) is not null)///if the task cant be delete
            throw new Exception($"Task with ID={id} cant be deleted");
        Engineer newEngineer = new Engineer(engineer.Id, engineer.Name, engineer.Email, engineer.Cost, engineer.Level, false);
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(newEngineer);
    }
}
