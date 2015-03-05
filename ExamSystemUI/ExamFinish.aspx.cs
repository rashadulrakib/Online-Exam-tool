using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using Entity;
using Common;
using BO;

public partial class ExamFinish : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Result oResult = new Result();
        CandidateExamProcessBO oCandidateExamProcessBO = new CandidateExamProcessBO();
        
        try
        {
            List<CandidateAnswerQuestion> oListCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();

            oListCandidateAnswerQuestion = (List<CandidateAnswerQuestion>)Utils.GetSession(SessionManager.csExamProcess);

            CandidateForExam oCandidateForExam = Utils.GetSession(SessionManager.csLoggedUser) as CandidateForExam;

            oCandidateForExam.CadidateCandidateExam.CandidateAnsweredQuestions = oListCandidateAnswerQuestion;

            
            
            //save candidate Descriptive answer File
            try
            {
                if (!Directory.Exists(DirectoryManager.csCandidateCVDirectory + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamName + "\\" + oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID + "\\"))
                {
                    Directory.CreateDirectory(DirectoryManager.csCandidateCVDirectory + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamName + "\\" + oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID + "\\");
                }

                foreach (CandidateAnswerQuestion oCandidateAnswerQuestioninList in oCandidateForExam.CadidateCandidateExam.CandidateAnsweredQuestions)
                {
                    if (oCandidateAnswerQuestioninList.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID==1)
                    {
                        if (oCandidateAnswerQuestioninList.ClientFileBytes != null && oCandidateAnswerQuestioninList.ClientFileBytes.Length > 0)
                        {

                            File.WriteAllBytes(DirectoryManager.csCandidateCVDirectory + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamName + "\\" + oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID + "\\" + oCandidateAnswerQuestioninList.QuestionForCandidateAnswer.QuestionID.ToString() + "_" + oCandidateAnswerQuestioninList.sAnswerAttachFilePath, oCandidateAnswerQuestioninList.ClientFileBytes);
                            oCandidateAnswerQuestioninList.sAnswerAttachFilePath = oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamName + "\\" + oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID + "\\" + oCandidateAnswerQuestioninList.QuestionForCandidateAnswer.QuestionID.ToString() + "_" + oCandidateAnswerQuestioninList.sAnswerAttachFilePath;


                            //oCandidateAnswerQuestioninList.ClientFile.SaveAs(DirectoryManager.csCandidateCVDirectory + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamName + "\\" + oCandidate.CandidateCompositeID + "\\" + oCandidateAnswerQuestioninList.QuestionForCandidateAnswer.QuestionID.ToString() + oCandidateAnswerQuestioninList.ClientFile.FileName);
                            //oCandidateAnswerQuestioninList.sAnswerAttachFilePath = oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamName + "\\" + oCandidate.CandidateCompositeID + "\\" + oCandidateAnswerQuestioninList.QuestionForCandidateAnswer.QuestionID.ToString() + oCandidateAnswerQuestioninList.ClientFile.FileName; // Here sAnswerAttachFilePath is used to store the Descriptive Answer Attached File Path
                        }
                        else
                        {
                            oCandidateAnswerQuestioninList.sAnswerAttachFilePath = String.Empty;
                        }
                    }
                    else
                    {
                        oCandidateAnswerQuestioninList.sAnswerAttachFilePath = String.Empty;
                    }
                }
            }
            catch (Exception oEx1)
            {

            }
            //End save candidate Descriptive answer File


            oResult = oCandidateExamProcessBO.SaveCandidateAnswers(oCandidateForExam);

            if (oResult.ResultIsSuccess)
            {
                lbl_error.ForeColor = Color.Green;
                lbl_error.Text = oResult.ResultMessage;
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = oResult.ResultMessage;
            }

            Session.Abandon();

        }
        catch (Exception oEx2)
        {
            Session.Abandon();
        }
    }

    //private bool IsValidAttachedFile(FileUpload oFileUpload)
    //{
    //    if (!oFileUpload.HasFile)
    //    {
    //        return false;
    //    }

    //    if (oFileUpload.FileName.ToLower().LastIndexOf(".htm") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".html") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".doc") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".pdf") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".rtf") > 0)
    //    {

    //    }
    //    else
    //    {
    //        return false;
    //    }

    //    for (int i = 0; i < oFileUpload.FileName.Length; i++)
    //    {
    //        if (oFileUpload.FileName[i] == '-')
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}
}
