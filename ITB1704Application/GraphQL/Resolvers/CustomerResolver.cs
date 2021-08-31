using ITB1704Application.DataRepositories;
using ITB1704Application.Model;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITB1704Application.GraphQL.Resolvers
{
    public class CustomerResolver
    {
        public async Task<IEnumerable<Transaction>> GetTransactionsByCustomerAsync(
            string? order,
            DateTime? from,
            DateTime? to,
           [Parent] Customer customer,
           [Service] CustomerRepository repository)
        {
            return await repository.GetTransactionsByCustomerAsync(customer.Id, order,from,to);
        }
    }
}
