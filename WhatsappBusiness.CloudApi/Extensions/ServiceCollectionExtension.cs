﻿using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Interfaces;
using WhatsappBusiness.CloudApi.WhasAppBusinessClientServices;
using WhatsappBusiness.CloudApi.WhasAppBusinessClientServicesInterface;

namespace WhatsappBusiness.CloudApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Creating a whatsapp business cloud api service collection to be used in projects that support dependency injection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="whatsAppBusinessPhoneNumberId"></param>
        public static void AddWhatsAppBusinessCloudApiService(this IServiceCollection services, WhatsAppBusinessCloudApiConfig whatsAppConfig, string graphAPIVersion = null)
        {
            Random jitterer = new Random();

            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(1, retryAttempt =>
                {
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 100));
                });

            var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

            services.AddTransient<IWhatsAppBusinessClientFactory, WhatsAppBusinessClientFactory>();
           
            services.AddSingleton(new WhatsAppBusinessCloudApiConfig
            {
                WhatsAppBusinessPhoneNumberId = whatsAppConfig.WhatsAppBusinessPhoneNumberId,
                WhatsAppBusinessAccountId = whatsAppConfig.WhatsAppBusinessAccountId,
                WhatsAppBusinessId = whatsAppConfig.WhatsAppBusinessId,
                AccessToken = whatsAppConfig.AccessToken,
                AppName = whatsAppConfig.AppName,
                Version = whatsAppConfig.Version
            });
            // services.AddScoped<IWhatsAppTemplateManagementInterface, WhatsAppTemplateManagementInterface>();
            services.AddHttpClient<IWhatsAppBusinessClient, WhatsAppBusinessClient>(options =>
            {
                options.BaseAddress = (string.IsNullOrWhiteSpace(graphAPIVersion)) ? WhatsAppBusinessRequestEndpoint.BaseAddress : new Uri(WhatsAppBusinessRequestEndpoint.GraphApiVersionBaseAddress.ToString().Replace("{{api-version}}", graphAPIVersion));
                options.Timeout = TimeSpan.FromMinutes(10);
            }).ConfigurePrimaryHttpMessageHandler(messageHandler =>
            {
                var handler = new HttpClientHandler();

                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                }

                return handler;
            }).AddPolicyHandler(request => request.Method.Equals(HttpMethod.Get) ? retryPolicy : noOpPolicy);

        }

        public static void AddWhatsAppBusinessCloudApiService<THandler>(this IServiceCollection services, WhatsAppBusinessCloudApiConfig whatsAppConfig, string graphAPIVersion = null) where THandler : HttpMessageHandler
        {
            Random jitterer = new Random();

            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(1, retryAttempt =>
                {
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 100));
                });

            var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

			services.AddTransient<IWhatsAppBusinessClientFactory, WhatsAppBusinessClientFactory>();

			services.AddSingleton(new WhatsAppBusinessCloudApiConfig
            {
                WhatsAppBusinessPhoneNumberId = whatsAppConfig.WhatsAppBusinessPhoneNumberId,
                WhatsAppBusinessAccountId = whatsAppConfig.WhatsAppBusinessAccountId,
                WhatsAppBusinessId = whatsAppConfig.WhatsAppBusinessId,
                AccessToken = whatsAppConfig.AccessToken,
                AppName = whatsAppConfig.AppName,
                Version = whatsAppConfig.Version
            });

            services.AddHttpClient<IWhatsAppBusinessClient, WhatsAppBusinessClient>(options =>
            {
                options.BaseAddress = (string.IsNullOrWhiteSpace(graphAPIVersion)) ? WhatsAppBusinessRequestEndpoint.BaseAddress : new Uri(WhatsAppBusinessRequestEndpoint.GraphApiVersionBaseAddress.ToString().Replace("{{api-version}}", graphAPIVersion));
                options.Timeout = TimeSpan.FromMinutes(10);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan)
              .ConfigurePrimaryHttpMessageHandler<THandler>()
              .AddPolicyHandler(request => request.Method.Equals(HttpMethod.Get) ? retryPolicy : noOpPolicy);


           
        }
    }
}
