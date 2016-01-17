using MassdropExerciseData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MassdropProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating a synchronic queue
            ConcurrentQueue<Job> jobs = new ConcurrentQueue<Job>();

            JobsReader reader = new JobsReader(jobs);
            JobsExecuter executer = new JobsExecuter(jobs);

            Task taskReader = Task.Run(() => reader.Run());
            Task taskExecuter1 = Task.Run(() => executer.Run());
            Task taskExecuter2 = Task.Run(() => executer.Run());
            Task taskExecuter3 = Task.Run(() => executer.Run());

            Task.WaitAll(new Task[] { taskReader, taskExecuter1, taskExecuter2, taskExecuter3 });
        }
    }
}
