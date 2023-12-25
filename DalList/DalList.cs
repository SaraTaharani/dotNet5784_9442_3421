using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal

{
    sealed internal class DalList : IDal
    {
        public static IDal Instance { get; } = new DalList();
        private DalList() { }
        public IEngineer Engineer => new EngineerImplementation();

        public ITask Task =>  new TaskImplementation();

        public IDependency Dependency =>  new DependencyImplementation();
       
        public DateTime? dateBeginProject => DataSource.Config.dateBeginProject;

        public DateTime? dateEndProject => DataSource.Config.dateEndProject;
        public void Reset()
        {

            Engineer.Reset();
            Task.Reset();
            Dependency.Reset();
        }


    }
}
