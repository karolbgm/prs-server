using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using prs_server.Data;
using prs_server.Models;

namespace prs_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public VendorsController(PrsDbContext context)
        {
            _context = context;
        }

        [HttpGet("po/{vendorId}")]
        public async Task<ActionResult<Po>> CreatePo(int vendorId)
        {
           var vendor = await _context.Vendors.FindAsync(vendorId);

            var polineData = await (from p in _context.Products
                                    join rl in _context.RequestLines on p.Id equals rl.ProductId
                                    join r in _context.Requests on rl.RequestId equals r.Id
                                    where p.VendorId == vendorId && r.Status == "APPROVED"
                                    select new
                             {
                             p.Id,
                             Product = p.Name,
                             rl.Quantity,
                             p.Price,
                             LineTotal = p.Price * rl.Quantity
                             }).ToListAsync();
            //total
            //var total = polineData.Sum(x => x.LineTotal);
            var sortedLines = new SortedList<int, Poline>();
            foreach (var line in polineData)
            {
                if (!sortedLines.ContainsKey(line.Id))
                {
                    var poline = new Poline()
                    {
                        Product = line.Product,
                        Quantity = 0,
                        Price = line.Price,
                        LineTotal = line.LineTotal
                    };
                    sortedLines.Add(line.Id, poline);
                }
                sortedLines[line.Id].Quantity += line.Quantity;
                sortedLines[line.Id].LineTotal = sortedLines[line.Id].Price * sortedLines[line.Id].Quantity;

            }

            var newPo = new Po
            {
                Vendor = vendor!,
                Polines = sortedLines.Values,
                PoTotal = sortedLines.Values.Sum(x => x.LineTotal)
            };
            return newPo;
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
