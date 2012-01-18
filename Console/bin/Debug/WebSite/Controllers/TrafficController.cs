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
    public class TrafficController : Controller
    {
        TrafficService _Service = new TrafficService();
        
        public TrafficController()
        {
            _Service = new TrafficService();
        }

        #region 取得會員
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            TrafficListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增會員
        public ActionResult Add()
        {
            TrafficModel entity = new TrafficModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(TrafficModel model)
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
            TrafficModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(TrafficModel user)
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
            TrafficModel user = _Service.Get(id);
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
