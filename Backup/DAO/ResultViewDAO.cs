using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;
using Entity;
using Utility;
using log4net;
using log4net.Config;

namespace DAO
{
    /// <summary>
    /// This class is used for result show 
    /// </summary>
    
    public class ResultViewDAO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ResultViewDAO));
        
        public ResultViewDAO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This method load question categories with their types of an exam
        /// </summary>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result LoadCategoriesWithTypeForAnExam(Exam oExam)
        {
            logger.Info("Start LoadCategoriesWithTypeForAnExam ResultViewDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelect = String.Empty;

            List<CandidateMenu> oListCandidateMenu = new List<CandidateMenu>();

            SqlDataReader oSqlDataReader = null;

            try
            {
                sSelect = "select distinct EX_Category.CategoryID, EX_Category.CategoryName from EX_Category inner join EX_Question on EX_Category.CategoryID=EX_Question.QuestionCategoryID inner join  EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='" +oExam.ExamID + "' order by EX_Category.CategoryID asc";

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    CandidateMenu oCandidateMenu = new CandidateMenu();

                    oCandidateMenu.CandidateMenuCategory.CategoryID = int.Parse(oSqlDataReader["CategoryID"].ToString());
                    oCandidateMenu.CandidateMenuCategory.CategoryName = oSqlDataReader["CategoryName"].ToString();

                    oListCandidateMenu.Add(oCandidateMenu);
                }

                oSqlDataReader.Close();

                foreach (CandidateMenu oCandidateMenuInList in oListCandidateMenu)
                {
                    sSelect = "select distinct EX_QuestionType.TypeID, EX_QuestionType.TypeName from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID=EX_Question.QuestionTypeID inner join  EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "' and EX_Question.QuestionCategoryID ='" + oCandidateMenuInList.CandidateMenuCategory.CategoryID + "' order by EX_QuestionType.TypeID asc";

                    List<QuestionType> oListQuestionType = new List<QuestionType>();

                    oSqlDataReader = oDAOUtil.GetReader(sSelect);

                    while (oSqlDataReader.Read())
                    {
                        QuestionType oQuestionType = new QuestionType();

                        oQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["TypeID"].ToString());
                        oQuestionType.QuestionTypeName = oSqlDataReader["TypeName"].ToString();

                        oListQuestionType.Add(oQuestionType);
                    }

                    oSqlDataReader.Close();

                    //foreach (QuestionType oQuestionTypeInList in oListQuestionType)
                    //{
                    //    sSelect = "select count(EX_Question.QuestionID) as TotalQuestions from EX_Question inner join EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "' and EX_Question.QuestionCategoryID ='" + oCandidateMenuInList.CandidateMenuCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestionTypeInList.QuestionTypeID + "'";

                    //    oQuestionTypeInList.QuestionTypeTotalQuestions = int.Parse(oDAOUtil.GetExecuteScalar(sSelect).ToString());
                    //}

                    oCandidateMenuInList.CandidateMenuCategoryQuestionType = oListQuestionType;
                }

                oResult.ResultObject = oListCandidateMenu;
                oResult.ResultMessage = "Candidate Menu Category with type Load Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception in Candidate Menu Category with type Load for Candidate...";
                oResult.ResultException = oEx;

                logger.Info("Exception LoadCategoriesWithTypeForAnExam ResultViewDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End LoadCategoriesWithTypeForAnExam ResultViewDAO+DAO");
            
            return oResult;
        }

        /// <summary>
        /// This method sums the question categoty & question type wise obtain mark & total mark.
        /// </summary>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <param name="oListCandidateMenu"> It takes List<CandidateMenu> Object </param>
        /// <returns> It returns Result Object </returns>
        public Result GetCandidateAVGMarksForTypeAndCategory(Exam oExam, List<CandidateMenu> oListCandidateMenu)
        {
            logger.Info("Start GetCandidateTotalMarksForTypeAndCategory ResultViewDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelect = String.Empty;

            SqlDataReader oSqlDataReader = null;

            //CandidateResultsView oCandidateResultsView = new CandidateResultsView();

            //oCandidateResultsView.ListCandidates = new List<Candidate>();
            //oCandidateResultsView.ListOfListCandidateResults = new List<List<CandidateMenu>>();
            List<Candidate> oListCandidate = new List<Candidate>();

            Object oObject = new Object();

            
            try
            {

                DataTable dt = new DataTable();

                DataColumn dcolName = new DataColumn("Name", typeof(System.String));
                dt.Columns.Add(dcolName);

                float fTotalMarksForColumn = 0f;
                
                foreach (CandidateMenu oCandidateMenuInList in oListCandidateMenu) //columns
                {
                    foreach (QuestionType oQuestionTypeInList in oCandidateMenuInList.CandidateMenuCategoryQuestionType) //columns
                    {
                        float fMarkForAColumn = 0f;

                        object oObjectForMarkForAColumn = new object();

                        sSelect = " select sum(EX_QuestionGeneration.SetupQuestionMark) as TotalSetupMarkOfTypeOfCategory"
                                + " from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID"
                                + " inner join EX_QuestionType on EX_QuestionType.TypeID = EX_Question.QuestionTypeID"
                                + " where EX_QuestionGeneration.ExamID='"+oExam.ExamID+"'"
                                + " and EX_Question.QuestionCategoryID='" + oCandidateMenuInList.CandidateMenuCategory.CategoryID + "'"
                                + " and EX_QuestionType.TypeID='" + oQuestionTypeInList.QuestionTypeID + "'";

                        oObjectForMarkForAColumn = oDAOUtil.GetExecuteScalar(sSelect);

                        if (oObjectForMarkForAColumn.ToString().Length <= 0)
                        {
                            fMarkForAColumn = 0f;
                        }
                        else
                        {
                            fMarkForAColumn = float.Parse(oObjectForMarkForAColumn.ToString());
                        }

                        fTotalMarksForColumn = fTotalMarksForColumn + fMarkForAColumn;

                        DataColumn dcolCategory = new DataColumn(oCandidateMenuInList.CandidateMenuCategory.CategoryName + "(" + oQuestionTypeInList.QuestionTypeName + ") = " + fMarkForAColumn.ToString(), typeof(System.Double));
                        dt.Columns.Add(dcolCategory);
                    }
                }

                DataColumn dcolTotal = new DataColumn("Total = " + fTotalMarksForColumn.ToString(), typeof(System.Double));
                dt.Columns.Add(dcolTotal);
                
                
                
                
                
                
                
                
                sSelect = "select distinct EX_CandidateExam.CandidateID,EX_Candidate.Name from"
                +" EX_Candidate inner join EX_CandidateExam on "
                +" EX_Candidate.CompositeCandidateID=EX_CandidateExam.CandidateID"
                +" where EX_CandidateExam.ExamID='"+oExam.ExamID+"'";
                
                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    Candidate oCandidate = new Candidate();

                    oCandidate.CandidateCompositeID = oSqlDataReader["CandidateID"].ToString();
                    oCandidate.CandidateName = oSqlDataReader["Name"].ToString();

                    oListCandidate.Add(oCandidate);
                }

                oSqlDataReader.Close();

                //int iCandidateCount = 0;

                foreach (Candidate oCandidateInList in oListCandidate)
                {
                    DataRow drow = dt.NewRow();

                    int iColumnIndex = 0;

                    //********orginal*****//
                    //drow["Name"] = oCandidateInList.CandidateName;
                    drow[iColumnIndex++] = oCandidateInList.CandidateName;
                    //*******end orginal**********//



                    float fTotalMarks = 0f;


                    
                    
                    //List<CandidateMenu> oListCandidateMenuForView = new List<CandidateMenu>();

                    //for (int i = 0; i < oListCandidateMenu.Count; i++)
                    //{
                    //    oListCandidateMenuForView.Add(oListCandidateMenu[i]);
                    //}

                    //oCandidateResultsView.ListOfListCandidateResults.Add(oListCandidateMenuForView);

                    foreach (CandidateMenu oCandidateMenuInList in oListCandidateMenu)
                    {
                        //int iTypeCount = 0;
                        
                        foreach (QuestionType oQuestionTypeInList in oCandidateMenuInList.CandidateMenuCategoryQuestionType)
                        {
                            //sSelect = "select sum(EX_CandidateExam.ObtainMark) as AvgTypeMark, EX_QuestionType.TypeID,EX_QuestionType.TypeName from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID = EX_Question.QuestionTypeID inner join EX_QuestionGeneration on EX_Question.QuestionID= EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_QuestionGeneration.QuestionID = EX_CandidateExam.QuestionID group by EX_CandidateExam.ExamID,EX_CandidateExam.CandidateID,EX_Question.QuestionCategoryID,EX_QuestionType.TypeID,EX_QuestionType.TypeName 
                            //having EX_CandidateExam.ExamID='" + oExam.ExamID + "' and EX_CandidateExam.CandidateID='" + oCandidateInList.CandidateCompositeID + "' 
                            //and EX_Question.QuestionCategoryID='" + oCandidateMenuInList.CandidateMenuCategory.CategoryID + "' and EX_QuestionType.TypeID='" + oQuestionTypeInList.QuestionTypeID+ "'";

                            sSelect = "select sum(EX_CandidateExam.ObtainMark) as AvgTypeMark, EX_QuestionType.TypeID,EX_QuestionType.TypeName" 
                            +" from EX_CandidateExam inner join EX_Question on EX_CandidateExam.QuestionID=EX_Question.QuestionID" 
                            +" inner join EX_QuestionType on EX_QuestionType.TypeID = EX_Question.QuestionTypeID"
                            + " where EX_CandidateExam.ExamID='" + oExam.ExamID + "' and"
                            + " EX_CandidateExam.CandidateID='" + oCandidateInList.CandidateCompositeID + "'"
                            +" and EX_Question.QuestionCategoryID='" + oCandidateMenuInList.CandidateMenuCategory.CategoryID + "'"
                            + " and EX_QuestionType.TypeID='" + oQuestionTypeInList.QuestionTypeID + "'"
                            +" group by EX_CandidateExam.ExamID,EX_CandidateExam.CandidateID,EX_Question.QuestionCategoryID,"
                            +" EX_QuestionType.TypeID,EX_QuestionType.TypeName";

                            oObject = oDAOUtil.GetExecuteScalar(sSelect);

                            if (oObject.ToString().Length <= 0)
                            {
                                //oQuestionTypeInList.AVGQuestionTypeMark = 0f;

                                
                                //********orginal*****//
                                //drow[oCandidateMenuInList.CandidateMenuCategory.CategoryName + "(" + oQuestionTypeInList.QuestionTypeName + ")"] = 0f;
                                drow[iColumnIndex++] = 0f;
                                //*******end orginal**********//


                                fTotalMarks = fTotalMarks + 0f;

                                //oCandidateMenuInList.CandidateMenuCategoryQuestionType[iTypeCount].AVGQuestionTypeMark = 0f;

                            }
                            else
                            {
                                //oQuestionTypeInList.AVGQuestionTypeMark = float.Parse(oObject.ToString());
                                //oCandidateMenuInList.CandidateMenuCategoryQuestionType[iTypeCount].AVGQuestionTypeMark = float.Parse(oObject.ToString());

                                //********orginal*****//
                                //drow[oCandidateMenuInList.CandidateMenuCategory.CategoryName + "(" + oQuestionTypeInList.QuestionTypeName + ")"] = float.Parse(oObject.ToString());
                                drow[iColumnIndex++] = float.Parse(oObject.ToString());
                                //*******end orginal**********//

                                fTotalMarks = fTotalMarks + float.Parse(oObject.ToString());
                            }

                            //iTypeCount=iTypeCount+1;
                        }
                    }
                    
                    //iCandidateCount = iCandidateCount + 1;
                    //oCandidateResultsView.ListOfListCandidateResults.Add(oListCandidateMenu);


                    //********orginal*****//
                    //drow["Total"] = fTotalMarks;
                    drow[iColumnIndex++] = fTotalMarks;
                    //*******end orginal**********//

                    dt.Rows.Add(drow);
                }



                oResult.ResultObject = dt;
                oResult.ResultMessage = "GetCandidateAVGMarksForTypeAndCategory Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "GetCandidateAVGMarksForTypeAndCategory Exception...";

                logger.Info("Exception GetCandidateAVGMarksForTypeAndCategory ResultViewDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End GetCandidateAVGMarksForTypeAndCategory ResultViewDAO+DAO");

            return oResult;
        }
    }
}
