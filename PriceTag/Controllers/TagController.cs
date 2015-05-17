using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PriceTag.Service;
using Repository.Pattern.UnitOfWork;
using PriceTag.Models;
using PriceTag.Entities.Models;
using Repository.Pattern.Infrastructure;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Web.Helpers;

namespace PriceTag.Controllers
{
    public class TagController : BaseController
    {
        private readonly ITagService _tagService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public TagController()
        {
            _unitOfWorkAsync = DependencyResolver.Current.GetService<IUnitOfWorkAsync>();
            _tagService = DependencyResolver.Current.GetService<ITagService>();
        }

        // GET: Tag
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Tag/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            ViewBag.UserName = User.Identity.GetUserName();

            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Create(CreateTagVM model)
        {
            if (ModelState.IsValid)
            {
                Tag tag = new Tag
                {
                    Title = model.Title,
                    UserId = User.Identity.GetUserId(),
                    Price = 0,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };


                tag.ObjectState = ObjectState.Added;
                _tagService.Insert(tag);

                try
                {
                    _unitOfWorkAsync.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw;
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload(int id)
        {
            var image = WebImage.GetImageFromRequest();
            //var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), image.FileName);

            //// Delete File - not working
            //var fi = new FileInfo(physicalPath);
            //fi.Delete();

            return Json("");
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
