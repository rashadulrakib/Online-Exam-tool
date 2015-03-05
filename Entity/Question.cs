using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is Question entity class.
    /// It defines a Question information
    /// </summary>
    
    [Serializable]
    public class Question
    {
        public Question()
        {
            Initialization();
        }

        private Guid ID;

        public Guid QuestionID
        {
            get { return ID; }
            set { ID = value; }
        }

        private String Text;

        public String QuestionText 
        {
            get { return Text; }
            set { Text = value; }
        }

        private float PossibleTime;

        public float QuestionPossibleAnswerTime
        {
            get { return PossibleTime; }
            set { PossibleTime = value; }
        }

        private SystemUser oSystemUser;

        public SystemUser QuestionCreator
        {
            get { return oSystemUser; }
            set { oSystemUser = value; }
        }

        private float DefaultMark;

        public float QuestionDefaultMark
        {
            get { return DefaultMark; }
            set { DefaultMark = value; }
        }

        private Category oCategory;

        public Category QuestionCategory
        {
            get { return oCategory; }
            set { oCategory = value; }
        }
  
        private QuestionType oQuestionType;

        public QuestionType QuestionQuestionType
        {
            get { return oQuestionType; }
            set { oQuestionType = value; }
        }

        private Objective ObjectiveType;

        public Objective QuestionObjectiveType
        {
            get { return ObjectiveType; }
            set { ObjectiveType = value; }
        }

        private Boolean IsUsed;

        public Boolean QuestionIsUsed
        {
            get { return IsUsed; }
            set { IsUsed = value; }
        }

        private Level QLevel;

        public Level QuestionLevel
        {
            get { return QLevel; }
            set { QLevel = value; }
        }

        private void Initialization()
        {
            this.oSystemUser = new SystemUser();
            this.oCategory = new Category();
            this.ObjectiveType = new Objective();
            this.oQuestionType = new QuestionType();
            this.QLevel = new Level();
            this.Text = String.Empty;
        }
    }
}
