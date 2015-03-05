using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is SystemUser entity class.
    /// It defines SystemUser information
    /// </summary>
    
    [Serializable]
    public class SystemUser
    {
        public SystemUser()
        {
            Initialization();
        }

        private void Initialization()
        {
            this.SystemUserEmail = String.Empty;
        }

        private Guid ID;

        public Guid SystemUserID
        {
            get { return ID; }
            set { ID = value; }
        }

        private String Name;

        public String SystemUserName
        {
            get { return Name; }
            set { Name = value; }
        }

        private String Password;

        public String SystemUserPassword
        {
            get { return Password; }
            set { Password = value; }
        }

        private String Email;

        public String SystemUserEmail
        {
            get { return Email; }
            set { Email = value; }
        }
	
        private DateTime oDateTime;

        public DateTime DeleteTime
        {
            get { return oDateTime; }
            set { oDateTime = value; }
        }
    }
}
