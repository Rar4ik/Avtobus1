using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Avtobus1.BLL.Interfaces;
using Avtobus1.Models.Entity;
using Avtobus1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Avtobus1.Controller
{
    public class UrlListController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUrlDisplayer _urlDisplayer;
        private readonly IMapper _mapper;

        public UrlListController(IUrlDisplayer urlDisplayer, IMapper mapper)
        {
            _urlDisplayer = urlDisplayer;
            _mapper = mapper;
        }

        public IActionResult Index() => View(_urlDisplayer.GetAll());

        public IActionResult Create()
        {
            return View(new ViewModelCreateUrl());
        }

        [HttpPost]
        public IActionResult Create(ViewModelCreateUrl newLink)
        {
            if (newLink is null)
                throw new ArgumentNullException(nameof(newLink));
            
            if (!ModelState.IsValid)
                return View(newLink);

            var id = newLink.Id;

            if (_urlDisplayer.GetById(id) == null)
                _urlDisplayer.Add(newLink);
            else
                throw new Exception("Эта ссылка уже была добавлена");
            
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var delLink = _urlDisplayer.GetById(id);
            if (delLink is null)
            {
                return NotFound();
            }

            return View(delLink);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _urlDisplayer.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Redirect(int id)
        {
            _urlDisplayer.Increment(id);
            var link =_urlDisplayer.GetById(id);
            return Redirect(link.LongUrl);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 0)
                return BadRequest();
            var link = _urlDisplayer.GetById(id);
            if (link is null)
                return NotFound();
            return View(link);
        }

        [HttpPost]
        public IActionResult Edit(int id, UrlEntity urlEntity)
        {
            if (urlEntity is null)
                throw new ArgumentOutOfRangeException(nameof(urlEntity));
            if (!ModelState.IsValid)
                return View(urlEntity);
            _urlDisplayer.Edit(id, urlEntity);
            return RedirectToAction("Index");
        }
    }
}
