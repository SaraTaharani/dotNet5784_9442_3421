using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IMilestone
    {
        public int Create(BO.Milestone item);//Creating the milestone project schedule
        public BO.Milestone? Read(int id); //Milestone details request
        public void Update(BO.Milestone item);//Update a milestone

    }
}
