using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Milestone
    {
        public int Id { get; init; }
        public required string Description { get; set; }

        public required string Alias { get; set; }
        public required DateTime CreatedAtDate { get; set; }
       
        public Status? Status { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public  DateTime? ForecastDate { get; set; } = null;
        public  DateTime? DeadLineDate { get; set; } = null;
        public  DateTime?  CompleteDate{ get; set; } = null;
        public  double? CompletionPercentage { get; set; } = null;
        public  string? Remarks { get; set; } = null;
        public List<BO.TaskInList>? Dependencies { get; set; } = null;
       // public override string ToString() => this.ToStringProperty();

    }
}
