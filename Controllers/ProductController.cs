using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.API.Models;
using WEB.API.DataAnnotations;
using System.Linq;

namespace WEB.API.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products
                .Include(product => product.Category)
                .AsNoTracking()
                .ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("{categories:int}")]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            var product = await context.Products
                .Include(product => product.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Id == id);

            return product;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context.Products
                .Include(product => product.Category)
                .AsNoTracking()
                .Where(product => product.CategoryId == id)
                .ToListAsync();

            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product model)
        {
            if(ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
                return BadRequest(ModelState);
        } 
    }
}