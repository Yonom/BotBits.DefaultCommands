using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BotBits.Commands;

namespace BotBits.DefaultCommands
{
    public sealed class DefaultCommandsExtension : Extension<DefaultCommandsExtension>
    {
        public static bool LoadInto(BotBitsClient client)
        {
            CommandsExtension.LoadInto(client);
            return LoadInto(client, null);
        }
    }
}
