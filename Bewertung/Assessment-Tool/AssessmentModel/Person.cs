using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AssessmentModel
{
    public class Person
    {
        [Key]
        public string Name { get; set; }
        public int Points { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Points}pts.";
        }

        public virtual List<PersonTask> PersonTasks { get; set; }
    }
}
