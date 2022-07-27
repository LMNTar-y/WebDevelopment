using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Providers;
using WebDevelopment.Email.Providers.Interfaces;

namespace WebDevelopment.Email.Settings
{
    public class EmailProviderSetupFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailProviderSetupFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEmailProvider Create(EmailProviderName emailProviderName)
        {
            switch (emailProviderName)
            {
                case EmailProviderName.Yandex:
                    return new YandexEmailProvider(_serviceProvider, emailProviderName);
                default:
                    return new BaseEmailProvider(_serviceProvider, emailProviderName);
            }
        }
    }
}
