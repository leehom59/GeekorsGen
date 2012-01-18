﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JamZooMng.Models;

namespace JamZooMng.Controllers
{
    using JamZooMng.Services;

    [Authorize]
    public class ImagesController : Controller
    {
        ImagesService _Service = new ImagesService();
        
        public ImagesController()
        {
            _Service = new ImagesService();
        }

        #region 取得會員
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            ImagesListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增會員
        public ActionResult Add()
        {
            ImagesModel entity = new ImagesModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(ImagesModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsSuccess =_Service.Create(model);

                if (IsSuccess)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("", "新增失敗"); 
                }
            }
            return View(model);
        }
        #endregion

        #region 修改會員

        // Get: /Account/Edit/{id}
        public ActionResult Edit(string id)
        {
            ImagesModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(ImagesModel user)
        {
            if (ModelState.IsValid)
            {
                bool IsSuccess = _Service.Update(user);
                if (IsSuccess)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("", "修改失敗");
                }
            }
            return View("Edit", user);
        }

        #endregion

        #region 刪除會員,詳細會員

        // Get : /Account/Detail/{id}
        public ActionResult Detail(string id)
        {
            ImagesModel user = _Service.Get(id);
            return View(user);
        }

        //Post: /Account/Delete/id
        public ActionResult Delete(string id)
        {
            bool IsDeleted = _Service.Delete(id);

            return RedirectToAction("List");
        }

        #endregion
    }
}
