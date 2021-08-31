using ITB1704Application.DataRepositories;
using ITB1704Application.Model;
using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITB1704Application.GraphQL
{
    public class Query
    {
        private readonly TestRepository _testRepository;
        private readonly CustomerRepository _customerRepository;

        public Query( TestRepository testRepository, CustomerRepository customerRepository)
        {
            _testRepository = testRepository
               ?? throw new ArgumentNullException(nameof(testRepository));
            _customerRepository = customerRepository
               ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Test>> GetTestsAsync(string? name)
        {
            return await _testRepository.GetTestsAsync(name);
        }

        public async Task<Test> GetTestAsync(int id)
        {
            return await _testRepository.GetTestAsync(id);
        }

        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Customer>> GetCustomersAsync(string? name, string? code)
        {
            return await _customerRepository.GetCustomersAsync(name, code);
        }

    }
}
