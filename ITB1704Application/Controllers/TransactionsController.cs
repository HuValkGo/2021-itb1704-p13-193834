using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITB1704Application;
using ITB1704Application.Model;

namespace ITB1704Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(string? order, DateTime? from, DateTime? to)
        {
            var query = _context.Transactions.AsQueryable();
            if(order != null)
            {
                if(order=="desc") query = query.OrderByDescending(x => x.TransactionTime);
                if (order == "asc") query = query.OrderBy(x => x.TransactionTime);
            }
            if(from != null)
            {
                query = query.Where(x => x.TransactionTime >= from);
            }
            if (to != null)
            {
                query = query.Where(x => x.TransactionTime <= to);
            }

            return await query.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            
            var customer = _context.Customers.FirstOrDefault(x => x.Id == transaction.CustomerId);
            var product = _context.Products.FirstOrDefault(x => x.Id == transaction.ProductId);
            if (customer == null || product == null)
            {
                return NotFound();
            }
            if (transaction.Quantity <= 0)
            {
                return BadRequest();
            }
            var totalPrice = Math.Round(transaction.Quantity * product.Price,2);
            var productPrice = product.Price;
            var transactionNew = new Transaction()
            {
                CustomerId = transaction.CustomerId,
                ProductId = transaction.ProductId,
                Id = transaction.Id,
                Quantity = transaction.Quantity,
                TotalPrice = totalPrice,
                TransactionNo = transaction.TransactionNo,
                TransactionTime = transaction.TransactionTime,
                UnitPrice = product.Price
            };
            _context.Transactions.Add(transactionNew);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transactionNew);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return Ok(transaction);
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
