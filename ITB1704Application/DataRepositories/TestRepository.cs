using ITB1704Application.Model;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITB1704Application.DataRepositories
{
    public class TestRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TestRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Test>> GetTestsAsync(string? name)
        {
            using var context = _contextFactory.CreateDbContext(); 
            var query = context.Tests.AsQueryable();

            if (name != null)
                query = query.Where(x => x.Name.ToUpper().Contains(name.ToUpper()));

            return await query.ToListAsync();
        }

        public async Task<Test> GetTestAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Tests.FindAsync(id);
        }

        public async Task<Test> AddTestAsync(Test test)
        {
            Test result = null;
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Tests.Add(test);

                int i = await context.SaveChangesAsync();
                if (i >= 1)
                {
                    result = await GetTestAsync(test.Id);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public async Task<Test> UpdateTestAsync(Test test)
        {
            Test result = null;
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Entry(test).State = EntityState.Modified;

                int i = await context.SaveChangesAsync();
                if (i >= 1)
                {
                    result = await GetTestAsync(test.Id);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public async Task<bool> DeleteTestAsync(Test test)
        {
            bool result = false;
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Tests.Remove(test);

                int i = await context.SaveChangesAsync();
                if (i >= 1)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

    }
}
