using DO;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{// An interface for the engineer
    public interface IEngineer
    {
        public IEnumerable<BO.Engineer> ReadAll();//get all engineers
        public BO.Engineer? Read(int id);//GET A SPECIFIX ENGINEER

        public int Create(BO.Engineer boEngineer);//add new engineer
       public void Update(BO.Engineer item);//update an engineer
       public void Delete(int id);//delete an engineer

    }



}
}
