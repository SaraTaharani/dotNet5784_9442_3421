namespace BlImplementation;
using BlApi;

using System.Collections.Generic;

internal class EngineerImplementation :IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)
    {
        throw new NotImplementedException();
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

