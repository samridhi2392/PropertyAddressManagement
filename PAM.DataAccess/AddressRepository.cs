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
   public class AddressRepository : IRepository<AddressEntity>, IDisposable
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

        public bool Insert(AddressEntity entity)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("INSERT [dbo].[Address] ");
                sb.Append("( ");
                sb.Append("[StreetNumber],");
                sb.Append("[Street], ");
                sb.Append("[SuburbId], ");
                sb.Append("[PostCode], ");
                sb.Append("[StateId], ");
                sb.Append("[UnitNumber], ");
                sb.Append("[PropertyType], ");
                sb.Append("[StreetType] ");
                sb.Append(") ");
                sb.Append("VALUES ");
                sb.Append("( ");
                sb.Append("@StreetNumber,");
                sb.Append("@Street,");
                sb.Append("@SuburbId,");
                sb.Append("@PostCode,");
                sb.Append("@StateId,");
                sb.Append("@UnitNumber,");
                sb.Append("@PropertyType,");
                sb.Append("@StreetType");    
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [Address] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@StreetNumber", CsType.String, ParameterDirection.Input, entity.StreetNumber );
                        _dataHandler.AddParameterToCommand(dbCommand, "@Street", CsType.String, ParameterDirection.Input, entity.Street );
                        _dataHandler.AddParameterToCommand(dbCommand, "@SuburbId", CsType.Int, ParameterDirection.Input, entity.SuburbId );
                        _dataHandler.AddParameterToCommand(dbCommand, "@PostCode", CsType.Int, ParameterDirection.Input, entity.PostCode );
                        _dataHandler.AddParameterToCommand(dbCommand, "@StateId", CsType.Int, ParameterDirection.Input, entity.StateId );
                        _dataHandler.AddParameterToCommand(dbCommand, "@UnitNumber", CsType.Int, ParameterDirection.Input, entity.UnitNumber );
                        _dataHandler.AddParameterToCommand(dbCommand, "@PropertyType", CsType.String, ParameterDirection.Input, entity.PropertyType );
                        _dataHandler.AddParameterToCommand(dbCommand, "@StreetType", CsType.String, ParameterDirection.Input, entity.StreetType );
                        //Output Parameters
                       _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Insert method for entity [Address] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("AddressRepository::Insert::Error occured.", ex);
            }
        }

        public bool Update(AddressEntity  entity)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("UPDATE [dbo].[Address] ");
                sb.Append("SET ");
                sb.Append("[StreetNumber] = @StreetNumber,");
                sb.Append("[Street] = @Street,");
                sb.Append("[SuburbId] = @SuburbId,");
                sb.Append("[PostCode] = @PostCode,");
                sb.Append("[StateId]= @StateId,");
                sb.Append("[UnitNumber] = @UnitNumber,");
                sb.Append("[PropertyType] = @PropertyType,");
                sb.Append("[StreetType] = @StreetType ");
                sb.Append("WHERE ");
                sb.Append("[Id] = @Id ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [Address] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@Id", CsType.Int, ParameterDirection.Input, entity.StateId);
                        _dataHandler.AddParameterToCommand(dbCommand, "@StreetNumber", CsType.String, ParameterDirection.Input, entity.StreetNumber);
                        _dataHandler.AddParameterToCommand(dbCommand, "@Street", CsType.String, ParameterDirection.Input, entity.Street);
                        _dataHandler.AddParameterToCommand(dbCommand, "@SuburbId", CsType.Int, ParameterDirection.Input, entity.SuburbId);
                        _dataHandler.AddParameterToCommand(dbCommand, "@PostCode", CsType.Int, ParameterDirection.Input, entity.PostCode);
                        _dataHandler.AddParameterToCommand(dbCommand, "@StateId", CsType.Int, ParameterDirection.Input, entity.StateId);
                        _dataHandler.AddParameterToCommand(dbCommand, "@UnitNumber", CsType.Int, ParameterDirection.Input, entity.UnitNumber);
                        _dataHandler.AddParameterToCommand(dbCommand, "@PropertyType", CsType.String, ParameterDirection.Input, entity.PropertyType);
                        _dataHandler.AddParameterToCommand(dbCommand, "@StreetType", CsType.String, ParameterDirection.Input, entity.StreetType);
                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Update method for entity [Address] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("AddressRepository::Update::Error occured.", ex);
            }
        }

        public bool DeleteById(int id)
        {
            return true;
            //}
            //catch (Exception ex)
            //{
            //    Log exception error
            //    _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

            //    Bubble error to caller and encapsulate Exception object
            //    throw new Exception("Repository::Delete::Error occured.", ex);
            //}
        }

        public AddressEntity SelectById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            AddressEntity returnedEntity = null;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT * ");
                sb.Append("FROM [dbo].[Address] ");
                sb.Append("WHERE ");
                sb.Append("[Id] = @Id ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Address] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@Id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new AddressEntity();
                                    entity.Id = reader.GetInt32(0);
                                    entity.StreetNumber  = reader.GetString(1);
                                    entity.Street = reader.GetString(2);
                                    entity.SuburbId = reader.GetInt32(3);
                                    entity.PostCode  = reader.GetInt32(4);
                                    entity.StateId  = reader.GetInt32(5);
                                    if (reader["UnitNumber"] != DBNull.Value)
                                    {
                                        entity.UnitNumber = reader.GetInt32(6);
                                    }
                                    if (reader["PropertyType"] != DBNull.Value)
                                    {
                                        entity.PropertyType = reader.GetString(7);
                                    }

                                        if (reader["StreetType"] != DBNull.Value)
                                        {
                                            entity.StreetType = reader.GetString(8);
                                    }
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Address] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("AddressRepository::SelectById::Error occured.", ex);
            }
        }

        public List<AddressEntity> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<AddressEntity>();

            try
            {
                var sb = new StringBuilder();
                sb.Append("Select AD.*,SB.SuburbName,St.StateName from [dbo].[Address] AD ");
                sb.Append("INNER JOIN[dbo].[Suburb] SB ON AD.SuburbId = SB.SuburbId ");
                sb.Append("INNER JOIN[dbo].[State] ST ON ST.StateId = SB.StateId ");
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
                            throw new ArgumentNullException("dbCommand" + " The db command for entity [Address] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new AddressEntity();
                                    entity.Id = reader.GetInt32(0);
                                    entity.StreetNumber = reader.GetString(1);
                                    entity.Street = reader.GetString(2);
                                    entity.SuburbId = reader.GetInt32(3);
                                    entity.PostCode = reader.GetInt32(4);
                                    entity.StateId = reader.GetInt32(5);
                                    entity.SuburbName  = reader.GetString(9);
                                    entity.StateName  = reader.GetString(10);
                                    entity.FullAddress = entity.StreetNumber + " " + entity.Street + ", " + entity.SuburbName + ", " + entity.StateName + " " + Convert.ToString(entity.PostCode);
                                    returnedEntities.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectAll method for entity [State] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("StateRepository::SelectAll::Error occured.", ex);
            }
        }

       
        public IEnumerable<StateEntity> GetStates()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<StateEntity>();

            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT ");
                sb.Append("[StateId], ");
                sb.Append("[StateName] ");
                sb.Append("FROM [dbo].[State] ");
                sb.Append("ORDER BY [StateName] ");
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
                            throw new ArgumentNullException("dbCommand" + " The db command for entity [State] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new StateEntity();
                                    entity.StateId = reader.GetInt32(0);
                                    entity.StateName = reader.GetString(1);
                                    returnedEntities.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The GetStates method for entity [State] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("AddressRepository::GEtState::Error occured.", ex);
            }
        }

        public IEnumerable<SuburbEntity> SelectSuburbById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<SuburbEntity>();

            try
            {
                var sb = new StringBuilder();
                sb.Append("SELECT ");
                sb.Append("[SuburbId], ");
                sb.Append("[SuburbName],");
                sb.Append("st.[StateId],");
                sb.Append("[StateName] ");
                sb.Append("FROM [dbo].[Suburb] sb ,  [dbo].[State] st ");
                sb.Append("WHERE sb.StateId =st.StateId and ");
                sb.Append("st.[StateId] = @StateId ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectSuburbById command for entity [Suburb] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@StateId", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
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
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectSuburbById method for entity [Suburb] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("AddressRepository::SelectSuburbById::Error occured.", ex);
            }
        }

        public AddressRepository()
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
