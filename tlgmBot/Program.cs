using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace tlgmBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        private static string comandos =
@"
O que o bot responde:

Quem o nome começar com:
    raul

Palavras:
    danado
    danada

Comandos:
    /command1
    /command2
    /command3
    /helpzacabot

Texto Contendo:
    pesou
    bom dia
    doideira
    nude
    nudes
";

        static async Task Run()
        {
            var Bot = new Api("");
            var me = await Bot.GetMe();
            var offset = 0;

            if (me != null && me.FirstName.Equals("ZacaBot"))
            {
                while (true)
                {
                    var updates = await Bot.GetUpdates(offset);

                    foreach (var update in updates)
                    {
                        if (update.Message != null && !string.IsNullOrEmpty(update.Message.Text))
                        {
                            Message t;

                            switch (update.Message.Chat.Type)
                            {
                                case ChatType.Private:
                                case ChatType.Channel:
                                case ChatType.Group:
                                case ChatType.Supergroup:
                                    {
                                        if (comandos.Contains(update.Message.Text.ToLower()))
                                        {
                                            Console.WriteLine("Recebido: " + update.Message.Text);

                                            await Bot.SendChatAction(update.Message.Chat.Id, ChatAction.Typing);
                                            await Task.Delay(2000);
                                        }

                                        #region Usuários

                                        if (!string.IsNullOrEmpty(update.Message.Chat.FirstName) && update.Message.Chat.FirstName.ToLower().StartsWith("raul"))
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Pesou!");
                                        }

                                        #endregion

                                        #region Equals

                                        else if (update.Message.Text.ToLower().Equals("danado"))
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Danadoooooooooo!");
                                        }
                                        else if (update.Message.Text.ToLower().Equals("danada"))
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Danadaaaaaaaaaa!");
                                        }

                                        #endregion

                                        #region Commands

                                        else if (update.Message.Text == "/command1")
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Oi Danado(a)!");
                                        }
                                        else if (update.Message.Text == "/command2")
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Beijos!");
                                        }
                                        else if (update.Message.Text == "/command3")
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Eu? Eu o que Danado(a)?");
                                        }
                                        else if (update.Message.Text == "/helpzacabot")
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, comandos);
                                        }

                                        #endregion

                                        #region Contains

                                        else if (update.Message.Text.ToLower().Contains("pesou"))
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Pesou meu irmão!");
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "E vai pesar mais ainda!");
                                        }
                                        else if (update.Message.Text.ToLower().Contains("bom dia"))
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Bom dia Danados(as)!");
                                        }
                                        else if (update.Message.Text.ToLower().Contains("doideira"))
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "KKK");
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Doideira hein?");
                                        }
                                        else if (update.Message.Text.ToLower().Contains("nudes") || update.Message.Text.ToLower().Contains(" nude"))
                                        {
                                            t = await Bot.SendTextMessage(update.Message.Chat.Id, "Mandaaaa nudessss!!!");
                                        }

                                        #endregion

                                        break;
                                    }

                                default:
                                    break;
                            }
                        }

                        offset = update.Id + 1;
                    }

                    await Task.Delay(1000);
                    Console.WriteLine("Notificando atividade.");
                }
            }
            else
                Console.WriteLine("Falha ao carregar o bot");
        }
    }
}