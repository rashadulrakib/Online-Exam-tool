using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is CandidateMenu entity class.
    /// It defines a question category with their types(objective,descriptive)
    /// </summary>
    
    [Serializable]
    public class CandidateMenu //this can be used for result view For showing Category Header in Grid View
    {
        public CandidateMenu()
        {
            Initialization();
        }

        private Category oCategory;

        public Category CandidateMenuCategory
        {
            get { return oCategory; }
            set { oCategory = value; }
        }

        private List<QuestionType> oCategoryType;

        public List<QuestionType> CandidateMenuCategoryQuestionType
        {
            get { return oCategoryType; }
            set { oCategoryType = value; }
        }

        private void Initialization()
        {
            this.oCategory = new Category();
            this.oCategoryType = new List<QuestionType>();
        }	
    }
}
