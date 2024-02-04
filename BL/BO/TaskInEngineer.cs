using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO
{
    public class TaskInEngineer
    {
        public int Id { get; init; }
        public required string Alias { get; set; }
        public override string ToString() => $" Id: {Id}, Alias: {Alias}";
    }
}
