
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;


internal class DependencyImplementation : IDependency
{
    const string dependencysFile = @"..\xml\dependencys.xml";
    XDocument dependencysDocument = XDocument.Load(dependencysFile);
    public int Create(Dependency item)
    {
        int newDependencyId = Config.NextDependencyId;

        XElement? dependencyElement = new XElement("Dependency",
            new XElement("Id", newDependencyId),
            new XElement("DependentTask", item.DependentTask),
            new XElement("DependsOnTask", item.DependsOnTask));

        dependencysDocument.Root?.Add(dependencyElement);
        dependencysDocument.Save(dependencysFile);

        return newDependencyId;
        //List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        //lst.Add(item);
        //XMLTools.SaveListToXMLSerializer<Dependency>(lst, "dependencies");
        //return item.Id;
    }

    public void Delete(int id)
    {
        if (dependencysDocument.Root != null)
        {
            XElement? dependencyElement = dependencysDocument.Root
                .Elements("Dependency")
                .FirstOrDefault(e => (int)e.Element("Id")! == id);

            if (dependencyElement != null)
            {
                dependencyElement.Remove();
                dependencysDocument.Save(dependencysFile);
            }
            else
            {
                throw new DalDoesNotExistException("Dependency with the specified ID does not exist.");
            }
        }
        else
        {
            throw new DalDoesNotExistException("Dependencies document is empty.");
        }

        //List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        //lst.Remove(Read(id)!);
        //XMLTools.SaveListToXMLSerializer<Dependency>(lst, "dependencies");
    }

    public Dependency? Read(int id)
    {
        XElement? dependencyElement = dependencysDocument.Root!
                .Elements("Dependency")
                .FirstOrDefault(e => (int)e.Element("Id")! == id);
        if (dependencyElement != null)
        {
            Dependency? dependency = new Dependency(
                (int)dependencyElement.Element("Id")!,
                (int)dependencyElement.Element("DependentTask")!,
                (int)dependencyElement.Element("DependsOnTask")!
            );

            return dependency;
        }

        throw new DalDoesNotExistException("Dependency with the specified ID does not exist.");
        //List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        //return lst.FirstOrDefault(dependency=>dependency?.Id==id);
    }
    public Dependency? Read(Func<Dependency, bool>? filter =null)
    {

        Dependency? dependency = dependencysDocument.Root?
            .Elements("Dependency")
            ?.Select(e => new Dependency(
                (int)e.Element("Id")!,
                (int)e.Element("DependentTask")!,
                (int)e.Element("DependsOnTask")!
            ))
            !.FirstOrDefault(filter);

        return dependency;
        //List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        //if (filter != null)
        //{
        //    return lst.Where(filter).FirstOrDefault();
        //}
        //return lst.FirstOrDefault(dependency => filter!(dependency!));
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {

        XElement? dependenciesElement = XMLTools.LoadListFromXMLElement("dependencys");

        IEnumerable<Dependency> dependencies = dependenciesElement
            .Elements("Dependency")
            .Select(e => new Dependency(
                Id: (int)e.Element("Id")!,
                DependentTask: (int)e.Element("DependentTask")!,
                DependsOnTask: (int)e.Element("DependsOnTask")!
            ));

        if (filter != null)
        {
            dependencies = dependencies.Where(filter);
        }

        return dependencies;//.ToList(); // Convert to List before returning//.ToList(); // Convert to List before returning
        //List<Dependency> lst = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        //if (filter != null)
        //{
        //    return lst.Where(filter);
        //}
        //return lst;
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
