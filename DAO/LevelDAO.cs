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
    /// This class is used for Question Level manipulation
    /// </summary>
    public class LevelDAO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(LevelDAO));
        
        public LevelDAO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This method inserts a Level
        /// </summary>
        /// <param name="oLevel"> It takes Level Object </param>
        /// <returns> It returns Result Object </returns>
        public Result LevelEntry(Level oLevel)
        {
            logger.Info("Start LevelEntry LevelDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sInsert = String.Empty;

            //List<String> oListString = new List<String>();

            List<SqlCommand> oListSqlCommand = new List<SqlCommand>();
            List<int> oListInt = new List<int>();

            try
            {
                sInsert = "if not exists(select LabelName from EX_Label where LabelName='" + oLevel.LevelName + "') insert into EX_Label(LabelID,LabelName,LabelPrerequisite)"
                +" values('"+oLevel.LevelID+"','"+oLevel.LevelName+"','"+oLevel.LevelDescription+"')";

                //oListString.Add(sInsert);

                //if (oDAOUtil.ExecuteNonQuery(oListString))
                //{
                //    oResult.ResultObject = oLevel;
                //    oResult.ResultMessage = "Level Entry Success(if not inserted, then level name is existed)...";
                //    oResult.ResultIsSuccess = true;
                //}
                //else
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "Level Entry failed...";
                //}


                SqlCommand oSqlCommand = new SqlCommand(sInsert);
                oSqlCommand.CommandType = CommandType.Text;

                oListSqlCommand.Add(oSqlCommand);

                oListInt = oDAOUtil.ExecuteNonQueryForStoredProcedure(oListSqlCommand);

                if (oListInt.Count > 0)
                {
                    if (oListInt[0] > 0)
                    {
                        oResult.ResultObject = oLevel;
                        oResult.ResultMessage = "Level Entry Success...";
                        oResult.ResultIsSuccess = true;  
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Level name is existed...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Level Entry failed...";
                }
                

            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Level Entry...";

                logger.Info("Exception LevelEntry LevelDAO+DAO", oEx);
            }

            logger.Info("End LevelEntry LevelDAO+DAO");

            return oResult;
        }


        /// <summary>
        /// This method Load all levels
        /// </summary>
        /// <returns> It returns Result Object </returns>
        public Result LoadAllLevels()
        {
            logger.Info("Start LoadAllLevels LevelDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;

            List<Level> oListLevel = new List<Level>();

            try
            {
                sSelect = "select EX_Label.LabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite from"
                +" EX_Label order by EX_Label.LabelName";

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    Level oLevel = new Level();

                    oLevel.LevelID = new Guid(oSqlDataReader["LabelID"].ToString());
                    oLevel.LevelName = oSqlDataReader["LabelName"].ToString();
                    oLevel.LevelDescription = oSqlDataReader["LabelPrerequisite"].ToString();

                    oListLevel.Add(oLevel);
                }

                oSqlDataReader.Close();

                oResult.ResultObject = oListLevel;
                oResult.ResultMessage = "Load All Levels Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Load All Levels Exception...";

                logger.Info("Exception LoadAllLevels LevelDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader != null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End LoadAllLevels LevelDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method updates level information
        /// </summary>
        /// <param name="oLevel"> It takes Level Object </param>
        /// <returns> It returns Result Object </returns>
        public Result LevelUpdate(Level oLevel)
        {
            logger.Info("Start LevelUpdate LevelDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            //String sUpdate = String.Empty;

            //List<String> oListString = new List<String>();

            List<SqlCommand> oListSqlCommand = new List<SqlCommand>();
            List<int> oListInt = new List<int>();

            try
            {
                //sUpdate = "if not exists(select LabelName from EX_Label where LabelName='" + oLevel.LevelName + "')"
                //+ " update EX_Label set LabelName='" + oLevel.LevelName 
                //+ "',LabelPrerequisite='" + oLevel.LevelDescription 
                //+ "' where LabelID='" + oLevel.LevelID + "'";

                //sUpdate = "update EX_Label set" 
                //    + " LabelName=" 
                //    + " ( case when not exists" 
                //    + " (select LabelName from EX_Label where LabelName='" + oLevel.LevelName + "')" 
                //    + " then '" + oLevel.LevelName + "'" 
                //    + " end )," 
                //    + " LabelPrerequisite='" + oLevel.LevelDescription 
                //    + "' where LabelID='" + oLevel.LevelID + "'"; 



                //oListString.Add(sUpdate);

                //if (oDAOUtil.ExecuteNonQuery(oListString))
                //{
                //    oResult.ResultObject = oLevel;
                //    oResult.ResultMessage = "Level Update Success(if not updated, then level name is existed)...";
                //    oResult.ResultIsSuccess = true;
                //}
                //else
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "Level Update failed...";
                //}

                SqlCommand oSqlCommand = new SqlCommand("SP_LevelUpdate");
                oSqlCommand.CommandType = CommandType.StoredProcedure;

                oSqlCommand.Parameters.Add("@LabelID", SqlDbType.UniqueIdentifier);
                oSqlCommand.Parameters["@LabelID"].Value = oLevel.LevelID;

                oSqlCommand.Parameters.Add("@LabelName", SqlDbType.VarChar);
                oSqlCommand.Parameters["@LabelName"].Value = oLevel.LevelName;

                oSqlCommand.Parameters.Add("@LabelPrerequisite", SqlDbType.VarChar);
                oSqlCommand.Parameters["@LabelPrerequisite"].Value = oLevel.LevelDescription;

                oListSqlCommand.Add(oSqlCommand);

                oListInt = oDAOUtil.ExecuteNonQueryForStoredProcedure(oListSqlCommand);

                if (oListInt.Count > 0)
                {
                    if (oListInt[0] > 0)
                    {
                        oResult.ResultObject = oLevel;
                        oResult.ResultMessage = "Level Update Success...";
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Level name is existed...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Level Update failed...";
                }

            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Level Update Exception...";

                logger.Info("Exception LevelUpdate LevelDAO+DAO", oEx);
            }

            logger.Info("End LevelUpdate LevelDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method deletes a level if that level is not
        /// used for any question
        /// </summary>
        /// <param name="oLevel"> It takes Level Object </param>
        /// <returns> It returns Result Object </returns>
        public Result Delete(Level oLevel)
        {
            logger.Info("Start Delete LevelDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sDelete= String.Empty;

            //List<String> oListString = new List<String>();

            List<SqlCommand> oListSqlCommand = new List<SqlCommand>();
            List<int> oListInt = new List<int>();

            try
            {
                sDelete = "delete from EX_Label where EX_Label.LabelID='" + oLevel.LevelID + "' and EX_Label.LabelID not in (select EX_Question.QuestionLabelID from EX_Question where EX_Question.QuestionLabelID='" + oLevel.LevelID + "')";

                //oListString.Add(sDelete);

                //if (oDAOUtil.ExecuteNonQuery(oListString))
                //{
                //    oResult.ResultObject = oLevel;
                //    oResult.ResultMessage = "Level Delete Success(if not deleted, then it is used)...";
                //    oResult.ResultIsSuccess = true;
                //}
                //else
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "Level Delete failed...";
                //}



                SqlCommand oSqlCommand = new SqlCommand(sDelete);
                oSqlCommand.CommandType = CommandType.Text;

                oListSqlCommand.Add(oSqlCommand);

                oListInt = oDAOUtil.ExecuteNonQueryForStoredProcedure(oListSqlCommand);

                if (oListInt.Count > 0)
                {
                    if (oListInt[0] > 0)
                    {
                        oResult.ResultObject = oLevel;
                        oResult.ResultMessage = "Level Delete Success...";
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Level name is used...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Level Delete failed...";
                }

            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Level Delete Exception...";

                logger.Info("Exception Delete LevelDAO+DAO", oEx);
            }

            logger.Info("End Delete LevelDAO+DAO");

            return oResult;
        }
    }
}
