using GitHubUsersInfoDemoByJiahuaTong.DTOs;
using GitHubUsersInfoDemoByJiahuaTong.Service.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics.Eventing.Reader;

namespace GitHubUsersInfoDemoByJiahuaTong.Controllers
{
    [ApiController]
    [Route("api/githubusersinfo")]
    public class GitHubUsersController : ControllerBase
    {
        private readonly IGHPublicApi _githubPublicApiService;
        public GitHubUsersController(IGHPublicApi githubPublicApi)
        {
            _githubPublicApiService = githubPublicApi ?? throw new ArgumentNullException(nameof(githubPublicApi));
        }

        // GET: GitHubUsersController
        [HttpGet("acquireUsersInfo")]
    
        public async Task<ActionResult<IEnumerable<GithubUserInfo>>> RetrieveUsers([FromQuery] List<string> UserNameList)
        {
            var result = await _githubPublicApiService.GetUserInfoByUserNames(UserNameList);
            if(result?.Count()>0)
                return Ok (result);
            else if (result == null)
                return BadRequest("Invalid User Names requested, at:" + DateTime.Now.ToShortDateString());
            else
                return NotFound("No matched user info found!");

        }
        
    }
}
