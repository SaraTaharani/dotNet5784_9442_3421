using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    internal interface ITask
    {
        public int Create(BO.Task item);//Add a new task
        public IEnumerable<BO.Task> ReadAll();//Task list request
        public BO.Task? Read(int id);//Task details request
        public void Update(BO.Task item);//Update a task
        public void Delete(int id);//Delete a task
    }
}
