

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    //Creates new entity object in DAL
    public int Create(Engineer item)
    {
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    //Reads entity object by its ID 
    public Engineer? Read(int id)
    {
        return DataSource.Engineers.FirstOrDefault(engineer => engineer?.Id == id);
    }
    //reads the entity object that the filter function returns for it true
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(engineer => filter(engineer!));
    }
    //Reads all entity objects
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }


    //Updates entity object
    public void Update(Engineer item)
    {
        Engineer? engineer = DataSource.Engineers.FirstOrDefault(engineer => engineer?.Id == item.Id);
        if (engineer is null)
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} is not exists");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(item);
    }

    //Deletes an object by its Id
    public void Delete(int id)
    {
        Engineer? engineer = DataSource.Engineers.FirstOrDefault(engineer => engineer?.Id == id);
        if (engineer is null)
            throw new DalDoesNotExistException($"Engineer with ID={id} is not exists");
        if (DataSource.Tasks.Find(x => x?.EngineerId == id) is not null)///if the task cant be delete
            throw new DalDeletionImpossible($"Task with ID={id} cant be deleted");
        Engineer newEngineer = new Engineer(engineer.Id, engineer.Name, engineer.Email, engineer.Cost, engineer.Level, false);
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(newEngineer);
    }
}
