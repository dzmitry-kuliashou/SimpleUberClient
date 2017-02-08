using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using System.Threading.Tasks;
using MVCTypeScriptReact.Model.AuthorComments;
using WebApiCaller.Services.AuthorComments;
using WebApiCaller.Services.Authorization;
using System.Linq;

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

        private async Task GetAuthorComments()
        {
            var serviceResult = await _authorCommentService.GetAuthorComments();
            if (serviceResult.IsSucceed)
            {
                _comments = serviceResult.Result;
            }
            else
            {
                _comments = new List<CommentModel>();
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
                if (serviceResponse.ErrorCodes.First().Code == 401)
                {
                    _comments.Clear();
                    return Content("unauthorized");
                }

                return Content("fail");
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

            return Content("fail");
        }
    }
}