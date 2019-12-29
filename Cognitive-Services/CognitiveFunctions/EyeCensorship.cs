using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CognitiveFunctions
{
    public static class EyeCensorship
    {
        [FunctionName("EyeCensorship")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log, ExecutionContext context)
        {
            #region Preparation

            var imageUrl = req.Query["imageUrl"];

            var config = new ConfigurationBuilder()
               .SetBasePath(context.FunctionAppDirectory)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            var faceEndpoint = config["faceEndpoint"];
            var subscriptionKey = config["faceSubscriptionKey"];
            #endregion

            FaceClient faceClient = new FaceClient(new ApiKeyServiceClientCredentials(subscriptionKey), new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = faceEndpoint
            };
            IList<DetectedFace> faceList = await faceClient.Face.DetectWithUrlAsync(imageUrl, true, true);

            #region Output
            var firstFace = faceList.FirstOrDefault();
            HttpClient client = new HttpClient();
            byte[] buffer = await client.GetByteArrayAsync(imageUrl);

            System.IO.MemoryStream imageStream = new System.IO.MemoryStream();
            SixLabors.ImageSharp.Formats.IImageFormat format;

            using (Image image = Image.Load(buffer, out format))
            {
                var centerWidth = image.Width / 2;
                var centerHeight = image.Height / 2;

                var eyeSize = firstFace.FaceLandmarks.EyeLeftInner.X - firstFace.FaceLandmarks.EyeLeftOuter.X;

                var blurRectangle = new SixLabors.Primitives.Rectangle(
                         System.Convert.ToInt32(firstFace.FaceLandmarks.EyeLeftOuter.X - eyeSize),
                         System.Convert.ToInt32(firstFace.FaceLandmarks.EyeLeftOuter.Y - eyeSize),
                         System.Convert.ToInt32(firstFace.FaceLandmarks.EyeRightOuter.X + eyeSize - (firstFace.FaceLandmarks.EyeLeftOuter.X - eyeSize)),
                         System.Convert.ToInt32(firstFace.FaceLandmarks.EyeRightOuter.Y + eyeSize - (firstFace.FaceLandmarks.EyeLeftOuter.Y - eyeSize)));

                image.Mutate(x => x
                     .BokehBlur(blurRectangle));

                image.Save(imageStream, format);
            }

            return new FileContentResult(imageStream.ToArray(), "image/jpeg");
            #endregion
        }

    }
}
