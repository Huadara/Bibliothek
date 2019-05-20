using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentModel
{
    public class PersonTask
    {
        public int Id { get; set; }
        public int PointsInvested { get; set; }

        public override string ToString()
        {
            return $"Hat {PointsInvested} Punkte in Aufgabe \"{Task}\" investiert.";
        }

        public virtual Person Person { get; set; }
        public virtual Task Task { get; set; }
    }
}
