# GHUserInfoRetrieveApiDemo
A demo .net core web api to retrieve github user info via github public api

.NET Core 6 API Demo Project Description:
This solution includes a .NET core 6 Webapi project that has an API endpoint called retrieveUsers and an xunit test project.
The Web api features and requirements:
This endpoint takes a List<string>  usernames: This is a list of github usernames that will be used to look up basic information from GitHub's public API. Only users in this list to be retrieved from Github. 
→ The endpoint take these usernames and hit GitHub's public API to get basic user information

→ This API call returns to the user a list of basic information for those Github users including: 
{
Name:string ;
 login name :string; 
company :string; 
number of followers :int; 
number of public repositories :int;
The average number of followers per public repository :int
(ie. number of followers divided by the number of public repositories)
} 
→ The returned users are sorted alphabetically by name 
→ If duplicate usernames are provided, the api code only hit github api endpoint one time and the matching user only to be returned once 
→ If some usernames cannot be found, this should not fail the other usernames that were requested → Use regular http calls to hit GitHub's API, avoid using any pre made GitHub net core libraries to integrate with GitHub's API 
→Providing sound error handling and logging within the solution
* The API endpoint (with documentation) needed to get Github user information is https://docs.github.com/en/rest/reference/users#get-a-user 

