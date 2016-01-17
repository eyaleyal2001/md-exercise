using MassdropExercise.Models;
using MassdropExerciseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace MassdropExercise.Controllers
{
    /// <summary>
    /// The API to add jobs. At the moment, processes only URL Jobs
    /// </summary>
    public class JobsController : ApiController
    {
        private MassdropContext _dbContext { get; set; }

        public JobsController()
        {
            _dbContext = new MassdropContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            base.Dispose(disposing);
        }

        // GET: api/Jobs
        public IQueryable<jobDTO> Get()
        {
            var jobsList = from job in _dbContext.UrlJobs
                           select new jobDTO
                           {
                               Id = job.Id,
                               JobStatus = job.JobStatus.ToString(),
                               CreationDate = job.CreationDate
                           };

            return jobsList;
        }

        // GET: api/Jobs/5
        public async Task<IHttpActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobItem = GetJobById(id);

            if (jobItem != null)
            {
                return Ok(jobItem);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Gets the job by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private UrlJob GetJobById(int id)
        {
            return GeneralServices.GetUrlJobById(_dbContext, id);
        }

        // POST: api/Jobs
        public async Task<IHttpActionResult> Post(UrlJob value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            UrlJob newJob = new UrlJob()
            {
                Url = value.Url
            };

            _dbContext.UrlJobs.Add(newJob);

            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = newJob.Id }, newJob);
        }

        // PUT: api/Jobs/5
        // Since it's a jobs queue, i've decided not to enable updates of existing jobs
        public async Task<IHttpActionResult> Put(int id, [FromBody]string value)
        {
            return BadRequest(ModelState);
        }

        // DELETE: api/Jobs/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobItem = GetJobById(id);

            if (jobItem != null)
            {
                _dbContext.UrlJobs.Remove(jobItem);

                await _dbContext.SaveChangesAsync();

                return Ok(jobItem);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
