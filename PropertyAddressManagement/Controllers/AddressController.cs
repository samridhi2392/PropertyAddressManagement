using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAM.BusinessLogic;
using PAM.Common;
using PAM.Model;

namespace PropertyAddressManagement.Controllers
{
    public class AddressController : Controller
    {
        private LoggingHandler _loggingHandler;
        AddressEntity objaddress = new AddressEntity();
        public AddressController()
        {
            _loggingHandler = new LoggingHandler();
        }
        // GET: Address
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
        // GET: Address
        public ActionResult Index()
        {
            return View();
        }

        // GET: Address/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Address/Create
        public ActionResult Create()
        {
            
            ViewBag.State = new SelectList(GetStates(), "StateId", "StateName", objaddress.StateId);
            ViewBag.Suburb = new SelectList(GetSuburbs(), "SuburbId", "SuburbName", objaddress.SuburbId);
            return View(objaddress);
        }

        // POST: Address/Create
        [HttpPost]
        public ActionResult Create(AddressEntity obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                InsertAddress(obj);

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

        // GET: Address/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var Address = SelectAddressById(id);
                ViewBag.State = new SelectList(GetStates(), "StateId", "StateName", Address.StateId);
                ViewBag.Suburb = new SelectList(GetSuburbs(), "SuburbId", "SuburbName", Address.SuburbId);
                return View(Address);
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

        // POST: Address/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AddressEntity objadd)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                try
                {
                    UpdateAddress(objadd);

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

        // GET: Address/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Address/Delete/5
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

        public JsonResult GetSuburb(string id)
        {
              
            return Json(new SelectList(GetSuburbById(int.Parse(id)), "SuburbId", "SuburbName"));
        }
        public ActionResult ListAll()
        {
            try
            {
               return View();
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        public JsonResult GetAddresses()
        {
            var Addresss = from e in ListAllAddresss()
                           orderby e.Id
                           select e;
            return Json(Addresss, JsonRequestBehavior.AllowGet);
        }
        #region Private Methods

        private List<AddressEntity> ListAllAddresss()
        {
            try
            {
                using (var Addresss = new AddressBusiness())
                {
                    return Addresss.SelectAllAddresss();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private AddressEntity SelectAddressById(int id)
        {
            try
            {
                using (var Addresss = new AddressBusiness())
                {
                    return Addresss.SelectAddressById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }


        private void InsertAddress(AddressEntity entity)
        {
            try
            {
                using (var Addresss = new AddressBusiness())
                {
                     var opSuccessful = Addresss.InsertAddress(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void UpdateAddress(AddressEntity entity)
        {
            try
            {
                using (var Addresss = new AddressBusiness())
                {
                    var opSuccessful = Addresss.UpdateAddress(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }
        private List<StateEntity> GetStates()
        {
            try
            {
                using (var Addresss = new AddressBusiness())
                {
                    return Addresss.SelectAllStates();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                return null;
            }
        }

        private List<SuburbEntity> GetSuburbById(int Stateid)
        {
            try
            {
                using (var Addresss = new AddressBusiness())
                {
                    return Addresss.SelectSuburbByState(Stateid);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                return null;
            }
        }

        private List<SuburbEntity> GetSuburbs()
        {
            try
            {
                using (var Addresss = new AddressBusiness())
                {
                    return Addresss.SelectSuburbs();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                return null;
            }
        }




        #endregion
    }
}