using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Assignment
{
    internal class PriorityPreemptive
    {
        public static (float, float, List<GanttBlock>) Priority_P(List<Process> processes)
        {
            float totaWaitingTime = 0, totaTurnAroundTime = 0;
            List<GanttBlock> ganttBlocks = new List<GanttBlock>();

            List<float> checkTimes = new List<float>();
            foreach (Process process in processes)
            {
                checkTimes.Add(process.arrivalTime);
            }
            checkTimes.Sort();

            PriorityQueue<Process, int> pq = new PriorityQueue<Process, int>();

            for (int i = 0; i < checkTimes.Count; i++)
            {

                foreach (Process process in processes)
                {
                    if (process.arrivalTime == checkTimes[i])
                        pq.Enqueue(process, process.priority.Value);
                }

                if (i == checkTimes.Count - 1)
                    break;

                Process p_max_priority = pq.Dequeue();
                if (p_max_priority.remainingTime < checkTimes[i + 1] - checkTimes[i])
                {
                    checkTimes.Insert(i + 1, p_max_priority.burstTime - checkTimes[i]);
                }

                p_max_priority.remainingTime = p_max_priority.remainingTime - (checkTimes[i + 1] - checkTimes[i]);
                if (p_max_priority.remainingTime > 0)
                {
                    pq.Enqueue(p_max_priority, p_max_priority.priority.Value);
                }

                ganttBlocks.Add(new GanttBlock(p_max_priority.name, checkTimes[i], checkTimes[i + 1]));
            }

            float time = checkTimes[checkTimes.Count - 1];
            while (pq.Count > 0)
            {
                Process p = pq.Dequeue();
                ganttBlocks.Add(new GanttBlock(p.name, time, time + p.remainingTime));

                time = time + p.remainingTime;
                p.remainingTime = 0;
            }

            foreach (var process in processes)
            {
                var matchedList = ganttBlocks.FindAll(delegate (GanttBlock p)
                {
                    return p.name == process.name;
                });

                totaTurnAroundTime += matchedList[matchedList.Count - 1].endTime - process.arrivalTime;
                totaWaitingTime += matchedList[matchedList.Count - 1].endTime - process.arrivalTime - process.burstTime;
            }

            return (totaWaitingTime / processes.Count, totaTurnAroundTime / processes.Count, ganttBlocks);
        }
    }
}
