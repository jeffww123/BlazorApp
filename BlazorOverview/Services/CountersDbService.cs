using BlazorOverview.Models;
using BlazorOverview.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOverview.Services
{
    public class CountersDbService : ICountersService
    {
        private MyNoteDbContext _MyNoteDbContext;

        // 使用建構式注入方式，注入 MyNoteDbContext 類別執行個體，以便可以存取 SQLite 資料庫
        public CountersDbService(MyNoteDbContext myNoteDbContext)
        {
            _MyNoteDbContext = myNoteDbContext;
        }

        // 查詢所有記事紀錄
        public async Task<List<NoteCludeCount>> RetriveAsync()
        {
            var Counts = await _MyNoteDbContext.Counters.ToListAsync();
            var Notes = await _MyNoteDbContext.MyNotes.ToListAsync();

            var noteCludeCounts = Notes
                .GroupJoin(Counts,
                    note => note.Id,
                    count => count.NoteId ?? 0,  // 確保 count.NoteId 不為 null
                    (note, noteCounts) => new NoteCludeCount
                    {
                        Id = note.Id,
                        Title = note.Title,
                        Count = noteCounts.Any() ? noteCounts.Sum(c => c.Count) : 0  // 確保 Count 不為 null
                    })
                .ToList();

            return noteCludeCounts;
        }
        // 修改記事紀錄
        public async Task UpdateAsync(Counters origData, Counters counters)
        {
            var fooItem = await _MyNoteDbContext.Counters.FirstOrDefaultAsync(x => x.NoteId == origData.NoteId);
            var NoteItem = await _MyNoteDbContext.MyNotes.FirstOrDefaultAsync(y => y.Id == origData.NoteId);
            // 兩邊資料庫都有正常更新
            if (fooItem != null && NoteItem != null)
            {
                fooItem.Count = counters.Count;
                await _MyNoteDbContext.SaveChangesAsync();
            }
            // Notes有，Count沒有，要新增
            else if (fooItem == null && NoteItem != null)
            {
                // 新增Count 資料
                var newCount = new Counters
                {
                    ID = Guid.NewGuid().ToString(),
                    NoteId = NoteItem.Id,
                    Count = counters.Count
                };
                await _MyNoteDbContext.Counters.AddAsync(newCount);
                await _MyNoteDbContext.SaveChangesAsync();
            }
        }
    }
}
