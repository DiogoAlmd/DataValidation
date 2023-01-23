using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Data.SqlClient;

namespace dataValidation
{
    class Program
    {
        static void Main(string[] args) {

            Connection connection = new Connection();
            connection.StartConnection();
            string databaseName = "AllTenants";
            string collectionName = "Tenants";

            string connectionString = new Connection().connectionString;
            var cliente = new MongoClient(connectionString);
            var db = cliente.GetDatabase(databaseName);
            var collection = db.GetCollection<BsonDocument>(collectionName);
            var result = collection.Find(new BsonDocument()).FirstOrDefault();
            var result2 = result.ToString();

            FilterData lookfordata = new FilterData();
            Console.WriteLine(lookfordata.seekData(result2));
        }

        public class Connection
        {
            public string connectionString = "mongodb://localhost:27017";
            
            public void StartConnection() {
                Console.WriteLine("Mongo DB Connection");
                Console.WriteLine("====================================================");
                Console.WriteLine("Initializaing connection");

                Console.WriteLine("Creating Client..........");
                MongoClient client = null;
                try {
                    client = new MongoClient(connectionString);
                    Console.WriteLine("Client Created Successfuly........");
                    Console.WriteLine("Client: " + client.ToString());
                }
                catch (Exception ex) {
                    Console.WriteLine("Failed to Create Client.......");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public class FilterData
        {
            public bool seekData(string alldata) {
                if (alldata.Contains("something") && alldata.Contains("WhateverCollection")) {
                    return true;
                }
                return false;
            }
        }
    }
}
