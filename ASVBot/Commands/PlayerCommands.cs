﻿using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASVPack.Models;
using ASVBot.Data;
using SavegameToolkit;
using System.Text.Unicode;
using System.IO;
using NLog.LayoutRenderers.Wrappers;
using System.Drawing;

namespace ASVBot.Commands
{
    public class PlayerCommands: ApplicationCommandModule
    {
        IContentContainer arkPack;
        IDiscordPlayerManager playerManager;
        List<ICreatureMap> creatureMap;
        ContentContainerGraphics graphicsContainer;


        public PlayerCommands(IContentContainer arkPack, IDiscordPlayerManager discordPlayerManager,  List<ICreatureMap> creatureMap, ContentContainerGraphics graphicsContainer)
        {
            this.arkPack = arkPack;
            this.playerManager = discordPlayerManager;
            this.creatureMap = creatureMap;
            this.graphicsContainer = graphicsContainer;
        }

        [SlashCommand("asv-link", "Link your discord handle to your game handle.")]
        public async Task LinkPlayer(InteractionContext ctx, [Option("gamerTag", "Steam Id / Epic Id / Gamertag")] string playerId)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            string responseString = string.Empty;

            //attempt to find match in game data of user detail provided

            //first by playerId
            long.TryParse(playerId, out long playerIdLong);
            ContentPlayer arkPlayer = arkPack.Tribes.SelectMany(t => t.Players.Where(p => p.Id == playerIdLong && playerIdLong != 0)).FirstOrDefault();
            if(arkPlayer==null)
            {
                arkPlayer = arkPack.Tribes.SelectMany(t => t.Players.Where(p => p.NetworkId == playerId)).FirstOrDefault();
            }
            if (arkPlayer == null)
            {
                arkPlayer = arkPack.Tribes.SelectMany(t => t.Players.Where(p => p.Name == playerId)).FirstOrDefault();
            }
            if(arkPlayer == null)
            {
                arkPlayer = arkPack.Tribes.SelectMany(t => t.Players.Where(p => p.CharacterName == playerId)).FirstOrDefault();
            }

            if (arkPlayer != null)
            {
                var existingLink = playerManager.GetPlayers().FirstOrDefault(p => p.DiscordUsername == ctx.Member.Username);
                if (existingLink != null)
                {
                    var otherAssociate = playerManager.GetPlayers().FirstOrDefault(p=>p.ArkPlayerId == arkPlayer.Id);
                    if(otherAssociate != null && otherAssociate.DiscordUsername != ctx.Member.Username)
                    {
                        //already associated with another discord user
                        responseString = $"ARK player is already associated with another discord user: {otherAssociate.DiscordUsername}";
                    }
                    else
                    {
                        playerManager.LinkPlayer(ctx.Member.Username, arkPlayer.Id, arkPlayer.CharacterName, 1);
                        responseString = $"{ctx.Member.DisplayName} successfully re-linked to {arkPlayer.Name} - ({arkPlayer.Id})";

                    }
                    

                }
                else
                {
                    playerManager.LinkPlayer(ctx.Member.Username, arkPlayer.Id, arkPlayer.CharacterName, 1);
                    responseString = $"{ctx.Member.DisplayName} successfully linked to {arkPlayer.Name} - ({arkPlayer.Id})";
                }
                
            }
            else
            {
                responseString = $"Failed to link player.  No player found on this ARK matching id/tag provided.";
            }

