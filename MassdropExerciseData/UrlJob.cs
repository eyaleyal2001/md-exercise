using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassdropExerciseData
{
    /// <summary>
    /// A specific type of a job that contains URL as data
    /// </summary>
    public partial class UrlJob : Job
    {
        [Required]
        [Url]
        public string Url { get; set; }


    }
}
