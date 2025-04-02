using BlazorOverview.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorOverview.Services
{
    public interface ICountersService
    {
        Task<List<NoteCludeCount>> RetriveAsync();
        Task UpdateAsync(Counters origMyNote, Counters myNote);
    }
}