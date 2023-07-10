using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using ResourcesWebApi.Models;
using Azure.Identity;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Rest.Azure.Authentication;
using Azure.Core;
using System;
using System.Threading.Tasks;
using Finops.Data;

namespace ResourcesWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly FinopsDbContext resourceDbContext;

        public SubscriptionController(FinopsDbContext resourceDbContext)
        {
            this.resourceDbContext = resourceDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResources([FromQuery] ResourcesData data)
        {
            var tenantId = data.tenantId;
            var clientId = data.clientId;
            var objectId = data.objectId;
            var clientSecret = data.clientSecret;
            var subscriptionId = data.subscriptionId;

            var credentials = new ClientCredential(clientId, clientSecret);
            var context = new AuthenticationContext($"https://login.microsoftonline.com/{tenantId}");
            var result = await context.AcquireTokenAsync("https://management.azure.com/", credentials);
            var accessToken = result.AccessToken;

            var resourceClient = new ResourceManagementClient(new TokenCredentials(accessToken));
            resourceClient.SubscriptionId = subscriptionId;

            var resources = await resourceClient.Resources.ListAsync();

            foreach (var resource in resources)
            {
                Console.WriteLine($"Id: {resource.Id}");

                if (resource.Id != null)
                {
                    Console.WriteLine("Subscription exists:");
                }
                else
                {
                    Console.WriteLine("Invalid Subscription:");
                }

                if (resource.Tags != null)
                {
                    foreach (var tag in resource.Tags)
                    {
                        Console.WriteLine($"{tag.Key}: {tag.Value}");
                    }
                }

                Console.WriteLine();
            }

            return Ok(resources);
        }

        // [HttpPost]
        // public async Task<IActionResult> AddResource(Resources resource)
        // {
        //     resourceDbContext.Resources.Add(resource);
        //     await resourceDbContext.SaveChangesAsync();

        //     if (resource.Tags != null)
        //     {
        //         foreach (var tag in resource.Tags)
        //         {
        //             var Tag = new Tag
        //             {
        //                 Key = tag.Key,
        //                 Value = tag.Value,
        //                 ResourceId = resource.Id
        //             };

        //             resourceDbContext.Tags.Add(Tag);
        //         }
        //         await resourceDbContext.SaveChangesAsync();
        //     }

        //     return Ok(resource);
        // }
    }
}
