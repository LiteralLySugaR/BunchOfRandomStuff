using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BunchOfRandomStuff
{
    public class Titles
    {
        public static string ConfigPath { get; } = @"%appdata%\EXILED\Configs\7777-config.yml";
        public class Title
        {
            public string ID;
            public string Name;
        }
        public static List<Title> AllTitles = new List<Title>
        {
            new Title { ID = "a1", Name = "This. Is. FOUNDATION!" },
            new Title { ID = "a2", Name = "Reverse Disposal" },
            new Title { ID = "a3", Name = "Like a Boss" },
            new Title { ID = "a4", Name = "All your Facility..." },
            new Title { ID = "a5", Name = "NANI?!" },

            new Title { ID = "b1", Name = "Allergic to Peanut" },
            new Title { ID = "b2", Name = "Y'all are Sickos" },
            new Title { ID = "b3", Name = "WHAT ARE YOU DOING??" },
            new Title { ID = "b4", Name = "Installed Successfully" },
            new Title { ID = "b5", Name = "Yessir" },
            new Title { ID = "b6", Name = "WHY WONT YOU DIE??" },
            new Title { ID = "b7", Name = "Peanut at Home:" },

            new Title { ID = "c1", Name = "SCP-001" }
        };
        public static void NotifyAchievement(Player player, string TitleID, string ColourHEX)
        {
            string notification = $"";
            Title Title = new();
            foreach (Title title in AllTitles)
            {
                if (title.ID == TitleID || title.ID == TitleID.ToLower())
                {
                    notification = $"Title \"<color={ColourHEX}>{title.Name}</color>\" achieved!\nSee console for more info!";
                    Title = title;
                }
            }
            player.ShowHint(notification, 5);
            player.SendConsoleMessage($"Title {Title.Name} (ID:{Title.ID}) has been Achieved. you can use command \"title enable/disable/toggle {Title.ID}\" to enable or disable it, if your are not sure if it is enabled or not, you can toggle it to set opposite value.", "cyan");
        }
        public static List<string> StringsActive = new List<string>
        {
            BORSMain.Instance.Config.ActiveTitleA1,
            BORSMain.Instance.Config.ActiveTitleA2,
            BORSMain.Instance.Config.ActiveTitleA3,
            BORSMain.Instance.Config.ActiveTitleA4,
            BORSMain.Instance.Config.ActiveTitleA5,
            BORSMain.Instance.Config.ActiveTitleB1,
            BORSMain.Instance.Config.ActiveTitleB2,
            BORSMain.Instance.Config.ActiveTitleB3,
            BORSMain.Instance.Config.ActiveTitleB4,
            BORSMain.Instance.Config.ActiveTitleB5,
            BORSMain.Instance.Config.ActiveTitleB6,
            BORSMain.Instance.Config.ActiveTitleB7,
            BORSMain.Instance.Config.ActiveTitleC1
        };
        public static List<string> StringsHas = new List<string>
        {
            BORSMain.Instance.Config.HasTitleA1,
            BORSMain.Instance.Config.HasTitleA2,
            BORSMain.Instance.Config.HasTitleA3,
            BORSMain.Instance.Config.HasTitleA4,
            BORSMain.Instance.Config.HasTitleA5,
            BORSMain.Instance.Config.HasTitleB1,
            BORSMain.Instance.Config.HasTitleB2,
            BORSMain.Instance.Config.HasTitleB3,
            BORSMain.Instance.Config.HasTitleB4,
            BORSMain.Instance.Config.HasTitleB5,
            BORSMain.Instance.Config.HasTitleB6,
            BORSMain.Instance.Config.HasTitleB7,
            BORSMain.Instance.Config.HasTitleC1
        };
        public static List<string> IDs = new List<string> { "a1", "a2", "a3", "a4", "a5", "b1", "b2", "b3", "b4", "b5", "b6", "b7", "c1" };
        public static string EditActiveTitle(string TitleID, Player player, int Enable) //Enable: 0 Disable | 1 Enable | 2 Toggle
        {
            TitleID.ToLower();

            string toReturn = "Failed to Enable/Disable.";
            int index = IDs.IndexOf(TitleID);

            if (Enable == 1)
            {
                if (!StringsActive[index].Contains(player.UserId))
                {
                    StringsActive[index] += $",{player.UserId}";

                    toReturn = $"Enabled Successfully at {index}.";
                    Log.Debug($"(EditActiveTitle) Enabled Successfully at {index}.");
                }
                InitialiseChanges("EditActiveTitle");
            }
            else if (Enable == 0)
            {
                if (StringsActive[index].Contains(player.UserId))
                {
                    string[] str = StringsActive[index].Split(',');
                    str[str.IndexOf(player.UserId)] = "";
                    string[] str1 = str.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                    StringsActive[index] = string.Join(",", str1);

                    toReturn = $"Disabled Successfully at {index}.";
                    Log.Debug($"(EditActiveTitle) Disabled Successfully at {index}.");
                }
                InitialiseChanges("EditActiveTitle");
            }
            else if (Enable == 2)
            {
                if (StringsActive[IDs.IndexOf(TitleID)].Contains(player.UserId))
                {
                    string[] str = StringsActive[index].Split(',');
                    str[str.IndexOf(player.UserId)] = "";
                    string[] str1 = str.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                    StringsActive[index] = string.Join(",", str1);

                    toReturn = $"Disabled Successfully at {index}.";
                    Log.Debug($"(EditActiveTitle) Disabled Successfully at {index}.");
                }
                else
                {
                    StringsActive[index] += $",{player.UserId}";
                    toReturn = $"Enabled Successfully at {index}.";
                    Log.Debug($"(EditActiveTitle) Enabled Successfully at {index}.");
                }
                InitialiseChanges("EditActiveTitle");
            }
            return toReturn;
        }
        public static string AddTitle(string TitleID, Player player, bool DoAdd)
        {
            TitleID.ToLower();

            string toReturn = "Failed to Add/Remove.";
            int index = IDs.IndexOf(TitleID);

            if (StringsHas[index].Contains(player.UserId) && !DoAdd)
            {
                string[] str = StringsHas[index].Split(',');
                str[str.IndexOf(player.UserId)] = "";
                string[] str1 = str.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                StringsHas[index] = string.Join(",", str1);

                toReturn = $"Removed Successfully at {index}.";
                Log.Debug($"(AddTitle) Removed Successfully at {index}.");

                toReturn += $" {EditActiveTitle(TitleID, player, 0)}";
            }
            else if (!StringsHas[index].Contains(player.UserId) && DoAdd)
            {
                StringsHas[index] += $",{player.UserId}";

                toReturn = $"Added Successfully at {index}.";
                Log.Debug($"(AddTitle) Added Successfully at {index}.");

                NotifyAchievement(player, TitleID, "#00FF00");

                InitialiseChanges("AddTitle");
            }
            return toReturn;
        }
        public static void InitialiseChanges(string sender)
        {
            string[] file = File.ReadAllLines(ConfigPath);

            string hasTitle = "  has_title_";
            string activeTitle = "  active_title_";
            int indexHas = 0;
            int indexActive = 0;
            int strIndex = 0;

            foreach (string str in file.ToArray())
            {
                if (str.StartsWith($"{hasTitle}{IDs[indexHas]}:"))
                {
                    string str1 = $"{hasTitle}{IDs[indexHas]}: '{StringsHas[indexHas]}'";
                    Log.Debug($"({sender}-Has:{indexHas},{strIndex}) Changing Config at \n'{file[strIndex]}' to \n'{str1}'");
                    file[strIndex] = str1;

                    if (indexHas < StringsHas.Count - 1)
                    {
                        indexHas++;
                    }
                }
                if (str.StartsWith($"{activeTitle}{IDs[indexActive]}:"))
                {
                    string str1 = $"{activeTitle}{IDs[indexActive]}: '{StringsActive[indexActive]}'";
                    Log.Debug($"({sender}-Active:{indexActive},{strIndex}) Changing Config at \n'{file[strIndex]}' to \n'{str1}'");
                    file[strIndex] = str1;

                    if (indexActive < StringsActive.Count - 1)
                    {
                        indexActive++;
                    }
                }
                strIndex++;
            }
            Log.Debug($"({sender}) Writing Config...");
            File.WriteAllLines(ConfigPath, file);

            Log.Debug($"({sender}) Updating variables...");
            UpdateVars();
        }
        public static void UpdateVars()
        {
            BORSMain.Instance.Config.HasTitleA1 = StringsHas[0];
            BORSMain.Instance.Config.HasTitleA2 = StringsHas[1];
            BORSMain.Instance.Config.HasTitleA3 = StringsHas[2];
            BORSMain.Instance.Config.HasTitleA4 = StringsHas[3];
            BORSMain.Instance.Config.HasTitleA5 = StringsHas[4];
            BORSMain.Instance.Config.HasTitleB1 = StringsHas[5];
            BORSMain.Instance.Config.HasTitleB2 = StringsHas[6];
            BORSMain.Instance.Config.HasTitleB3 = StringsHas[7];
            BORSMain.Instance.Config.HasTitleB4 = StringsHas[8];
            BORSMain.Instance.Config.HasTitleB5 = StringsHas[9];
            BORSMain.Instance.Config.HasTitleB6 = StringsHas[10];
            BORSMain.Instance.Config.HasTitleB7 = StringsHas[11];
            BORSMain.Instance.Config.HasTitleC1 = StringsHas[12];

            BORSMain.Instance.Config.ActiveTitleA1 = StringsActive[0];
            BORSMain.Instance.Config.ActiveTitleA2 = StringsActive[1];
            BORSMain.Instance.Config.ActiveTitleA3 = StringsActive[2];
            BORSMain.Instance.Config.ActiveTitleA4 = StringsActive[3];
            BORSMain.Instance.Config.ActiveTitleA5 = StringsActive[4];
            BORSMain.Instance.Config.ActiveTitleB1 = StringsActive[5];
            BORSMain.Instance.Config.ActiveTitleB2 = StringsActive[6];
            BORSMain.Instance.Config.ActiveTitleB3 = StringsActive[7];
            BORSMain.Instance.Config.ActiveTitleB4 = StringsActive[8];
            BORSMain.Instance.Config.ActiveTitleB5 = StringsActive[9];
            BORSMain.Instance.Config.ActiveTitleB6 = StringsActive[10];
            BORSMain.Instance.Config.ActiveTitleB7 = StringsActive[11];
            BORSMain.Instance.Config.ActiveTitleC1 = StringsActive[12];
        }
    }
}
