using CommandSystem;
using System;
using System.Linq;
using Exiled.Permissions.Extensions;
using Exiled.API.Features;

namespace CustomInjuries.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class InjuriesIgnore : ICommand
    {
        public string Command => "cignore";
        public string Description => "Adding immunity to plugin activity to current player. Usage: cignore <player id>";

        public string[] Aliases => Array.Empty<string>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("ci.ignore"))
            {
                response = "You don't have enough permissions to use this command.";
                return false;
            }

            if (!arguments.Any())
            {
                response = "Missing arguments (1). Usage: cignore <player id>";
                return false;
            }

            if (!int.TryParse(arguments.First<string>(), out int playerId)) 
            {
                response = "Arguments parsing failure (1). Usage: cignore <player id>";
                return false;
            }

            Player targetPlayer = Player.Get(playerId);

            CustomInjuries.Instance.Data.SwitchImmunity(targetPlayer);

            response = $"Custom injuries immunity for {targetPlayer.Nickname} successfully switched";
            return true;
        }
    }
}
