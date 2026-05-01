using Microsoft.AspNetCore.Mvc;
using Shop.Entities;
using Shop.Services;

namespace Shop.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("all")]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(userService.GetAll());
        }
    }
}