            //Some time consuming task like a database call or a complex operation
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(responseString));
        }

        
        [SlashCommand("asv-server-summary", "Displays an overview summary of the loaded map data.")]
        public async Task GetSummary(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            StringBuilder sb = new StringBuilder();

            var tameCount = arkPack.Tribes.SelectMany(x => x.Tames).Count();
            var playerCount = arkPack.Tribes.SelectMany(x => x.Players).Count();
            var structureCount = arkPack.Tribes.SelectMany(x => x.Structures).Count();

            sb.AppendLine($"**{arkPack.LoadedMap.MapName}**");
            sb.AppendLine($"\tWilds: {arkPack.WildCreatures.Count()}");
            sb.AppendLine($"\tTribes: {arkPack.Tribes.Count()}");
            sb.AppendLine($"\tPlayers: {playerCount}");
            sb.AppendLine($"\tStructures: {structureCount}");
            sb.AppendLine($"\tTames: {tameCount}");
            sb.AppendLine($"\tTimestamp: {arkPack.GameSaveTime.ToString()}");

            var responseString = sb.ToString();

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(responseString));
        }
        

        [SlashCommand("asv-unlink", "Unlink your discord handle from your game handle.")]
        public async Task UnlinkPlayer(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var responseString = "";
            var discordUser = playerManager.GetPlayers().FirstOrDefault(p=>p.DiscordUsername == ctx.Member.Username);
            if (discordUser != null)
            {
                playerManager.RemovePlayer(ctx.Member.Username);
                responseString = $"{ctx.Member.DisplayName} unlinked from gamer tag.";
            }
            else
            {
                responseString = $"{ctx.Member.DisplayName} has no linked gamer tag.";
            }

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(responseString));
        }
        

        [SlashCommand("asv-maptime", "Return the map name and timestamp when game was last saved.")]
        public async Task GetMapTimestamp(InteractionContext ctx)
        {
            string responseString = $"**{arkPack.LoadedMap.MapName}** ({arkPack.GameSaveTime.ToString()})";


            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(responseString));


        }



        

        [SlashCommand("asv-wild-summary", "View summary of wild creatures available on this ARK.")]
        public async Task GetWildSummary(InteractionContext ctx)
        {
            var discordUser = playerManager.GetPlayers().FirstOrDefault(d => d.DiscordUsername.ToLower() == ctx.Member.Username.ToLower());
            if(discordUser == null || !discordUser.IsVerified) 
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Command unavailable until user-link has been verified."));
                return;
            }

            //await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"@{ctx.Member.DisplayName} - Building wild summary report, please wait.."));
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            float fromLat = 50;
            float fromLon = 50;
            float fromRadius = 100;

         
            if (discordUser != null)
            {
                fromRadius = discordUser.MaxRadius;

                var arkPlayerId = discordUser.ArkPlayerId;
                if (arkPlayerId != 0)
                {

                    var arkPlayer = arkPack.Tribes.SelectMany(x => x.Players.Where(p => p.Id == arkPlayerId)).FirstOrDefault();
                    if (arkPlayer != null)
                    {
                        if (arkPlayer.Longitude.HasValue)
                        {
                            fromLat = arkPlayer.Latitude.GetValueOrDefault(50);
                            fromLon = arkPlayer.Longitude.GetValueOrDefault(50);


                        }
                    }
                }

            }



            var responseString = GetWildSummary(fromLat, fromLon, fromRadius);
            var tmpFilename = Path.GetTempFileName();
            File.WriteAllText(tmpFilename, responseString);
            
            FileStream fileStream = new FileStream(tmpFilename,FileMode.Open, FileAccess.Read);



            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"<@{ctx.Member.Id}> - Here's your wild summary showing creatures within a radius of {fromRadius} from {fromLat.ToString("f1")}/{fromLon.ToString("f1")}.").AddFile("WildSummary.txt",fileStream).AddMention(new UserMention(ctx.Member)));

            fileStream.Dispose();

        }
        

        [SlashCommand("asv-wild-detail", "Show details of selected wild creature types nearby.")]
        public async Task GetWildDetail(InteractionContext ctx, [Option("class_name", "Creature Type")]string selectedClass)
        {
            var discordUser = playerManager.GetPlayers().FirstOrDefault(d => d.DiscordUsername.ToLower() == ctx.Member.Username.ToLower());
            if (discordUser == null || !discordUser.IsVerified)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Command unavailable until user-link has been verified."));
                return;
            }


            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var arkPlayer = arkPack.Tribes.SelectMany(t=>t.Players.Where(p=>p.Id == discordUser.ArkPlayerId)).FirstOrDefault();
            if (arkPlayer == null)
            {

                return;
            }

            if(selectedClass.Trim().Length == 0)
            {

                return;
            }

            float fromLat = arkPlayer.Latitude.GetValueOrDefault(0);
            float fromLon = arkPlayer.Longitude.GetValueOrDefault(0);
            float fromRadius = discordUser.MaxRadius;

            var hasMatches = arkPack.WildCreatures.Any(c => c.ClassName.ToLower().Contains(selectedClass.ToLower()));
            if (!hasMatches)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"<@{ctx.Member.Id}> - There were no matches for the selected creature type(s) within a radius of {fromRadius} from {fromLat.ToString("f1")}/{fromLon.ToString("f1")}.").AddMention(new UserMention(ctx.Member)));

                return;
            }

            var wildDetails = arkPack.WildCreatures
                .Where(w =>
                            (
                                (Math.Abs(w.Latitude.GetValueOrDefault(0) - fromLat) <= fromRadius) 
                                && (Math.Abs(w.Longitude.GetValueOrDefault(0) - fromLon) <= fromRadius)
                            )
                            && w.ClassName.ToLower().Contains(selectedClass.ToLower()))
                .OrderBy(o=>o.ClassName).ThenByDescending(o=>o.BaseLevel).ToList();

            
            if(wildDetails== null || wildDetails.Count == 0)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"<@{ctx.Member.Id}> - There were no matches for the selected creature type(s) within a radius of {fromRadius} from {fromLat.ToString("f1")}/{fromLon.ToString("f1")}.").AddMention(new UserMention(ctx.Member)));
                return;
            }

            StringBuilder sbHeader = new StringBuilder();
            sbHeader.Append("Creature,Gender,Level");
            if (discordUser.ResultLocation)
            {
                sbHeader.Append(",Lat,Lon");
            }
            if (discordUser.ResultStats)
            {
                sbHeader.Append(",HP,Stam,Melee,Weight,Speed,Food,Oxy,Craft");
            }
            List<string> lineItems = new List<string>();

            foreach(var wild in wildDetails)
            {
                StringBuilder sbLine = new StringBuilder();

                string creatureType = wild.ClassName;
                var cMap = creatureMap.FirstOrDefault(c => c.ClassName.ToLower() == wild.ClassName.ToLower());
                if (cMap != null) creatureType = cMap.FriendlyName;


                sbLine.Append($"{creatureType},{wild.Gender},{wild.BaseLevel}");

                if (discordUser.ResultLocation)
                {
                    sbLine.Append($",{wild.Latitude.GetValueOrDefault(0).ToString("f1")},{wild.Longitude.GetValueOrDefault(0).ToString("f1")}");
                }
                if (discordUser.ResultStats)
                {
                    sbLine.Append($",{wild.BaseStats[0]}");
                    sbLine.Append($",{wild.BaseStats[1]}");
                    sbLine.Append($",{wild.BaseStats[8]}");
                    sbLine.Append($",{wild.BaseStats[7]}");
                    sbLine.Append($",{wild.BaseStats[9]}");
                    sbLine.Append($",{wild.BaseStats[4]}");
                    sbLine.Append($",{wild.BaseStats[3]}");
                    sbLine.Append($",{wild.BaseStats[11]}");
                }

                lineItems.Add(sbLine.ToString());
            }

            var responseString = FormatResponseTable(sbHeader.ToString(),lineItems);

            var tmpFilename = Path.GetTempFileName();
            File.WriteAllText(tmpFilename, responseString);
            FileStream fileStream = new FileStream(tmpFilename, FileMode.Open, FileAccess.Read);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"<@{ctx.Member.Id}> - Here's your report showing selected creature type(s) within a radius of {fromRadius} from {fromLat.ToString("f1")}/{fromLon.ToString("f1")}.").AddFile("WildDetails.txt", fileStream).AddMention(new UserMention(ctx.Member)));

            fileStream.Close();
            fileStream.Dispose();

        }
        
        [SlashCommand("asv-wild-map", "Show map of selected wild creature types nearby.")]
        public async Task GetWildMapImage(InteractionContext ctx, [Option("class_name", "Creature Type")] string creatureType)
        {
            var discordUser = playerManager.GetPlayers().FirstOrDefault(d => d.DiscordUsername.ToLower() == ctx.Member.Username.ToLower());
            if (discordUser == null || !discordUser.IsVerified)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Command unavailable until user-link has been verified."));
                return;
            }

            if (!discordUser.MarkedMaps)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Your account does not have permission to receive map images."));
                return;
            }

            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var discordPlayer = playerManager.GetPlayers().FirstOrDefault(p => p.DiscordUsername.ToLower() == ctx.Member.Username.ToLower());
            if (discordPlayer == null)
            {


                return;
            }

            var arkPlayer = arkPack.Tribes.SelectMany(t => t.Players.Where(p => p.Id == discordPlayer.ArkPlayerId)).FirstOrDefault();
            if (arkPlayer == null)
            {

                return;
            }

            if (creatureType.Trim().Length == 0)
            {

                return;
            }

            float fromLat = arkPlayer.Latitude.GetValueOrDefault(0);
            float fromLon = arkPlayer.Longitude.GetValueOrDefault(0);
            float fromRadius = discordPlayer.MaxRadius;

            var mapImage = graphicsContainer.GetMapImageWild(creatureType, fromLat,fromLon, fromRadius);


            string tmpFilename = Path.GetTempFileName();
            mapImage.Save(tmpFilename,System.Drawing.Imaging.ImageFormat.Jpeg);
            FileStream fileStream = new FileStream(tmpFilename, FileMode.Open, FileAccess.Read);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"<@{ctx.Member.Id}> - Here's your map showing selected creature type(s) within a radius of {fromRadius} from {fromLat.ToString("f1")}/{fromLon.ToString("f1")}.").AddFile("WildDetails.jpg", fileStream).AddMention(new UserMention(ctx.Member)));

        }

        private string FormatResponseTable(string header, List<string> lines)
        {
            if (lines.Count == 0) return header.Length > 0 ? header: string.Empty;

            StringBuilder sb = new StringBuilder();

            if (header.Contains(",")) //multi-col input
            {
                //check header and first line match column count
                var headerSplit = header.Split(',');
                var firstLineSplit = lines.First().Split(',');

                if(headerSplit.Length != firstLineSplit.Length)
                {

                    return string.Empty;
                }

                int colCount = headerSplit.Length;
                var colSizes = new int[colCount];

                for(int col = 0; col < colCount; col++)
                {
                    var maxColHeader = headerSplit[col].Length;
                    var maxColLine = lines.Max(l => l.Split(',')[col].Length);
                    if(maxColHeader >= maxColLine)
                    {
                        colSizes[col] = maxColHeader;
                    }
                    else
                    {
                        colSizes[col] = maxColLine;
                    }
                }

                //now parse it out providing spacing as necessary
                for(int col = 0; col < colCount; col++)
                {
                    string colHeader = headerSplit[col];
                    sb.Append(colHeader.PadRight(colSizes[col] + 2));
                }
                sb.Append('\n');

                foreach(var line in lines)
                {
                    var lineSplit = line.Split(',');
                    if(lineSplit.Length != headerSplit.Length)
                    {
                        //line cols dont match header, skip this one?
                    }
                    else
                    {
                        for (int col = 0; col < colCount; col++)
                        {
                            string colText = lineSplit[col];
                            sb.Append(colText.PadRight(colSizes[col] + 2));
                        }
                        sb.Append('\n');
                    }
                }
            }
            else
            {
                //no cols so just append the header to the lines and return
                sb.AppendLine(header);
                sb.AppendJoin('\n', lines);
            }

            return sb.ToString();
        }
        
        private string GetWildSummary(float fromLat, float fromLon, float fromRadius)
        {
     

            var wildSummary = arkPack.WildCreatures
                .Where(w => ((Math.Abs(w.Latitude.GetValueOrDefault(0) - fromLat) <= fromRadius) && (Math.Abs(w.Longitude.GetValueOrDefault(0) - fromLon) <= fromRadius)))
                .GroupBy(c => c.ClassName)
                .Select(g => new { ClassName = g.Key, Name = creatureMap.Count(d => d.ClassName == g.Key) == 0 ? g.Key : creatureMap.Where(d => d.ClassName == g.Key).First().FriendlyName, Count = g.Count(), Min = g.Min(l => l.BaseLevel), Max = g.Max(l => l.BaseLevel) })
                .OrderBy(o => o.Name);

            var summaryMin = wildSummary.Min(s => s.Min);
            var summaryMax = wildSummary.Max(s => s.Max);

            List<string> lineData = new List<string>();


            lineData.Add($"[All Wild],{wildSummary.Sum(s => s.Count)},{summaryMin},{summaryMax}");

            foreach (var summary in wildSummary)
            {
                lineData.Add($"{summary.Name},{summary.Count},{summary.Min},{summary.Max}");



            }

            
            string responseString = FormatResponseTable("Creature,Count,Min,Max", lineData);

            return responseString;
        }

        private long GetPlayerId(string discordUsername)
        {
            var discordUser = playerManager.GetPlayers().FirstOrDefault(p => p.DiscordUsername == discordUsername);
            if (discordUser != null) return discordUser.ArkPlayerId;
            return 0;
        }

    }
}