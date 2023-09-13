using GitHubUsersInfoDemoByJiahuaTong.DTOs;
using GitHubUsersInfoDemoByJiahuaTong.Service.Interfaces;

using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<GithubUserInfo>>?> RetrieveUsers([FromQuery] List<string> UserNameList)
        {
            var result = await _githubPublicApiService.GetUserInfoByUserNamesAsync(UserNameList);
            
            if (Response.StatusCode == 200&& result?.Count() > 0)
                return Ok(result);
            else
            {
                //await Response.Body.FlushAsync();
                return null;
            }
        }
    }
}
