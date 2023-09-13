using GitHubUsersInfoDemoByJiahuaTong.Controllers;
using GitHubUsersInfoDemoByJiahuaTong.DTOs;
using GitHubUsersInfoDemoByJiahuaTong.Service.Interfaces;

using Newtonsoft.Json;

using System.Net;

namespace GitHubUsersInfoDemoByJiahuaTong.Service
{
    public class GHPublicAPIService : IGHPublicApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<GitHubUsersController> _logger;
        public GHPublicAPIService
        (
            ILogger<GitHubUsersController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration config
        ) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<GithubUserInfo>?> GetUserInfoByUserNamesAsync(List<string> UserNameList)
        {
            if (UserNameList == null || UserNameList.Count == 0)
            {
                _logger.LogError("Invalid User names in Request");
                return null;
            }
            var GHUserInfoList = new List<GithubUserInfo>();
            foreach (var nameGrp in UserNameList
                    .GroupBy(name => name,
                    (key, grp) => new { uniname = key, samenms = grp }
            ))
            {
                var searchName = nameGrp.samenms.First();

                string[] uriStrings = { _config["GitHubAPIUsersEndpoint:BaseUri"],
                            "users",
                            $"{searchName}"};
                var requestUri = string.Join("/", uriStrings);

                var httpClient = _httpClientFactory.CreateClient();

                httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _config["GitHubToken"]);

                var httpRespMessage = await httpClient.GetAsync(requestUri);
                //var resp = httpRespMessage.EnsureSuccessStatusCode();
                if (httpRespMessage.IsSuccessStatusCode == false && httpRespMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation($"UserName:{searchName} could not be found by Github API. No user info retrieved.");
                    continue;
                }

                using var contentStream = await httpRespMessage.Content.ReadAsStreamAsync();
                var usrInfo = await GetUserInfoAsync(contentStream);
                if (usrInfo != null)
                    GHUserInfoList.Add(usrInfo);
            };
            if (GHUserInfoList.Count == 0)
                throw new KeyNotFoundException("No matched user info found!");
            return (from usr in GHUserInfoList
                    orderby usr.Name
                    select usr).ToList();
            
        }

        private async Task<GithubUserInfo?> GetUserInfoAsync(Stream contentStream)
        {
            var usrStr = string.Empty;

            using var reader = new StreamReader(contentStream);
            usrStr = await reader.ReadToEndAsync();

            var usrInfo = JsonConvert.DeserializeObject<GithubUserInfo>(usrStr);
            if (usrInfo?.Followers != null &&
                usrInfo?.Public_repos != null &&
                usrInfo.Public_repos > 0)
                usrInfo.AvgNumFollowersPerPublicRepo = usrInfo.Followers / usrInfo.Public_repos;
            return usrInfo;
        }
    }
}
