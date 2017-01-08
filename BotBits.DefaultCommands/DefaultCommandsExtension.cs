using System;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    public sealed class DefaultCommandsExtension : Extension<DefaultCommandsExtension>
    {
        [Obsolete("Invalid to use \"new\" on this class. Use the static .Of(botBits) method instead.", true)]
        public DefaultCommandsExtension()
        {
        }

        public static bool LoadInto(BotBitsClient client)
        {
            if (!CommandsExtension.IsLoadedInto(client))
                throw new InvalidOperationException("BotBits.CommandsExtension must be loaded for DefaultCommands to work.");
            if (!PermissionsExtension.IsLoadedInto(client))
                throw new InvalidOperationException("BotBits.PermissionsExtension must be loaded for DefaultCommands to work.");
            return LoadInto(client, null);
        }
    }
}