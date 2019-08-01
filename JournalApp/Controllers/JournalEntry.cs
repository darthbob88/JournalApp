using System;

namespace JournalApp.Controllers
{
    public struct JournalEntry
    {
        public DateTime timeStamp { get; set; }
        public string actualText { get; set; }
    }
}