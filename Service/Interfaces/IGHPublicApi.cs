using GitHubUsersInfoDemoByJiahuaTong.DTOs;

namespace GitHubUsersInfoDemoByJiahuaTong.Service.Interfaces
{
    public interface IGHPublicApi
    {
        Task<IEnumerable<GithubUserInfo>?> GetUserInfoByUserNamesAsync(List<string> userNames);
    }
}
