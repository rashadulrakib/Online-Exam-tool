using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    /// Store the Database Information from DB.XML file
    /// Property: ConnectionString
    /// </summary>
    public class DBInfo
    {
        public DBInfo()
        {

        }

        private string m_sConnectionPath;

        public string ConnectionString
        {
            get { return m_sConnectionPath; }
            set { m_sConnectionPath = value; }
        }

    }
}
