using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avtobus1.Models.Entity;

namespace Avtobus1.BLL.Infrustructure
{
    public class UrlComplete
    {
        private readonly UrlEntity _url;

        public UrlComplete(UrlEntity url)
        {
            _url = url;
        }

        public UrlEntity GetWholeUrl()
        {
            _url.CreateDate = DateTime.Now;
            return _url;
        }
    }
}
