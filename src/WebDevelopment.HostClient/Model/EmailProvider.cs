using AutoMapper;

namespace WebDevelopment.HostClient.Model;

public enum EmailProvider
{
    Yandex,
    Google,
    Hotmail
}

//TODO make mapper from appsettings to enum
public static class Extensions
{
    public static IMappingExpression<TSource, TDestination> MapTo<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> expression)
    {
        return expression;
    }
}