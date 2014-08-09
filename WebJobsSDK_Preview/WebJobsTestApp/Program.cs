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

        public class OrnekKuyrukMesaji
        {
            public string Icerik1 { get; set; }
            public string Icerik2 { get; set; }
        }

        public class GidenMesaj
        {
            public string Icerik1 { get; set; }
            public string Icerik2 { get; set; }
        }
        public static void KuyruktanGelenKuyrugaGider(
            [QueueInput("gelenkuyruk")] OrnekKuyrukMesaji parametre,
            [QueueOutput("gidenkuyruk")] out GidenMesaj gidenParametre,
            [QueueOutput("log")] out string log)
        {
            gidenParametre = new GidenMesaj();
            gidenParametre.Icerik2 = parametre.Icerik1;
            gidenParametre.Icerik1 = parametre.Icerik1;
            log = gidenParametre.Icerik1;
        }

    }
}
