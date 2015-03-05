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
	declare @iTotalRowsInEX_CandidateForExam int
	
	select @iTotalRowsInEX_QuestionGeneration =
	count(EX_QuestionGeneration.ExamID) from EX_QuestionGeneration 
	where EX_QuestionGeneration.ExamID=@ExamID

	select @iTotalRowsInEX_CandidateExam =
	count(EX_CandidateExam.ExamID) from EX_CandidateExam 
	where EX_CandidateExam.ExamID=@ExamID

	select @iTotalRowsInEX_CandidateForExam =
	count(EX_CandidateForExam.ExamID) from EX_CandidateForExam 
	where EX_CandidateForExam.ExamID=@ExamID

	if @iTotalRowsInEX_QuestionGeneration =0 and @iTotalRowsInEX_CandidateExam=0 and @iTotalRowsInEX_CandidateForExam=0
		begin
			delete from EX_Exam where ExamID=@ExamID
		end

--Procedure for Candidate Setup
CREATE PROCEDURE SP_CandidateSetup
(
	@CompositeCandidateID as varchar(200),
	@CandidatePassword as varchar(200),
	@Name as varchar(100),
	@LastResult as float,
	@LastInstitution as varchar(50),
	@LastPassingYear as int,
	@CvPath as varchar(400),
	@EmailAddress as varchar(100),
	@LastResultRange as float,
	@LastResultTypeName as varchar(100),
	@CandidatePicturePath as varchar(400),
	@CandidateID as varchar(200),
	@ExamID as uniqueidentifier

)
AS
	if not exists(select EmailAddress from EX_Candidate where EmailAddress=@EmailAddress)
		begin
			insert into EX_Candidate(CompositeCandidateID,CandidatePassword,Name,LastResult,LastInstitution,
									LastPassingYear,CvPath,EmailAddress,LastResultRange,LastResultTypeName,
									CandidatePicturePath)
						values(@CompositeCandidateID,@CandidatePassword,@Name,@LastResult,@LastInstitution,
							@LastPassingYear,@CvPath,@EmailAddress,@LastResultRange,
							@LastResultTypeName,@CandidatePicturePath);

			insert into EX_CandidateForExam(CandidateID,ExamID)
					values(@CandidateID,@ExamID)		

		end 



--Procedure for CandidateUpdate
create PROCEDURE SP_CandidateUpdate
(
	@CompositeCandidateID as varchar(200),
	@CandidatePassword as varchar(200),
	@Name as varchar(100),
	@LastResult as float,
	@LastInstitution as varchar(50),
	@LastPassingYear as int,
	@CvPath as varchar(400),
	@EmailAddress as varchar(100),
	@LastResultRange as float,
	@LastResultTypeName as varchar(100),
	@CandidatePicturePath as varchar(400)
)
as
	declare @iTotalRowsForCandidateEmail int

	declare @sUpdatedEmail varchar(100)		

	select @iTotalRowsForCandidateEmail = 
	count(EmailAddress) from EX_Candidate 
	where EmailAddress=@EmailAddress

	if @iTotalRowsForCandidateEmail=0
		set @sUpdatedEmail=@EmailAddress
	else
		select @sUpdatedEmail=EmailAddress from EX_Candidate 
		where CompositeCandidateID=@CompositeCandidateID

	update EX_Candidate set 
		CandidatePassword=@CandidatePassword ,
		Name=@Name ,
		LastResult=@LastResult ,
		LastInstitution=@LastInstitution ,
		LastPassingYear=@LastPassingYear ,
		CvPath=@CvPath,
		EmailAddress=@sUpdatedEmail,
		LastResultRange=@LastResultRange ,
		LastResultTypeName=@LastResultTypeName ,
		CandidatePicturePath=@CandidatePicturePath 
	where CompositeCandidateID=@CompositeCandidateID


--procedure for Level Update
create PROCEDURE SP_LevelUpdate
(
	@LabelID as uniqueidentifier,
	@LabelName as varchar(200),
	@LabelPrerequisite as varchar(1000)
)
as
	declare @iTotalRowsForLevelName int

	declare @sUpdatedLevelName varchar(200)

	select @iTotalRowsForLevelName= 
	count(LabelName) from EX_Label
	where LabelName=@LabelName

	if @iTotalRowsForLevelName=0
		set @sUpdatedLevelName=@LabelName
	else
		select @sUpdatedLevelName = 
		LabelName from EX_Label where LabelID=@LabelID
	
	update EX_Label set
		LabelName=@sUpdatedLevelName,
		LabelPrerequisite = @LabelPrerequisite
	where LabelID=@LabelID
		 		
	



--Procedure for SystemUser Setup
if exists (select * from sysobjects where name = N'SP_SystemUserEntry')

   drop procedure SP_SystemUserEntry

