using Microsoft.WindowsAzure.Jobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJobsTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            JobHost host = new JobHost();
            host.RunAndBlock();
        }

        public static void LowerCase(
            [BlobInput(@"gelenblobcontainer/{name}")] Stream gelenStream,
            [BlobOutput(@"gidenblobcontainer/{name}")] Stream gidenStream)
        {
            string tumMetin = "";

            using (StreamReader st = new StreamReader(gelenStream))
            {
                tumMetin = st.ReadToEnd();
            }

            using (StreamWriter stw = new StreamWriter(gidenStream))
            {
                stw.Write(tumMetin.ToLower());
            }
        }
    }
}
