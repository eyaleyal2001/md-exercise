using MassdropExerciseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MassdropExercise.Models
{
    /// <summary>
    /// A class used to reply partial data to the client
    /// </summary>
    public class jobDTO
    {
        public int Id { get; set; }

        public string JobStatus { get; set; }

        public DateTime CreationDate { get; set; }
    }
}