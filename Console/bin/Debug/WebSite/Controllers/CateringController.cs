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
    public class CateringController : Controller
    {
        CateringService _Service = new CateringService();
        
        public CateringController()
        {
            _Service = new CateringService();
        }

        #region 取得Catering
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            CateringListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增Catering
        public ActionResult Add()
        {
            CateringModel entity = new CateringModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(CateringModel model)
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

        #region 修改Catering

        // Get: /Account/Edit/{id}
        public ActionResult Edit(string id)
        {
            CateringModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(CateringModel user)
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

        #region 刪除Catering,詳細Catering

        // Get : /Account/Detail/{id}
        public ActionResult Detail(string id)
        {
            CateringModel user = _Service.Get(id);
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
