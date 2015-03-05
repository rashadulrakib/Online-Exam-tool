using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is CandidateForExam entity class.
    /// It defines, which candidate is apeared for which exam
    /// </summary>
    
    [Serializable]
    public class CandidateForExam
    {
        public CandidateForExam()
        {
            Initialization();
        }

        private Candidate oCandite;

        public Candidate CandidateForExamCandidate
        {
            get { return oCandite; }
            set { oCandite = value; }
        }

        private CandidateExam oCandidateExam;

        public CandidateExam CadidateCandidateExam
        {
            get { return oCandidateExam; }
            set { oCandidateExam = value; }
        }
        
        private void Initialization()
        {
            this.oCandite = new Candidate();
            this.oCandidateExam = new CandidateExam();

        }
    }
}
