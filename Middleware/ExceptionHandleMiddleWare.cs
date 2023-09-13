using Newtonsoft.Json;

using System.Net;

namespace GitHubUsersInfoDemoByJiahuaTong.Middleware
{

    /// <summary>
    /// Application Centre Excepiton Handle Middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var msgOpt = "";
            try
            {
                await _next(context);
            }
            catch (Exception err)
            {

                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";

                }
                else
                {
                    _logger.LogWarning("Can't write error response. Response has already started.");
                    return;
                }

                if (err is not OperationCanceledException && err.InnerException != null)
                {
                    while (err.InnerException != null)
                    {
                        err = err.InnerException;
                    }
                }
                if (err is not OperationCanceledException)
                    _logger.LogError(err, err.Message);

                switch (err)
                {
                    case ApplicationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case OperationCanceledException e:
                        response.StatusCode = (int)HttpStatusCode.TemporaryRedirect;
                        //_logger.LogWarning(err, "Process has been cancelled.");
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        msgOpt = @"We came across a technology problem while processing your request,
                                           Please contact Jiahua Tong(0426127207) 
                                           to discuss his opportunity of working for your company. Thanks :)";

                        break;
                }
                await response.WriteAsync(JsonConvert.SerializeObject(
                    new
                    {
                        exceptionMsg = response.StatusCode == 500
                    ? msgOpt
                    : err.Message + Environment.NewLine + @"Kindly Notice:Please contact Jiahua Tong(0426127207) to discuss his opportunity of working for your company. Thanks :)"
                    }
                )).ConfigureAwait(false);
                await response.Body.FlushAsync().ConfigureAwait(false);
            }
        }
    }
}
