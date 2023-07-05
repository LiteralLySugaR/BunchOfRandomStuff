using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Features;

namespace BunchOfRandomStuff.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Title : ICommand
    {
        public string Command { get; } = "title";

        public string[] Aliases { get; } = new[] { "title" };

        public string Description { get; } = "Usage: title [enable/disable/toggle/add/remove/list] [titleID]";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            string ActionType = "";
            string TitleID = "";
            if (arguments.Array.Length == 2)
            {
                ActionType = arguments.Array[1];
                if (ActionType.Equals("list"))
                {

                }
            }
            if (arguments.Array.Length >= 3)
            {
                ActionType = arguments.Array[1];
                TitleID = arguments.Array[2];

                response = $"Title Command '{ActionType} {TitleID}' sent.";
                if (!Titles.IDs.Contains(TitleID))
                {
                    response = $"Invalid Title ID! ({TitleID})";
                }
                else if (ActionType != null && Titles.IDs.Contains(TitleID))
                {
                    if (ActionType.Equals("enable"))
                    {
                        response = Titles.EditActiveTitle(TitleID, player, 1);
                    }
                    else if (ActionType.Equals("disable"))
                    {
                        response = Titles.EditActiveTitle(TitleID, player, 0);
                    }
                    else if (ActionType.Equals("toggle"))
                    {
                        response = Titles.EditActiveTitle(TitleID, player, 2);
                    }
                    else if (ActionType.Equals("add"))
                    {
                        response = Titles.AddTitle(TitleID, player, true);
                    }
                    else if (ActionType.Equals("remove"))
                    {
                        response = Titles.AddTitle(TitleID, player, false);
                    }
                }
            }
            else
            {
                response = $"Usage: title [enable/disable/toggle/add/remove/list] [titleID]";
            }

            //response = $"Title Command '{ActionType} {TitleID}' sent.";
            return true;
        }
    }
}
