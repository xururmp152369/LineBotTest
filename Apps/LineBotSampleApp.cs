using LineBotTest1.Interfaces;
using NetCoreLineBotSDK;
using NetCoreLineBotSDK.Enums;
using NetCoreLineBotSDK.Interfaces;
using NetCoreLineBotSDK.Models;
using NetCoreLineBotSDK.Models.LineObject;
using NetCoreLineBotSDK.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineBotTest1.Apps
{
    public class LineBotSampleApp : LineBotApp
    {
        private readonly ILineMessageUtility lineMessageUtility;
        public LineBotSampleApp(ILineMessageUtility _lineMessageUtility) : base(_lineMessageUtility)
        {
            lineMessageUtility = _lineMessageUtility;
        }

        protected override async Task OnMessageAsync(LineEvent ev)
        {
            if (ev.message.Type.Equals(LineMessageType.text))
            {
                var msg = ev.message.Text;
                switch (msg)
                {
                    case "電子喜帖":
                        var imageMessage = new ImageMessage()
                        {
                            OriginalContentUrl = "https://i.imgur.com/Dfp2Smy.jpg",
                            PreviewImageUrl = "https://i.imgur.com/Dfp2Smy.jpg"
                        };
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, new List<IMessage> {imageMessage});
                        break;
                    case "前導影片":
                        var videoMessage = new VideoMessage()
                        {
                            OriginalContentUrl = "https://i.imgur.com/n8QsXTk.mp4",
                            PreviewImageUrl = "https://i.imgur.com/oLvTjtu.png"
                        };
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, new List<IMessage> { videoMessage });
                        break;
                    default:
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, $"{ev.message.Text} PuiPui OoO");
                        break;
                }
            }
        }

        protected override async Task OnFollowAsync(LineEvent ev)
        {
            var user = await lineMessageUtility.GetUserProfile(ev.source.userId);
            var replymessage = new TextMessage() { Text = $@"Hi {user.displayName}, 感謝您加入婚禮小助理PuiPui！"};
            var replysticker = new StickerMessage() { PackageId = "11537", StickerId = "52002734"};
            await lineMessageUtility.ReplyMessageAsync(ev.replyToken, new List<IMessage>
            {
                replymessage,
                replysticker
            });
        }
    }
}
