using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using WebDevelopment.Email.Model;

namespace WebDevelopment.Email.Providers;

public class YandexEmailProvider : BaseEmailProvider
{

    public YandexEmailProvider(IServiceProvider serviceProvider, EmailProviderName emailProviderName) : base(serviceProvider, emailProviderName)
    {
    }

}
