using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAM.BusinessLogic;
using PAM.Common;
using PAM.Model;
using PropertyAddressManagement.Models;

namespace PropertyAddressManagement.Controllers
{
    public class SuburbController : Controller
    {
        private LoggingHandler _loggingHandler;
        PropertyAddressManagementEntities obj = new PropertyAddressManagementEntities();
        SuburbEntity objsub = new SuburbEntity();
        public SuburbController()
        {
            _loggingHandler = new LoggingHandler();
        }
        // GET: Suburb
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_loggingHandler != null)
                {
                    _loggingHandler.Dispose();
                    _loggingHandler = null;
                }
            }

            base.Dispose(disposing);
        }
        // GET: Suburb
        public ActionResult Index()
        {
            return View();
        }

        // GET: Suburb/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Suburb/Create
        public ActionResult Create()
        {
            
            ViewBag.State = new SelectList(obj.States , "StateId", "StateName",objsub.StateId);
            return View(objsub);
        }

        // POST: Suburb/Create
        [HttpPost]
        public ActionResult Create(SuburbEntity obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                InsertSuburb(obj.StateId,obj.SuburbName                                 
                                    );

                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // GET: Suburb/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var Suburb = SelectSuburbById(id);
                ViewBag.State = new SelectList(obj.States , "StateId", "StateName", Suburb.StateId);
                return View(Suburb);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
            return View();
        }

        // POST: Suburb/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                try
                {
                    UpdateSuburb(int.Parse(collection["SuburbId"]),
                                    collection["SuburbName"]
                                    );

                    return RedirectToAction("ListAll");
                }
                catch (Exception ex)
                {
                    //Log exception error
                    _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                    ViewBag.Message = Server.HtmlEncode(ex.Message);
                    return View("Error");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Suburb/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Suburb/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ListAll()
        {
            try
            {
                var Suburbs = from e in ListAllSuburbs()
                             orderby e.SuburbId
                             select e;
                return View(Suburbs);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }
        #region Private Methods

        private List<SuburbEntity> ListAllSuburbs()
        {
            try
            {
                using (var Suburbs = new SuburbBusiness())
                {
                    return Suburbs.SelectAllSuburbs();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private SuburbEntity SelectSuburbById(int id)
        {
            try
            {
                using (var suburbs = new SuburbBusiness())
                {
                    return suburbs.SelectSuburbById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }
   

        private void InsertSuburb(int id,string name)
        {
            try
            {
                using (var Suburbs = new SuburbBusiness())
                {
                    var entity = new SuburbEntity();
                    entity.StateId = id;
                    entity.SuburbName = name;
                    var opSuccessful = Suburbs.InsertSuburb(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void UpdateSuburb(int SuburbId, string Suburbname)
        {
            try
            {
                using (var Suburbs = new SuburbBusiness())
                {
                    var entity = new SuburbEntity();
                    entity.SuburbId = SuburbId;
                    entity.SuburbName = Suburbname;
                    var opSuccessful = Suburbs.UpdateSuburb(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }




        #endregion
    }
}