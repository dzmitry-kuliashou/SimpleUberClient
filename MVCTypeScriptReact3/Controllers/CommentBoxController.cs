using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using System.Threading.Tasks;
using MVCTypeScriptReact.Model.AuthorComments;
using WebApiCaller.Services.AuthorComments;
using WebApiCaller.Services.Authorization;
using System.Linq;
using WebApiCaller.Common;
using System.Text;

namespace MVCTypeScriptReact3.Controllers
{
    public class CommentBoxController : Controller
    {
        private static List<CommentModel> _comments;

        private AuthorCommentsService _authorCommentService;

        private AuthorizationService _authorizationService;

        public CommentBoxController()
        {
            _authorCommentService = new AuthorCommentsService();
            _authorizationService = new AuthorizationService();
        }

        private async Task<ActionResult> GetAuthorComments()
        {
            var serviceResult = await _authorCommentService.GetAuthorComments();
            if (serviceResult.IsSucceed)
            {
                _comments = serviceResult.Result;
                return Content("ok");
            }
            else
            {
                _comments = new List<CommentModel>();
                return HandleUnsucceedServiceResponse(serviceResult.ErrorCodes);
            }
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            await GetAuthorComments();
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult Comments()
        {
            return Json(_comments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(CommentModel comment)
        {
            var serviceResponse = await _authorCommentService.PostNewAuthorComment(comment);

            if(serviceResponse.IsSucceed)
            {
                _comments.Add(comment);
                return Content("ok");
            }
            else 
            {
                return HandleUnsucceedServiceResponse(serviceResponse.ErrorCodes);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Login()
        {
            var serviceResponse = await _authorizationService.Authorize();
            if (serviceResponse.IsSucceed)
            {
                await GetAuthorComments();
                return Content("ok");
            }
            else
            {
                return HandleUnsucceedServiceResponse(serviceResponse.ErrorCodes);
            }
        }

        private ActionResult HandleUnsucceedServiceResponse(List<ErrorCode> errorCodes)
        {
            if(errorCodes.First().Code == 401)
            {
                _comments.Clear();
                return Content("unauthorized");
            }
            else
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach(var errorCode in errorCodes)
                {
                    errorMessage.AppendFormat("{0}|",errorCode.Message);
                }

                return Content(errorMessage.ToString().TrimEnd('|'));
            }
        }
    }
}