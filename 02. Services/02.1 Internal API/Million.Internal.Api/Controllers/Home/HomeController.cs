namespace Million.Internal.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectToAction(RutesPathInternalApiDto.Index.index, RutesPathInternalApiDto.Index.swagger);
        }

        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ResponseServices<ErrorViewModel> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var errorView = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            var response = new ResponseServices<ErrorViewModel>();
            response.Info = errorView;
            response.Type = context.Error.Source;
            response.Message = context.Error.Message;
            return response;
        }
    }
}