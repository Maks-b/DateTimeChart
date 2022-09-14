using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartData.Models
{
    public class Charts
    {
        public Charts(List<DateTime> dateAxis, List<DateTime> timeAxis)
        {
            TimeAxis = timeAxis;
            DateAxis = dateAxis;
        }

        public List<DateTime> TimeAxis { get; }
        public List<DateTime> DateAxis { get; }
    }
}
