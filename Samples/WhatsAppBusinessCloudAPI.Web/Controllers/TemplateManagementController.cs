using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
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
        private WhatsAppBusinessCloudApiConfig _whatsAppConfig;
        public TemplateManagementController(IWhatsAppBusinessClient whatsAppBusinessClient, WhatsAppBusinessCloudApiConfig whatsAppConfig)
        {
            _whatsAppBusinessClient = whatsAppBusinessClient;
            _whatsAppConfig = whatsAppConfig;
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

        [HttpGet]
        public async Task<IActionResult> CreateTemplate()
        {
            var viewmodel = new WhatsAppTemplateResponse
            {
                Data = new TemplateData(),
                Components = new List<WhatsAppBusinessHSMWhatsAppHSMComponentGet>
        {
            new WhatsAppBusinessHSMWhatsAppHSMComponentGet { Type = "HEADER" },
            new WhatsAppBusinessHSMWhatsAppHSMComponentGet { Type = "BODY" }
            /*new WhatsAppBusinessHSMWhatsAppHSMComponentGet { Type = "FOOTER" }*/
           /* new WhatsAppBusinessHSMWhatsAppHSMComponentGet
            {
                Type = "BUTTONS",
                Buttons = new List<TemplateButton>
                {
                    new TemplateButton()
                }
            }*/
        }
            };

            return View(viewmodel);
        }



        
        [HttpPost]
        public async Task<IActionResult> CreateTemplates([FromBody] WhatsAppTemplateRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name) || request.Components == null || !request.Components.Any())
            {
                return BadRequest("Invalid template data.");
            }

            try
            {
                var wabaId = _whatsAppConfig.WhatsAppBusinessAccountId;

                var convertedComponents = request.Components.Select(c => new WhatsAppBusinessHSMWhatsAppHSMComponentGet
                {
                    Type = c.Type,
                    Text = c.Text,
                    Format = c.Type == "HEADER" ? c.Format : null,
                    Example = c.Example
                }).ToList();

                var template = new TemplateData
                {
                    Name = request.Name,
                    Language = request.Language,
                    Category = request.Category,
                    Components = convertedComponents
                };

                 var apiResponse = await _whatsAppBusinessClient.CreateTemplateMessageAsync(wabaId, template);

                if (apiResponse == null)
                    return StatusCode(500, "Template creation failed.");

                return Ok(new
                {
                    apiResponse.Id,
                    apiResponse.Status,
                    apiResponse.Category
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }



        //[Route("api/create-template")]

        public async Task<IActionResult> CreateCatalogTemplate()
        {
            var viewmodel = new TemplateData
            {
                Name = "Catalog Template",
                Language = "en_US",
                Category = "CATALOG_UPDATE",
                Components = new List<WhatsAppBusinessHSMWhatsAppHSMComponentGet>
                {
                    new WhatsAppBusinessHSMWhatsAppHSMComponentGet
                    {
                        Type = "text",
                        Text = "Check out our new catalog!",
                        Format = "plain_text"
                    },
                    new WhatsAppBusinessHSMWhatsAppHSMComponentGet
                    {
                        Type = "catalog",
                        Text = "Your catalog is ready to view.",
                        Format = "plain_text"
                    }
                }
            };
            return View(viewmodel);


        }




        [HttpPost] 
        public async Task<IActionResult> DeleteTemplate(string hsm_id, string templateName)
        {
           
            try
            {

                var wabaId = _whatsAppConfig.WhatsAppBusinessAccountId;
                var isDeletedResponce = _whatsAppBusinessClient.DeleteTemplatebyId(wabaId, hsm_id, templateName);

                if (isDeletedResponce == null)
                {
                    return NotFound("Template not found.");
                }

                return Ok(new {
                    isDeletedResponce.Success
                });
            }
            catch (Exception ex)
            {
                // Log error (ex)
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}
