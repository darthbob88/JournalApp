using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalApp.Repository
{
 public class JournalDBSettings : IJournalDBSettings
    {
        public string JournalCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IJournalDBSettings
    {
        string JournalCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
