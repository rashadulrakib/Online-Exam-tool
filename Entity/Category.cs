using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is Category entity class.
    /// It defines a category information
    /// </summary>
    
    [Serializable]
    public class Category
    {
        public Category()
        {

        }

        private int ID;

        public int CategoryID
        {
            get { return ID; }
            set { ID = value; }
        }
        private String Name;

        public String CategoryName
        {
            get { return Name; }
            set { Name = value; }
        }
    }
}
