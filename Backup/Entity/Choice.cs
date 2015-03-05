using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is Choice entity class.
    /// It defines a Choice information
    /// </summary>
    
    [Serializable]
    public class Choice
    {
        public Choice()
        { 
        
        }

        private String Name;

        public String ChoiceName
        {
            get { return Name; }
            set { Name = value; }
        }

        private Boolean IsValid;

        public Boolean ChoiceIsValid
        {
            get { return IsValid; }
            set { IsValid = value; }
        }
    }
}
