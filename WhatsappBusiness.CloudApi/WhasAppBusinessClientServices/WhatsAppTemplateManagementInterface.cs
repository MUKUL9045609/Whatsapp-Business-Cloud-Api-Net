using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Response;
using WhatsappBusiness.CloudApi.Templates;
using WhatsappBusiness.CloudApi.WhasAppBusinessClientServicesInterface;

namespace WhatsappBusiness.CloudApi.WhasAppBusinessClientServices
{
    public class WhatsAppTemplateManagementInterface: IWhatsAppTemplateManagementInterface
    {
        private readonly IWhatsAppTemplateManagementInterface _whatsAppTemplateManagement;
        private  WhatsAppBusinessCloudApiConfig _whatsAppConfig;
        public WhatsAppTemplateManagementInterface(IWhatsAppTemplateManagementInterface whatsAppTemplateManagement, WhatsAppBusinessCloudApiConfig whatsAppConfig)
        {
            _whatsAppTemplateManagement = whatsAppTemplateManagement;
            _whatsAppConfig = whatsAppConfig;
        }

       /* public async Task<WhatsAppTemplate> GetTemplateListAsync(WhatsAppBusinessCloudApiConfig? cloudApiConfig = null, CancellationToken cancellationToken = default)
        {
            if (cloudApiConfig is not null)
            {
                _whatsAppConfig = cloudApiConfig;
            }

            var builder = new StringBuilder();

            builder.Append(WhatsAppBusinessRequestEndpoint.GetAllTemplateMessage);

            //builder.Replace("{{Phone-Number-ID}}", _whatsAppConfig.WhatsAppBusinessPhoneNumberId);
            
           // var formattedWhatsAppEndpoint = builder.ToString();
            return await WhatsAppBusinessGetAsync<QRCodeMessageFilterResponse>(formattedWhatsAppEndpoint, cancellationToken);
        }*/

    }
}
