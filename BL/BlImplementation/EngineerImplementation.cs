namespace BlImplementation;
using BlApi;

using System.Collections.Generic;

internal class EngineerImplementation :IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)//add an engineer
    {//validation
        if (boEngineer.Id < 0)//if the id isnt valid
        throw new NotImplementedException();
        if (boEngineer.Name =="")//if the name is empty
            throw new NotImplementedException();
        if (boEngineer.Cost < 0)//if the COST isnt valid
            throw new NotImplementedException();
       // if(boEngineer.Email) validate email

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer? Raed(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }

    

    BO.Engineer? IEngineer.Raed(int id)
    {
        throw new NotImplementedException();
    }

    IEnumerable<BO.Engineer> IEngineer.ReadAll()
    {
        throw new NotImplementedException();
    }
}

