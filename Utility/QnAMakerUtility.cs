using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LineBotTest1.Utility
{
    public class QnAMakerUtility
    {
        public async Task<QnAMakerAnswer> Get(string question, int? scope = null)
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), "https://qna-maker-bot.azurewebsites.net/qnamaker/knowledgebases/7074482e-d7a7-4be4-b99a-23fc59ffc0f5/generateAnswer");
            request.Headers.TryAddWithoutValidation("Authorization", "EndpointKey e13d371e-ed12-412a-be76-023e242e4eee");
            request.Content = new StringContent("{'question':'" + question + "'}");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            var qnaResponse = JsonConvert.DeserializeObject<QnAMakerResponse>(result);

            // 取得分數最高的回答
            var answers = qnaResponse.Answers;

            // 選擇性參數，大於多少分數才回傳，這樣看起來會比較準
            if (scope.HasValue)
            {
                answers = answers.Where(c => c.Score >= scope.Value);
            }
            var answer = answers.OrderByDescending(c => c.Score).FirstOrDefault();

            // 無法辨識
            if (answer == null || answer.Id == -1) return null;
            return answer;
        }
    }

    public class QnAMakerResponse
    {
        public IEnumerable<QnAMakerAnswer> Answers { get; set; }
        public bool ActiveLearningEnabled { get; set; }
    }

    public class QnAMakerAnswer
    {
        public object[] Questions { get; set; }
        public string Answer { get; set; }
        public float Score { get; set; }
        public int Id { get; set; }
        public object[] Metadata { get; set; }
    }
}
