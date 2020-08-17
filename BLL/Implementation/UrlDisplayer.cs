using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Avtobus1.BLL.Infrustructure;
using Avtobus1.BLL.Interfaces;
using Avtobus1.DAL.Context;
using Avtobus1.Models.Entity;
using Avtobus1.Models.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Avtobus1.BLL.Implementation
{
    public class UrlDisplayer : IUrlDisplayer
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public UrlDisplayer(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<UrlEntity> GetAll() => _db.Set<UrlEntity>().AsNoTracking().ToList();
        public UrlEntity GetById(int id) => _db.stringUrl.Find(id);

        public void Add(ViewModelCreateUrl ViewUrlEntity)
        {
            UrlConvertor convertor = new UrlConvertor(ViewUrlEntity);
            convertor.GetUrls();

            var urlEntity = _mapper.Map<UrlEntity>(ViewUrlEntity);

            UrlComplete dateLink = new UrlComplete(urlEntity);
            var urlComplete = dateLink.GetWholeUrl();

            urlComplete.Id = _db.stringUrl.Count() == 0 ? 1 : _db.stringUrl.Max(c => c.Id) + 1;

            _db.stringUrl.Add(urlComplete);
            SaveChanges();
        }

        public void Edit(int id, UrlEntity urlEntity)
        {
            var dbUrl = _db.stringUrl.FirstOrDefault(c => c.Id == id);
            dbUrl.ShortUrl = urlEntity.ShortUrl;
            dbUrl.LongUrl = urlEntity.LongUrl;
            dbUrl.CreateDate = DateTime.Now;
            _db.stringUrl.Update(dbUrl);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var dbUrl = _db.stringUrl.FirstOrDefault(c => c.Id == id);

            if (dbUrl is null) return;
            _db.stringUrl.Remove(dbUrl);
            SaveChanges();

        }

        public void Increment(int id)
        {
            var currentLink = _db.stringUrl.FirstOrDefault(c => c.Id == id);
            if (currentLink is null) return;
            currentLink.Counter++;
            SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
