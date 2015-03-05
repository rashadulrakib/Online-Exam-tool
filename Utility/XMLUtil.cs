using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Security;
using log4net;
using log4net.Config;

namespace Utility
{
    /// <summary>
    /// This class is used for xml manipulation
    /// </summary>
    public class XMLUtil
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(XMLUtil));
        
        public XMLUtil()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This method parse any configuration file and returns an object.
        /// If you want to access it ,use the following example of next line
        /// Student oStudent= (Student)GetConfigFile(typeof(Student),"Student.xml");
        /// remember that the xml file should be in the bin folder
        /// </summary>
        public Object GetConfigFile(Type oType, string sFileName)
        {
            logger.Info("start GetConfigFile Utility+XMLUtil");
            
            Object oObject = new Object();

            try
            {
                string sFilePath = AppDomain.CurrentDomain.BaseDirectory + sFileName;
                FileStream oFileStream = new FileStream(sFilePath, FileMode.Open);
                XmlSerializer oXmlSerializer = new XmlSerializer(oType);

                oObject = oXmlSerializer.Deserialize(oFileStream);
                oFileStream.Close();
            }
            catch (Exception ex)
            {
                logger.Info("exception GetConfigFile Utility+XMLUtil",ex);
            }

            logger.Info("end GetConfigFile Utility+XMLUtil");
            return oObject;
        }
    }
}
