using System.ComponentModel.DataAnnotations;

namespace Avtobus1.Models.ViewModels
{
    public class ViewModelCreateUrl
    {
        public int Id { get; set; }
        [Required]
        [Url]
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
    }
}
