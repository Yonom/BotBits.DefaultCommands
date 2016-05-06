using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BotBits.Commands;
using BotBits.Permissions;

namespace BotBits.DefaultCommands
{
    public sealed class DefaultCommandsExtension : Extension<DefaultCommandsExtension>
    {
        public static bool LoadInto(BotBitsClient client)
        {
            if (!CommandsExtension.IsLoadedInto(client)) throw new InvalidOperationException("CommandsExtension must be loaded for DefaultCommands to work.");
            if (!PermissionsExtension.IsLoadedInto(client)) throw new InvalidOperationException("PermissionsExtension must be loaded for DefaultCommands to work.");
            return LoadInto(client, null);
        }
    }
}
