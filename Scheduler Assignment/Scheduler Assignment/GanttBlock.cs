using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Assignment
{
    internal class GanttBlock
    {
        public string name;
        public float startTime, endTime;

        public GanttBlock(string name, float startTime, float endTime)
        {
            this.name = name;
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }

}
