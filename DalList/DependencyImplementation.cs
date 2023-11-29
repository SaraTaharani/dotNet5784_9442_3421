

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    //Creates new entity object in DAL
    public int Create(Dependency item)
    {
        //for entities with auto id
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        DataSource.Dependencies.Add(copy);
        return id;
    }

    //Reads entity object by its ID 
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(dependency => dependency?.Id == id);
    }
    //reads the entity object that the filter function returns for it true
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(dependency => filter(dependency!));
    }
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;
    }


    //Updates entity object
    public void Update(Dependency item)
    {
        Dependency? dependency = DataSource.Dependencies.FirstOrDefault(dependency => dependency?.Id == item.Id);
        if (dependency is null)
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} is not exists");
        DataSource.Dependencies.Remove(dependency);
        DataSource.Dependencies.Add(item);
    }

    //Deletes an object by is Id
    public void Delete(int id)
    {
        Dependency? dependency = DataSource.Dependencies.FirstOrDefault(dependency => dependency?.Id == id);
        if (dependency is null)
            throw new DalDoesNotExistException($"Dependency with ID={id} is not exists");
        DataSource.Dependencies.Remove(dependency);
    }
}
