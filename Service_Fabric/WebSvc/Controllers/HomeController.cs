using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CounterInterfaces;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Client;
using System.Fabric;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using ActorSvc.Interfaces;

namespace WebSvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            ICounter counter = ServiceProxy.Create<ICounter>(new Uri("fabric:/Application1/StatefulSvc"), new ServicePartitionKey(0));

            long count = await counter.GetCountAsync();

            ViewData["Message"] = count.ToString();

            return View();
        }

        public async Task<IActionResult> Contact()
        {
            // Create a randomly distributed actor ID
            ActorId actorId = ActorId.CreateRandom();

            actorId = new ActorId("NewActor");

            // This only creates a proxy object, it does not activate an actor or invoke any methods yet.
            IActorSvc myActor = ActorProxy.Create<IActorSvc>(actorId, new Uri("fabric:/Application1/ActorSvcActorService"));

            // This will invoke a method on the actor. If an actor with the given ID does not exist, it will be activated by this method call.
           
            int count = await myActor.GetCountAsync();
            await myActor.SetCountAsync(count + 1);

            ViewData["Message"] = count.ToString();

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
