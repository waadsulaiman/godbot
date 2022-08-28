namespace GodBot.Infrastructure
{
    internal abstract class AppSettings
    {
        public static string HumorApiKey => SecretSettings.HumorApiKey;
    }
}