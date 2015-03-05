using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    /// <summary>
    /// This is Exam entity class.
    /// It defines a Exam information
    /// </summary>
    
    [Serializable]
    public class Exam
    {
        public Exam()
        {
            Initialization();
        }

        private Guid ID;

        public Guid ExamID
        {
            get { return ID; }
            set { ID = value; }
        }

        private String Name;

        public String ExamName
        {
            get { return Name; }
            set { Name = value; }
        }

        private int TotalMarks;

        public int ExamTotalMarks
        {
            get { return TotalMarks; }
            set { TotalMarks = value; }
        }

        private DateTime DateWithStartingTime;

        public DateTime ExamDateWithStartingTime
        {
            get { return DateWithStartingTime; }
            set { DateWithStartingTime = value; }
        }

        private float DurationinHour;

        public float ExamDurationinHour
        {
            get { return DurationinHour; }
            set { DurationinHour = value; }
        }

        private int Constraint;

        public int ExamConstraint
        {
            get { return Constraint; }
            set { Constraint = value; }
        }
        private void Initialization()
        {
            this.Constraint = 0;
        }
    }
}
