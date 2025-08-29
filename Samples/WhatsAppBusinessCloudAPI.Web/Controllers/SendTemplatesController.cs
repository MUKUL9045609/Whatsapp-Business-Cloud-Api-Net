using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WhatsappBusiness.CloudApi;
using WhatsappBusiness.CloudApi.AccountMetrics;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Exceptions;
using WhatsappBusiness.CloudApi.Interfaces;
using WhatsappBusiness.CloudApi.Media.Requests;
using WhatsappBusiness.CloudApi.Messages.Requests;
using WhatsappBusiness.CloudApi.Response;
using WhatsAppBusinessCloudAPI.Web.Extensions.Alerts;
using WhatsAppBusinessCloudAPI.Web.Models;
using WhatsAppBusinessCloudAPI.Web.ViewModel;
namespace WhatsAppBusinessCloudAPI.Web.Controllers
{
    public class SendTemplatesController:Controller
    {
        private readonly ILogger<SendTemplatesController> _logger;
        private readonly IWhatsAppBusinessClient _whatsAppBusinessClient;
        private readonly WhatsAppBusinessCloudApiConfig _whatsAppConfig;
        private readonly IWebHostEnvironment _environment;

        public SendTemplatesController(ILogger<SendTemplatesController> logger, IWhatsAppBusinessClient whatsAppBusinessClient,
            IOptions<WhatsAppBusinessCloudApiConfig> whatsAppConfig, IWebHostEnvironment environment)
        {
            _logger = logger;
            _whatsAppBusinessClient = whatsAppBusinessClient;
            _whatsAppConfig = whatsAppConfig.Value;
            _environment = environment;
        }
        public IActionResult SendWhatsAppTemplateMessage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendWhatsAppTemplateMessage(SendTemplateRequest sendTemplaterequest)
        {
            try
            {
                TextTemplateMessageRequest textTemplateMessage = new TextTemplateMessageRequest();
                textTemplateMessage.To = sendTemplaterequest.RecipientPhoneNumber;
                textTemplateMessage.Template = new TextMessageTemplate();
                textTemplateMessage.Template.Name = sendTemplaterequest.TemplateName;
                textTemplateMessage.Template.Language = new TextMessageLanguage();
                textTemplateMessage.Template.Language.Code = sendTemplaterequest.Langcode;

                var results = await _whatsAppBusinessClient.SendTextMessageTemplateAsync(textTemplateMessage);

                if (results != null)
                {
                    return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully sent template text message");
                }
                else
                {
                    return RedirectToAction(nameof(SendWhatsAppTemplateMessage));
                }
            }
            catch (WhatsappBusinessCloudAPIException ex)
            {
                _logger.LogError(ex, ex.Message);
                return RedirectToAction(nameof(SendWhatsAppTemplateMessage)).WithDanger("Error", ex.Message);
            }
        }

    }
}
