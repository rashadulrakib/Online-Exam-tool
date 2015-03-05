using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
//using Logging;
using Common;
using log4net;
using log4net.Config;

namespace Utility
{
    /// <summary>
    /// DAOUtil Calss is used for Execute Non query,Get DataReader and Get Scaler.
    /// All kind of transaction with Database is performed by this class.
    /// </summary>

    public class DAOUtil
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DAOUtil));
        
        public DAOUtil()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        private static SqlConnection oSqlconnection = null;
        

        /// <summary>
        /// This method provides to create a connection.
        /// The connection object is Singleton.
        /// </summary>
        /// <returns> return typr is SqlConnection object</returns>
        private static SqlConnection CreateConnection()
        {
            //SqlConnection m_oSqlConnection = null;

            DBInfo oDBInfo;

            try
            {
                if (oSqlconnection == null)
                {
                    //if(oSqlconnection!=null && (oSqlconnection.State == ConnectionState.Open))
                    //{
                    //    oSqlconnection.Close();
                    //}

                    oDBInfo = (DBInfo)new XMLUtil().GetConfigFile(typeof(DBInfo), "DB.xml");

                    oSqlconnection = new SqlConnection(oDBInfo.ConnectionString);

                    oSqlconnection.Open();
                }

                //new CLogger("Connection Success", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Connection Success", ELogLevel.Debug);

                logger.Info("Connection Success Utility+DAOUtil");

            }
            catch (SqlException oSqlException)
            {
                reconnectDatabase();

                //new CLogger("Connection Exception", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Connection Exception", ELogLevel.Debug, oSqlException);

                logger.Info("Connection SqlException Utility+DAOUtil:" + oSqlException.Message);
            }
            catch (Exception oException)
            {
                reconnectDatabase();

                //new CLogger("Connection Exception", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Connection Exception", ELogLevel.Debug, oException);

                logger.Info("Connection Exception Utility+DAOUtil", oException);
            }

            return oSqlconnection;
        }


        /// <summary>
        /// This method close the connection and make the connection null
        /// </summary>
        private static void reconnectDatabase()
        {
            if (oSqlconnection != null && (oSqlconnection.State == ConnectionState.Open))
            {
                oSqlconnection.Close();
            }

            oSqlconnection = null;
        }

        /// <summary>
        /// This Methods provides to Execute any non query like delete,insert,update.
        /// </summary>
        /// <param name="liNonQuerySQL"> It takes list of Non Query string type command </param>
        /// <returns> return true if success other wise return false </returns>
        public bool ExecuteNonQuery(List<string> liNonQuerySQL)
        {
            logger.Info("Start ExecuteNonQuery Utility+DAOUtil");
            
            SqlConnection m_oSqlConnection = Utility.DAOUtil.CreateConnection();

            if (m_oSqlConnection != null)
            {
                try
                {
                    SqlTransaction oSqlTransaction = DAOUtil.CreateConnection().BeginTransaction();

                    List<SqlCommand> liSqlCommand = new List<SqlCommand>();

                    foreach (string sNonQuerySql in liNonQuerySQL)
                    {
                        SqlCommand oSqlCommand = new SqlCommand(sNonQuerySql, m_oSqlConnection);

                        oSqlCommand.Transaction = oSqlTransaction;

                        liSqlCommand.Add(oSqlCommand);
                    }

                    try
                    {
                        foreach (SqlCommand oSqlCommand in liSqlCommand)
                        {
                            oSqlCommand.ExecuteNonQuery();
                        }

                        oSqlTransaction.Commit();

                        return true;
                    }
                    catch (SqlException oSqlException1)
                    {
                        logger.Info("ExecuteNonQuery SqlException Utility+DAOUtil:" + oSqlException1.Message);
                        
                        oSqlTransaction.Rollback();

                        reconnectDatabase();

                        if (oSqlException1.ErrorCode == (Int64)Eint.ErrorCodeOfTransfer)
                        {
                            return ExecuteNonQueryAfterHandleException(liNonQuerySQL);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception oException)
                    {
                        logger.Info("ExecuteNonQuery Exception Utility+DAOUtil", oException);
                        
                        oSqlTransaction.Rollback();

                        reconnectDatabase();

                        return false;
                    }
                    finally
                    {
                        //if (m_oSqlConnection != null)
                        //{
                        //    m_oSqlConnection.Close();
                        //}
                    }
                }
                catch (SqlException oSqlException2)
                {
                    logger.Info("ExecuteNonQuery SqlException Utility+DAOUtil:" + oSqlException2.Message);
                    
                    reconnectDatabase();

                    if (oSqlException2.ErrorCode == (Int64)Eint.ErrorCodeOfTransfer)
                    {
                        return ExecuteNonQueryAfterHandleException(liNonQuerySQL);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception oEx)
                {
                    logger.Info("ExecuteNonQuery Exception Utility+DAOUtil", oEx);
                    
                    reconnectDatabase();

                    return false;
                }
            }
            else
            {
                return false;
            }

            logger.Info("End ExecuteNonQuery Utility+DAOUtil");

            return true;
        }


        /// <summary>
        /// This Methods provides to Execute any non query like delete,insert,update.
        /// It is used, after handle ansqlexception
        /// </summary>
        /// <param name="liNonQuerySQL"> It takes list of Non Query string type command </param>
        /// <returns> return true if success other wise return false </returns>
        private bool ExecuteNonQueryAfterHandleException(List<string> liNonQuerySQL)
        {
            logger.Info("Start ExecuteNonQueryAfterHandleException Utility+DAOUtil");
            
            SqlConnection m_oSqlConnection = Utility.DAOUtil.CreateConnection();

            if (m_oSqlConnection != null)
            {
                try
                {
                    SqlTransaction oSqlTransaction = DAOUtil.CreateConnection().BeginTransaction();

                    List<SqlCommand> liSqlCommand = new List<SqlCommand>();

                    foreach (string sNonQuerySql in liNonQuerySQL)
                    {
                        SqlCommand oSqlCommand = new SqlCommand(sNonQuerySql, m_oSqlConnection);

                        oSqlCommand.Transaction = oSqlTransaction;

                        liSqlCommand.Add(oSqlCommand);
                    }

                    try
                    {
                        foreach (SqlCommand oSqlCommand in liSqlCommand)
                        {
                            oSqlCommand.ExecuteNonQuery();
                        }

                        oSqlTransaction.Commit();

                        return true;
                    }
                    catch (SqlException oSqlException1)
                    {
                        logger.Info("ExecuteNonQueryAfterHandleException SqlException Utility+DAOUtil:" + oSqlException1.Message);
                        
                        oSqlTransaction.Rollback();

                        return false;
                    }
                    catch (Exception oException)
                    {
                        logger.Info("Exception ExecuteNonQueryAfterHandleException Utility+DAOUtil",oException);
                        
                        oSqlTransaction.Rollback();

                        return false;
                    }
                    finally
                    {
                        //if (m_oSqlConnection != null)
                        //{
                        //    m_oSqlConnection.Close();
                        //}
                    }
                }
                catch (SqlException oSqlException2)
                {
                    logger.Info("ExecuteNonQueryAfterHandleException SqlException Utility+DAOUtil:" + oSqlException2.Message);
                    
                    return false;
                }
                catch (Exception oEx)
                {
                    logger.Info("Exception ExecuteNonQueryAfterHandleException Utility+DAOUtil", oEx);

                    return false;
                }
            }
            else
            {
                return false;
            }

            logger.Info("End ExecuteNonQueryAfterHandleException Utility+DAOUtil");
            
            return true;
        }

       

        /// <summary>
        /// This Method provides to get a SqlDataReader.
        /// </summary>
        /// <param name="sQuerySQL"> It takes one SQL Query string </param>
        /// <returns> it returns SqlDataReader </returns>
        public SqlDataReader GetReader(string sQuerySQL)
        {
            logger.Info("Start GetReader Utility+DAOUtil");
            
            SqlDataReader oDataReader = null;

            SqlConnection m_oSqlConnection = Utility.DAOUtil.CreateConnection();

            if (m_oSqlConnection != null)
            {
                try
                {
                    SqlCommand oSqlCommand = new SqlCommand(sQuerySQL, m_oSqlConnection);
                    //Parameter should be added here;
                    // return the reader.
                    oDataReader = oSqlCommand.ExecuteReader();
                    return oDataReader;
                }
                catch (SqlException oSqlException)
                {
                    logger.Info("GetReader SqlException Utility+DAOUtil:" + oSqlException.Message);
                    
                    reconnectDatabase();

                    if (oSqlException.ErrorCode == (Int64)Eint.ErrorCodeOfTransfer)
                    {
                        return GetReaderAfterHandleException(sQuerySQL);
                    }
                    else
                    {
                        return oDataReader;
                    }
                }
                catch (Exception oException)
                {
                    logger.Info("Exception GetReader Utility+DAOUtil", oException);
                    
                    reconnectDatabase();

                    return oDataReader;
                }
                finally
                {
                    //if (m_oSqlConnection != null)
                    //{
                    //    m_oSqlConnection.Close();
                    //}
                }
            }
            else
            {
                return oDataReader;
            }

            logger.Info("End GetReader Utility+DAOUtil");

            return oDataReader;
        }

        /// <summary>
        /// This Method provides to get a SqlDataReader.
        /// It is used after a sqlexception
        /// </summary>
        /// <param name="sQuerySQL"> It takes one SQL Query string </param>
        /// <returns> it returns SqlDataReader </returns>
        public SqlDataReader GetReaderAfterHandleException(string sQuerySQL)
        {
            logger.Info("Start GetReaderAfterHandleException Utility+DAOUtil");
            
            SqlDataReader oDataReader = null;

            SqlConnection m_oSqlConnection = Utility.DAOUtil.CreateConnection();

            if (m_oSqlConnection != null)
            {
                try
                {
                    SqlCommand oSqlCommand = new SqlCommand(sQuerySQL, m_oSqlConnection);
                    //Parameter should be added here;
                    // return the reader.
                    oDataReader = oSqlCommand.ExecuteReader();
                    return oDataReader;
                }
                catch (SqlException oSqlException)
                {
                    logger.Info("GetReaderAfterHandleException SqlException Utility+DAOUtil:" + oSqlException.Message);
                    
                    return oDataReader;
                }
                catch (Exception oException)
                {
                    logger.Info("Exception GetReaderAfterHandleException Utility+DAOUtil", oException);
                    
                    return oDataReader;
                }
                finally
                {
                    //if (m_oSqlConnection != null)
                    //{
                    //    m_oSqlConnection.Close();
                    //}
                }
            }
            else
            {
                return oDataReader;
            }

            logger.Info("End GetReaderAfterHandleException Utility+DAOUtil");

            return oDataReader;
        
        }



        /// <summary>
        /// This Methods provides to Execute SqlCommand
        /// </summary>
        /// <param name="liNonQuerySQL"> It takes List<SqlCommand> </param>
        /// <returns> return List<int> to get the row effects for the sql command </returns>
        public List<int> ExecuteNonQueryForStoredProcedure(List<SqlCommand> oListSqlCommand)
        {
            logger.Info("Start ExecuteNonQueryForStoredProcedure Utility+DAOUtil");
            
            List<int> oListInt = new List<int>();

            SqlConnection m_oSqlConnection = Utility.DAOUtil.CreateConnection();

            if (m_oSqlConnection != null)
            {
                try
                {
                    SqlTransaction oSqlTransaction = DAOUtil.CreateConnection().BeginTransaction();

                    try
                    {
                        foreach (SqlCommand oSqlCommand in oListSqlCommand)
                        {
                            oSqlCommand.Connection = m_oSqlConnection;
                            oSqlCommand.Transaction = oSqlTransaction;
                        }

                        foreach (SqlCommand oSqlCommand in oListSqlCommand)
                        {
                            oListInt.Add(oSqlCommand.ExecuteNonQuery());
                        }

                        oSqlTransaction.Commit();
                    }
                    catch (SqlException oSqlException1)
                    {
                        logger.Info("SqlException ExecuteNonQueryForStoredProcedure Utility+DAOUtil:"+ oSqlException1.Message);
                        
                        oSqlTransaction.Rollback();

                        oListInt = new List<int>();

                        reconnectDatabase();

                        if (oSqlException1.ErrorCode == (Int64)Eint.ErrorCodeOfTransfer)
                        {
                            return ExecuteNonQueryForStoredProcedureAfterHandleException(oListSqlCommand);
                        }

                    }
                    catch (Exception oException1)
                    {
                        logger.Info("Exception ExecuteNonQueryForStoredProcedure Utility+DAOUtil",oException1);
                        
                        oSqlTransaction.Rollback();

                        oListInt = new List<int>();

                        reconnectDatabase();
                    }
                    finally
                    {
                        //if (m_oSqlConnection != null)
                        //{
                        //    m_oSqlConnection.Close();
                        //}
                    }
                }
                catch (SqlException oSqlException2)
                {
                    logger.Info("SqlException ExecuteNonQueryForStoredProcedure Utility+DAOUtil:" + oSqlException2.Message);
                    
                    oListInt = new List<int>();

                    reconnectDatabase();

                    if (oSqlException2.ErrorCode == (Int64)Eint.ErrorCodeOfTransfer)
                    {
                        return ExecuteNonQueryForStoredProcedureAfterHandleException(oListSqlCommand);
                    }
                }
                catch (Exception oEx)
                {
                    logger.Info("Exception ExecuteNonQueryForStoredProcedure Utility+DAOUtil", oEx);
                    
                    oListInt = new List<int>();

                    reconnectDatabase();
                }
            }
            else
            {
                oListInt = new List<int>();
            }

            logger.Info("End ExecuteNonQueryForStoredProcedure Utility+DAOUtil");

            return oListInt;
        }

        /// <summary>
        /// This Methods provides to Execute SqlCommand
        /// It is used after sqlexception
        /// </summary>
        /// <param name="liNonQuerySQL"> It takes List<SqlCommand> </param>
        /// <returns> return List<int> to get the row effects for the sql command </returns>
        private List<int> ExecuteNonQueryForStoredProcedureAfterHandleException(List<SqlCommand> oListSqlCommand)
        {
            logger.Info("Start ExecuteNonQueryForStoredProcedureAfterHandleException Utility+DAOUtil");
            
            List<int> oListInt = new List<int>();

            SqlConnection m_oSqlConnection = Utility.DAOUtil.CreateConnection();

            if (m_oSqlConnection != null)
            {
                try
                {
                    SqlTransaction oSqlTransaction = DAOUtil.CreateConnection().BeginTransaction();

                    try
                    {
                        foreach (SqlCommand oSqlCommand in oListSqlCommand)
                        {
                            oSqlCommand.Connection = m_oSqlConnection;
                            oSqlCommand.Transaction = oSqlTransaction;
                        }

                        foreach (SqlCommand oSqlCommand in oListSqlCommand)
                        {
                            oListInt.Add(oSqlCommand.ExecuteNonQuery());
                        }

                        oSqlTransaction.Commit();
                    }
                    catch (SqlException oSqlException1)
                    {
                        logger.Info("SqlException ExecuteNonQueryForStoredProcedureAfterHandleException Utility+DAOUtil:" + oSqlException1.Message);
                        
                        oSqlTransaction.Rollback();

                        oListInt = new List<int>();
                    }
                    catch (Exception oException1)
                    {
                        logger.Info("Exception ExecuteNonQueryForStoredProcedureAfterHandleException Utility+DAOUtil", oException1);
                        
                        oSqlTransaction.Rollback();

                        oListInt = new List<int>();
                    }
                    finally
                    {
                        //if (m_oSqlConnection != null)
                        //{
                        //    m_oSqlConnection.Close();
                        //}
                    }
                }
                catch (SqlException oSqlException2)
                {
                    logger.Info("SqlException ExecuteNonQueryForStoredProcedureAfterHandleException Utility+DAOUtil:" + oSqlException2.Message);
                    
                    oListInt = new List<int>();
                }
                catch (Exception oEx)
                {
                    logger.Info("Exception ExecuteNonQueryForStoredProcedureAfterHandleException Utility+DAOUtil", oEx);
                    
                    oListInt = new List<int>();
                }
            }
            else
            {
                oListInt = new List<int>();
            }

            logger.Info("End ExecuteNonQueryForStoredProcedureAfterHandleException Utility+DAOUtil");

            return oListInt;
        }


        /// <summary>
        /// This Methods provides to Execute SqlCommand as a StoredProcedure
        /// </summary>
        /// <param name="oSqlCommandWithParameter"> It takes SqlCommand </param>
        /// <returns> return SqlDataReader as the resultset of the stored procedure </returns>
        public SqlDataReader GetReaderForStoredProcedure(string sProcedureName, SqlCommand oSqlCommandWithParameter) // it is not used
        {
            SqlDataReader oDataReader = null;

            try
            {

                return oDataReader;
            }
            catch (SqlException oSqlException)
            {
                reconnectDatabase();

                if (oSqlException.ErrorCode == (Int64)Eint.ErrorCodeOfTransfer)
                {
                    return GetReaderForStoredProcedureAfterHandleException(sProcedureName, oSqlCommandWithParameter);
                }
                else
                {
                    return oDataReader;
                }
            }
            catch (Exception oException)
            {
                reconnectDatabase();

                return oDataReader;
            }

            return oDataReader;
        }

        private SqlDataReader GetReaderForStoredProcedureAfterHandleException(string sProcedureName, SqlCommand oSqlCommandWithParameter) // it is not used
        {
            SqlDataReader oDataReader = null;

            try
            {

                return oDataReader;
            }
            catch (SqlException oSqlException)
            {
                return oDataReader;
            }
            catch (Exception oException)
            {
                return oDataReader;
            }

            return oDataReader;
        }

        /// <summary>
        /// This method provides to get a Aggrigate function.
        /// example: count(*),avg,sum, etc.
        /// </summary>
        /// <param name="sQuerySQL"> It takes one SQL Query string  </param>
        /// <returns> its return type is object </returns>
        public Object GetExecuteScalar(string sQuerySQL)
        {
            logger.Info("Start GetExecuteScalar Utility+DAOUtil");
            
            Object oObject = null;

            SqlConnection m_oSqlConnection = Utility.DAOUtil.CreateConnection();

            if (m_oSqlConnection != null)
            {
                try
                {
                    SqlCommand oSqlCommand = new SqlCommand(sQuerySQL, m_oSqlConnection);

                    oObject = oSqlCommand.ExecuteScalar();
                    return oObject;
                }
                catch (SqlException oSqlException)
                {
                    logger.Info("SqlException GetExecuteScalar Utility+DAOUtil:" + oSqlException.Message);
                    
                    reconnectDatabase();

                    if (oSqlException.ErrorCode == (Int64)Eint.ErrorCodeOfTransfer)
                    {
                        return GetExecuteScalarAfterHandleException(sQuerySQL);
                    }
                    else
                    {
                        return oObject;
                    }
                }
                catch (Exception oException)
                {
                    logger.Info("Exception GetExecuteScalar Utility+DAOUtil", oException);
                    
                    reconnectDatabase();

                    return oObject;
                }
                finally
                {
                    //if (m_oSqlConnection != null)
                    //{
                    //    m_oSqlConnection.Close();
                    //}
                }
            }
            else
            {
                return oObject;
            }

            logger.Info("End GetExecuteScalar Utility+DAOUtil");
            
            return oObject;
        }

        /// <summary>
        /// This method provides to get a Aggrigate function.
        /// example: count(*),avg,sum, etc.
        /// it is used after sqlexception
        /// </summary>
        /// <param name="sQuerySQL"> It takes one SQL Query string  </param>
        /// <returns> its return type is object </returns>
        private object GetExecuteScalarAfterHandleException(string sQuerySQL)
        {
            logger.Info("Start GetExecuteScalarAfterHandleException Utility+DAOUtil");
            
            Object oObject = null;

            SqlConnection m_oSqlConnection = Utility.DAOUtil.CreateConnection();

            if (m_oSqlConnection != null)
            {
                try
                {
                    SqlCommand oSqlCommand = new SqlCommand(sQuerySQL, m_oSqlConnection);

                    oObject = oSqlCommand.ExecuteScalar();
                    return oObject;
                }
                catch (SqlException oSqlException)
                {
                    logger.Info("SqlException GetExecuteScalarAfterHandleException Utility+DAOUtil:" + oSqlException.Message);
                    
                    return oObject;
                }
                catch (Exception oException)
                {
                    logger.Info("Exception GetExecuteScalarAfterHandleException Utility+DAOUtil", oException);
                    
                    return oObject;
                }
                finally
                {
                    //if (m_oSqlConnection != null)
                    //{
                    //    m_oSqlConnection.Close();
                    //}
                }
            }
            else
            {
                return oObject;
            }

            logger.Info("End GetExecuteScalarAfterHandleException Utility+DAOUtil");
            
            return oObject;
        }
    }
}
