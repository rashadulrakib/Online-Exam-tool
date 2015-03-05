using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Entity;
using DAO;
//using Logging;
using Utility;

namespace BO
{
    public class LevelBO
    {
        public LevelBO()
        { 
        
        }

        public Result LevelEntry(Level oLevel)
        {
            Result oResult = new Result();

            try
            {
                LevelDAO oLevelDAO = new LevelDAO();

                oResult = oLevelDAO.LevelEntry(oLevel);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Level Entry..";
            }

            return oResult;
        }



        public Result LoadAllLevels()
        {
            Result oResult = new Result();

            try
            {
                LevelDAO oLevelDAO = new LevelDAO();

                oResult = oLevelDAO.LoadAllLevels();
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Load All Levels..";
            }

            return oResult;
        }

        public Result LevelUpdate(Level oLevel)
        {
            Result oResult = new Result();

            try
            {
                LevelDAO oLevelDAO = new LevelDAO();

                oResult = oLevelDAO.LevelUpdate(oLevel);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Level Update..";
            }

            return oResult;
        }

        public Result Delete(Level oLevel)
        {
            Result oResult = new Result();

            try
            {
                LevelDAO oLevelDAO = new LevelDAO();

                oResult = oLevelDAO.Delete(oLevel);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Level Delete..";
            }

            return oResult;
        }
    }
}
