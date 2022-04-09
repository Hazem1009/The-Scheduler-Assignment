using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Assignment
{
    internal class GanttBlock
    {
        string name { get; set; }
        int startTime { get; set; }
        int endTime { get; set; }

        GanttBlock(string name, int startTime, int endTime)
        {
            this.name = name;
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }

}
