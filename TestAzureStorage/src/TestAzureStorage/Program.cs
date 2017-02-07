using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAzureStorage
{
    public class Program
    {
        public async static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorageAccount=True");

            var tableClient = storageAccount.CreateCloudTableClient();    //oggetto con tutti i metodi per lavorare con il table storage

            var productTable = tableClient.GetTableReference("itsproducts");  //il nome deve essere in minuscolo e senza spazi o caratteri particolari


            //verifico se la tabella esiste
            //altrimenti la creo
            await productTable.CreateIfNotExistsAsync();
        }
    }
}
