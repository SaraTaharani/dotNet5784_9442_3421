using DalApi;
using DO;
namespace Dal;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        lst.Add(item);
        XMLTools.SaveListToXMLSerializer<Dependency>(lst, "dependencies");
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        lst.Remove(Read(id)!);
        XMLTools.SaveListToXMLSerializer<Dependency>(lst, "dependencies");
    }

    public Dependency? Read(int id)
    {
        List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        return lst.FirstOrDefault(dependency=>dependency?.Id==id);
    }
    public Dependency? Read(Func<Dependency, bool>? filter =null)
    {
        List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        if (filter != null)
        {
            return lst.Where(filter).FirstOrDefault();
        }
        return lst.FirstOrDefault(dependency => filter!(dependency!));
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        if (filter != null)
        {
            return lst.Where(filter);
        }
        return lst;
    }

    public void Update(Dependency item)
    {
        List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        Dependency? dependency = lst.FirstOrDefault(dependency => dependency?.Id == item.Id);
        if (dependency is null)
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} is not exists");
        lst.Remove(dependency);
        lst.Add(item);
    }
}
