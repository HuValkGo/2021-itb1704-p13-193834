using ITB1704Application.DataRepositories;
using ITB1704Application.GraphQL.Inputs;
using ITB1704Application.Model;
using HotChocolate;
using HotChocolate.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITB1704Application.GraphQL
{
    public class Mutation
    {
        private readonly TestRepository _repository;

        public Mutation(TestRepository repository)
        {
            _repository = repository
               ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Test> UpdateTest(TestInput testInput)
        {
            Test test = new Test();
            if (testInput.Id > 0)
            {
                test.Id = (int)testInput.Id;
                test.Name = testInput.Name;
                test = await _repository.UpdateTestAsync(test);
            }
            else
            {
                throw new QueryException(
                    ErrorBuilder.New()
                            .SetMessage("Id is empty!")
                            .SetCode("NO_KEY")
                            .Build());
            }
            if (test == null)
                throw new KeyNotFoundException("No item exists with specified key");

            return test;
        }

        public async Task<Test> AddTest(TestInput testInput)
        {
            Test test = new Test();
            test.Name = testInput.Name;
            test = await _repository.AddTestAsync(test);

            if (test == null)
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Conflict!")
                        .SetCode("CONFLICT")
                        .Build());

            return test;
        }

        public async Task<bool> DeleteTest(int id)
        {
            Test course = await _repository.GetTestAsync(id);
            if (course == null)
            {
                throw new KeyNotFoundException("No item exists with specified key");
            }

            return await _repository.DeleteTestAsync(course); ;
        }


    }
}
