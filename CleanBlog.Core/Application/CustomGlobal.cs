using SlackBotMessages;
using SlackBotMessages.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace CleanBlog.Core.Application
{
    public class CustomGlobal : UmbracoApplication
    {
        protected override void OnApplicationError(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var error = HttpContext.Current.Server.GetLastError();

            try
            {
                var url = request.Url.GetLeftPart(UriPartial.Authority) + request.Url;
                var client = new SbmClient(WebConfigurationManager.AppSettings["SlackBotMessagesWebHookUrl"]);

                var message = new Message
                {
                    Username = "Website Robot",
                    IconEmoji = ":robot_face:",
                    Attachments = new List<Attachment>()
                    {
                        new Attachment()
                        {
                            Fallback = error.Message,
                            Color = "danger",
                            Fields = new List<Field>()
                            {
                                new Field()
                                {
                                    Title = Emoji.Warning + " Error",
                                    Value = error.Message
                                },
                                new Field()
                                {
                                    Title = "Stack Trace",
                                    Value = error.StackTrace
                                },
                                new Field()
                                {
                                    Title = "Url",
                                    Value = url
                                }
                            }
                        }
                    }
                };

                client.Send(message);
            }
            catch (Exception ex)
            {
                Current.Logger.Error(typeof(CustomGlobal), ex, "Unable to send slack message to notify unhandled exception");
            }

        }

    }
}
