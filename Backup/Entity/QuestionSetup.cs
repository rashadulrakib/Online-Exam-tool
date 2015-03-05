using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is QuestionSetup entity class.
    /// It defines which question is setup for which exam
    /// </summary>
    
    [Serializable]
    public class QuestionSetup
    {
        public QuestionSetup()
        {
            Initialization();
        }

        private Exam oExam;

        public Exam QuestionSetupExam
        {
            get { return oExam; }
            set { oExam = value; }
        }

        private Question oQuestion;

        public Question QuestionSetupQuestion
        {
            get { return oQuestion; }
            set { oQuestion = value; }
        }

        private float Mark;

        public float QuestionSetupMark
        {
            get { return Mark; }
            set { Mark = value; }
        }

        private SystemUser oSystemUser ;

        public SystemUser QuestionSetupSystemUser
        {
            get { return oSystemUser ; }
            set { oSystemUser  = value; }
        }

        private DateTime GenerationTime;

        public DateTime QuestionSetupGenerationTime
        {
            get { return GenerationTime; }
            set { GenerationTime = value; }
        }

        private void Initialization()
        {
            this.oExam = new Exam();
            this.oQuestion = new Question();
            this.oSystemUser = new SystemUser();
        }
    }
}
