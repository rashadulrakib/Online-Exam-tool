using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// This is result class to store result of any function.
    /// This class is used to return result type object from any method
    /// </summary>
    public class Result
    {
        public Result()
        { 
        
        }

        private Boolean IsSuccess;

        public Boolean ResultIsSuccess
        {
            get { return IsSuccess; }
            set { IsSuccess = value; }
        }

        private Exception oException;

        public Exception ResultException
        {
            get { return oException; }
            set { oException = value; }
        }

        private Object oObject;

        public Object ResultObject
        {
            get { return oObject; }
            set { oObject = value; }
        }

        private String Message;

        public String ResultMessage
        {
            get { return Message; }
            set { Message = value; }
        }
	
    }
}
