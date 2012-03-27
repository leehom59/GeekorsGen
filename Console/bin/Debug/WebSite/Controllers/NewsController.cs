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
    public class NewsController : Controller
    {
        NewsService _Service = new NewsService();
        
        public NewsController()
        {
            _Service = new NewsService();
        }

        #region 取得News
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            NewsListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增News
        public ActionResult Add()
        {
            NewsModel entity = new NewsModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(NewsModel model)
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

        #region 修改News

        // Get: /Account/Edit/{id}
        public ActionResult Edit(string id)
        {
            NewsModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(NewsModel user)
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

        #region 刪除News,詳細News

        // Get : /Account/Detail/{id}
        public ActionResult Detail(string id)
        {
            NewsModel user = _Service.Get(id);
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
