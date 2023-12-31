namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;


internal class EngineerImplementation : IEngineer//למה זה אדום??????
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)//add an engineer
    {//validation

        if (boEngineer.Id < 0)//if the id isnt valid
            throw new BO.BlNotValidInputException("The id must be positive");
        if (boEngineer.Name == "")//if the name is empty
            throw new BO.BlNotValidInputException("The name must contain atleast one letter");
        if (boEngineer.Cost < 0)//if the COST isnt valid
            throw new BO.BlNotValidInputException("The cost must be positive");
        if (!boEngineer.Email.Contains("@"))// validate email
            throw new BO.BlNotValidInputException("The email must contain @");
        DO.Engineer doEngineer = new DO.Engineer
            (boEngineer.Id, boEngineer.Name, boEngineer.Email, (int)boEngineer.Cost, (DO.EngineerExperience)boEngineer.Level!, true);
        try//chekcing if the engineer who added is alredy exist
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)//catch the exeption from Dal and   throw the exeption from Bl
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={doEngineer.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);//create a dal engineer object
        if (doEngineer == null)//if this engineer dosnt exist throwan exeption
            throw new BO.BlDoesNotExistExeption($"Engineer with ID={id} does Not exist");
        DO.Task? task = _dal.Task.ReadAll().FirstOrDefault(task => task?.EngineerId == id);//get the first task the engineer is in charge of it
        if (task == null)//if the eng. can be delete
        {
            try
            {
                _dal.Engineer.Delete(id);//try to delete
            }
            catch(Exception ex ){
                throw new BO.BlcantBeDelited("");//It is not possible to delete an engineer who has already finished performing a task or is actively performing a task
            }
        }
        else
        {
            throw new BlcantBeDelited("the engineer is in charge of tasks so he cant be delited");
        }

        
    }
    /*לבדוק האם נכון לתפוס כאן חריגה*/
    public BO.Engineer? Raed(int id)
    {  //try
        //{
        //    DO.Engineer? doEngineer = _dal.Engineer.Read(id);//create a dal engineer object
        //}
        //catch (DO.DalDoesNotExistException ex)
        //{
        //    //  if (doEngineer == null)//if this engineer dosnt exist throwan exeption
        //    throw new BO.BlDoesNotExistExeption($"Engineer with ID={id} does Not exist");
        //}
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);//create a dal engineer object
        if (doEngineer == null)//if this engineer dosnt exist throwan exeption
            throw new BO.BlDoesNotExistExeption($"Engineer with ID={id} does Not exist");
        DO.Task? task = _dal.Task.ReadAll().FirstOrDefault(task => task?.EngineerId == id);//get one of the tasks that this engineer is in charge of it
        return new BO.Engineer()//create the bl object for return
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level!,
            Cost = (double)doEngineer.Cost!,
            Task = new BO.TaskInEngineer() { Id = task!.Id, Alias = task.Alias! },
        };

        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        IEnumerable<BO.Engineer?> allEngineers =//create a list of all the engineers with linqToObject
            from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
            let task= _dal.Task.ReadAll().FirstOrDefault(task => task?.EngineerId == doEngineer.Id)
            select new BO.Engineer()//create the objects in the list
             {
                 Id = doEngineer.Id,
                 Name = doEngineer.Name,
                 Email = doEngineer.Email,
                 Level = (BO.EngineerExperience)doEngineer.Level!,
                 Cost = (double)doEngineer.Cost!,
                 Task = new BO.TaskInEngineer() 
                 { Id = task!.Id, Alias = task.Alias! },
             };
            if (filter == null)
            return allEngineers!;
        return allEngineers.Where(filter!)!;//Filter by function

    }

    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Id < 0)//if the id isnt valid
            throw new BO.BlNotValidInputException("The id must be positive");
        if (boEngineer.Name == "")//if the name is empty
            throw new BO.BlNotValidInputException("The name must contain atleast one letter");
        if (boEngineer.Cost < 0)//if the COST isnt valid
            throw new BO.BlNotValidInputException("The cost must be positive");
        if (!boEngineer.Email.Contains("@"))// validate email
            throw new BO.BlNotValidInputException("The email must contain @");
        DO.Engineer doEngineer = new DO.Engineer
            (boEngineer.Id, boEngineer.Name, boEngineer.Email, (int)boEngineer.Cost, (DO.EngineerExperience)boEngineer.Level!, true);
        try//chekcing if the engineer who added is alredy exist
        {
            _dal.Engineer.Update(doEngineer);
            
        }
        catch (DO.DalDoesNotExistException ex)//catch the exeption from Dal and   throw the exeption from Bl
        {
            throw new BO.BlDoesNotExistExeption($"Engineer with ID={doEngineer.Id} dousnt exists");
        }
    }
}

