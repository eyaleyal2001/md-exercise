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
    /// Will read jobs from queue and execute them
    /// </summary>
    public class JobsExecuter
    {
        // the queue with all the jobs
        private ConcurrentQueue<Job> _JobsQueue;

        public JobsExecuter(ConcurrentQueue<Job> jobsQ)
        {
            _JobsQueue = jobsQ;
        }



        internal void Run()
        {
            Console.WriteLine("Starting queue executer");

            while (true)
            {
                Job objTempJobItem;

                Task.Delay(1000);

                // Will try to get item from queue
                if (_JobsQueue.TryDequeue(out objTempJobItem))
                {
                    Console.WriteLine(string.Format("Task ID: {0} is working on job ID:{1}",Task.CurrentId, objTempJobItem.Id));

                    // Will update it's status and save it in DB
                    using (var dbContext = new MassdropContext())
                    {
                        var jobItem1 = (from jobItem in dbContext.UrlJobs
                                       where jobItem.Id == objTempJobItem.Id
                                       select jobItem).FirstOrDefault();

                        jobItem1.JobStatus = JobStatus.InProcess;

                        dbContext.SaveChanges();
                    }

                    // Will get the executer
                    JobExecuter executerItem = GetJobExecuter(objTempJobItem);

                    if (executerItem != null)
                    {
                        JobStatus finalStatus = JobStatus.Done;
                        string result = null;

                        try
                        {
                            Console.WriteLine(string.Format("Task ID: {0} is executing on job ID:{1}", Task.CurrentId, objTempJobItem.Id));
                            // Will run the executer job
                            result = executerItem.Execute();
                            Console.WriteLine(string.Format("Task ID: {0} is executed on job ID:{1}", Task.CurrentId, objTempJobItem.Id));
                        }
                        catch (Exception ex)
                        {
                            result = ex.Message;
                            finalStatus = JobStatus.Failed;
                        }
                        finally
                        {
                            // Will save the new status and the result in the db
                            using (var dbContext = new MassdropContext())
                            {
                                var jobItem2 = (from jobItem in dbContext.UrlJobs
                                                where jobItem.Id == objTempJobItem.Id
                                                select jobItem).FirstOrDefault();

                                jobItem2.Result = result;
                                jobItem2.JobStatus = finalStatus;

                                dbContext.SaveChanges();
                            }
                        }
                        

                        Console.WriteLine(string.Format("Task ID: {0} is saved results on job ID:{1}", Task.CurrentId, objTempJobItem.Id));
                    }
                }
            } 
        }

        /// <summary>
        /// The job executers factory.
        /// </summary>
        /// <param name="objTempJobItem">The object temporary job item.</param>
        /// <returns></returns>
        private JobExecuter GetJobExecuter(Job objTempJobItem)
        {
            switch (objTempJobItem.GetType().Name)
            {
                case "UrlJob" :
                    return new UrlJobExecuter((UrlJob)objTempJobItem);
            }

            return null;
        }
    }
}
