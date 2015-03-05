--Procedure for exam delete
CREATE PROCEDURE SP_ExamDelete
(
	@ExamID uniqueidentifier,
	@UserID uniqueidentifier,
	@UserName varchar(200),
	@UserPassword varchar(200)
)
AS
 	declare @iTotalRowsInEX_QuestionGeneration int
	declare @iTotalRowsInEX_CandidateExam int
	declare @iTotalRowsInEX_Candidate int
	
	
	select @iTotalRowsInEX_QuestionGeneration =
	count(EX_QuestionGeneration.ExamID) from EX_QuestionGeneration 
	where EX_QuestionGeneration.ExamID=@ExamID

	select @iTotalRowsInEX_CandidateExam =
	count(EX_CandidateExam.ExamID) from EX_CandidateExam 
	where EX_CandidateExam.ExamID=@ExamID

	select @iTotalRowsInEX_Candidate =
	count(EX_Candidate.ExamID) from EX_Candidate 
	where EX_Candidate.ExamID=@ExamID

	if @iTotalRowsInEX_QuestionGeneration =0 and @iTotalRowsInEX_CandidateExam=0 and @iTotalRowsInEX_Candidate=0
		begin
			delete from EX_Exam where ExamID=@ExamID
		end
	
		
	
	

	

	
   
 

	





	
	

	

