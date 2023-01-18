using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GitHubUsersCaptialTransportByJiahuaTong.Controllers
{
    [ApiController]
    public class ErrorHandlingController : ControllerBase
    {
        [Route("error/{code}")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error(int code)
        {
            var errInfo = string.Empty;
            switch (code)
            {
                case 401:
                    errInfo = "Unauthorized Request";
                    break;
                case 403:
                    errInfo = "Request Forbidden";
                    break;
                case 404:
                        errInfo="Resource not found";
                        break;
                case 500:
                        errInfo= "A server side error occurred, Please contact the site administrator.";
                        break;
                default:
                    break;
            }
            return new ObjectResult(errInfo);
        }
    }
}
