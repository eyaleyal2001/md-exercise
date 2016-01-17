using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassdropExerciseData;

namespace MassdropProcess
{
    /// <summary>
    /// Will read jobs from DB 
    /// </summary>
    public class JobsReader
    {
        private ConcurrentQueue<Job> _JobsQueue;

        public JobsReader(ConcurrentQueue<Job> jobsQ)
        {
            _JobsQueue = jobsQ;
        }

        internal void Run()
        {
            Console.WriteLine("Starting queue reader");

            while (true)
            {
                Task.Delay(2000);

                using (var dbContext = new MassdropContext())
                {
                    var jobs = from jobItem in dbContext.UrlJobs
                               where jobItem.JobStatus == JobStatus.New
                               select jobItem;

                    foreach (Job jobItem in jobs)
                    {
                        jobItem.JobStatus = JobStatus.InQueue;
                        _JobsQueue.Enqueue(jobItem);
                    }

                    dbContext.SaveChanges();
                }

             
            }
        }
    }
}
