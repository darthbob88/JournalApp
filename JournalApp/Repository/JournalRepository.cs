using JournalApp.Model;
using JournalApp.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace JournalApp
{
    public class JournalRepository
    {
        private readonly IMongoCollection<JournalModel> _journals;
        private string userName = "journaldb";
        private string host = "journaldb.documents.azure.com";
        private string password = "irUIB6JkHQUdurMNkNIrzulOvb6PU8e9ccXngQdhJN9KRBLlE4YiFNAu31Vo9VHVSjjZCvdPI0B7MoFR6UE1aQ==";

        public JournalRepository(IJournalDBSettings DBsettings)
        {
            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);

            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity("JournalApp", userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase("JournalApp");

            _journals = database.GetCollection<JournalModel>("JournalEntries");
        }

        public List<JournalModel> Get() =>
            _journals.Find(journal => true).ToList();

        public JournalModel Get(string id) =>
            _journals.Find<JournalModel>(journal => journal.Id == id).FirstOrDefault();

        public JournalModel Create(JournalModel journal)
        {
            _journals.InsertOne(journal);
            return journal;
        }

        public void Update(string id, JournalModel journalIn) =>
            _journals.ReplaceOne(journal => journal.Id == id, journalIn);
        public void Append(string id, JournalEntry newEntry)
        {
            var filter = Builders<JournalModel>.Filter.Eq("id", id);
            var update = Builders<JournalModel>.Update.Push("entries", newEntry);
            var result = _journals.UpdateOne(filter, update);
        }

        public void Remove(JournalModel journalIn) =>
            _journals.DeleteOne(journal => journal.Id == journalIn.Id);

        public void Remove(string id) =>
            _journals.DeleteOne(journal => journal.Id == id);
    }
}
