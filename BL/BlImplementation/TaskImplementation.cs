namespace BlImplementation;
using BlApi;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(BO.Task item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        return new BO.Student()
        {
            Id = id,
            Name = doStudent.Name,
            Alias = doStudent.Alias,
            IsActive = doStudent.IsActive,
            BirthDate = doStudent.BirthDate,
            RegistrationDate = doStudent.RegistrationDate,
            CurrentYear = (BO.Year)(DateTime.Now.Year - doStudent.RegistrationDate.Year)
        };

    }

    public IEnumerable<BO.Task> ReadAll()
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select new BO.TaskInList
                {
                    Id = doTask.Id,
                    Description = doTask.Description,
                    Alias=doTask.Alias,
                 //   Status =doTask מה מציבים פה איזה סטטוס מכל האינמים
                });
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
}

