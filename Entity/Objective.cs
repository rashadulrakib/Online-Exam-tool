using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{

    /// <summary>
    /// This is Objective entity class.
    /// It defines a Objective information
    /// </summary>
    
    [Serializable]
    public class Objective
    {
        public Objective()
        {
            Initialization();
        }

        private List<Choice> Choices;

        public List<Choice> ListOfChoices // These choices are setup by the SystemUsers/Examiner
        {
            get { return Choices; }
            set { Choices = value; }
        }

        private List<Choice> Answers;

        public List<Choice> ListOfAnswers // These Answers are setup by the Candidates
        {
            get { return Answers; }
            set { Answers = value; }
        }
	
        private void Initialization()
        {
            this.Choices = new List<Choice>();
            this.Answers = new List<Choice>();
        }
    }
}
