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
    public class FacilityController : Controller
    {
        FacilityService _Service = new FacilityService();
        
        public FacilityController()
        {
            _Service = new FacilityService();
        }

        #region 取得Facility
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            FacilityListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增Facility
        public ActionResult Add()
        {
            FacilityModel entity = new FacilityModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(FacilityModel model)
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

        #region 修改Facility

        // Get: /Account/Edit/{id}
        public ActionResult Edit(string id)
        {
            FacilityModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(FacilityModel user)
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

        #region 刪除Facility,詳細Facility

        // Get : /Account/Detail/{id}
        public ActionResult Detail(string id)
        {
            FacilityModel user = _Service.Get(id);
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
