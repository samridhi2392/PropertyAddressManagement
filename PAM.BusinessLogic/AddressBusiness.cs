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
    public class AddressBusiness:IDisposable 
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        #endregion

        #region Class Methods

        public bool InsertAddress(AddressEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new AddressRepository())
                {
                    bOpDoneSuccessfully = repository.Insert(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:AddressBusiness::InsertAddress::Error occured.", ex);
            }
        }

        public bool UpdateAddress(AddressEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new AddressRepository())
                {
                    bOpDoneSuccessfully = repository.Update(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:AddressBusiness::UpdateAddress::Error occured.", ex);
            }
        }



        public AddressEntity SelectAddressById(int AddressId)
        {
            try
            {
                AddressEntity returnedEntity;
                using (var repository = new AddressRepository())
                {
                    returnedEntity = repository.SelectById(AddressId);

                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:AddressBusiness::SelectAddressById::Error occured.", ex);
            }
        }

        public List<AddressEntity> SelectAllAddresss()
        {
            var returnedEntities = new List<AddressEntity>();

            try
            {
                using (var repository = new AddressRepository())
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

                throw new Exception("BusinessLogic:AddressBusiness::SelectAllAddress::Error occured.", ex);
            }
        }
        public List<StateEntity> SelectAllStates()
        {
            var returnedEntities = new List<StateEntity>();

            try
            {
                using (var repository = new AddressRepository())
                {
                    foreach (var entity in repository.GetStates())
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

                throw new Exception("BusinessLogic:AddressBusiness::SelectAllState::Error occured.", ex);
            }
        }

        public List<SuburbEntity> SelectSuburbByState(int stateId)
        {
            var returnedEntities = new List<SuburbEntity>();

            try
            {
                using (var repository = new AddressRepository())
                {
                    foreach (var entity in repository.SelectSuburbById(stateId))
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

                throw new Exception("BusinessLogic:AddressBusiness::SelectSuburbByState::Error occured.", ex);
            }
        }

        public List<SuburbEntity> SelectSuburbs()
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

                throw new Exception("BusinessLogic:AddressBusiness::SelectAllState::Error occured.", ex);
            }
        }

        public AddressBusiness()
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
