using System;
using System.Collections.Generic;
using System.Text;
using Common;
using log4net;
using log4net.Config;

namespace Utility
{

    /// <summary>
    /// This is used to get current time from database
    /// </summary>
    public class GetCurrentTimeFromDataBase
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(GetCurrentTimeFromDataBase));
        
        public GetCurrentTimeFromDataBase()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        /// <summary>
        /// This Methods provides to get current time from database
        /// </summary>
        /// <param name="sFormatNumber"> It takes format string to convert the sqlserver datetime </param>
        /// <returns> return result</returns>
        public Result GetCurrentTime(String sFormatNumber)
        {
            logger.Info("start GetCurrentTime Utility+GetCurrentTimeFromDataBase");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelect = String.Empty;

            Object oObject = null;

            try
            {
                if (!sFormatNumber.Equals(String.Empty))
                {
                    sSelect = "select convert(varchar, getdate(), " + sFormatNumber + ") as currentTime";

                    oObject=oDAOUtil.GetExecuteScalar(sSelect);

                    if (oObject != null)
                    {
                        oResult.ResultObject = oObject;
                        oResult.ResultMessage = "Get time from DataBase success...";
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Get time from DataBase failed...";
                    }
                }
                else
                {
                    sSelect = "select getdate() as currentTime";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception at geting time from DataBase...";
                oResult.ResultException = oEx;

                logger.Info("Esception GetCurrentTime Utility+GetCurrentTimeFromDataBase",oEx);
            }

            logger.Info("End GetCurrentTime Utility+GetCurrentTimeFromDataBase");

            return oResult;
        }
    }
}
