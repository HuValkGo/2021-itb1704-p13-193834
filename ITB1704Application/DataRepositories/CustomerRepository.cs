using ITB1704Application.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITB1704Application.DataRepositories
{
    public class CustomerRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public CustomerRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Transaction>> GetTransactionsByCustomerAsync(int customerId,string? order, DateTime? from , DateTime? to)
        {
            using var context = _contextFactory.CreateDbContext();

            var query = context.Transactions.Where(x=> x.CustomerId == customerId).AsQueryable();
            if (order != null)
            {
                if (order == "desc") query = query.OrderByDescending(x => x.TransactionTime);
                if (order == "asc") query = query.OrderBy(x => x.TransactionTime);
            }
            if (from != null)
            {
                query = query.Where(x => x.TransactionTime >= from);
            }
            if (to != null)
            {
                query = query.Where(x => x.TransactionTime <= to);
            }
            return await query.ToListAsync();
        }
        public async Task<List<Customer>> GetCustomersAsync(string? name, string? code)
        {
            using var context = _contextFactory.CreateDbContext();
            var query = context.Customers.AsQueryable();
            if (name != null)
                query = query.Where(x => x.Name.ToUpper().Contains(name.ToUpper()));
            if (code != null)
                query = query.Where(x => x.Code.ToUpper().Contains(code.ToUpper()));
            return await query.ToListAsync();
        }

    }
}
