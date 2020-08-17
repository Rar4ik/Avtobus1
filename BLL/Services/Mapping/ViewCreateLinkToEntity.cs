using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Avtobus1.Models.Entity;
using Avtobus1.Models.ViewModels;

namespace Avtobus1.BLL.Services.Mapping
{
    public class ViewCreateLinkToEntity : Profile
    {
        public ViewCreateLinkToEntity()
        {
            CreateMap<UrlEntity, ViewModelCreateUrl>().ReverseMap();
        }
    }
}
