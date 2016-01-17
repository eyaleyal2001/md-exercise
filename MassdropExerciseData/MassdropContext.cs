using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassdropExerciseData
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MassdropContext : DbContext
    {
        public MassdropContext()
            : base("name=MassdropContext")
        {
        }

        /// <summary>
        /// Gets or sets the URL jobs.
        /// </summary>
        /// <value>
        /// The URL jobs.
        /// </value>
        public virtual DbSet<UrlJob> UrlJobs { get; set; }
        
    }
}
