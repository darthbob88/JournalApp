using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JournalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        // GET: api/Journal
        [HttpGet("[action]")]
        public IEnumerable<JournalEntry> GetAllEntries()
        {
            JournalEntry entry1 = new JournalEntry(){ timeStamp= new DateTime(2019, 1, 1), actualText= "asdasdfsdfafk" };
            return new JournalEntry[] {  entry1};
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
