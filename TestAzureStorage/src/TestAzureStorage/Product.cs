using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAzureStorage
{
    public class Product : TableEntity
    {
        public Product()
        {

        }

        public Product(int categoryId, Guid id)
        {
            this.PartitionKey = categoryId.ToString();
            this.CategoryId = categoryId;
            this.RowKey = Id.ToString();
            this.Id = Id;
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
