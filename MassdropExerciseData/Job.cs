using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassdropExerciseData
{
    /// <summary>
    /// A job is a possible task in the system
    /// </summary>
    public abstract partial class Job
    {
        public Job()
        {
            CreationDate = DateTime.Now;
            JobStatus = MassdropExerciseData.JobStatus.New;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the job status.
        /// </summary>
        /// <value>
        /// The job status.
        /// </value>
        public JobStatus JobStatus { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public string Result { get; set; }
    }
}
