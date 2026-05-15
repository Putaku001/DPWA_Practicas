using Microsoft.AspNetCore.Mvc;
using practica11.Models;
using practica11.Repositories;

namespace practica11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;

        public RoleController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public IActionResult Get()
        {
            var roles = _roleRepository.GetAllRoles();

            return Ok(roles);
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // POST api/<RoleController>
        [HttpPost]
        public IActionResult Post(RoleModel roleModel)
        {
            _roleRepository.AddRole(roleModel);

            return CreatedAtAction(nameof(Get), new { id = roleModel.Id }, roleModel);
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, RoleModel roleModel)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = roleModel.Name;
            role.Description = roleModel.Description;
            _roleRepository.UpdateRole(role);

            return Accepted();
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }

            _roleRepository.DeleteRole(id);

            return NoContent();
        }
    }
}
