using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;
using Entity;
using Utility;
using log4net;
using log4net.Config;

namespace DAO
{
    /// <summary>
    /// This class is used for SystemUser manipulation
    /// </summary>
    
    public class SystemUserDAO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SystemUserDAO));
        
        public SystemUserDAO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This method checks the systemuser login
        /// </summary>
        /// <param name="oSystemUser"> It takes SystemUser Object </param>
        /// <returns> It returns Result Object </returns>
        public Result SystemUserLogin(SystemUser oSystemUser)
        {
            logger.Info("Start SystemUserLogin SystemUserDAO+DAO");
            
            Result oResult = new Result();
            SqlDataReader oSqlDataReader = null;

            try
            {
                DAOUtil oDAOUtil = new DAOUtil();
                SystemUser oPopulatedSystemUser = new SystemUser();
                
                Boolean flag = false;

                String sSelect = "select SystemUserID,SystemUserName,SystemUserPassword,EmailAddress from EX_SystemUser where SystemUserName='" + oSystemUser.SystemUserName + "' and SystemUserPassword='" + oSystemUser.SystemUserPassword + "' and DeleteTime is NULL";
                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                if (oSqlDataReader.HasRows)
                {
                    while (oSqlDataReader.Read())
                    {
                        oPopulatedSystemUser.SystemUserID = new Guid(oSqlDataReader["SystemUserID"].ToString());
                        oPopulatedSystemUser.SystemUserName = oSqlDataReader["SystemUserName"].ToString();
                        oPopulatedSystemUser.SystemUserPassword = oSqlDataReader["SystemUserPassword"].ToString();
                        oPopulatedSystemUser.SystemUserEmail = oSqlDataReader["EmailAddress"].ToString();

                        flag = true;
                    }

                    oSqlDataReader.Close();

                    if (flag)
                    {
                        oResult.ResultObject = oPopulatedSystemUser;
                        oResult.ResultMessage = "Login Success...";
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "System User Login Fail...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "System User Not Found...";
                }
                
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "SystemUserLogin Exception...";
                oResult.ResultException = oEx;

                logger.Info("Exception SystemUserLogin SystemUserDAO+DAO",oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End SystemUserLogin SystemUserDAO+DAO");
            
            return oResult;
        }

        /// <summary>
        /// This method inserts systemuser, if that systemuser name is not existed
        /// </summary>
        /// <param name="oSystemUser"> It takes SystemUser Object </param>
        /// <returns> It returns Result Object </returns>
        public Result SystemUserEntry(SystemUser oSystemUser)
        {
            logger.Info("Start SystemUserEntry SystemUserDAO+DAO");
            
            Result oResult = new Result();

            List<SqlCommand> oListSqlCommand = new List<SqlCommand>();
            List<int> oListInt = new List<int>();

            try
            {
                DAOUtil oDAOUtil = new DAOUtil();

                //Boolean flag = true;

                //String sSelect = "select SystemUserName from EX_SystemUser where (SystemUserName='" + oSystemUser.SystemUserName + "' or EmailAddress='" + oSystemUser.SystemUserEmail + "') and DeleteTime is NULL";
                //String sInsert = String.Empty;
                //List<String> oListOfString = new List<String>();

                //oSqlDataReader = oDAOUtil.GetReader(sSelect);

                //if (oSqlDataReader.HasRows)
                //{
                //    flag = false;
                //}

                //oSqlDataReader.Close();

                //if (flag)
                //{
                //    oSystemUser.SystemUserID = Guid.NewGuid();

                //    sInsert = "if not exists(select SystemUserName from EX_SystemUser where (SystemUserName='" + oSystemUser.SystemUserName + "' or EmailAddress='" + oSystemUser.SystemUserEmail + "')" 
                //    +" and DeleteTime is NULL)"
                //    +" insert into EX_SystemUser(SystemUserID,SystemUserName,SystemUserPassword,EmailAddress)"
                //    +" values('" + oSystemUser.SystemUserID + "','" + oSystemUser.SystemUserName + "','" + oSystemUser.SystemUserPassword + "','" + oSystemUser.SystemUserEmail + "')";
                //    oListOfString.Add(sInsert);

                //    if (oDAOUtil.ExecuteNonQuery(oListOfString))
                //    {
                //        oResult.ResultObject = oSystemUser;
                //        oResult.ResultMessage = "System User Entry Success...";
                //        oResult.ResultIsSuccess = true;
                //    }
                //    else
                //    {
                //        oResult.ResultIsSuccess = false;
                //        oResult.ResultMessage = "System User Entry Failed...";
                //    }
                //}
                //else
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "System User is already Existed...";
                //}


                SqlCommand oSqlCommand = new SqlCommand("SP_SystemUserEntry");
                oSqlCommand.CommandType = CommandType.StoredProcedure;

                oSystemUser.SystemUserID = Guid.NewGuid();

                oSqlCommand.Parameters.Add("@SystemUserID", SqlDbType.UniqueIdentifier);
                oSqlCommand.Parameters["@SystemUserID"].Value = oSystemUser.SystemUserID;

                oSqlCommand.Parameters.Add("@SystemUserName", SqlDbType.VarChar);
                oSqlCommand.Parameters["@SystemUserName"].Value = oSystemUser.SystemUserName;

                oSqlCommand.Parameters.Add("@SystemUserPassword", SqlDbType.VarChar);
                oSqlCommand.Parameters["@SystemUserPassword"].Value = oSystemUser.SystemUserPassword;

                oSqlCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                oSqlCommand.Parameters["@EmailAddress"].Value = oSystemUser.SystemUserEmail;

                oListSqlCommand.Add(oSqlCommand);

                oListInt = oDAOUtil.ExecuteNonQueryForStoredProcedure(oListSqlCommand);

                if (oListInt.Count > 0)
                {
                    if (oListInt[0] > 0)
                    {
                        oResult.ResultObject = oSystemUser;
                        oResult.ResultMessage = "System User Entry Success...";
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "System User Name or Email is already Existed...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "SystemUser Entry Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "System User Entry Exception...";

                logger.Info("Exception SystemUserEntry SystemUserDAO+DAO", oEx);
            }

            logger.Info("End SystemUserEntry SystemUserDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method show all systemusers
        /// </summary>
        /// <returns> It returns Result Object </returns>
        public Result ShowAllSystemUsers()
        {
            logger.Info("Start ShowAllSystemUsers SystemUserDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            List<SystemUser> oListSystemUser = new List<SystemUser>();

            String sSelect = String.Empty;

            DateTime oDateTime = DateTime.MinValue;

            try
            {
                sSelect = "select SystemUserID,SystemUserName,SystemUserPassword,DeleteTime,EmailAddress from EX_SystemUser where DeleteTime is NULL order by SystemUserName asc";

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    SystemUser oSystemUser = new SystemUser();

                    oSystemUser.SystemUserID = new Guid(oSqlDataReader["SystemUserID"].ToString());
                    oSystemUser.SystemUserName = oSqlDataReader["SystemUserName"].ToString();
                    oSystemUser.SystemUserPassword = oSqlDataReader["SystemUserPassword"].ToString();
                    oSystemUser.SystemUserEmail = oSqlDataReader["EmailAddress"].ToString();

                    if (DateTime.TryParse(oSqlDataReader["DeleteTime"].ToString(), out oDateTime))
                    {
                        oSystemUser.DeleteTime = DateTime.Parse(oSqlDataReader["DeleteTime"].ToString());   
                    }

                    oListSystemUser.Add(oSystemUser);
                }

                oSqlDataReader.Close();

                oResult.ResultObject = oListSystemUser;
                oResult.ResultMessage = "System User Load Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "System User Show Exception...";

                logger.Info("Exception ShowAllSystemUsers SystemUserDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End ShowAllSystemUsers SystemUserDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method update the systemuser delete time, indicating that, the systemuser is deleted.
        /// </summary>
        /// <param name="oListSystemUser"> It takes List<SystemUser> Object </param>
        /// <param name="iArrCheck"> It takes integer array which indicates the marked system user</param>
        /// <returns> It returns Result Object </returns>
        public Result SystemUserDelete(List<SystemUser> oListSystemUser, int[] iArrCheck)
        {
            logger.Info("Start SystemUserDelete SystemUserDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            List<SystemUser> oListSystemUser1 = new List<SystemUser>();

            List<String> oListString = new List<String>();

            String sUpdate = String.Empty;

            int i = 0;

            try
            {
                for (i = 0; i < iArrCheck.Length; i++)
                {
                    if (iArrCheck[i] == 1)
                    {
                        sUpdate = "update EX_SystemUser set DeleteTime='" + DateTime.Now + "' where SystemUserID='" + oListSystemUser[i].SystemUserID + "'";
                        oListString.Add(sUpdate);
                        oListSystemUser1.Add(null);
                    }
                    else
                    {
                        oListSystemUser1.Add(oListSystemUser[i]);
                    }
                }

                oListSystemUser1.RemoveAll(delegate(SystemUser oSystemUser) { if (oSystemUser != null) { return false; } return true; });

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultObject = oListSystemUser1;
                    oResult.ResultMessage = "System User Delete Success...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "System User Delete Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during System User Delete...";

                logger.Info("Exception SystemUserDelete SystemUserDAO+DAO", oEx);
            }

            logger.Info("End SystemUserDelete SystemUserDAO+DAO");

            return oResult;
        }


        /// <summary>
        /// This method update the systemuser
        /// It updates systemuser name, if the name is not duplicated
        /// </summary>
        /// <param name="oListSystemUser"> It takes List<SystemUser> Object </param>
        /// <param name="iArrCheck"> It takes integer array which indicates the marked system user</param>
        /// <returns> It returns Result Object </returns>
        public Result UpdateSystemUser(List<SystemUser> oListSystemUser, int[] iArrCheck)
        {
            logger.Info("Start UpdateSystemUser SystemUserDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            //List<String> oListString = new List<String>();

            List<SqlCommand> oListSqlCommand = new List<SqlCommand>();

            List<int> oListInt = new List<int>();

            //String sUpdate = String.Empty;

            int i = 0;

            try
            {
                for (i = 0; i < iArrCheck.Length; i++)
                {
                    if (iArrCheck[i] == 1)
                    {
                        //sUpdate = "if not exists(select SystemUserName from EX_SystemUser where SystemUserName='" + oListSystemUser[i].SystemUserName + "') update EX_SystemUser set SystemUserName='" + oListSystemUser[i].SystemUserName + "',SystemUserPassword='" + oListSystemUser[i].SystemUserPassword + "',EmailAddress='" + oListSystemUser[i].SystemUserEmail + "' where SystemUserID='" + oListSystemUser[i].SystemUserID + "' and DeleteTime is NULL";

                        SqlCommand oSqlCommand = new SqlCommand("SP_SystemUserModification");
                        oSqlCommand.CommandType = CommandType.StoredProcedure;

                        oSqlCommand.Parameters.Add("@SystemUserID", SqlDbType.UniqueIdentifier);
                        oSqlCommand.Parameters["@SystemUserID"].Value = oListSystemUser[i].SystemUserID;

                        oSqlCommand.Parameters.Add("@SystemUserName", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@SystemUserName"].Value = oListSystemUser[i].SystemUserName;

                        oSqlCommand.Parameters.Add("@SystemUserPassword", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@SystemUserPassword"].Value = oListSystemUser[i].SystemUserPassword;

                        oSqlCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@EmailAddress"].Value = oListSystemUser[i].SystemUserEmail;

                        oListSqlCommand.Add(oSqlCommand);

                        
                        //sUpdate = "update EX_SystemUser set"
                        //        + " SystemUserName="
                        //        + " ( case when not exists(select SystemUserName from EX_SystemUser where SystemUserName='" + oListSystemUser[i].SystemUserName + "')"
                        //        + " then  '" + oListSystemUser[i].SystemUserName + "' else '" + oListSystemUser[i].SystemUserName + "' end )"
                        //        + " ,SystemUserPassword='" + oListSystemUser[i].SystemUserPassword + "'"
                        //        + ",EmailAddress= ( case  when not exists(select EmailAddress from EX_SystemUser where EmailAddress='" + oListSystemUser[i].SystemUserEmail + "')"
                        //        + " then '" + oListSystemUser[i].SystemUserEmail + "' else '" + oListSystemUser[i].SystemUserEmail + "' end )"
                        //        + " where SystemUserID='" + oListSystemUser[i].SystemUserID + "' and DeleteTime is NULL";
                        
                        //oListString.Add(sUpdate);

                    }
                }

                oListInt = oDAOUtil.ExecuteNonQueryForStoredProcedure(oListSqlCommand);

                if (oListInt.Count > 0)
                {
                    oResult.ResultObject = oListSystemUser;
                    oResult.ResultMessage = "SystemUser Update Success(if email or name not update, then it is duplicate)...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "SystemUser Update Failed...";
                }
                
                
                //if (oDAOUtil.ExecuteNonQuery(oListString))
                //{
                //    oResult.ResultObject = oListSystemUser;
                //    oResult.ResultMessage = "SystemUser Update Success(if email or name not update, then it is duplicate)...";
                //    oResult.ResultIsSuccess = true;
                //}
                //else
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "SystemUser Update Fail...";
                //}
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "SystemUser Update Exception...";

                logger.Info("Exception UpdateSystemUser SystemUserDAO+DAO", oEx);
            }

            logger.Info("End UpdateSystemUser SystemUserDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method change the systemuser password
        /// </summary>
        /// <param name="oSystemUser"> It takes SystemUser Object </param>
        /// <param name="sNewPassword"> It takes String Object</param>
        /// <param name="sConfirmPassword"> It takes String Object</param>
        /// <returns> It returns Result Object </returns>
        public Result ChangePassword(SystemUser oSystemUser, String sNewPassword, String sConfirmPassword)
        {
            logger.Info("Start ChangePassword SystemUserDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sUpadte = String.Empty;

            List<String> oListString = new List<String>();

            try
            {
                sUpadte = "update EX_SystemUser set SystemUserPassword='" + sNewPassword + "' where SystemUserID='" + oSystemUser.SystemUserID + "' and SystemUserPassword='" + oSystemUser.SystemUserPassword + "' and DeleteTime is NULL";

                oListString.Add(sUpadte);

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oSystemUser.SystemUserPassword = sNewPassword;
                    
                    oResult.ResultObject = oSystemUser;
                    oResult.ResultMessage = "Your password have been changed successfully...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "SystemUser ChangePassword Fail...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "SystemUser ChangePassword Exception...";

                logger.Info("Exception ChangePassword SystemUserDAO+DAO", oEx);
            }

            logger.Info("End ChangePassword SystemUserDAO+DAO");

            return oResult;
        }
    }
}
