using GitHubUsersCaptialTransportByJiahuaTong.DTOs;

namespace GitHubUsersCaptialTransportByJiahuaTong.Service.Interfaces
{
    public interface IGHPublicApi
    {
        Task<IEnumerable<GithubUserInfo>> GetUserInfoByUserNames(List<string> userNames);
    }
}
