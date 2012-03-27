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
    public class EnvController : Controller
    {
        EnvService _Service = new EnvService();
        
        public EnvController()
        {
            _Service = new EnvService();
        }

        #region 取得Env
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            EnvListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增Env
        public ActionResult Add()
        {
            EnvModel entity = new EnvModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(EnvModel model)
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

        #region 修改Env

        // Get: /Account/Edit/{id}
        public ActionResult Edit(string id)
        {
            EnvModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(EnvModel user)
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

        #region 刪除Env,詳細Env

        // Get : /Account/Detail/{id}
        public ActionResult Detail(string id)
        {
            EnvModel user = _Service.Get(id);
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
