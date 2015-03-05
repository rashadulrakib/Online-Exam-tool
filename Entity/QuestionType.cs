using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is QuestionType entity class.
    /// It defines QuestionType information
    /// </summary>
    
    [Serializable]
    public class QuestionType
    {
        private int ID;

        public int QuestionTypeID
        {
            get { return ID; }
            set { ID = value; }
        }
                
        private String Name;

        public String QuestionTypeName
        {
            get { return Name; }
            set { Name = value; }
        }

        private int totalQuestions;

        public int QuestionTypeTotalQuestions //this is for tree view/ result view 
        {
            get { return totalQuestions; }
            set { totalQuestions = value; }
        }

        private float fAvg;

        public float AVGQuestionTypeMark //this is not used
        {
            get { return fAvg; }
            set { fAvg = value; }
        }
    }
}
