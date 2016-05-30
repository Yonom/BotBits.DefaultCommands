namespace BotBits.DefaultCommands
{
    public sealed class DefaultCommandsExtension : Extension<DefaultCommandsExtension>
    {
        public static bool LoadInto(BotBitsClient client)
        {
            return LoadInto(client, null);
        }
    }
}
