using System;
using Avtobus1.Models.ViewModels;

namespace Avtobus1.BLL.Infrustructure
{
    public class UrlConvertor
    {
        private readonly ViewModelCreateUrl _url;

        public UrlConvertor(ViewModelCreateUrl url)
        {
            _url = url;
        }

        public ViewModelCreateUrl GetUrls()
        {
            string shortened = GetShortUrl(_url.LongUrl);
            _url.ShortUrl = $"https://ssylki/{shortened}/";
            return _url;
        }
        
        private string GetShortUrl(string longUrl)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[4];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new string(stringChars);
            return finalString;
        }
    }
}
