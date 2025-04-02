using BlazorOverview.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOverview.Services
{
    public class CountersService : ICountersService
    {
        // 記事清單的私有欄位物件
        List<Counters> Counters { get; set; }
        List<NoteCludeCount> NoteCludeCounts { get; set; }
        public CountersService()
        {
        }

        // 查詢所有記事紀錄
        public Task<List<NoteCludeCount>> RetriveAsync()
        {
            // 因為這裡都是使用同步呼叫，所以需要回傳一個工作物件
            return Task.FromResult(NoteCludeCounts);
        }
        // 修改記事紀錄
        public Task UpdateAsync(Counters origMyNote, Counters myNote)
        {
            Counters.FirstOrDefault(x => x.NoteId == origMyNote.NoteId).Count = myNote.Count;
            // 因為這裡都是使用同步呼叫，所以需要回傳一個工作物件
            return Task.FromResult(0);
        }

    }
}
