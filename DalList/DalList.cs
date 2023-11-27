using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal

{
    sealed public class DalList : IDal
    {
        public IEngineer Engineer => throw new NotImplementedException();

        public ITask Task => throw new NotImplementedException();

        public IDependency Dependency => throw new NotImplementedException();
    }
}
