using System.Web.Mvc;

namespace PlayR.Controllers
{
    public class ChatController : Controller
    {
        //
        // GET: /Chat/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
