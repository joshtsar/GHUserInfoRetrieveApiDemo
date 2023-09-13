namespace GitHubUsersInfoDemoByJiahuaTong.DTOs
{
    public class GithubUserInfo
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string? Company { get; set; }
        public int? Followers { get; set; }
        public int? Public_repos { get; set; }
        public int? AvgNumFollowersPerPublicRepo { get; set; }
    }
}
