using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAM.Common;
using PAM.DataAccess;
using PAM.Model;

namespace PAM.BusinessLogic
{
   public class SuburbBusiness : IDisposable 
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        #endregion

        #region Class Methods

        public bool InsertSuburb(SuburbEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new SuburbRepository())
                {
                    bOpDoneSuccessfully = repository.Insert(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SuburbBusiness::InsertSuburb::Error occured.", ex);
            }
        }

        public bool UpdateSuburb(SuburbEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new SuburbRepository())
                {
                    bOpDoneSuccessfully = repository.Update(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SuburbBusiness::UpdateSuburb::Error occured.", ex);
            }
        }



        public SuburbEntity SelectSuburbById(int SuburbId)
        {
            try
            {
                SuburbEntity returnedEntity;
                using (var repository = new SuburbRepository())
                {
                    returnedEntity = repository.SelectById(SuburbId);

                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SuburbBusiness::SelectSuburbById::Error occured.", ex);
            }
        }

        public List<SuburbEntity> SelectAllSuburbs()
        {
            var returnedEntities = new List<SuburbEntity>();

            try
            {
                using (var repository = new SuburbRepository())
                {
                    foreach (var entity in repository.SelectAll())
                    {
                        returnedEntities.Add(entity);
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:SuburbBusiness::SelectAllSuburb::Error occured.", ex);
            }
        }
        public List<StateEntity> SelectAllStates()
        {
            var returnedEntities = new List<StateEntity>();

            try
            {
                using (var repository = new StateRepository())
                {
                    foreach (var entity in repository.SelectAll())
                    {
                        returnedEntities.Add(entity);
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:StateBusiness::SelectAllState::Error occured.", ex);
            }
        }



        public SuburbBusiness()
        {
            _loggingHandler = new LoggingHandler();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _loggingHandler = null;
                }
            }
            _bDisposed = true;
        }
        #endregion
    }
}
