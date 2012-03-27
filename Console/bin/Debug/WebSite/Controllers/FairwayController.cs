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
    public class FairwayController : Controller
    {
        FairwayService _Service = new FairwayService();
        
        public FairwayController()
        {
            _Service = new FairwayService();
        }

        #region 取得Fairway
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            FairwayListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增Fairway
        public ActionResult Add()
        {
            FairwayModel entity = new FairwayModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(FairwayModel model)
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

        #region 修改Fairway

        // Get: /Account/Edit/{id}
        public ActionResult Edit(string id)
        {
            FairwayModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(FairwayModel user)
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

        #region 刪除Fairway,詳細Fairway

        // Get : /Account/Detail/{id}
        public ActionResult Detail(string id)
        {
            FairwayModel user = _Service.Get(id);
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
