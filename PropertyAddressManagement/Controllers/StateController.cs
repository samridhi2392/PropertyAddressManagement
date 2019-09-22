using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAM.BusinessLogic;
using PAM.Common;
using PAM.Model;

namespace PropertyAddressManagement.Controllers
{
    public class StateController : Controller
    {

        private LoggingHandler _loggingHandler;

        public StateController()
        {
            _loggingHandler = new LoggingHandler();
        }

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
        // GET: State
        public ActionResult Index()
        {
            return View();
        }

        // GET: State/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: State/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: State/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                InsertState(collection["StateName"]
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

        // GET: State/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var state = SelectStateById(id);
                return View(state);
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

        // POST: State/Edit/5
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
                    UpdateState(int.Parse(collection["StateId"]),
                                    collection["StateName"]
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

        // GET: State/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: State/Delete/5
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
                var states = from e in ListAllStates()
                                orderby e.StateId
                                select e;
                return View(states);
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

        private List<StateEntity> ListAllStates()
        {
            try
            {
                using (var states = new StateBusiness())
                {
                    return states.SelectAllStates();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private StateEntity SelectStateById(int id)
        {
            try
            {
                using (var employees = new StateBusiness())
                {
                    return employees.SelectStateById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private void InsertState(string name)
        {
            try
            {
                using (var states = new StateBusiness())
                {
                    var entity = new StateEntity();
                    entity.StateName = name;
                    var opSuccessful = states.InsertState(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void UpdateState(int StateId, string Statename)
        {
            try
            {
                using (var states = new StateBusiness())
                {
                    var entity = new StateEntity();
                    entity.StateId = StateId;
                    entity.StateName = Statename ;
                   var opSuccessful = states.UpdateState(entity);
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
