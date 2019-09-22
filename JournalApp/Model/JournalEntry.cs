using System;

namespace JournalApp.Model
{
    public struct JournalEntry
    {
        public DateTime timeStamp { get; set; }
        public string actualText { get; set; }
    }
}