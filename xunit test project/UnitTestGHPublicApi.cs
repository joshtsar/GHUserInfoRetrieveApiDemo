using GitHubUsersInfoDemoByJiahuaTong.Controllers;
using GitHubUsersInfoDemoByJiahuaTong.DTOs;
using GitHubUsersInfoDemoByJiahuaTong.Service.Interfaces;

using Moq;

namespace GHPublicAPITest
{
    public class UnitTestGHPublicApi
    {
        private readonly Mock<IGHPublicApi> _ghApiService;

        public UnitTestGHPublicApi()
        {
            _ghApiService = new Mock<IGHPublicApi>();
        }
        [Fact]
        public async Task TestGetUserInfoByUsernames()
        {
            //arrange
            var userNameList = GetUserNames();
            _ghApiService
            .Setup(x => x.GetUserInfoByUserNames(new List<string> { "Jason", "Mike" }))
            .ReturnsAsync(userNameList);
            var userController = new GitHubUsersController(_ghApiService.Object);

            //act
            var backResult =await userController.RetrieveUsers(new List<string> { "Jason", "Mike" });

            //assert
            Assert.NotNull(backResult);
            //Assert.Equal(GetUserNames().Count(), backResult?.Value?.Count());
            //Assert.Equal(GetUserNames().ToString(), backResult?.ToString());
            //Assert.True(userNameList.Equals(backResult));
        }

        private IEnumerable<GithubUserInfo> GetUserNames()
        {
            return new List<GithubUserInfo>
            {
                new GithubUserInfo
                {
                    Name= "Jason",
                    Company="company" ,
                    Followers=9,
                    Public_repos=8,
                    Login="loginJason",
                    AvgNumFollowersPerPublicRepo=5
                },
                new GithubUserInfo
                {
                    Name="Mike" ,
                    Company="ABC",
                    Followers=7,
                    Login="LoginMike",
                    Public_repos=5,
                    AvgNumFollowersPerPublicRepo=1
                }
            };
        }
    }
}