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
    public class Fairway_IntroController : Controller
    {
        Fairway_IntroService _Service = new Fairway_IntroService();
        
        public Fairway_IntroController()
        {
            _Service = new Fairway_IntroService();
        }

        #region 取得Fairway_Intro
        // Get : /Account/List
        public ActionResult List(CriteriaMode2 criteria)
        {
            Fairway_IntroListModel query = _Service.GetList(criteria);

            return View(query);
        }
        #endregion

        #region 新增Fairway_Intro
        public ActionResult Add()
        {
            Fairway_IntroModel entity = new Fairway_IntroModel();

            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(Fairway_IntroModel model)
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

        #region 修改Fairway_Intro

        // Get: /Account/Edit/{id}
        public ActionResult Edit(string id)
        {
            Fairway_IntroModel user = _Service.Get(id);
            return View(user);
        }

        // Post: /Account/Update/{id}
        [HttpPost]
        public ActionResult Update(Fairway_IntroModel user)
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

        #region 刪除Fairway_Intro,詳細Fairway_Intro

        // Get : /Account/Detail/{id}
        public ActionResult Detail(string id)
        {
            Fairway_IntroModel user = _Service.Get(id);
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
