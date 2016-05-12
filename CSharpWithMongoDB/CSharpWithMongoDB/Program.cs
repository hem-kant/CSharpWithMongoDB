using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace CSharpWithMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            CallMain(args).Wait();
            Console.ReadLine();
        }
        static async Task CallMain(string[] args)
        {
            var conString = "mongodb://localhost:27017";
            var Client = new MongoClient(conString);
            var DB = Client.GetDatabase("MyFirstMongoDatabase");
            var collection = DB.GetCollection<BsonDocument>("MyFirstMongoDatabase");

            //Method 1
            using (var cursor = await collection.Find(new BsonDocument()).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var doc in cursor.Current)
                    {
                        Console.WriteLine(doc);
                    }
                }
            }

            // Method 2
            var list = await collection.Find(new BsonDocument()).ToListAsync();
            foreach (var dox in list)
            {
                Console.WriteLine(dox);
            }

            // Method 3
            await collection.Find(new BsonDocument()).ForEachAsync(X => Console.WriteLine(X));

        }
    }
}
