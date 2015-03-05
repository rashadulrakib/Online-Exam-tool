using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is Level entity class.
    /// It defines a Level information
    /// </summary>
    
    [Serializable]
    public class Level
    {
        public Level()
        { 
        
        }

        private Guid ID;

        public Guid LevelID
        {
            get { return ID; }
            set { ID = value; }
        }

        private String Name;

        public String LevelName
        {
            get { return Name; }
            set { Name = value; }
        }
        private String Description;

        public String LevelDescription
        {
            get { return Description; }
            set { Description = value; }
        }
    }
}

