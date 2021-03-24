using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineBotTest1.Interfaces
{
    public interface IReplyIntent
    {
        Task ReplyAsync(string replyToken);
    }
}
