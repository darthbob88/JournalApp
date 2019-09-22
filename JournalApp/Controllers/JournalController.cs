using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JournalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly JournalRepository _journalRepo;

        public JournalController(JournalRepository journalRepo)
        {
            _journalRepo = journalRepo;
        }
        // GET: api/Journal
        [HttpGet("[action]")]
        public IEnumerable<JournalEntry> GetAllEntries()
        {
            var journals = _journalRepo.Get();
            var journalEntries = journals.First().entries.ToList() ;
            //TODO: Pull this from the database.
            journalEntries.Add( new JournalEntry(){ timeStamp= new DateTime(2019, 1, 1), actualText= "asdasdfsdfafk" });
            return journalEntries;
        }

        // GET: api/Journal/5
        [HttpGet("[action]")]
        public string GetPrompt()
        {
            return "How did your day go? Anything you want to share?";
        }

        // POST: api/Journal
        [HttpPost]
        public void SetPrompt([FromBody] string value)
        {
        }

        // POST: api/Journal
        [HttpPost]
        public void AddEntry([FromBody] string value)
        {
        }
    }
}
