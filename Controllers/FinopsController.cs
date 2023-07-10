using Finops.Data;
using Finops.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Finops.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class FinopsController : Controller
    {
        private readonly FinopsDbContext finopsDbContext;

        public FinopsController(FinopsDbContext finopsDbContext)

        {
            this.finopsDbContext = finopsDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            var resources = await this.finopsDbContext.Resources.ToListAsync();
            return Ok(resources);


 
        }

        [HttpPost]
        public async Task<IActionResult> AddResource([FromBody] Resource resourceRequest)

        {
            resourceRequest.Id = Guid.NewGuid();
            await this.finopsDbContext.Resources.AddAsync(resourceRequest);
            await this.finopsDbContext.SaveChangesAsync();


            return Ok(resourceRequest);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetResource([FromRoute] Guid id)
        {
            var resource =
           await this.finopsDbContext.Resources.FirstOrDefaultAsync(x => x.Id == id);

            if (resource == null)
            {
                return NotFound();
            }
            return Ok(resource);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateResource([FromRoute] Guid id, Resource updateResourceRequest)
        {
            var resource = await this.finopsDbContext.Resources.FindAsync(id);

            if (resource == null)
            {
                return NotFound();
            }
            resource.Name = updateResourceRequest.Name;
            resource.Owner = updateResourceRequest.Owner;
            resource.Owner_Email_Tag = updateResourceRequest.Owner_Email_Tag;
            resource.Ignore_Tag = updateResourceRequest.Ignore_Tag;
            resource.Target_Productivity = updateResourceRequest.Target_Productivity;
            resource.OEE = updateResourceRequest.OEE;
           

            await this.finopsDbContext.SaveChangesAsync();

            return Ok(resource);
        }
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteResource([FromRoute] Guid id)
        {
            var resource = await this.finopsDbContext.Resources.FindAsync(id);

            if (resource == null)
            {
                return NotFound();
            }
            this.finopsDbContext.Resources.Remove(resource);
            await this.finopsDbContext.SaveChangesAsync();

            return Ok(resource);

        }

    }
}
    
