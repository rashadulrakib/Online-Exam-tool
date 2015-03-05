// -----------------------------------------------------------------------------
// File:        CLogger.cs
// Module:      Logging, version 3.0
// Version:     1.0
// Date:        2007.04.11
// Authors:     Rahima Shaheen rs@pyxisnet.com
// -----------------------------------------------------------------------------
// Description: The purpose of this class to log all the information 
// based on the log setting
//
// -----------------------------------------------------------------------------
// Copyright:   © 2007 Pyxisnet
// -----------------------------------------------------------------------------
// History      version / versiondate / author
//              versiondescription
// -----------------------------------------------------------------------------
// History:     1.0 / 2007.04.11 / Rahima Shaheen
//              First version.
//             
// -----------------------------------------------------------------------------


using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Config;
using log4net.Appender;
using log4net.Core;

using log4net.Repository; 


namespace Logging
{
    public class CLogger
    {

        protected log4net.Repository.Hierarchy.Logger m_oFPLogger = null;
        protected string sDefaultFileExtension = "log";


        /// <summary>
        /// constructor that initialize logger name, file name and log level
        /// </summary>
        public CLogger(string sLoggerName, string sFilename, int iLogLevel)
        {
            ILog oLogger = LogManager.GetLogger(sLoggerName);
            string sLogFileName = string.Empty;
            sLogFileName = GetLogFileName(sFilename);

            m_oFPLogger = (log4net.Repository.Hierarchy.Logger)oLogger.Logger;
            log4net.Appender.RollingFileAppender oFPAppender = new RollingFileAppender();
            oFPAppender.File = sLogFileName;
            oFPAppender.MaxSizeRollBackups = 100;
            oFPAppender.MaximumFileSize = "1MB";
            oFPAppender.RollingStyle = RollingFileAppender.RollingMode.Size;
            oFPAppender.StaticLogFileName = true;
            oFPAppender.AppendToFile = true;
            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout();
            layout.ConversionPattern = "%d %-5p - %m%n%exception";
            layout.ActivateOptions();
            oFPAppender.Layout = layout;
            oFPAppender.ActivateOptions();
            /** Programmatically configuration will not work it it is not set true**/
            m_oFPLogger.Hierarchy.Configured = true;

            try
            {
                m_oFPLogger.AddAppender(oFPAppender);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to Add Appender", ex);
            }
            m_oFPLogger.Hierarchy.Root.Level = m_oFPLogger.Hierarchy.LevelMap[GetLogLevel(iLogLevel)];

            oLogger = null;

        }


        /// <summary>
        /// This function derrives log file path from environment variable
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        protected string GetLogFileName(string sFileName)
        {
            string sLogfile = string.Empty;
            string[] arFileDirectory = sFileName.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            if (arFileDirectory.Length > 1)
            {
                if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(arFileDirectory[0])))
                {
                    sLogfile = sFileName;
                }
                else
                {
                    sLogfile = Environment.GetEnvironmentVariable(arFileDirectory[0]) + "\\" + arFileDirectory[1];
                }
            }
            else if (arFileDirectory.Length == 1)
            {
                sLogfile = arFileDirectory[0];
            }
            if (!HasFileExtension(sLogfile))
            {
                sLogfile += "." + sDefaultFileExtension;
            }

            return sLogfile;
        }

        /// <summary>
        /// This function checks whether the lo file has any extension
        /// </summary>
        /// <param name="sfilename"></param>
        /// <returns></returns>
        protected bool HasFileExtension(string sfilename)
        {
            bool bHasExtension = true;
            int iDotPosition = -1;
            iDotPosition = sfilename.IndexOf(".");
            if (iDotPosition > 0 && iDotPosition < (sfilename.Length - 1))
            {
                bHasExtension = true;
            }
            else
            {
                bHasExtension = false;
            }

            return bHasExtension;
        }


        /// <summary>
        /// This function gives system defined log level for a given integer number
        /// </summary>
        /// <param name="iLevel"></param>
        /// <returns></returns>
        protected string GetLogLevel(int iLevel)
        {
            switch (iLevel)
            {
                case (0):
                    {
                        return "ALL";
                    }
                case (1):
                    {
                        return "DEBUG";
                    }
                case (2):
                    {
                        return "INFO";
                    }
                case (3):
                    {
                        return "WARN";
                    }
                case (4):
                    {
                        return "ERROR";
                    }
                case (5):
                    {
                        return "FATAL";
                    }

                default:
                    return "DEBUG";
            }
        }


        /// <summary>
        /// This method writes log according to log level.
        /// In each message it adds caller name (method name) as a prefix
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eLevel"></param>
        public void WriteLog(string sMessage, ELogLevel eLevel)
        {
            WriteLogToFile( sMessage, eLevel, null);
        }

                         

        public void WriteLog(string message, ELogLevel eLevel, Exception oEx)
        {
            WriteLogToFile(message, eLevel, oEx);
        }

      
        /// <summary>
        /// This method writes log according to log level.
        /// In each message it adds caller name (method name) as a prefix
        /// </summary>
        /// <param name="sRequestID"></param>
        /// <param name="message"></param>
        /// <param name="eLevel"></param>
        protected void WriteLogToFile(string message, ELogLevel eLevel,Exception oEx)
        {
            
            StackFrame sf = null;
            sf = new StackFrame(2); 
            MethodBase mb = sf.GetMethod();
            string assemblyName = mb.DeclaringType.Assembly.GetName().Name;
            string classFunctionNames = mb.DeclaringType.Name + "." + mb.Name + "()";
            message = assemblyName + "." + classFunctionNames + ":: " + message;

            switch (Enum.GetName(typeof(ELogLevel), eLevel))
            {
                case "Debug":
                    if (m_oFPLogger.IsEnabledFor(log4net.Core.Level.Debug))
                    {
                        m_oFPLogger.Log(log4net.Core.Level.Debug, message, oEx);
                    }
                    break;
                case "Info":
                    if (m_oFPLogger.IsEnabledFor(log4net.Core.Level.Info))
                    {
                        m_oFPLogger.Log(log4net.Core.Level.Info, message, oEx);
                    }
                    break;
                case "Warn":
                    if (m_oFPLogger.IsEnabledFor(log4net.Core.Level.Warn))
                    {
                        m_oFPLogger.Log(log4net.Core.Level.Warn, message, oEx);
                    }
                    break;
                case "Error":
                    if (m_oFPLogger.IsEnabledFor(log4net.Core.Level.Error))
                    {
                        m_oFPLogger.Log(log4net.Core.Level.Error, message, oEx);
                    }
                    break;
                case "Fatal":
                    if (m_oFPLogger.IsEnabledFor(log4net.Core.Level.Fatal))
                    {
                        m_oFPLogger.Log(log4net.Core.Level.Fatal, message, oEx);
                    }
                    break;
                default:
                    break;
            }
        }

    }//class
}//ns
       