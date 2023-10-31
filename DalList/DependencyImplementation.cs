

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
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
        return DataSource.Dependencies.Find(x => x?.Id == id);
    }

    //Reads all entity objects
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    //Updates entity object
    public void Update(Dependency item)
    {
        Dependency? dependency = Read(item.Id);
        if (dependency is null)
            throw new Exception($"Dependency with ID={item.Id} is not exists");
        DataSource.Dependencies.Remove(dependency);
        DataSource.Dependencies.Add(item);
    }

    //Deletes an object by is Id
    public void Delete(int id)
    {
        Dependency? dependency = Read(id);
        if (dependency is null)
            throw new Exception($"Dependency with ID={id} is not exists");
        DataSource.Dependencies.Remove(dependency);
    }
}
