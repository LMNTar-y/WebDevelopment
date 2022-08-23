using WebDevelopment.Email.Model;

namespace WebDevelopment.Email.Providers;

public class YandexEmailProvider : BaseEmailProvider
{

    public YandexEmailProvider(IServiceProvider serviceProvider, EmailProviderName emailProviderName) : base(serviceProvider, emailProviderName)
    {
    }

}
