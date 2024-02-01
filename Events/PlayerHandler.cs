using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp079;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace BunchOfRandomStuff.Events
{
    internal sealed class PlayerHandler
    {
        public static void PingTitleC1(Player player)
        {
            if (BORSMain.Instance.Config.HasTitleA1.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleA2.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleA3.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleA4.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleA5.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleB1.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleB2.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleB3.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleB4.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleB5.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleB6.Contains(player.UserId) &&
                BORSMain.Instance.Config.HasTitleB7.Contains(player.UserId))
            {
                BORSMain.Instance.Config.HasTitleC1 += $",{player.UserId}";
                BORSMain.Instance.Config.ActiveTitleC1 += $",{player.UserId}";
                Titles.NotifyAchievement(player, "c1", "#FF0000");
            }
        }
        public static List<Player> Players = new();
        public List<RoleTypeId> HumanRoles = new List<RoleTypeId>
        {
            RoleTypeId.ClassD,
            RoleTypeId.Scientist,
            RoleTypeId.FacilityGuard,
            RoleTypeId.NtfPrivate,
            RoleTypeId.NtfSergeant,
            RoleTypeId.NtfSpecialist,
            RoleTypeId.NtfCaptain,
            RoleTypeId.ChaosConscript,
            RoleTypeId.ChaosMarauder,
            RoleTypeId.ChaosRepressor,
            RoleTypeId.ChaosRifleman
        };
        public List<RoleTypeId> NTFRoles = new List<RoleTypeId>
        {
            RoleTypeId.NtfPrivate,
            RoleTypeId.NtfSergeant,
            RoleTypeId.NtfSpecialist,
            RoleTypeId.NtfCaptain
        };
        public List<RoleTypeId> ChaosRoles = new List<RoleTypeId>
        {
            RoleTypeId.ChaosConscript,
            RoleTypeId.ChaosMarauder,
            RoleTypeId.ChaosRepressor,
            RoleTypeId.ChaosRifleman
        };
        public List<RoleTypeId> SCPsRoles = new List<RoleTypeId>
        {
            RoleTypeId.Scp049,
            RoleTypeId.Scp079,
            RoleTypeId.Scp096,
            RoleTypeId.Scp106,
            RoleTypeId.Scp173,
            RoleTypeId.Scp939
        };
        public List<RoleTypeId> C1SCPsRoles = new List<RoleTypeId>
        {
            RoleTypeId.Scp049,
            RoleTypeId.Scp096,
            RoleTypeId.Scp106,
            RoleTypeId.Scp173,
            RoleTypeId.Scp939,
            RoleTypeId.Scp0492
        };
        public static void CheckPlayersVictory() //Fires only when Chaos Victory
        {
            /*
             *  ######      ##
             *  ##  ##    ####
             *  ######  ##  ##
             *  ##  ##  ######
             *  ##  ##      ##
            */
            foreach (Player player in Players)
            {
                foreach (PlayerLifeKills plk in PlayerKills)
                {
                    if (plk.player == player && 
                        (player.Role == RoleTypeId.ChaosConscript || 
                        player.Role == RoleTypeId.ChaosMarauder || 
                        player.Role == RoleTypeId.ChaosRepressor || 
                        player.Role == RoleTypeId.ChaosRifleman))
                    {
                        if (plk.ChaosDeaths == 0)
                        {
                            BORSMain.Instance.Config.HasTitleA4 += $",{player.UserId}";
                            BORSMain.Instance.Config.ActiveTitleA4 += $",{player.UserId}";
                            Titles.NotifyAchievement(player, "a4", "#FF0000");
                            PingTitleC1(player);
                        }
                    }
                }
            }
        }
        public void PLKDie(PlayerLifeKills plk, bool isChaos)
        {
            plk.Deaths++;
            plk.ChaosKilled = 0;
            plk.ClassDKilled = 0;
            plk.ElseTarget = 0;
            plk.GuardKilled = 0;
            plk.MTFKilled = 0;
            plk.ScientistKilled = 0;
            plk.SCPKilled = 0;
            plk.SCP0492Killed = 0;
            plk.SCPGrenadeKilled = 0;
            plk.HumanKilled = 0;
            if (isChaos) { plk.ChaosDeaths++; }
        }
        public class PlayerLifeKills
        {
            public Player player;
            public float ClassDKilled;
            public float ScientistKilled;
            public float GuardKilled;
            public float MTFKilled;
            public float ChaosKilled;
            public float SCPKilled;
            public float SCP0492Killed;
            public float Deaths;
            public float ElseTarget;
            public float ChaosDeaths;
            public float SCPGrenadeKilled;
            public float HumanKilled;
        }
        public static List<PlayerLifeKills> PlayerKills = new List<PlayerLifeKills>();

        public static class RankColours
        {
            public enum RankColour
            {
                pink,           //#FF96DE
                red,            //#C50000
                brown,          //#944710
                silver,         //#A0A0A0
                light_green,    //#32CD32
                crimson,        //#DC143C
                cyan,           //#00B7EB
                aqua,           //#00FFFF
                deep_pink,      //#FF1493
                tomato,         //#FF6448
                yellow,         //#FAFF86
                magenta,        //#FF0090
                blue_green,     //#4DFFB8
                orange,         //#FF9966
                lime,           //#BFFF00
                green,          //#228B22
                emerald,        //#50C878
                carmine,        //#960018
                nickel,         //#727472
                mint,           //#98FB98
                army_green,     //#4B5320
                pumpkin         //#EE7600
            }
            public static List<string> Colours = new List<string>
            {
                "pink",
                "red",
                "brown",
                "silver",
                "light_green",
                "crimson",
                "cyan",
                "aqua",
                "deep_pink",
                "tomato",
                "yellow",
                "magenta",
                "blue_green",
                "orange",
                "lime",
                "green",
                "emerald",
                "carmine",
                "nickel",
                "mint",
                "army_green",
                "pumpkin"
            };
            public static string GetColour(RankColour rankColour)
            {
                return Colours[(int)rankColour];
            }
        }

        public class PlRank
        {
            public Player player;
            public string Rank;
            public string Colour;
        }
        internal List<PlRank> plRanks = new List<PlRank>();
        internal void SetPlayerRank(Player player, string RankName, string Colour)
        {
            plRanks.Add(new PlRank { player = player, Rank = player.RankName, Colour = player.RankColor });

            player.RankName = RankName;
            player.RankColor = Colour;
        }
        internal void ResetPlayerRank(Player player)
        {
            foreach (PlRank rank in plRanks.ToArray())
            {
                if (rank.player == player)
                {
                    player.RankColor = rank.Colour;
                    player.RankName = rank.Rank;

                    plRanks.Remove(rank);
                    break;
                }
            }
        }
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            if (ev.LeadingTeam == Exiled.API.Enums.LeadingTeam.ChaosInsurgency)
            {
                CheckPlayersVictory();
            }
        }
        public void MutuallyExclusiveTitles(Player player)
        {

            foreach (string str in Titles.StringsActive.ToArray())
            {
                if (str.Contains(player.UserId) && Titles.StringsActive.IndexOf(str) < Titles.IDs.IndexOf("c1"))
                {
                    string[] str1 = str.Split(',');
                    str1[str1.IndexOf(player.UserId)] = "";
                    string[] str2 = str1.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                    Titles.StringsActive[Titles.StringsActive.IndexOf(str)] = string.Join(",", str2);
                }
            }
            Titles.InitialiseChanges("MutuallyExclusiveTitles");
        }
        public void OnVerified(VerifiedEventArgs ev)
        {
            Players.Add(ev.Player);
            PlayerKills.Add(new() { player = ev.Player });
            if (BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
            {
                MutuallyExclusiveTitles(ev.Player);
            }
        }
        public void OnJoined(JoinedEventArgs ev)
        {
            //Log.Info($"{ev.Player.DisplayNickname} Rank Colour is {ev.Player.RankColor}");
            //Players.Add(ev.Player);
        }
        public void OnLeft(LeftEventArgs ev)
        {
            ResetPlayerRank(ev.Player);
            Players.Remove(ev.Player);

            foreach (PlayerLifeKills plk in PlayerKills.ToArray())
            {
                if (plk.player == ev.Player)
                {
                    PlayerKills.Remove(plk);
                    break;
                }
            }
        }
        public void OnGainingExperience(GainingExperienceEventArgs ev)
        {
            if (BORSMain.Instance.Config.ActiveTitleB4.Contains(ev.Player.UserId) || BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
            {
                ev.Amount += 2;
            }
        }
        public void OnGainingLevel(GainingLevelEventArgs ev)
        {
            /*
             *  ####        ##
             *  ##  ##    ####
             *  ######  ##  ##
             *  ##  ##  ######
             *  ####        ##
            */
            foreach (PlayerLifeKills plk in PlayerKills)
            {
                if (plk.player == ev.Player)
                {
                    plk.ElseTarget++;

                    if (plk.ElseTarget >= 4 && Round.ElapsedTime < TimeSpan.FromMinutes(5))
                    {
                        BORSMain.Instance.Config.HasTitleB4 += $",{ev.Player.UserId}";
                        BORSMain.Instance.Config.ActiveTitleB4 += $",{ev.Player.UserId}";
                        Titles.NotifyAchievement(ev.Player, "b4", "#FF0000");
                        PingTitleC1(ev.Player);
                    }
                }
            }
        }
        public void OnDied (DiedEventArgs ev)
        {
            if (ev.Player is null) { return; }

            ResetPlayerRank(ev.Player);

            foreach (PlayerLifeKills plk in PlayerKills.ToArray())
            {
                //if nobody will kill an SCP with a grenade, im clearing it then
                if (plk.SCPGrenadeKilled - Time.time > 5f) { plk.SCPGrenadeKilled = 0; }

                if (ev.Player.Role == RoleTypeId.ClassD && !SCPsRoles.Contains(ev.Attacker.Role))
                {
                    if (plk.player == ev.Attacker)
                    {
                        plk.ClassDKilled++;
                    }
                    if (plk.player == ev.Player)
                    {
                        PLKDie(plk, false);
                    }
                }
                else if (ev.Player.Role == RoleTypeId.Scientist && !SCPsRoles.Contains(ev.Attacker.Role))
                {
                    if (plk.player == ev.Attacker)
                    {
                        plk.ScientistKilled++;

                        /*
                         *  ######  ######
                         *  ##  ##      ##
                         *  ######  ######
                         *  ##  ##  ##
                         *  ##  ##  ######
                        */
                        if (ev.Attacker.Role == RoleTypeId.ClassD && plk.ScientistKilled >= 5)
                        {
                            BORSMain.Instance.Config.HasTitleA2 += $",{ev.Attacker.UserId}";
                            BORSMain.Instance.Config.ActiveTitleA2 += $",{ev.Attacker.UserId}";
                            Titles.NotifyAchievement(ev.Attacker, "a2", "#FF0000");
                            PingTitleC1(ev.Attacker);
                        }
                    }
                    if (plk.player == ev.Player)
                    {
                        PLKDie(plk, false);
                    }
                }
                else if (ev.Player.Role == RoleTypeId.FacilityGuard && !SCPsRoles.Contains(ev.Attacker.Role))
                {
                    if (plk.player == ev.Attacker)
                    {
                        plk.GuardKilled++;
                    }
                    if (plk.player == ev.Player)
                    {
                        PLKDie(plk, false);
                    }
                }
                else if (NTFRoles.Contains(ev.Player.Role) && !SCPsRoles.Contains(ev.Attacker.Role))
                {
                    if (plk.player == ev.Attacker)
                    {
                        plk.MTFKilled++;
                    }
                    if (plk.player == ev.Player)
                    {
                        PLKDie(plk, false);
                    }
                }
                else if (ChaosRoles.Contains(ev.Player.Role) && !SCPsRoles.Contains(ev.Attacker.Role))
                {
                    if (plk.player == ev.Attacker)
                    {
                        plk.ChaosKilled++;

                        /*
                         *  ######  ####
                         *  ##  ##    ##
                         *  ######    ##
                         *  ##  ##    ##
                         *  ##  ##  ######
                        */
                        if (ev.Attacker.Role == RoleTypeId.Scientist && plk.ChaosKilled >= 10)
                        {
                            BORSMain.Instance.Config.HasTitleA1 += $",{ev.Attacker.UserId}";
                            BORSMain.Instance.Config.ActiveTitleA1 += $",{ev.Attacker.UserId}";
                            Titles.NotifyAchievement(ev.Attacker, "a1", "#FF0000");
                            PingTitleC1(ev.Attacker);
                        }
                    }
                    if (plk.player == ev.Player)
                    {
                        PLKDie(plk, true);
                    }
                }
                else if (SCPsRoles.Contains(ev.Player.Role) && !SCPsRoles.Contains(ev.Attacker.Role))
                {
                    if (plk.player == ev.Attacker)
                    {
                        plk.SCPKilled++;
                        /*
                         *  ######  ######
                         *  ##  ##      ##
                         *  ######  ######
                         *  ##  ##      ##
                         *  ##  ##  ######
                        */
                        if (NTFRoles.Contains(ev.Attacker.Role) && plk.SCPKilled >= 2)
                        {
                            BORSMain.Instance.Config.HasTitleA3 += $",{ev.Attacker.UserId}";
                            BORSMain.Instance.Config.ActiveTitleA3 += $",{ev.Attacker.UserId}";
                            Titles.NotifyAchievement(ev.Attacker, "a3", "#FF0000");
                            PingTitleC1(ev.Attacker);
                        }
                        /*
                         *  ######  ######
                         *  ##  ##  ## 
                         *  ######    ####
                         *  ##  ##      ##
                         *  ##  ##  ######
                        */
                        if (!(SCPsRoles.Contains(ev.Attacker.Role) || ev.Attacker.Role == RoleTypeId.Scp0492))
                        {
                            if (ev.DamageHandler.Type == Exiled.API.Enums.DamageType.Explosion && plk.SCPGrenadeKilled == 0)
                            {
                                plk.SCPGrenadeKilled = Time.time;
                            }
                            else if (ev.DamageHandler.Type == Exiled.API.Enums.DamageType.Explosion && plk.SCPGrenadeKilled != 0)
                            {
                                if (plk.SCPGrenadeKilled - Time.time <= 1f && plk.SCPKilled >= 2)
                                {
                                    BORSMain.Instance.Config.HasTitleA5 += $",{ev.Attacker.UserId}";
                                    BORSMain.Instance.Config.ActiveTitleA5 += $",{ev.Attacker.UserId}";
                                    Titles.NotifyAchievement(ev.Attacker, "a5", "#FF0000");
                                    PingTitleC1(ev.Attacker);
                                }
                            }
                        }
                    }
                    if (plk.player == ev.Player)
                    {
                        PLKDie(plk, false);
                    }
                }
                else if (ev.Player.Role == RoleTypeId.Scp0492 && !SCPsRoles.Contains(ev.Attacker.Role))
                {
                    if (plk.player == ev.Attacker)
                    {
                        plk.SCP0492Killed++;

                        /*
                         *  ######  ######
                         *  ##  ##  ## 
                         *  ######    ####
                         *  ##  ##      ##
                         *  ##  ##  ######
                        */
                        if (!(SCPsRoles.Contains(ev.Attacker.Role) || ev.Attacker.Role == RoleTypeId.Scp0492))
                        {
                            if (ev.DamageHandler.Type == Exiled.API.Enums.DamageType.Explosion && plk.SCPGrenadeKilled == 0)
                            {
                                plk.SCPGrenadeKilled = Time.time;
                            }
                            else if (ev.DamageHandler.Type == Exiled.API.Enums.DamageType.Explosion && plk.SCPGrenadeKilled != 0)
                            {
                                if (plk.SCPGrenadeKilled - Time.time <= 2f && plk.SCP0492Killed >= 4)
                                {
                                    BORSMain.Instance.Config.HasTitleA5 += $",{ev.Attacker.UserId}";
                                    BORSMain.Instance.Config.ActiveTitleA5 += $",{ev.Attacker.UserId}";
                                    Titles.NotifyAchievement(ev.Attacker, "a5", "#FF0000");
                                    PingTitleC1(ev.Attacker);
                                }
                            }
                        }
                    }
                    if (plk.player == ev.Player)
                    {
                        PLKDie(plk, false);
                    }
                }
                else if (HumanRoles.Contains(ev.Player.Role) && !HumanRoles.Contains(ev.Attacker.Role))
                {
                    if (plk.player == ev.Attacker)
                    {
                        plk.HumanKilled++;

                        /*
                         *  ####    ####
                         *  ##  ##    ##
                         *  ######    ##
                         *  ##  ##    ##
                         *  ####    ######
                        */
                        if (ev.Attacker.Role == RoleTypeId.Scp173 && plk.HumanKilled >= 15)
                        {
                            BORSMain.Instance.Config.HasTitleB1 += $",{ev.Attacker.UserId}";
                            BORSMain.Instance.Config.ActiveTitleB1 += $",{ev.Attacker.UserId}";
                            Titles.NotifyAchievement(ev.Attacker, "b1", "#FF0000");
                            PingTitleC1(ev.Attacker);
                        }
                        /*
                         *  ####    ######
                         *  ##  ##      ##
                         *  ######  ######
                         *  ##  ##  ##
                         *  ####    ######
                        */
                        else if (ev.Attacker.Role == RoleTypeId.Scp049 && plk.HumanKilled >= 15)
                        {
                            BORSMain.Instance.Config.HasTitleB2 += $",{ev.Attacker.UserId}";
                            BORSMain.Instance.Config.ActiveTitleB2 += $",{ev.Attacker.UserId}";
                            Titles.NotifyAchievement(ev.Attacker, "b2", "#FF0000");
                            PingTitleC1(ev.Attacker);
                        }
                        /*
                         *  ####    ######
                         *  ##  ##      ##
                         *  ######  ######
                         *  ##  ##      ##
                         *  ####    ######
                        */
                        else if (ev.Attacker.Role == RoleTypeId.Scp0492 && plk.HumanKilled >= 10)
                        {
                            BORSMain.Instance.Config.HasTitleB3 += $",{ev.Attacker.UserId}";
                            BORSMain.Instance.Config.ActiveTitleB3 += $",{ev.Attacker.UserId}";
                            Titles.NotifyAchievement(ev.Attacker, "b3", "#FF0000");
                            PingTitleC1(ev.Attacker);
                        }
                        /*
                         *  ####    ######
                         *  ##  ##  ## 
                         *  ######    ####
                         *  ##  ##      ##
                         *  ####    ######
                        */
                        else if (ev.Attacker.Role == RoleTypeId.Scp939 && plk.HumanKilled >= 20)
                        {
                            BORSMain.Instance.Config.HasTitleB5 += $",{ev.Attacker.UserId}";
                            BORSMain.Instance.Config.ActiveTitleB5 += $",{ev.Attacker.UserId}";
                            Titles.NotifyAchievement(ev.Attacker, "b5", "#FF0000");
                            PingTitleC1(ev.Attacker);
                        }
                        /*
                         *  ####    ######
                         *  ##  ##      ##
                         *  ######    ##
                         *  ##  ##    ##  
                         *  ####      ##  
                        */
                        else if (ev.Attacker.Role == RoleTypeId.Scp096 && plk.HumanKilled >= 10 && Round.ElapsedTime < TimeSpan.FromMinutes(5))
                        {
                            BORSMain.Instance.Config.HasTitleB7 += $",{ev.Attacker.UserId}";
                            BORSMain.Instance.Config.ActiveTitleB7 += $",{ev.Attacker.UserId}";
                            Titles.NotifyAchievement(ev.Attacker, "b7", "#FF0000");
                            PingTitleC1(ev.Attacker);
                        }
                    }
                    if (plk.player == ev.Player)
                    {
                        PLKDie(plk, false);
                    }
                }
            }
        }
        public void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
        {
            /*
             *  ####    ######
             *  ##  ##  ## 
             *  ######  ######
             *  ##  ##  ##  ##
             *  ####    ######
            */
            foreach (PlayerLifeKills plk in PlayerKills)
            {
                if (plk.player == ev.Player)
                {
                    plk.ElseTarget++;

                    if (plk.ElseTarget >= 3)
                    {
                        BORSMain.Instance.Config.HasTitleB6 += $",{ev.Player.UserId}";
                        BORSMain.Instance.Config.ActiveTitleB6 += $",{ev.Player.UserId}";
                        Titles.NotifyAchievement(ev.Player, "b6", "#FF0000");
                        PingTitleC1(ev.Player);
                    }
                }
            }
        }
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            ResetPlayerRank(ev.Player);
            foreach (PlayerLifeKills plk in PlayerKills)
            {
                if (plk.player == ev.Player)
                {
                    PLKDie(plk, false);
                }
            }
        }
        public void OnFlippingCoin(FlippingCoinEventArgs ev)
        {
            System.Random rng = new();

            if (rng.Next(1, 101) >= 100)
            {
                ev.Player.Kill(Exiled.API.Enums.DamageType.CardiacArrest);
                ev.Player.ShowHint("Lucky Day!", 5);
            }
        }
        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            List<RoleTypeId> AuthorisedPersonnel = new List<RoleTypeId>
            {
                RoleTypeId.NtfSpecialist,
                RoleTypeId.NtfSergeant,
                RoleTypeId.NtfCaptain,
                RoleTypeId.NtfPrivate,
                RoleTypeId.Scientist,
                RoleTypeId.FacilityGuard,
                RoleTypeId.Scp079
            };

            if (AuthorisedPersonnel.Contains(ev.Player.Role))
            {
                ev.DisableTesla = true;
            }
            else { ev.DisableTesla = false; }
        }
        public void OnSpawned(SpawnedEventArgs ev)
        {
            System.Random rng = new();

            if (HumanRoles.Contains(ev.Player.Role) && BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
            {
                float health = (ev.Player.Health / 100f) * 15f;
                ev.Player.Health += health;
            }
            if (C1SCPsRoles.Contains(ev.Player.Role) && BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
            {
                float health = (ev.Player.Health / 100f) * 20f;
                ev.Player.Health += health;
            }

            if (ev.Player.Role == RoleTypeId.ClassD)
            {
                ev.Player.AddItem(ItemType.Coin);

                if (BORSMain.Instance.Config.ActiveTitleA2.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 85)
                    {
                        ev.Player.AddItem(ItemType.KeycardJanitor);

                        SetPlayerRank(ev.Player, $"Janitor", RankColours.GetColour(RankColours.RankColour.pumpkin));
                    }
                    if (rng.Next(1, 101) >= 85)
                    {
                        ev.Player.AddItem(ItemType.Medkit);
                    }
                }
                else if (BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 50)
                    {
                        ev.Player.AddItem(ItemType.KeycardJanitor);
                        ev.Player.AddItem(ItemType.Medkit);

                        SetPlayerRank(ev.Player, $"Janitor", RankColours.GetColour(RankColours.RankColour.pumpkin));
                    }
                }
                else if (rng.Next(1, 101) >= 85)
                {
                    ev.Player.AddItem(ItemType.KeycardJanitor);

                    SetPlayerRank(ev.Player, $"Janitor", RankColours.GetColour(RankColours.RankColour.pumpkin));
                }
            }

            if (ev.Player.Role == RoleTypeId.Scientist)
            {
                if (BORSMain.Instance.Config.ActiveTitleA1.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 85)
                    {
                        ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Nato9, 20);
                        ev.Player.AddItem(ItemType.GunCOM15);
                    }
                    if (rng.Next(1, 101) >= 50)
                    {
                        foreach (Item item in ev.Player.Items)
                        {
                            if (item.Type == ItemType.KeycardScientist)
                            {
                                ev.Player.RemoveItem(item);
                                break;
                            }
                        }
                        ev.Player.AddItem(ItemType.KeycardResearchCoordinator);

                        SetPlayerRank(ev.Player, $"Research Supervisor", RankColours.GetColour(RankColours.RankColour.yellow));
                    }
                }
                else if (BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
                {
                    ev.Player.AddItem(ItemType.Radio);

                    if (rng.Next(1, 101) >= 25)
                    {
                        foreach (Item item in ev.Player.Items)
                        {
                            if (item.Type == ItemType.KeycardScientist)
                            {
                                ev.Player.RemoveItem(item);
                                break;
                            }
                        }
                        ev.Player.AddItem(ItemType.KeycardResearchCoordinator);

                        SetPlayerRank(ev.Player, $"Research Supervisor", RankColours.GetColour(RankColours.RankColour.yellow));
                    }
                }
                else if (rng.Next(1, 101) >= 75)
                {
                    foreach (Item item in ev.Player.Items)
                    {
                        if (item.Type == ItemType.KeycardScientist)
                        {
                            ev.Player.RemoveItem(item);
                            break;
                        }
                    }
                    ev.Player.AddItem(ItemType.KeycardResearchCoordinator);

                    SetPlayerRank(ev.Player, $"Research Supervisor", RankColours.GetColour(RankColours.RankColour.yellow));
                }
            }

            if (ev.Player.Role == RoleTypeId.FacilityGuard)
            {
                if (BORSMain.Instance.Config.ActiveTitleA5.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 65)
                    {
                        foreach (Item item in ev.Player.Items)
                        {
                            if (item.Type == ItemType.KeycardGuard)
                            {
                                ev.Player.RemoveItem(item);
                                break;
                            }
                        }
                        ev.Player.AddItem(ItemType.KeycardNTFOfficer);
                        ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Nato9, 90);

                        SetPlayerRank(ev.Player, $"Head of Security", RankColours.GetColour(RankColours.RankColour.silver));
                    }
                    if (rng.Next(1, 101) >= 85)
                    {
                        foreach (Item item in ev.Player.Items)
                        {
                            if (item.Type == ItemType.GrenadeFlash)
                            {
                                ev.Player.RemoveItem(item);
                                break;
                            }
                        }
                        ev.Player.AddItem(ItemType.GrenadeHE);
                    }
                }
                else if (BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 25)
                    {
                        foreach (Item item in ev.Player.Items)
                        {
                            if (item.Type == ItemType.GrenadeFlash)
                            {
                                ev.Player.RemoveItem(item);
                                break;
                            }
                        }
                        ev.Player.AddItem(ItemType.GrenadeHE);
                        foreach (Item item in ev.Player.Items)
                        {
                            if (item.Type == ItemType.KeycardGuard)
                            {
                                ev.Player.RemoveItem(item);
                                break;
                            }
                        }
                        ev.Player.AddItem(ItemType.KeycardNTFOfficer);
                        ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Nato9, 90);

                        SetPlayerRank(ev.Player, $"Head of Security", RankColours.GetColour(RankColours.RankColour.silver));
                    }
                }
                else if (rng.Next(1, 101) >= 75)
                {
                    foreach (Item item in ev.Player.Items)
                    {
                        if (item.Type == ItemType.KeycardGuard)
                        {
                            ev.Player.RemoveItem(item);
                            break;
                        }
                    }
                    ev.Player.AddItem(ItemType.KeycardNTFOfficer);
                    ev.Player.AddAmmo(Exiled.API.Enums.AmmoType.Nato9, 90);

                    SetPlayerRank(ev.Player, $"Head of Security", RankColours.GetColour(RankColours.RankColour.silver));
                }
            }

            if (ev.Player.Role == RoleTypeId.NtfCaptain)
            {
                if (BORSMain.Instance.Config.ActiveTitleA3.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 65)
                    {
                        ev.Player.AddItem(ItemType.MicroHID);
                    }
                }
                else if (BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 75)
                    {
                        ev.Player.AddItem(ItemType.MicroHID);
                    }
                }
            }
            if (ev.Player.Role == RoleTypeId.NtfSergeant || ev.Player.Role == RoleTypeId.NtfSpecialist)
            {
                if (BORSMain.Instance.Config.ActiveTitleA3.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 80)
                    {
                        ev.Player.AddItem(ItemType.Jailbird);
                    }
                }
                else if (BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 80)
                    {
                        ev.Player.AddItem(ItemType.Jailbird);
                    }
                }
            }
            if (ev.Player.Role == RoleTypeId.NtfPrivate)
            {
                if (BORSMain.Instance.Config.ActiveTitleA3.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 90)
                    {
                        ev.Player.AddItem(ItemType.ParticleDisruptor);
                    }
                }
                else if (BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 85)
                    {
                        ev.Player.AddItem(ItemType.ParticleDisruptor);
                    }
                }
            }

            if (ChaosRoles.Contains(ev.Player.Role))
            {
                if (BORSMain.Instance.Config.ActiveTitleA4.Contains(ev.Player.UserId))
                {
                    float health = (ev.Player.Health / 100f) * 10f;
                    ev.Player.Health += health;

                    if (rng.Next(1, 101) >= 70)
                    {
                        ev.Player.AddItem(ItemType.GrenadeHE);
                    }
                }
                else if (BORSMain.Instance.Config.ActiveTitleC1.Contains(ev.Player.UserId))
                {
                    if (rng.Next(1, 101) >= 50)
                    {
                        ev.Player.AddItem(ItemType.GrenadeHE);
                    }
                }
            }

            if (ev.Player.Role == RoleTypeId.Scp173 && BORSMain.Instance.Config.ActiveTitleB1.Contains(ev.Player.UserId))
            {
                float health = (ev.Player.Health / 100f) * 10f;
                ev.Player.Health += health;
            }
            if (ev.Player.Role == RoleTypeId.Scp049 && BORSMain.Instance.Config.ActiveTitleB2.Contains(ev.Player.UserId))
            {
                float health = (ev.Player.Health / 100f) * 10f;
                ev.Player.Health += health;
            }
            if (ev.Player.Role == RoleTypeId.Scp0492 && BORSMain.Instance.Config.ActiveTitleB3.Contains(ev.Player.UserId))
            {
                float health = (ev.Player.Health / 100f) * 35f;
                ev.Player.Health += health;
            }
            if (ev.Player.Role == RoleTypeId.Scp939 && BORSMain.Instance.Config.ActiveTitleB5.Contains(ev.Player.UserId))
            {
                float health = (ev.Player.Health / 100f) * 10f;
                ev.Player.Health += health;
            }
            if (ev.Player.Role == RoleTypeId.Scp106 && BORSMain.Instance.Config.ActiveTitleB6.Contains(ev.Player.UserId))
            {
                float health = (ev.Player.Health / 100f) * 15f;
                ev.Player.Health += health;
            }
            if (ev.Player.Role == RoleTypeId.Scp096 && BORSMain.Instance.Config.ActiveTitleB7.Contains(ev.Player.UserId))
            {
                float health = (ev.Player.Health / 100f) * 10f;
                ev.Player.Health += health;
            }
        }
    }
}
