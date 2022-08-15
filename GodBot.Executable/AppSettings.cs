namespace GodBot.Executable
{
    internal abstract class AppSettings
    {
        public static string DiscordToken => SecretSettings.DiscordToken;

        public static string CommandPrefix => "!";
    }
}