using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is CandidateExam entity class.
    /// It is for Exam information with the candidate answers of questions
    /// </summary>
    
    [Serializable]
    public class CandidateExam
    {
        public CandidateExam()
        {
            Initialization();
        }

        private Exam oExam;

        public Exam CandiadteExamExam
        {
            get { return oExam; }
            set { oExam = value; }
        }

        private List<CandidateAnswerQuestion> oListCandidateAnswerQuestion;

        public List<CandidateAnswerQuestion> CandidateAnsweredQuestions
        {
            get { return oListCandidateAnswerQuestion; }
            set { oListCandidateAnswerQuestion = value; }
        }
        
        private void Initialization()
        {
            this.oExam = new Exam();
            this.oListCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();
        }
    }
}
