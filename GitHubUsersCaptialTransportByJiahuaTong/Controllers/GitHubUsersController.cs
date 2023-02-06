using GitHubUsersCaptialTransportByJiahuaTong.DTOs;
using GitHubUsersCaptialTransportByJiahuaTong.Service.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System.Data;
using System.Net;

namespace GitHubUsersCaptialTransportByJiahuaTong.Controllers
{
    [ApiController]
    [Route("api/githubusers")]
    public class GitHubUsersController : ControllerBase
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IConfiguration _config;
        private readonly ILogger<GitHubUsersController> _logger;
        private readonly IGHPublicApi _githubPublicApiService;
        public GitHubUsersController(
            //ILogger<GitHubUsersController> logger,
            IGHPublicApi githubPublicApi
            //IHttpClientFactory httpClientFactory,
            //IConfiguration config
        )
        {
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_config = config ?? throw new ArgumentNullException(nameof(config));
            //_httpClientFactory = httpClientFactory;
            _githubPublicApiService = githubPublicApi ?? throw new ArgumentNullException(nameof(githubPublicApi));

        }

        public GitHubUsersController(
            ILogger<GitHubUsersController> logger,
            IGHPublicApi githubPublicApi
        //IHttpClientFactory httpClientFactory,
        //IConfiguration config
        ):this(githubPublicApi)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_config = config ?? throw new ArgumentNullException(nameof(config));
            //_httpClientFactory = httpClientFactory;
            //_githubPublicApiService = githubPublicApi ?? throw new ArgumentNullException(nameof(githubPublicApi));

        }

        // GET: GitHubUsersController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GithubUserInfo>>> RetrieveUsers([FromQuery] List<string> UserNameList)//, [FromServices]Logger<GitHubUsersController> _logger)
        {
            if(UserNameList==null||UserNameList.Count==0)
            {
               _logger.LogError("Invalid User names in Request");
                return BadRequest("Invalid User Names requested, at:"+DateTime.Now.ToShortDateString());
            }
            var result = await _githubPublicApiService.GetUserInfoByUserNames(UserNameList);
            if(result.Count()>0)
                return Ok (result);
            else
                return NotFound("No matched user info found!");

        }

        
    }
}
