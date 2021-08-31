using ITB1704Application.DataRepositories;
using ITB1704Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITB1704Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly TestRepository _repository;

        public TestsController(TestRepository repository)
        {
            _repository = repository
               ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> GetTests(string name = null)
        {
            return await _repository.GetTestsAsync(name);
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(int id)
        {
            var course = await _repository.GetTestAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTest(int id, Test test)
        {
            if (id != test.Id)
            {
                return BadRequest();
            }

            test = await _repository.UpdateTestAsync(test);

            return Ok(test);

        }

        [HttpPost]
        public async Task<ActionResult<Test>> PostTest(Test test)
        {
            Test result = await _repository.AddTestAsync(test);

            if (result == null)
                return Conflict();


            return CreatedAtAction("GetTest", new { id = test.Id }, test);
        }

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            Test course = await _repository.GetTestAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            await _repository.DeleteTestAsync(course);

            return Ok();
        }
    }
}
