using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace DataServices.Models
{
    public class BaseDataObject
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
