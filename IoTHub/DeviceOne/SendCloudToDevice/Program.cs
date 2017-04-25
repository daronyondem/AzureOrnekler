using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendCloudToDevice
{
    class Program
    {
        static string connectionString = "HostName=daron-test.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=dhNCwRuz4hXM6XK4Bw+KZroIDR3YFm5+Fsby+hsygkw=";
        static ServiceClient serviceClient;

        static void Main(string[] args)
        {
            Console.WriteLine("Send Cloud-to-Device message\n");
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            Console.WriteLine("Press any key to send a C2D message.");
            Console.ReadLine();
            SendCloudToDeviceMessageAsync().Wait();
            Console.ReadLine();
        }

        private async static Task SendCloudToDeviceMessageAsync()
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes("Cloud to device message."));
            await serviceClient.SendAsync("myFirstDevice", commandMessage);
        }
    }
}
