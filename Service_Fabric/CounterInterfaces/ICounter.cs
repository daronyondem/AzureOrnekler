using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterInterfaces
{
    //Step by step guidance is here; https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-add-a-web-frontend 
    public interface ICounter : IService
    {
        Task<long> GetCountAsync();
    }
}
