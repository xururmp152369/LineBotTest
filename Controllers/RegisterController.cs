using LineBotTest1.Data;
using LineBotTest1.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineBotTest1.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        [HttpPost]  //新增FromBody才能讀取來自postman與其他支程式的json資料
        public async Task<IActionResult> Post([FromBody]Users user)
        {
            CloudTable table = await AzureTableUtility.CreateTableAsync("Users");
            user.PartitionKey = Guid.NewGuid().ToString("N");
            user.RowKey = Guid.NewGuid().ToString("N");
            await AzureTableUtility.InsertOrMergeEntityAsync(table, user);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            CloudTable table = await AzureTableUtility.CreateTableAsync("Users");
            List<Users> linqQuery = table
            .CreateQuery<Users>()
            .ToList();

            return Ok(linqQuery);

        }
    }
}
