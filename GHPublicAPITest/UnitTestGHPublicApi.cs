using GitHubUsersCaptialTransportByJiahuaTong.Controllers;
using GitHubUsersCaptialTransportByJiahuaTong.DTOs;
using GitHubUsersCaptialTransportByJiahuaTong.Service.Interfaces;

using Moq;

namespace GHPublicAPITest
{
    public class UnitTestGHPublicApi
    {
        //GitHubUsersController _controller;
        //IGHPublicApi _service;
        private readonly Mock<IGHPublicApi> _ghApiService;

        //private readonly Mock<ILogger<GitHubUsersController>> _logger;

        public UnitTestGHPublicApi()
        {
            //_service = new GHPublicAPIService();
            _ghApiService = new Mock<IGHPublicApi>();
            //_logger = new Mock<ILogger<GitHubUsersController>>();
            //_controller = new GitHubUsersController(_service);

        }
        [Fact]
        public async Task TestGetUserInfoByUsernames()
        {
            //_logger = Mock.Of<ILogger<GitHubUsersController>>();
            //arrange
            var userNameList = GetUserNames();
            _ghApiService
            .Setup(x => x.GetUserInfoByUserNames(new List<string> { "Jason", "Mike" }))
            .ReturnsAsync(userNameList);
              //  .Returns(userNameList);
            var userController = new GitHubUsersController(_ghApiService.Object);

            //act
            var backResult =await userController.RetrieveUsers(new List<string> { "Jason", "Mike" });

            //assert
            Assert.NotNull(backResult);
            Assert.Equal(GetUserNames().Count(), backResult.Value?.Count());
            Assert.Equal(GetUserNames().ToString(), backResult.ToString());
            Assert.True(userNameList.Equals(backResult));
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