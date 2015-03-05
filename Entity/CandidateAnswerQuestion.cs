using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Entity
{
    /// <summary>
    /// This is CandidateAnswerQuestion entity class.
    /// For Candidate Answer Information for a Question
    /// </summary>
    
    [Serializable]
    public class CandidateAnswerQuestion
    {
        public CandidateAnswerQuestion()
        {
            Initialization();   
        }

        private Question oQuestion;

        public Question QuestionForCandidateAnswer
        {
            get { return oQuestion; }
            set { oQuestion = value; }
        }

        private String AnswerText;

        public String DescriptiveQuestionAnswerText
        {
            get { return AnswerText; }
            set { AnswerText = value; }
        }

        private String sAttachFilePath;

        public String sAnswerAttachFilePath
        {
            get { return sAttachFilePath; }
            set { sAttachFilePath = value; }
        }

        private Byte[] oArr;

        public Byte[] ClientFileBytes
        {
            get { return oArr; }
            set { oArr = value; }
        }

        private float Mark;

        public float ObtainMark
        {
            get { return Mark; }
            set { Mark = value; }
        }

        private void Initialization()
        {
            this.oQuestion = new Question();
            this.AnswerText = String.Empty;
            //this.oFileUpload = new FileUpload();
        }
    }
}
