using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlApi
{// An interface for the engineer
    public interface IEngineer
    {
        public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);//get all engineers
        public BO.Engineer? Read(int id);//GET A SPECIFIX ENGINEER
        public int Create(BO.Engineer boEngineer);//add new engineer
       public void Update(BO.Engineer item);//update an engineer
       public void Delete(int id);//delete an engineer

    }
}

