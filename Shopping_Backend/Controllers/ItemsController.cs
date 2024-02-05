using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping_Backend.Data;
using Shopping_Backend.Models;

namespace Shopping_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Items
        [HttpGet]
        public async Task<List<Item>> Products()
        {
            List<Item> items = await _context.Items.ToListAsync();
            return items;
        }
        [HttpGet]
        public async Task<Item> GetProductDetail(long id)
        {
            Item item = new Item();
            if (!ItemExists(id))
            {
                return item;
            }
            item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("name,description,price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, [Bind("id,name,description,price")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return Ok();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([Bind("id")]long id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool ItemExists(long id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
