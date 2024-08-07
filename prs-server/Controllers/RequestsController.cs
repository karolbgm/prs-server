﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prs_server.Data;
using prs_server.Models;

namespace prs_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public RequestsController(PrsDbContext context)
        {
            _context = context;
        }

        //CUSTOM METHODS
        //REVIEW - PUT: /api/Requests/review/5
        [HttpPut("review/{id}")]
        public async Task<IActionResult> Review(int id, Request request)
        {
            if (request.Total <= 50)
            {
                request.Status = "APPROVED";
            } else
            {
                request.Status = "REVIEW";
            }

            return await PutRequest(id, request);

        }

        //APPROVE - PUT: /api/requests/approve/5
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id, Request request)
        {
            request.Status = "APPROVED";

            return await PutRequest(id, request);

        }

        //REJECT - PUT: /api/requests/reject/5
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> Reject(int id, Request request)
        {
            request.Status = "REJECTED";

           return await PutRequest(id, request);

        }

        //GET REVIEWS - GET: /api/requests/reviews/{userId}
        [HttpGet("reviews/{userId}")]

        public async Task<ActionResult<IEnumerable<Request>>> GetReviews(int userId)
        {
            return await _context.Requests.Where(x => x.Status == "REVIEW" && x.UserId != userId).Include(x => x.User).ToListAsync();
        }

        //GET REQUESTS BY STATUS - GET: /api/requests/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestByStatus(string status)
        {
            return await _context.Requests
                                    .Include(x => x.User)
                                    .Where(x => x.Status == status).ToListAsync();
        }


        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.Include(x => x.User).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests
                                        .Include(r => r.User).Include(r => r.RequestLines)!            
                                           .ThenInclude(rl => rl.Product)           
                                        .SingleOrDefaultAsync(r => r.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
