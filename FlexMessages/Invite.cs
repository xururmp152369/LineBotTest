using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineBotTest1.FlexMessages
{
    public class Invite
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public string  Action()
        {
            var jsonstring = @"{
  ""type"": ""bubble"",
  ""hero"": {
                ""type"": ""image"",
    ""url"": ""https://i.imgur.com/6VqALsy.jpg"",
    ""size"": ""full"",
    ""aspectRatio"": ""20:13"",
    ""aspectMode"": ""cover"",
    ""action"": {
                    ""type"": ""uri"",
      ""uri"": ""http://linecorp.com/""
    }
            },
  ""body"": {
                ""type"": ""box"",
    ""layout"": ""vertical"",
    ""contents"": [
      {
                    ""type"": ""text"",
        ""text"": ""GuineaPig Home"",
        ""weight"": ""bold"",
        ""size"": ""xl""
      },
      {
                    ""type"": ""box"",
        ""layout"": ""baseline"",
        ""margin"": ""md"",
        ""contents"": [
          {
                        ""type"": ""text"",
            ""text"": ""Here is our warm home !"",
            ""size"": ""sm"",
            ""color"": ""#999999""
          }
        ]
      },
      {
                    ""type"": ""box"",
        ""layout"": ""vertical"",
        ""margin"": ""md"",
        ""contents"": [
          {
                        ""type"": ""box"",
            ""layout"": ""baseline"",
            ""spacing"": ""sm"",
            ""contents"": [
              {
                            ""type"": ""icon"",
                ""url"": ""https://i.imgur.com/AOUhwVH.png"",
                ""size"": ""sm"",
                ""position"": ""absolute""
              },
              {
                            ""type"": ""text"",
                ""text"": ""2021/03/26"",
                ""size"": ""sm"",
                ""margin"": ""xxl"",
                ""weight"": ""bold""
              },
              {
                            ""type"": ""text"",
                ""text"": ""日期"",
                ""wrap"": false,
                ""color"": ""#666666"",
                ""size"": ""sm"",
                ""align"": ""end"",
                ""flex"": 0
              }
            ]
          },
          {
                        ""type"": ""box"",
            ""layout"": ""baseline"",
            ""spacing"": ""sm"",
            ""contents"": [
              {
                            ""type"": ""icon"",
                ""url"": ""https://i.imgur.com/4udeJ3b.png"",
                ""size"": ""sm"",
                ""position"": ""absolute""
              },
              {
                            ""type"": ""text"",
                ""text"": ""08:30"",
                ""size"": ""sm"",
                ""margin"": ""xxl"",
                ""weight"": ""bold""
              },
              {
                            ""type"": ""text"",
                ""text"": ""時間"",
                ""wrap"": true,
                ""color"": ""#666666"",
                ""size"": ""sm"",
                ""align"": ""end"",
                ""flex"": 0
              }
            ]
          },
          {
                        ""type"": ""box"",
            ""layout"": ""baseline"",
            ""contents"": [
              {
                            ""type"": ""icon"",
                ""url"": ""https://i.imgur.com/BOKgrLV.png"",
                ""size"": ""sm"",
                ""position"": ""absolute""
              },
              {
                            ""type"": ""text"",
                ""size"": ""sm"",
                ""text"": ""南成里三成街146號豪宅"",
                ""margin"": ""xxl"",
                ""weight"": ""bold""
              },
              {
                            ""type"": ""text"",
                ""text"": ""地點"",
                ""size"": ""sm"",
                ""color"": ""#666666"",
                ""align"": ""end"",
                ""wrap"": true,
                ""flex"": 0
              }
            ]
          },
          {
                        ""type"": ""separator""
          },
          {
                        ""type"": ""text"",
            ""size"": ""sm"",
            ""text"": ""雖然這機器人貴為天竺鼠車車，但其實沒辦點毛關係，先做來放著，日後再來修改，OK吧OK吧?"",
            ""wrap"": true
          }
        ],
        ""spacing"": ""sm""
      }
    ]
  },
  ""footer"": {
                ""type"": ""box"",
    ""layout"": ""vertical"",
    ""spacing"": ""sm"",
    ""contents"": [
      {
                    ""type"": ""spacer"",
        ""size"": ""sm""
      },
      {
                    ""type"": ""button"",
        ""action"": {
                        ""type"": ""uri"",
          ""label"": ""Add Calender"",
          ""uri"": ""https://calendar.google.com/calendar/u/0/r/eventedit/NTBybWdsc21zcWR1bTQ4dmhmczZybHJwNWQgeHVydXJtcDE1MjM2OUBt?hl=zh_tw&pli=1""
        },
        ""style"": ""secondary""
      }
    ],
    ""flex"": 0
  }
        }";
            return jsonstring;
        }
    }
}
