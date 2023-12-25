namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    const string data_config_xml = @"data-config"; 
    public int Create(Task item)
    {
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        int id = XMLTools.GetAndIncreaseNextId("data-config", "NextTaskId");
        Task copy = item with { Id = id };
        lst.Add(copy);
        XMLTools.SaveListToXMLSerializer<Task>(lst, "tasks");
        return id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible($"Task with ID={id} cannot be deleted");
    }

    public Task? Read(int id)
    {
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return lst.FirstOrDefault(tasks => tasks?.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter != null)
        {
            return lst.Where(filter).FirstOrDefault();
        }
        return lst.FirstOrDefault(tasks => filter!(tasks!));
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        return XMLTools.LoadListFromXMLSerializer<Task>("tasks");
    }

    public void Update(Task item)
    {
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? task = lst.FirstOrDefault(task => task?.Id == item.Id);
        if (task is null)
            throw new DalDoesNotExistException($"Task with ID={item.Id} is not exists");
        lst.Remove(task);
        lst.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(lst, "tasks");
    }
    
    public void Reset()
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        root.Element("NextTaskId")?.SetValue((1).ToString());
        XMLTools.SaveListToXMLElement(root, data_config_xml);
        const string tasksFile = @"..\xml\tasks.xml";
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(tasksFile);
        tasks.Clear();
        XMLTools.SaveListToXMLSerializer<Task>(tasks, tasksFile);
        //XDocument tasksDocument = XDocument.Load(tasksFile);
        //XElement? tasksElements = tasksDocument.Root;
        //if (tasksElements != null)
        //{
        //    tasksDocument.Elements().Remove();
        //    tasksDocument.Save(tasksFile);
        //}
    }
}





