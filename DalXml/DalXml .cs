using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
     public  class DalXml : IDal
    {
        public IEngineer Engineer => new EngineerImplementation();
        public ITask Task =>  new TaskImplementation();
        public IDependency Dependency => new DependencyImplementation();
       public DateTime? dateBeginProject => Config.dateBeginProject;
        public DateTime? dateEndProject => Config.dateEndProject;
        public void Reset()
        {
            Engineer.Reset();
            Task.Reset();
            Dependency.Reset();
        }
    }
}
