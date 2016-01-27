using System.Threading.Tasks;
using System.Web.Mvc;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.MVC.Helpers;

namespace Xero.Api.Example.MVC.Controllers
{
    public class OrganisationController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var api = XeroApiHelper.CoreApi();

            try
            {
                var organisation = await api.GetDefaultOrganisationAsync();

                return View(organisation);
            }
            catch (RenewTokenException e)
            {
                return RedirectToAction("Connect", "Home");
            }   
        }
    }
}
