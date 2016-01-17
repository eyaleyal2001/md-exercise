using MassdropExerciseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MassdropExercise.Models
{
    public class GeneralServices
    {
        /// <summary>
        /// Gets the job by identifier.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public static UrlJob GetUrlJobById(MassdropContext dbContext, int jobId)
        {
            var jobItem = (from job in dbContext.UrlJobs
                           where job.Id == jobId
                           select job).FirstOrDefault();

            return jobItem;
        }
    }
}