using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDBPlayground.Models;
using MongoDBPlayground.Services;

namespace MongoDBPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {

        private readonly IUserDetailService _userDetailService;

        public UserDetailController(IUserDetailService userDetailService)
        {
            _userDetailService = userDetailService;
        }
        [HttpGet]
        public async Task<List<UserDetail>> Get() =>
            await _userDetailService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<UserDetail>> Get(string id)
        {
            var userDetail = await _userDetailService.GetAsync(id);

            if (userDetail is null)
            {
                return NotFound();
            }

            return userDetail;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserDetail newUserDetail)
        {
            await _userDetailService.CreateAsync(newUserDetail);

            return CreatedAtAction(nameof(Get), new { id = newUserDetail.Id }, newUserDetail);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, UserDetail updatedUserDetail)
        {
            var userDetail = await _userDetailService.GetAsync(id);

            if (userDetail is null)
            {
                return NotFound();
            }

            updatedUserDetail.Id = userDetail.Id;

            await _userDetailService.UpdateAsync(id, updatedUserDetail);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userDetail = await _userDetailService.GetAsync(id);

            if (userDetail is null)
            {
                return NotFound();
            }

            await _userDetailService.RemoveAsync(id);

            return NoContent();
        }
    }
}
