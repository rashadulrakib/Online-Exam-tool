using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is cadidate entity class.
    /// For Candidate Information
    /// </summary>
    
    [Serializable]
    public class Candidate
    {
        public Candidate()
        {
            Initialization();
        }

        private String CompositeID;

        public String CandidateCompositeID
        {
            get { return CompositeID; }
            set { CompositeID = value; }
        }

        private String sEmail;

        public String CandidateEmail
        {
            get { return sEmail; }
            set { sEmail = value; }
        }

        private String Password;

        public String CandidatePassword
        {
            get { return Password; }
            set { Password = value; }
        }

        private String Name;

        public String CandidateName
        {
            get { return Name; }
            set { Name = value; }
        }

        private float LastResult;

        public float CandidateLastResult
        {
            get { return LastResult; }
            set { LastResult = value; }
        }

        private float LastResultRange;

        public float CandidateLastResultRange
        {
            get { return LastResultRange; }
            set { LastResultRange = value; }
        }

        private String LastResultType;

        public String LastResultTypaName
        {
            get { return LastResultType; }
            set { LastResultType = value; }
        }

        private String CPicture;

        public String CandidatePicturePath
        {
            get { return CPicture; }
            set { CPicture = value; }
        }

        private String LastInstitution;

        public String CandiadteLastInstitution
        {
            get { return LastInstitution; }
            set { LastInstitution = value; }
        }

        private int LastPassingYear;

        public int CandidateLastPassingYear
        {
            get { return LastPassingYear; }
            set { LastPassingYear = value; }
        }
	
        private String CvPath;

        public String CandidateCvPath
        {
            get { return CvPath; }
            set { CvPath = value; }
        }

        //private CandidateExam oCandidateExam;

        //public CandidateExam CadidateCandidateExam
        //{
        //    get { return oCandidateExam; }
        //    set { oCandidateExam = value; }
        //}

        private DateTime oDateTime;

        public DateTime CandidateLoginTime
        {
            get { return oDateTime; }
            set { oDateTime = value; }
        }

        private void Initialization()
        {
            this.Password = String.Empty;
            this.CPicture = String.Empty;
        }
    }
}
