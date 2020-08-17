using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avtobus1.Models.Entity;
using Avtobus1.Models.ViewModels;

namespace Avtobus1.BLL.Interfaces
{
   public interface IUrlDisplayer
   {
       IEnumerable<UrlEntity> GetAll();
       UrlEntity GetById(int id);
       void Add(ViewModelCreateUrl urlEntity);
       void Edit(int id, UrlEntity urlEntity);
       void Delete(int id);
       void Increment(int id);
       void SaveChanges();
   }
}
