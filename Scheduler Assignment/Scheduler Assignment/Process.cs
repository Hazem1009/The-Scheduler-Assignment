using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Assignment
{
    internal class Process
    {
        public String name;
        public float arrivalTime;
        public float burstTime;
        public int? priority;
        private static int count = 0;

        Process(float arrivalTime, float burstTime, int? priority = null)
        {
            count++;
            name = "P" + count;
            this.arrivalTime = arrivalTime;
            this.burstTime = burstTime;
            this.priority = priority;
        }
    }
}
