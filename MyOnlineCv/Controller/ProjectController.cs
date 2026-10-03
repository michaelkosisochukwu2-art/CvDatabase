using Microsoft.AspNetCore.Mvc;
using MyOnlineCv.Models;
using MyOnlineCv.Data;

namespace MyOnlineCv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/projects -> User submits a project entry
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] ProjectItem project)
        {
            if (string.IsNullOrWhiteSpace(project.Title))
            {
                return BadRequest(new { error = "Project Title is required." });
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(new { status = "Success", message = "Project added successfully!", data = project });
        }

        // GET api/projects -> View all submitted projects
        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = _context.Projects.ToList();
            return Ok(projects);
        }
    }
}