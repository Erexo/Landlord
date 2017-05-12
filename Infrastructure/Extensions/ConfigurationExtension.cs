using Microsoft.Extensions.Configuration;

namespace Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static T GetSettings<T>(this IConfiguration configuration) where T : new()
        {
            string name = typeof(T).Name.Replace("Settings", string.Empty);
            T settings = new T();
            configuration.GetSection(name).Bind(settings);
            return settings;
        }
    }
}
