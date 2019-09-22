using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAM.Model;
using PAM.Common;
using PAM.DataAccess.Common;
using System.Data;


namespace PAM.DataAccess
{
   public class SuburbRepository : IRepository<SuburbEntity>, IDisposable

    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private DataHandler _dataHandler;
        private ConfigurationHandler _configurationHandler;
        private DbProviderFactory _dbProviderFactory;
        private string _connectionString;
        private string _connectionProvider;
        private int _errorCode, _rowsAffected;
        private bool _bDisposed;

        #endregion

        #region Class Methods

        public bool Insert(SuburbEntity entity)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("INSERT [dbo].[Suburb] ");
                sb.Append("( ");
                sb.Append("[SuburbName],");
                sb.Append("[StateId]");
                sb.Append(") ");
                sb.Append("VALUES ");
                sb.Append("( ");
                sb.Append(" @SuburbName,");
                sb.Append(" @StateId");
                sb.Append(") ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");


                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [Suburb] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@SuburbName", CsType.String, ParameterDirection.Input, entity.SuburbName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@StateId", CsType.String, ParameterDirection.Input, entity.StateId );

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State  != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Insert method for entity [Suburb] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("SuburbRepository::Insert::Error occured.", ex);
            }
        }

        public bool Update(SuburbEntity entity)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("UPDATE [dbo].[Suburb] ");
                sb.Append("SET ");
                sb.Append("[SuburbName] = @SuburbName ");
                sb.Append("WHERE ");
                sb.Append("[SuburbId] = @SuburbId ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [Suburb] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@SuburbId", CsType.Int, ParameterDirection.Input, entity.SuburbId);
                        _dataHandler.AddParameterToCommand(dbCommand, "@SuburbName", CsType.String, ParameterDirection.Input, entity.SuburbName);
                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State  != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Update method for entity [Suburb] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("SuburbRepository::Update::Error occured.", ex);
            }
        }

        public bool DeleteById(int id)
        {
            //_errorCode = 0;
            //_rowsAffected = 0;

            //try
            //{
            //    var sb = new StringBuilder();
            //    sb.Append("DELETE FROM [HR].[Employees] ");
            //    sb.Append("WHERE ");
            //    sb.Append("[Id] = @intId ");
            //    sb.Append("SELECT @intErrorCode=@@ERROR; ");

            //    var commandText = sb.ToString();
            //    sb.Clear();

            //    using (var dbConnection = _dbProviderFactory.CreateConnection())
            //    {
            //        if (dbConnection == null)
            //            throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

            //        dbConnection.ConnectionString = _connectionString;

            //        using (var dbCommand = _dbProviderFactory.CreateCommand())
            //        {
            //            if (dbCommand == null)
            //                throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [Employees] can't be null. ");

            //            dbCommand.Connection = dbConnection;
            //            dbCommand.CommandText = commandText;

            //            Input Parameters
            //            _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, id);

            //            Output Parameters
            //            _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

            //            Open Connection
            //            if (dbConnection.Suburb != ConnectionSuburb.Open)
            //                dbConnection.Open();

            //            Execute query.
            //            _rowsAffected = dbCommand.ExecuteNonQuery();
            //            _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

            //            if (_errorCode != 0)
            //            {
            //                Throw error.
            //                throw new Exception("The Delete method for entity [Employees] reported the Database ErrorCode: " + _errorCode);
            //            }
            //        }
            //    }

            return true;
            //}
            //catch (Exception ex)
            //{
            //    Log exception error
            //    _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

            //    Bubble error to caller and encapsulate Exception object
            //    throw new Exception("EmployeesRepository::Delete::Error occured.", ex);
            //}
        }

        public SuburbEntity SelectById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            SuburbEntity returnedEntity = null;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT ");
                sb.Append("[SuburbId], ");
                sb.Append("[SuburbName], ");
                sb.Append("st.[StateId], ");
                sb.Append("[StateName] ");
                sb.Append("FROM [dbo].[Suburb] sb ,  [dbo].[State] st ");
                sb.Append("WHERE sb.StateId =st.StateId and");
                sb.Append("[SuburbId] = @SuburbId ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Suburb] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@SuburbId", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State  != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new SuburbEntity();
                                    entity.SuburbId = reader.GetInt32(0);
                                    entity.SuburbName = reader.GetString(1);
                                    entity .StateId = reader.GetInt32(2);
                                    entity.StateName = reader.GetString(3);
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Suburb] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("SuburbRepository::SelectById::Error occured.", ex);
            }
        }

        public List<SuburbEntity> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<SuburbEntity>();

            try
            {
                var sb = new StringBuilder();
               sb.Append("SELECT ");
                sb.Append("[SuburbId], ");
                sb.Append("[SuburbName], ");
                sb.Append("sb.[StateId], ");
                sb.Append("[StateName] ");
                sb.Append("FROM [dbo].[Suburb] sb ,  [dbo].[State] st ");
                sb.Append("WHERE sb.StateId =st.StateId ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");
                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db command for entity [Suburb] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State  != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new SuburbEntity();
                                    entity.SuburbId = reader.GetInt32(0);
                                    entity.SuburbName = reader.GetString(1);
                                    entity.StateId = reader.GetInt32(2);
                                    entity.StateName = reader.GetString(3);
                                    returnedEntities.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectAll method for entity [Suburb] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("SuburbRepository::SelectAll::Error occured.", ex);
            }
        }

        public SuburbRepository()
        {
            //Repository Initializations
            _configurationHandler = new ConfigurationHandler();
            _loggingHandler = new LoggingHandler();
            _dataHandler = new DataHandler();
            _connectionString = _configurationHandler.ConnectionString;
            _connectionProvider = _configurationHandler.ConnectionProvider;
            _dbProviderFactory = DbProviderFactories.GetFactory(_connectionProvider);
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
                    _configurationHandler = null;
                    _loggingHandler = null;
                    _dataHandler = null;
                    _dbProviderFactory = null;
                }
            }
            _bDisposed = true;
        }

        #endregion
    }
}
