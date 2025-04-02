using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorOverview.Models
{
    public class Counters : ICloneable
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("NoteId")]
        public int? NoteId { get; set; }
        [JsonPropertyName("Count")]
        public int Count { get; set; }
        // 使用淺層複製的方式，產生出相同屬性值的物件
        public Counters Clone()
        {
            return ((ICloneable)this).Clone() as Counters;
        }
        // 這裡為使用明確方式來實作 ICloneable 介面
        object ICloneable.Clone()
        {
           return this.MemberwiseClone();
        }
    }

    public class NoteCludeCount : MyNote
    {
        public int Count { get; set; }
    }
}