go
CREATE PROCEDURE SP_SystemUserEntry
(
	@SystemUserID uniqueidentifier,
	@SystemUserName varchar(200),
	@SystemUserPassword varchar(200),
	@EmailAddress varchar(200)
)
as	
	declare @iTotalRowsForSystemUserName int
	declare @iTotalRowsForSystemUserEmail int

	select @iTotalRowsForSystemUserName = 
	count(SystemUserName) from EX_SystemUser 
	where SystemUserName=@SystemUserName and DeleteTime is null

	select @iTotalRowsForSystemUserEmail = 
	count(EmailAddress) from EX_SystemUser 
	where EmailAddress=@EmailAddress and DeleteTime is null
	
	if(@iTotalRowsForSystemUserName=0 and @iTotalRowsForSystemUserEmail=0)
		begin
			insert into EX_SystemUser(SystemUserID,SystemUserName,SystemUserPassword,EmailAddress)
			values(@SystemUserID,@SystemUserName,@SystemUserPassword,@EmailAddress)
		end


--Procedure for SystemUser Update
if exists (select * from sysobjects where name = N'SP_SystemUserModification')

   drop procedure SP_SystemUserModification

go

create PROCEDURE SP_SystemUserModification
(
	@SystemUserID uniqueidentifier,
	@SystemUserName varchar(200),
	@SystemUserPassword varchar(200),
	@EmailAddress varchar(200)
)
as
	declare @iTotalRowsForSystemUserName int
	declare @iTotalRowsForSystemUserEmail int

	declare @sUpdatedName varchar(200)
	declare @sUpdatedEmail varchar(200)

	select @iTotalRowsForSystemUserName = 
	count(SystemUserName) from EX_SystemUser 
	where SystemUserName=@SystemUserName and DeleteTime is null

	select @iTotalRowsForSystemUserEmail = 
	count(EmailAddress) from EX_SystemUser 
	where EmailAddress=@EmailAddress and DeleteTime is null

	if @iTotalRowsForSystemUserName=0 
			set @sUpdatedName=@SystemUserName
	else
			select @sUpdatedName=SystemUserName from EX_SystemUser 
			where SystemUserID=@SystemUserID and DeleteTime is null
	
	if @iTotalRowsForSystemUserEmail=0
			set @sUpdatedEmail=@EmailAddress
	else
			select @sUpdatedEmail=EmailAddress from EX_SystemUser 
			where SystemUserID=@SystemUserID and DeleteTime is null

	update EX_SystemUser set
		SystemUserName=@sUpdatedName,
		EmailAddress=@sUpdatedEmail,
		SystemUserPassword=@SystemUserPassword
	where SystemUserID=@SystemUserID and DeleteTime is null
	
	
	


--procedure for candidate login

create procedure SP_CandidateLogin
(
	@EmailAddress varchar(100),
	@CandidatePassword varchar(200),
	@LoginStatus int output --@LoginStatus=-1 when it will be populated from CODE
)
--@LoginStatus=0 -> candidate not found in the system
--@LoginStatus=1 -> candidate is already appeared for the exam 
--@LoginStatus=2 -> Exam is not started for the candidate
--@LoginStatus=3 -> Exam time is over for the candidate
--@LoginStatus=4 -> candidate login success for the exam
as
	declare @tblExamsInfo table (colExamID uniqueidentifier,
									colExamDateWithTime datetime,
									ExamDuration float)

	declare @varExamName varchar(100)
	declare @varExamID uniqueidentifier
	declare @varExamTotalMarks int
	declare @varExamDateWithTime datetime
	declare @varExamDuration float
	declare @varExamConstraint int
	declare @varCompositeCandidateID varchar(200)
	declare @varCandidatePassword varchar(200)
	declare @varName varchar(100)
	declare @varLastResult float
	declare @varLastResultRange float
	declare @varLastResultTypeName varchar(100)
	declare @varLastInstitution varchar(50)
	declare @varLastPassingYear int
	declare @varCvPath varchar(400)
	declare @varEmailAddress varchar(100)
	declare @varCandidatePicturePath varchar(400)


	declare @bFlagForAnyRows int
	

	if (@LoginStatus=-1)
		begin
			if not exists(select EX_Candidate.CompositeCandidateID from 
						EX_Candidate where EX_Candidate.EmailAddress=@EmailAddress and 
						EX_Candidate.CandidatePassword=@CandidatePassword)
			begin
				set @LoginStatus=0	
			end
		end
	else if (@LoginStatus=-1)
		begin
			
			set @bFlagForAnyRows=0

			declare cursorForInfo cursor read_only for 
				select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,
				EX_Exam.ExamTotalMarks,EX_Exam.ExamConstraint,EX_Exam.ExamID, 
				EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,
				EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,
				EX_Candidate.LastPassingYear,EX_Candidate.CvPath
				from EX_Exam,EX_CandidateForExam,EX_Candidate
				where EX_Exam.ExamID=EX_CandidateForExam.ExamID
				and EX_CandidateForExam.CandidateID=EX_Candidate.CompositeCandidateID
				and EX_Candidate.EmailAddress=@EmailAddress 
				and EX_Candidate.CandidatePassword=@CandidatePassword
				and EX_CandidateForExam.CandidateID not in 
					(select EX_CandidateExam.CandidateID from  EX_CandidateExam,EX_CandidateForExam
					where EX_CandidateForExam.ExamID=EX_CandidateExam.ExamID
					and EX_CandidateForExam.CandidateID=EX_CandidateExam.CandidateID)

			open cursorForInfo		
				
				fetch next from cursorForInfo into @varExamName,@varExamDateWithTime,@varExamDuration,@varExamTotalMarks,
					@varExamConstraint,@varExamID,
					@varCompositeCandidateID,@varCandidatePassword,@varName,@varLastResult,@varLastInstitution,
					@varLastPassingYear,@varCvPath
					while @@FETCH_STATUS = 0
						begin
							set @bFlagForAnyRows=1
							
													

							fetch next from cursorForInfo
						end

				

			close cursorForInfo
			deallocate cursorForInfo

			if(@bFlagForAnyRows=0)
				begin
					set @LoginStatus=1	
				end
		end
	 
	
			
	
		

		







