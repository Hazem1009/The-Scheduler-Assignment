using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_Assignment
{
    internal class SJFNonPreemptive
    {
        public static (float avgWaiting, List<GanttBlock>) SJFNon(List<Process> processes)
        {
            float timer = 0;
            //completion is number of completed processes
            int completion = 0;
            float totalWait = 0;

            List<GanttBlock> gantt = new List<GanttBlock>();

            
            PriorityQueue<Process, float> heap = new PriorityQueue<Process, float>();

            while (completion < processes.Count)
            {
                //add processes to heap in the arrived, priority based on shortest burst, inQueue flag ensures no process duplication
                foreach (Process process in processes)
                {
                    if (process.arrivalTime <= timer && process.burstTime > 0 && !process.inQueue)
                    {
                        heap.Enqueue(process, process.burstTime);
                        process.inQueue = true;
                        //Console.WriteLine("process in : "+ process.name +" "+ process.burstTime); //for debugging 
                    }

                }

                // popping from heap shortest job, adding its gantt block, updating timer and total waiting time
                Process temp = heap.Dequeue();

                gantt.Add(new GanttBlock(temp.name, timer, timer + temp.burstTime));
                totalWait += timer - temp.arrivalTime;
                timer += temp.burstTime;
                temp.endTime = timer;

                temp.burstTime = 0;
                //Console.WriteLine("process out: " + temp.name + " " + temp.burstTime); //for debugging

                //updates number of completed processes
                completion++;
            }

            return (totalWait / processes.Count, gantt);
        }
    }
}
