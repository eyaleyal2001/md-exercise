using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassdropExerciseData
{
    /// <summary>
    /// The statuses a job can have
    /// </summary>
    public enum JobStatus
    {
        New,

        InQueue,

        InProcess,

        Done,

        Failed
    }
}
