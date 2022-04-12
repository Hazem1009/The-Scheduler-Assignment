using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Assignment
{
    internal class PriorityPreemptive
    {
        /*Function Description: a Function to schedule processes according to priority preempitve scheduling
         *Input:a list of all processes
         *Output:A tuple of following order:(average waiting time,average turn around time and list of gantt blocks created)
         */
        public static (float, float, List<GanttBlock>) Priority_P(List<Process> processes)
        {
            float totaWaitingTime = 0, totaTurnAroundTime = 0;
            List<GanttBlock> ganttBlocks = new List<GanttBlock>();
            /*A list of all Arrival times To keep track of our check points in time*/
            List<float> checkTimes = new List<float>();
            foreach (Process process in processes)
            {
                checkTimes.Add(process.arrivalTime);
            }
            /*Sorting the check points*/
            checkTimes.Sort();
            /*To remove Duplicate check times*/
            checkTimes=checkTimes.Distinct().ToList();
            /*a priority queue to add already arrived items to keep track of processing of next and interrupted processes,prio is according to Process priority*/
            PriorityQueue<Process, int> pq = new PriorityQueue<Process, int>();
            /*looping on all check times*/
            for (int i = 0; i < checkTimes.Count; i++)
            {
                /*each process started in this check time is queued to our priority queue*/
                foreach (Process process in processes)
                {
                    if (process.arrivalTime == checkTimes[i])
                        pq.Enqueue(process, process.priority.Value);
                }
                /*We dont need to continue with same logic if its the last check point we can now start dequeuing based on prio only*/
                if (i == checkTimes.Count - 1)
                    break;

                Process p_max_priority = pq.Dequeue();
                /*Condition to insert a new check point if current process can be completely finished in time less than difference between 2 check times*/
                if (p_max_priority.remainingTime < checkTimes[i + 1] - checkTimes[i])
                {
                    checkTimes.Insert(i + 1, p_max_priority.remainingTime - checkTimes[i]);
                }
                p_max_priority.remainingTime = p_max_priority.remainingTime - (checkTimes[i + 1] - checkTimes[i]);
                /*We need to requeue the process if still got remaining time*/
                if (p_max_priority.remainingTime > 0)
                {
                    pq.Enqueue(p_max_priority, p_max_priority.priority.Value);
                }
                /*Creating a gantt block of processed process*/
                ganttBlocks.Add(new GanttBlock(p_max_priority.name, checkTimes[i], checkTimes[i + 1]));
            }
            /*Now we have passed all check points in Time(check times) and accordingly we just keep dequeuing and processing according to prio*/
            float time = checkTimes[checkTimes.Count - 1];
            while (pq.Count > 0)
            {
                Process p = pq.Dequeue();
                  /*Handles a problem if 2 processes have same prio but p2 has higher process number than 1 
                    *p2 arrived earlier than p1 but both have same prio p1 would interrupt p2 without following block
                    */
                if ((pq.Count!=0)&&(pq.Peek().priority.Value == p.priority.Value)) { 
                    
                    if (pq.Peek().arrivalTime < p.arrivalTime)
                    {
                        Process holder = pq.Dequeue();
                        pq.Enqueue(p,p.priority.Value);
                        p=holder;
                    }
                }
                     
                ganttBlocks.Add(new GanttBlock(p.name, time, time + p.remainingTime));

                time = time + p.remainingTime;/*To keep up with our timeline*/
                p.remainingTime = 0;/*Setting Remaining time of process to 0 as finished*/
            }
            /*Calculation for waiting time and turn around time*/
            foreach (var process in processes)
            {
                /*Finds a list of all gantt blocks containing process name to find start and departure time*/
                var matchedList = ganttBlocks.FindAll(delegate (GanttBlock p)
                {
                    return p.name == process.name;
                });

                totaTurnAroundTime += matchedList[matchedList.Count - 1].endTime - process.arrivalTime;
                totaWaitingTime += matchedList[matchedList.Count - 1].endTime - process.arrivalTime - process.burstTime;
            }
            List<GanttBlock> toDrawBlocks = new List<GanttBlock>();
            for (int it = 0; it < ganttBlocks.Count ; it++)
            {
                float start = ganttBlocks[it].startTime;
                float end = ganttBlocks[it].endTime;
                while ((it < ganttBlocks.Count - 1) && (ganttBlocks[it + 1].name == ganttBlocks[it].name))
                {
                    it++;
                    end = ganttBlocks[it].endTime;
                }
                toDrawBlocks.Add(new GanttBlock(ganttBlocks[it].name, start, end));
            }
           
            return (totaWaitingTime / processes.Count, totaTurnAroundTime / processes.Count, toDrawBlocks);
        }
    }
}
