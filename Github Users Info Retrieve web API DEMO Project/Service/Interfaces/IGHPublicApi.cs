using GitHubUsersInfoDemoByJiahuaTong.DTOs;

namespace GitHubUsersInfoDemoByJiahuaTong.Service.Interfaces
{
    public interface IGHPublicApi
    {
        Task<IEnumerable<GithubUserInfo>?> GetUserInfoByUserNames(List<string> userNames);
    }
}
