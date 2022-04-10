using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Assignment
{
    internal class PriorityNonPreemptive
    {
        public static (float, List<GanttBlock>) Priority_NP(List<Process> processes)
        {
            float totalWaitingTime = 0;
            List<GanttBlock> ganttBlocks = new List<GanttBlock>();

            /* Sorting The processes based on its Priority */
            processes.Sort((a, b) => a.priority.Value.CompareTo(b.priority.Value));

            /* adding the first gantt block which starts from 0 */
            ganttBlocks.Add(new GanttBlock(processes[0].name, 0, processes[0].burstTime));

            for (int i = 1; i < processes.Count; i++)
            {
                /* the endtime of previous gantt block is the same as the start time of the next gantt block */
                float pStartTime = ganttBlocks[i - 1].endTime;

                /* the time at which the process finishes its burst */
                float pFinishTime = pStartTime + processes[i].burstTime;

                /* Adding a new gantt block starts at the pervious endtime and ends after its burst time */
                ganttBlocks.Add(new GanttBlock(processes[i].name, pStartTime, pFinishTime));

                /*
                 * Calculating the waiting time of the current process
                 * waiting time = pFinishTime - pArrivalTime
                 * but the arrival time = zero 
                 * then, waiting time = pFinishTime
                 */
                totalWaitingTime += pFinishTime;
            }

            /* the average waiting time is the total waiting time over the number of processes */
            return (totalWaitingTime / processes.Count, ganttBlocks);
        }
    }
}
