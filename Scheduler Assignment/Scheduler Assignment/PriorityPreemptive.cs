using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Assignment
{
    internal class PriorityPreemptive
    {
        public static (float, float, List<GanttBlock>) priority_p(List<Process> processes)
        {
            float totaWaitingTime = 0, totaTurnAroundTime = 0;
            List<GanttBlock> ganttBlocks = new List<GanttBlock>();
            
            return (totaWaitingTime / processes.Count, totaTurnAroundTime / processes.Count, ganttBlocks);
        }
    }
}
