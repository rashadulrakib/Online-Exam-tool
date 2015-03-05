using System;
using System.Collections.Generic;
using System.Text;
using Common;
using log4net;
using log4net.Config;

namespace Utility
{
    /// <summary>
    /// This class is used for Load Mail From XML File
    /// </summary>
    public class MailUtil
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MailUtil));
        
        public MailUtil()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This method deserialize an xml file to an object of a particular type. this is a general method
        /// </summary>
        /// <param name="oType"> It takes Type Parameter</param>
        /// <param name="sFileName"> It takes a file name to be deserialized </param>
        /// <returns> It returns Result Object </returns>
        public Result LoadMail(Type oType, String sFileName)
        {
            logger.Info("Start LoadMail MailUtil+Utility");
            
            Result oResult = new Result();
            
            try
            {
                oResult.ResultMessage = "LoadMail Success..";
                oResult.ResultObject = new XMLUtil().GetConfigFile(oType, sFileName);
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "LoadMail Exception..";

                logger.Info("Exception LoadMail MailUtil+Utility",oEx);
            }

            logger.Info("End LoadMail MailUtil+Utility");

            return oResult;
        }
    }
}
