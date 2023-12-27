using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    internal interface IMilestone
    {
        public int Create(BO.Milestone item);//Creating the milestone project schedule
        public BO.Milestoe? Read(int id); //Milestone details request
        public void Update(BO.Milestone item);//Update a milestone

    }
}
