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
//using Logging;

namespace DAO
{
    /// <summary>
    /// This class is used for Category manipulation
    /// </summary>
    public class CategoryDAO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CategoryDAO));

        public CategoryDAO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        /// <summary>
        /// This method insert a category if it is not existed
        /// </summary>
        /// <param name="oCategory"> It takes Category Object </param>
        /// <returns> It returns Result Object </returns>
        public Result CategoryEntry(Category oCategory) //r
        {
            //new CLogger("Start CategoryEntry CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CategoryEntry CategoryDAO+DAO", ELogLevel.Debug);

            logger.Info("Start CategoryEntry CategoryDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelectCategory = String.Empty;
            String sInsertCategory = String.Empty;

            List<String> sListCategory = new List<String>();

            bool flag = true;

            sSelectCategory = "select CategoryName from EX_Category where CategoryName='" + oCategory.CategoryName + "'";

            try
            {
                oSqlDataReader = oDAOUtil.GetReader(sSelectCategory);

                if (oSqlDataReader.HasRows)
                {
                    flag = false;
                }

                oSqlDataReader.Close();

                if (flag)
                {
                    sInsertCategory = "insert into EX_Category(CategoryName) values('" + oCategory.CategoryName + "')";

                    sListCategory.Add(sInsertCategory);

                    if (oDAOUtil.ExecuteNonQuery(sListCategory))
                    {
                        oResult.ResultObject = oCategory;
                        oResult.ResultMessage = "Category Entry Success...";
                        oResult.ResultIsSuccess = true;

                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Category Entry Failed...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Category is already Existed";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Category Entry...";
                oResult.ResultException = oEx;

                logger.Info("Exception CategoryEntry CategoryDAO+DAO", oEx);

                //new CLogger("Exception CategoryEntry CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CategoryEntry CategoryDAO+DAO", ELogLevel.Debug, oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            //new CLogger("Out CategoryEntry CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CategoryEntry CategoryDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End CategoryEntry CategoryDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method loads the categories to set them in session
        /// </summary>
        /// <returns> It returns Result Object </returns>
        public Result CategoryGetFromDatabaseForSetSession() //r
        {
            //new CLogger("Start CategoryGetFromDatabaseForSetSession CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CategoryGetFromDatabaseForSetSession CategoryDAO+DAO", ELogLevel.Debug);

            logger.Info("Start CategoryGetFromDatabaseForSetSession CategoryDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelectCategory = String.Empty;
            String sCategoryName = String.Empty;

            SqlDataReader oSqlDataReader = null;

            List<Category> oListCategory = new List<Category>();

            int iCategoryID = 0;

            sSelectCategory = "select CategoryID,CategoryName from EX_Category";
            
            try
            {
                oSqlDataReader = oDAOUtil.GetReader(sSelectCategory);

                while (oSqlDataReader.Read())
                {
                    Category oCategory = new Category();

                    iCategoryID = int.Parse(oSqlDataReader["CategoryID"].ToString());
                    sCategoryName = oSqlDataReader["CategoryName"].ToString();

                    oCategory.CategoryID = iCategoryID;
                    oCategory.CategoryName = sCategoryName;

                    oListCategory.Add(oCategory);
                }

                oSqlDataReader.Close();

                oResult.ResultObject = oListCategory;
                oResult.ResultMessage = "Category Get success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Category Get...";

                logger.Info("Exception CategoryGetFromDatabaseForSetSession CategoryDAO+DAO", oEx);

                //new CLogger("Exception CategoryGetFromDatabaseForSetSession CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CategoryGetFromDatabaseForSetSession CategoryDAO+DAO", ELogLevel.Debug, oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            //new CLogger("Out CategoryGetFromDatabaseForSetSession CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CategoryGetFromDatabaseForSetSession CategoryDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End CategoryGetFromDatabaseForSetSession CategoryDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method Update name of a category if that name is not used for any category 
        /// </summary>
        /// <param name="oListCategory"> It takes List<Category> Object </param>
        /// <param name="iArrCheck"> It is an integer array to indicate the marked categories</param>
        /// <returns> It returns Result Object </returns>
        public Result CategoryUpdate(List<Category> oListCategory, int[] iArrCheck)
        {

            //new CLogger("Start CategoryUpdate CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CategoryUpdate CategoryDAO+DAO", ELogLevel.Debug);

            logger.Info("Start CategoryUpdate CategoryDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sUpdate = String.Empty;

            List<String> oListString = new List<String>();

            int i=0;

            try
            {
                for (i = 0; i < iArrCheck.Length; i++)
                {
                    if (iArrCheck[i] == 1)
                    {
                        //sUpdate = "update EX_Category set CategoryName='" + oListCategory[i].CategoryName + "' where CategoryID='" + oListCategory[i].CategoryID + "' and CategoryName not in (select CategoryName from EX_Category where CategoryName='" + oListCategory[i].CategoryName+"')";
                        sUpdate = "if not exists(select CategoryName from EX_Category where CategoryName='" + oListCategory[i].CategoryName + "') update EX_Category set CategoryName='" + oListCategory[i].CategoryName + "' where CategoryID='" + oListCategory[i].CategoryID + "'";
                        oListString.Add(sUpdate);
                    }
                }

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultObject = oListCategory;
                    oResult.ResultMessage = "Category Update Success(If not updated, then it is existed)...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Category Update Fail...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Category Update Exception...";

                //new CLogger("Exception CategoryUpdate CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CategoryUpdate CategoryDAO+DAO", ELogLevel.Debug, oEx);

                logger.Info("Exception CategoryUpdate CategoryDAO+DAO", oEx);
            }

            //new CLogger("Out CategoryUpdate CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CategoryUpdate CategoryDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End CategoryUpdate CategoryDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method Deletes a category if the category is not used for any question
        /// </summary>
        /// <param name="oListCategory"> It takes List<Category> Object </param>
        /// <param name="iArrCheck"> It is an integer array to indicate the marked categories</param>
        /// <returns> It returns Result Object </returns>
        public Result CategoryDelete(List<Category> oListCategory, int[] iArrCheck)
        {

            //new CLogger("Start CategoryDelete CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CategoryDelete CategoryDAO+DAO", ELogLevel.Debug);

            logger.Info("Start CategoryDelete CategoryDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sDelete = String.Empty;

            List<String> oListString = new List<String>();

            int i = 0;

            try
            {
                for (i = 0; i < iArrCheck.Length; i++)
                {
                    if (iArrCheck[i] == 1)
                    {
                        sDelete = "delete from EX_Category where CategoryID='" + oListCategory[i].CategoryID + "' and CategoryID not in (select QuestionCategoryID from EX_Question where QuestionCategoryID='" + oListCategory[i].CategoryID + "')";

                        oListString.Add(sDelete);
                    }
                }

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultObject = oListCategory; // This is not necessary Object
                    oResult.ResultMessage = "Category Delete Success(If not deleted then,it is used)...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Category Delete Fail...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Category Delete Exception...";

                logger.Info("Exception CategoryDelete CategoryDAO+DAO", oEx);

                //new CLogger("Exception CategoryDelete CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CategoryDelete CategoryDAO+DAO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out CategoryDelete CategoryDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CategoryDelete CategoryDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End CategoryDelete CategoryDAO+DAO");

            return oResult;
        }
    }
}
