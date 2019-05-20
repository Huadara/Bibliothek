using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentModel
{
    public class Task
    {
        [Key]
        public string Name { get; set; }
        public int TaskPoints { get; set; }
        public int PointsToDistribute { get; set; }

        public override string ToString()
        {
            return $"{Name} - {TaskPoints}pts.";
        }

        public virtual List<PersonTask> PersonTasks { get; set; }
    }
}