--Proceduere for Get result in a procedure & use the result set
if exists (select * from sysobjects where name = N'SP_TestGetResultInProcedureAndUse')

   drop procedure SP_TestGetResultInProcedureAndUse

go
create PROCEDURE SP_TestGetResultInProcedureAndUse
as
	declare @SystemUserName varchar(300);

	DECLARE c1 CURSOR READ_ONLY
	FOR
	SELECT SystemUserName
	FROM EX_SystemUser where deletetime is null

	
	

	OPEN c1

	FETCH NEXT FROM c1
	INTO @SystemUserName

	WHILE @@FETCH_STATUS = 0
	BEGIN

		PRINT @SystemUserName

			

		FETCH NEXT FROM c1
		INTO @SystemUserName

	END

	CLOSE c1
	DEALLOCATE c1


exec SP_TestGetResultInProcedureAndUse


--Proceduer for nested cursor from WEB
if exists (select * from sysobjects where name = N'SP_TestGetResultInNestedProcedureAndUseWEB')

   drop procedure SP_TestGetResultInNestedProcedureAndUseWEB

go
create PROCEDURE SP_TestGetResultInNestedProcedureAndUseWEB
as
	DECLARE @IterationID INT, @OrderDetail VARCHAR(1024), @ProductName VARCHAR(10)
	DECLARE @Result TABLE (PurchaseOrderID INT, OrderDetail VARCHAR(1024))

	DECLARE curOrdersForReport CURSOR FOR
		SELECT PurchaseOrderID FROM Purchasing.PurchaseOrderHeader
		WHERE PurchaseOrderID between 50 and 60

	OPEN curOrdersForReport
		FETCH NEXT FROM curOrdersForReport INTO @IterationID
		PRINT 'OUTER LOOP'
		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			SET @OrderDetail = ''

			DECLARE curDetailList CURSOR FOR
				SELECT p.productNumber FROM Purchasing.PurchaseOrderDetail pd
				INNER JOIN Production.Product p ON pd.ProductID = p.ProductID
				WHERE pd.PurchaseOrderID = @IterationID

			OPEN curDetailList
				FETCH NEXT FROM curDetailList INTO @ProductName
				PRINT 'INNER LOOP'
				WHILE (@@FETCH_STATUS = 0)
					BEGIN
						SET @OrderDetail = @OrderDetail + @ProductName + ', '
						FETCH NEXT FROM curDetailList INTO @ProductName
						PRINT 'INNER LOOP'
					END
			CLOSE curDetailList
			DEALLOCATE curDetailList
			INSERT INTO @Result VALUES (@IterationID, @OrderDetail)
			FETCH NEXT FROM curOrdersForReport INTO @IterationID
			PRINT 'OUTER LOOP'
		END
	CLOSE curOrdersForReport
	DEALLOCATE curOrdersForReport
	SELECT * FROM @Result
	GO

	
	
--procedure for nested cursor
if exists (select * from sysobjects where name = N'SP_TestGetResultInNestedProcedureAndUse')

   drop procedure SP_TestGetResultInNestedProcedureAndUse

go
create PROCEDURE SP_TestGetResultInNestedProcedureAndUse
as
	declare @SystemUserID uniqueidentifier
	declare @SystemUserName varchar(200)
	declare @QuestionText varchar(1000)
	
	declare cursorForSystemUserID cursor for
		select SystemUserID,SystemUserName from EX_SystemUser

	open cursorForSystemUserID
		
		fetch next from cursorForSystemUserID into @SystemUserID,@SystemUserName
		while(@@fetch_status=0)
		begin
			print convert(varchar(255),@SystemUserID) + ' ' +@SystemUserName
			
				declare cursorforQuestionText cursor for
					select QuestionText 
					from EX_Question inner join EX_SystemUser 
					on EX_Question.QuestionCreatorID=EX_SystemUser.SystemUserID
					where EX_Question.QuestionCreatorID=@SystemUserID

				open cursorforQuestionText
			
				fetch next from cursorforQuestionText into @QuestionText
				while(@@fetch_status=0)
				begin
					print @QuestionText
					
					fetch next from cursorforQuestionText into @QuestionText
				end
				close cursorforQuestionText
				deallocate cursorforQuestionText
			

			fetch next from cursorForSystemUserID into @SystemUserID,@SystemUserName
		end
		



	close cursorForSystemUserID
	deallocate cursorForSystemUserID



