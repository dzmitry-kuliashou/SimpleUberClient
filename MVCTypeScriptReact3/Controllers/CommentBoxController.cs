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

        private IAuthorCommentsService _authorCommentService;

        private IAuthorizationService _authorizationService;

        public CommentBoxController(
            IAuthorCommentsService authorCommentService,
            IAuthorizationService authorizationService)
        {
            _authorCommentService = authorCommentService;
            _authorizationService = authorizationService;
        }

        private ActionResult GetAuthorComments()
        {
            var serviceResult = _authorCommentService.GetAuthorComments();
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
        public ActionResult Index()
        {
            GetAuthorComments();
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult Comments()
        {
            return Json(_comments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddComment(CommentModel comment)
        {
            var serviceResponse = _authorCommentService.AddNewAuthorComment(comment);

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
        public ActionResult Login()
        {
            var serviceResponse = _authorizationService.Authorize();
            if (serviceResponse.IsSucceed)
            {
                GetAuthorComments();
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