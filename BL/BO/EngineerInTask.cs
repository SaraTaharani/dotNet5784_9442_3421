using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class EngineerInTask
    {
        public int Id { get; init; }
        public required string Name { get; set; }
        public override string ToString() => $"MyEngineer {{ Id: {Id}, Name: {Name} }}";
    }
}
