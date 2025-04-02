using BlazorOverview.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOverview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountersController : ControllerBase
    {
        public MyNoteDbContext MyNoteDbContext { get; }

        public CountersController(MyNoteDbContext myNoteDbContext)
        {
            MyNoteDbContext = myNoteDbContext;
        }

        [HttpGet]
        public IEnumerable<Counters> Get()
        {
            return MyNoteDbContext.Counters.ToList();
        }

        [HttpGet("{id}", Name = "CountersGet")]
        public Counters Get(int id)
        {
            return MyNoteDbContext.Counters.FirstOrDefault(x => x.NoteId == id);
        }

        //[HttpPost]
        //public void Post([FromBody] Counters counters)
        //{
        //    MyNoteDbContext.Counters.Add(counters);
        //    MyNoteDbContext.SaveChanges();
        //}

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Counters counter)
        {
            Counters NoteItem = MyNoteDbContext.Counters.FirstOrDefault(x => x.NoteId == id);
            NoteItem.Count = counter.Count;
            MyNoteDbContext.SaveChanges();
        }

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    MyNoteDbContext.MyNotes.Remove(MyNoteDbContext.MyNotes.FirstOrDefault(x => x.Id == id));
        //    MyNoteDbContext.SaveChanges();
        //}
    }
}
