using System;
using System.ComponentModel.DataAnnotations;

namespace Avtobus1.Models.Entity
{
    public class UrlEntity
    {
        public int Id { get; set; }
        [Url]
        public string LongUrl { get; set; }
        [Url]
        public string ShortUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public int Counter { get; set; }
    }
}
