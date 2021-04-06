using LineBotTest1.Data;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineBotTest1.Utility
{
    public class AzureTableUtility
    {
        public static async Task<CloudTable>  CreateTableAsync(string tableName)
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=linebotdb;AccountKey=1NdHRW6AZQCciP6LcBJgeh2zHT7KtRcB9xkKg91qQd6W8kyBp8h0n3klcuZWKmdMLQ5pkB0cW67sbBHqhq1uPQ==;TableEndpoint=https://linebotdb.table.cosmos.azure.com:443/;";
            // Storage 的 連線字串，來源可以用appSetting.json搭配 IOption 注入
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);
            // 新增 Azure Table
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            // 不存在則會新增，確認 Table 已存在可使用 await table.CreateIfNotExistsAsync()

            CloudTable table = tableClient.GetTableReference(tableName);
            if (await table.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Created Table named: {0}", tableName);
            }
            else
            {
                Console.WriteLine("Table {0} already exists", tableName);
            }
            return table;
        }

        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            return storageAccount;
        }

        public static async Task<Users> InsertOrMergeEntityAsync(CloudTable table, Users entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                // 新增或更新
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                Users insertedCustomer = result.Result as Users;

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }
                return insertedCustomer;
            }
            catch (StorageException)
            {
                throw;
            }
        }
    }
}
