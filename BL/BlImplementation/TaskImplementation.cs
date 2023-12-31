namespace BlImplementation;
using BlApi;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        if (boTask.Id <= 0)//check if the id is positive
            throw new BO.BlNotValidInputException("The id must be positive");
        if (boTask.Alias == "")//check if the user insert an alias
            throw new BO.BlNotValidInputException("The Alias must contain atleast one letter");
        DO.Task doTask = new DO.Task//Creating a task in the structure of DO
(boTask.Id, boTask.Description, boTask.Alias, boTask.Milestone == null ? false : true, null, null, null, null, null, null, null, null, null, true);
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

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        return new BO.Task()
        {
            Id = id,
            Description = doTask.Description!,
            Alias = doTask.Alias!,
            CreatedAtDate = doTask.CreatedAt ?? DateTime.MinValue,

        };

    }

    public IEnumerable<BO.Task> ReadAll()
    {
        //return (from DO.Task doTask in _dal.Task.ReadAll()
        //        select new BO.TaskInList
        //        {
        //            Id = doTask.Id,
        //            Description = doTask.Description,
        //            Alias=doTask.Alias,
        //         //   Status =doTask מה מציבים פה איזה סטטוס מכל האינמים
        //        });
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
}

