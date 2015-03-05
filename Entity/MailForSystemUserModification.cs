using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is MailForSystemUserModification entity class.
    /// It defines a MailForSystemUserModification information
    /// </summary>
    
    [Serializable]
    public class MailForSystemUserModification
    {
        public MailForSystemUserModification()
        { 
        
        }

        private String sFrom;

        public String From
        {
            get { return sFrom; }
            set { sFrom = value; }
        }

        private String sSubject;

        public String Subject
        {
            get { return sSubject; }
            set { sSubject = value; }
        }

        private String sBodyStart;

        public String BodyStart
        {
            get { return sBodyStart; }
            set { sBodyStart = value; }
        }

        private String sBodyEnd;

        public String BodyEnd
        {
            get { return sBodyEnd; }
            set { sBodyEnd = value; }
        }
    }
}
