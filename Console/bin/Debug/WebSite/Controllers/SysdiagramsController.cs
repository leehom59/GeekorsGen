using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JamZooMng.Models;

namespace JamZooMng.Controllers
{
    using JamZooMng.Services;

    [Authorize]
    public class SysdiagramsController : Controller
    {
        SysdiagramsService _Service = new SysdiagramsService();
        
        public SysdiagramsController()
        {
            _Service = new SysdiagramsService();
        }

        #region 取得Sysdiagrams
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            SysdiagramsListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增Sysdiagrams
        public ActionResult Add()
        {
            SysdiagramsModel entity = new SysdiagramsModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(SysdiagramsModel model)
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

        #region 修改Sysdiagrams

        // Get: /Account/Edit/{id}
        public ActionResult Edit(string id)
        {
            SysdiagramsModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(SysdiagramsModel user)
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

        #region 刪除Sysdiagrams,詳細Sysdiagrams

        // Get : /Account/Detail/{id}
        public ActionResult Detail(string id)
        {
            SysdiagramsModel user = _Service.Get(id);
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
