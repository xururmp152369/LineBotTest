using LineBotTest1.FlexMessages;
using LineBotTest1.Interfaces;
using NetCoreLineBotSDK;
using NetCoreLineBotSDK.Enums;
using NetCoreLineBotSDK.Interfaces;
using NetCoreLineBotSDK.Models;
using NetCoreLineBotSDK.Models.Action;
using NetCoreLineBotSDK.Models.LineObject;
using NetCoreLineBotSDK.Models.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                var locationname = "SweetHome";
                var msg = ev.message.Text;
                var Responsemsg = new TextMessage() {Text="" };
                switch (msg)
                {
                    case "help":
                        Responsemsg.Text = "以下是我們目前擁有的功能哦 PuiPui！";
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, new List<IMessage> { Responsemsg, HelpMessage_fun() });
                        break;
                    case "宣傳照": //發送一個圖片 可將圖片傳至imgur製造網址
                        Responsemsg.Text = "以下是我們這次的宣傳照 PuiPui！";
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, new List<IMessage> { Responsemsg, ImageMessage_fun() });
                        break;
                    case "宣傳照2": //發送一個圖片 可將圖片傳至imgur製造網址
                        //Responsemsg.Text = "以下是我們這次的宣傳照強化版 PuiPui！";
                        await lineMessageUtility.ReplyMessageByJsonAsync(ev.replyToken,Flexmsg_json("Invite"));
                        break;
                    case "預告片": //發送一個影片 可將影片傳至imgur製造網址
                        Responsemsg.Text = "以下是我們這次的影片 PuiPui！";
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, new List<IMessage> { Responsemsg, VideoMessage_fun() });
                        break;
                    case "舉辦地點": //googlemap取得經緯度
                        Responsemsg.Text = $"以下是我們{locationname}的地點資訊 PuiPui！";                        
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, new List<IMessage> { Responsemsg, LocationMessage_fun(locationname) });
                        break;
                    case "角色輪播": //輪播圖片
                        //Responsemsg.Text = "今天登場的是以下三隻 PuiPui！";
                        await lineMessageUtility.ReplyTemplateMessageAsync(ev.replyToken, new ImageCarouselTemplate {Columns = ImageCarouseColumn_fun() });
                        break;
                    case "交通資訊": //輪播資訊
                        //Responsemsg.Text = $"如何到我們{locationname}呢 PuiPui！";
                        await lineMessageUtility.ReplyTemplateMessageAsync(ev.replyToken, new CarouselTemplate { Columns = CarouselColumnMultiple_fun() });
                        break;
                    case "從高鐵":
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, "至高鐵左營站下車後轉搭乘捷運至草衙捷運站，步行約30min PuiPui" + Environment.NewLine + "著實健康d(`･∀･)b");
                        break;                                                      
                    case "從捷運":                                                  
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, "搭乘至草衙捷運站，步行約30min PuiPui" + Environment.NewLine + "真低健康d(`･∀･)b");
                        break;                                                      
                    case "從機場":                                                  
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, "搭乘至小港國際機場，車程約10min PuiPui" + Environment.NewLine + "好迅速(*´∀`)~♥");
                        break;                                                      
                    case "停車資訊":                                                
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, "停車場為戶外停車場，只有兩格 PuiPui" + Environment.NewLine + "哈哈σ ﾟ∀ ﾟ) ﾟ∀ﾟ)σ");
                        break;                                                      
                    case "開車提醒":                                                
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, "切記開車不喝酒，如要喝酒請搭乘Taxi前來，安全第一 PuiPui" + Environment.NewLine + "讚(ゝ∀･)b");
                        break;                                                      
                    default:                                                        
                        await lineMessageUtility.ReplyMessageAsync(ev.replyToken, new List<IMessage> { HelpMessageQuickReply_fun() });
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

        protected override async Task OnUnPostbackAsync(LineEvent ev)
        {
            var locationname = "SweetHome";
            var msg = ev.postback.data;
            switch(msg)
            {
                case "舉辦地點":
                    await lineMessageUtility.ReplyMessageAsync(ev.replyToken, new List<IMessage> {LocationMessage_fun(locationname) });
                    break;
                default:
                    await lineMessageUtility.ReplyMessageAsync(ev.replyToken, "");
                    break;
            }
        }

        //求救訊息 回傳目前有的指令
        private static TextMessage HelpMessage_fun()
        {
            var text = new TextMessage()
            {
                Text = "宣傳照" + Environment.NewLine + "宣傳照2" + Environment.NewLine + "預告片" + Environment.NewLine + "舉辦地點" + Environment.NewLine + "角色輪播" + Environment.NewLine + "交通資訊"
            };
            return text;
        }

        //快速回覆 下方會有小圈圈提示
        private static TextMessage HelpMessageQuickReply_fun()
        {
            var unknow = new TextMessage() { Text = $@"若有疑問可以參考下列快速鍵唷！【不支援電腦版】" };
            unknow.QuickReply = new QuickReply();
            unknow.QuickReply.Items.Add(new QuickReplyItem()
            {
                action = new MessageAction("help")
            });
            return unknow;
        }

        //圖片訊息 1.為圖片 2.為預覽圖片
        private static ImageMessage ImageMessage_fun()
        {
            var image = new ImageMessage()
            {
                OriginalContentUrl = "https://i.imgur.com/It0CoBV.jpg",
                PreviewImageUrl = "https://i.imgur.com/It0CoBV.jpg"
            };
            return image;
        }

        //影片訊息 1.為影片 2.為預覽圖片
        private static VideoMessage VideoMessage_fun()
        {
            var video = new VideoMessage()
            {
                OriginalContentUrl = "https://i.imgur.com/n8QsXTk.mp4",
                PreviewImageUrl = "https://i.imgur.com/oLvTjtu.png"
            };
            return video;
        }

        //位置訊息 1.標題 2.地址 3.&4.經緯度
        private static LocationMessage LocationMessage_fun(string locationname)
        {
            var locationMessage = new LocationMessage()
            {
                Title = $"Liu Home - {locationname}",
                Address = "高雄市鳳山區南成里三成街146號",
                Latitude = Convert.ToDecimal(22.587022571295545),
                Longitude = Convert.ToDecimal(120.34902801730752)
            };
            return locationMessage;
        }

        //圖片輪播訊息 1.圖片 2.圖片動作(有7種)
        private static List<ImageCarouselColumnAction> ImageCarouseColumn_fun()
        {
            var columns = new List<ImageCarouselColumnAction>();
            var photos = new List<string>()
            {
                "https://i.imgur.com/B6ks4BR.jpg",
                "https://i.imgur.com/Qn1FcdF.jpg",
                "https://i.imgur.com/i2hacvk.jpg",
                "https://i.imgur.com/OnpzUhP.jpg",
                "https://i.imgur.com/rJiXCH2.jpg",
                "https://i.imgur.com/l2dvnmg.jpg"
            };
            var selected = photos.OrderBy(x => new Random().Next()).Take(3); //隨機取三張圖片
            foreach (var photo in selected)
            {
                columns.Add(new ImageCarouselColumnAction()
                {
                    ImageUrl = photo,
                    Action = new UriAction(photo, "learn more")
                });
            };
            return columns;
        }


        private static List<CarouselColumnMultipleAction> CarouselColumnMultiple_fun()
        {
            var columns = new List<CarouselColumnMultipleAction>
            {
                new CarouselColumnMultipleAction()
                {
                    ThumbnailImageUrl = "https://imgur.com/2u6F3dV.jpg",
                    Title = "搭乘大眾運輸工具",
                    Text = "簡單方便又能喝酒 Pui",
                    Actions = new List<IAction>()
                {
                    new MessageAction("從高鐵"),
                    new MessageAction("從捷運"),
                    new MessageAction("從機場")
                }
                },
                new CarouselColumnMultipleAction()
                {
                    ThumbnailImageUrl = "https://imgur.com/FGitN5M.jpg",
                    Title = "自行開車前往",
                    Text = "開車不喝酒，喝酒叫Taxi Pui",
                    Actions = new List<IAction>()
                {
                    new PostbackAction("舉辦地點", "如何前往"),
                    new MessageAction("停車資訊"),
                    new MessageAction("開車提醒")
                }
                }
            };
            return columns;
        }

        private static string Flexmsg_json(string Filename)
        {
            switch (Filename)
            {
                case "Invite":
                    Invite jsontext = new();
                    return jsontext.Action();
                default:
                    return null;
            }
        }
    }
}
