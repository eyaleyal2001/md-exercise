using MassdropExerciseData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MassdropProcess
{
    public class UrlJobExecuter : JobExecuter
    {
        private UrlJob _UrlJob = null;

        public UrlJobExecuter(UrlJob jobItem)
        {
            _UrlJob = jobItem;
        }


        public override string Execute()
        {
            string result = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_UrlJob.Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                result = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }

            return result;
        }
    }
}
