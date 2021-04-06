using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineBotTest1.Data
{
    public class Users : TableEntity
    {
        public Users()
        {
        }

        public Users(string _partitionKey, string _rowKey)
        {
            PartitionKey = _partitionKey;
            RowKey = _rowKey;
        }

        public string LineUserId { get; set; }
        public string LineUserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int AttendEvent { get; set; }
        public int InviteType { get; set; }
        public int Relation { get; set; }
        public int AttendPeople { get; set; }
        public int Child { get; set; }
        public bool Speical { get; set; }
        public string Message { get; set; }
    }
}
