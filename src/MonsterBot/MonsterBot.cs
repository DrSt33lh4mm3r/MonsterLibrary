using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using MonsterLibrary.Monsters.Repositories;
using MonsterLibrary.Monsters.Model;

namespace MonsterLibrary.MonsterBot
{
    public class MonsterBot
    {
        private TelegramBotClient botClient;
        private IMonstersRepository monsterRepo;

        public MonsterBot(string botKey, CancellationToken cancellationToken, IMonstersRepository monsterRepo)
        {
            this.monsterRepo = monsterRepo;
            botClient = new TelegramBotClient(botKey);

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken);
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;
            // Only process text messages
            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            await HandleMessage(messageText, chatId, cancellationToken).ConfigureAwait(false);
        }

        async Task HandleMessage(string messageText, long chatId, CancellationToken cancellationToken)
        {
            if (messageText == "/start")
            {
                await RespondMenu(chatId, cancellationToken).ConfigureAwait(false);
                return;
            }

            var monster = await this.monsterRepo.GetMonsterAsync(messageText);

            if (monster is null)
            {
                await RespondNotFound(messageText, chatId, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await RespondMonsterFound(monster, chatId, cancellationToken).ConfigureAwait(false);
            }
        }

        async Task RespondMonsterFound(Monster monster, long chatId, CancellationToken cancellationToken)
        {
            if (monster.token != "")
            {
                await botClient.SendPhotoAsync(
                    chatId: chatId,
                    photo: monster.token,
                    caption: this.GetMonsterCaption(monster),
                    parseMode: ParseMode.MarkdownV2,
                    cancellationToken: cancellationToken);
            }

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: this.GetMonsterText(monster),
                parseMode: ParseMode.MarkdownV2,
                cancellationToken: cancellationToken).ConfigureAwait(false);

        }

        string GetMonsterCaption(Monster monster)
        {
            return "*Name: *" + this.EscapeMesage(monster.name) + " *CR: *" + this.EscapeMesage(monster.cr);
        }

        string GetMonsterText(Monster monster)
        {
            List<string> lines = new List<string>();

            lines.Add("__*" + this.EscapeMesage(monster.name) + "*__");
            // todo: add size before type "small humanoid"; needs size mapping before this works
            lines.Add(this.EscapeMesage(monster.type) + ", " + this.EscapeMesage(monster.alignment));
            // todo: add xp to cr
            lines.Add("*CR: *" + this.EscapeMesage(monster.cr));
            if (monster.environment is not null && monster.environment != "")
            {
                lines.Add("*Environment: *" + this.EscapeMesage(monster.environment));
            }
            
            lines.Add("");
            lines.Add(this.GetStatBlock(monster.statBlock));            
            lines.Add("");
            lines.Add("*Armor Class: *" + this.EscapeMesage(String.Join(", ", monster.ac.Select(ac => ac.ToString()))));
            lines.Add("*Hit Points: *" + this.EscapeMesage(monster.hp.ToString()));
            lines.Add("*Speed: *" + this.EscapeMesage(monster.speed.ToString()));
            lines.Add("");
            if (monster.senses is not null && monster.senses != "")
            {
                lines.Add("*Senses: *" + this.EscapeMesage(monster.senses));
            }
            
            lines.Add("*Passive Perception: *" + monster.passivePerception);

            if (monster.languages is not null && monster.languages != "") 
            {
                lines.Add("*Languages: *" + this.EscapeMesage(monster.languages));
            }
            else
            {
                lines.Add("*Languages: *_none_");
            }
            

            if (monster.skills is not null && monster.skills.Length > 0)
            {
                lines.Add("");
                lines.Add("*Skills: *" + this.EscapeMesage(String.Join(", ", monster.skills.Select(s => s.ToString()))));
            }
            if (monster.saves is not null && monster.saves.Length > 0)
            {
                lines.Add("*Saving Throws: *" + this.EscapeMesage(String.Join(", ", monster.skills.Select(s => s.ToString()))));
            }
            
            if (monster.traits is not null && monster.traits.Length > 0)
            {
                lines.Add("");
                foreach (var trait in monster.traits)
                {
                    lines.Add("*" + this.EscapeMesage(trait.name) + "* " + this.EscapeMesage(trait.description));
                }
            }

            // NEWLINE
            if (monster.immunities is not null && monster.immunities.damage != "")
            {
                lines.Add("*Damage Immunities: *" + this.EscapeMesage(monster.immunities.damage));
            }

            if (monster.immunities is not null && monster.immunities.condition != "")
            {
                lines.Add("*Condition Immunities: *" + this.EscapeMesage(monster.immunities.condition));
            }

            if (monster.resistances is not null && monster.resistances != "")
            {
                lines.Add("*Damage Resistances: *" + this.EscapeMesage(monster.resistances));
            }

            if (monster.vulnerabilities is not null && monster.vulnerabilities != "")
            {
                lines.Add("*Damage Vulnerabilities: *" + this.EscapeMesage(monster.vulnerabilities));
            }

            if (monster.actions is not null && monster.actions.Length > 0)
            {
                lines.Add("");
                lines.Add("*Actions*");
                foreach (var action in monster.actions)
                {
                    lines.Add("*" + this.EscapeMesage(action.name) + "* " + this.EscapeMesage(action.description));
                }                
            }

            if (monster.reactions is not null && monster.reactions.Length > 0)
            {
                lines.Add("*Rections*");
                foreach (var reaction in monster.reactions)
                {
                    lines.Add("*" + this.EscapeMesage(reaction.name) + "* " + this.EscapeMesage(reaction.description));
                }                
            }

            // todo: add legendary actions 
            // todo: add fluff

            var message = String.Join("\n", lines.ToArray());

            Console.WriteLine(message);

            return message;
        }

        private string GetStatBlock(StatBlock sb) 
        {
            var str = "*STR: *" + this.EscapeMesage(sb.str.ToString());
            var dex = " *DEX: *" + this.EscapeMesage(sb.dex.ToString());
            var con = " *CON: *" + this.EscapeMesage(sb.con.ToString());
            var inte = "*INT: *" + this.EscapeMesage(sb.inte.ToString());
            var wis = " *WIS: *" + this.EscapeMesage(sb.wis.ToString());
            var cha = " *CHA: *" + this.EscapeMesage(sb.cha.ToString());

            return str + dex + con + "\n" + inte + wis + cha;
        }

        async Task RespondMenu(long chatId, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Welcome to my Monster Library, full of monsters to use in your DnD encounters!\nTo find information about a monster, just tell me its name.",
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        async Task RespondNotFound(string monsterName, long chatId, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Sorry, I couldn't find a monster named: " + monsterName,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private string EscapeMesage(string mesage) 
        {
            string[] charsToEscape = {"_", "*", "[", "]", "(", ")", "~", "`", ">", "#", "+", "-", "=", "|", "{", "}", ".", "!"};

            foreach (var c in charsToEscape)
            {
                mesage = mesage.Replace(c, "\\" + c);
            }

            return mesage;
        }
    }
}