using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAzureStorage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run().Wait();


        }

        private static async Task Run()
        {
            try
            {
                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorageAccount=True");

                var storageAccount = CloudStorageAccount.DevelopmentStorageAccount

            var tableClient = storageAccount.CreateCloudTableClient();    //oggetto con tutti i metodi per lavorare con il table storage

                var productTable = tableClient.GetTableReference("itsproducts");  //il nome deve essere in minuscolo e senza spazi o caratteri particolari


                //verifico se la tabella esiste
                //altrimenti la creo
                await productTable.CreateIfNotExistsAsync();

                var product = new Product(1, Guid.NewGuid())
                {
                    Code = "ABC123",
                    Name = $"Prodotto {DateTime.Now.ToString()}",
                };

                //var action = TableOperation.Insert(product);

                await productTable.ExecuteAsync(TableOperation.Insert(product));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }
    }
