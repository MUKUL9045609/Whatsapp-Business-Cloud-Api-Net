using Microsoft.AspNetCore.Mvc;
using System.Threading;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Interfaces;
using WhatsappBusiness.CloudApi.Response;
using WhatsappBusiness.CloudApi.Templates;

namespace WhatsAppBusinessCloudAPI.Web.Controllers
{
    public class TemplateManagementController : Controller
    {
        private readonly IWhatsAppBusinessClient _whatsAppBusinessClient;
        IConfiguration _configuration;
        public TemplateManagementController(IWhatsAppBusinessClient whatsAppBusinessClient, IConfiguration configuration )
        {
            _whatsAppBusinessClient = whatsAppBusinessClient;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllTemplate()   
        {
            string whatsAppBusinessAccountId = "101600782940170";

            var result = await _whatsAppBusinessClient.GetAllTemplatesAsync(whatsAppBusinessAccountId, null, null);

            var response = new TemplateResponse
            {
                Data = result.Data,
            };
            return View(response);
        }


        public async Task<IActionResult> CreateTemplate()
        { 
            var viewmodel = new WhatsAppTemplateResponse
            {
              
            };
            return View(viewmodel);
        
        
        }

        public async Task<IActionResult> CreateTemplate(WhatsAppTemplateRequest request)
        {
            var viewmodel = new WhatsAppTemplateResponse
            {

            };
            return View(viewmodel);


        }

    }
}
