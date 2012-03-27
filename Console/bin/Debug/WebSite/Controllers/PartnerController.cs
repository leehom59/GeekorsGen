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
    public class PartnerController : Controller
    {
        PartnerService _Service = new PartnerService();
        
        public PartnerController()
        {
            _Service = new PartnerService();
        }

        #region 取得Partner
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            PartnerListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增Partner
        public ActionResult Add()
        {
            PartnerModel entity = new PartnerModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(PartnerModel model)
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

        #region 修改Partner

        // Get: /Account/Edit/{id}
        public ActionResult Edit(string id)
        {
            PartnerModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(PartnerModel user)
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

        #region 刪除Partner,詳細Partner

        // Get : /Account/Detail/{id}
        public ActionResult Detail(string id)
        {
            PartnerModel user = _Service.Get(id);
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
