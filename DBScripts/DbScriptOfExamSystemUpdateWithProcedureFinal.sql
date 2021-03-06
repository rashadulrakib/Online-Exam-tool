use master
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'DBExamSystemUpdate')
DROP DATABASE DBExamSystemUpdate
use master
create database DBExamSystemUpdate

USE [DBExamSystemUpdate]
GO
/****** Object:  Table [dbo].[EX_Label]    Script Date: 01/24/2008 15:12:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_Label](
	[LabelID] [uniqueidentifier] NOT NULL,
	[LabelName] [varchar](200) NOT NULL,
	[LabelPrerequisite] [varchar](1000) NULL,
 CONSTRAINT [PK_EX_Label] PRIMARY KEY CLUSTERED 
(
	[LabelID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_Candidate]    Script Date: 01/24/2008 15:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_Candidate](
	[CompositeCandidateID] [varchar](200) NOT NULL,
	[CandidatePassword] [varchar](200) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[LastResult] [float] NOT NULL,
	[LastResultRange] [float] NOT NULL,
	[LastResultTypeName] [varchar](100) NOT NULL,
	[LastInstitution] [varchar](50) NOT NULL,
	[LastPassingYear] [int] NOT NULL,
	[CvPath] [varchar](400) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[CandidatePicturePath] [varchar](400) NULL,
 CONSTRAINT [PK_EX_Candidate] PRIMARY KEY CLUSTERED 
(
	[CompositeCandidateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_Category]    Script Date: 01/24/2008 15:12:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_EX_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_QuestionType]    Script Date: 01/24/2008 15:13:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_QuestionType](
	[TypeID] [int] IDENTITY(0,1) NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_EX_QuestionType] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_Exam]    Script Date: 01/24/2008 15:12:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_Exam](
	[ExamID] [uniqueidentifier] NOT NULL,
	[ExamName] [varchar](100) NOT NULL,
	[ExamTotalMarks] [int] NOT NULL,
	[ExamDateWithTime] [datetime] NOT NULL,
	[ExamDuration] [float] NOT NULL,
	[ExamConstraint] [int] NOT NULL,
 CONSTRAINT [PK_EX_Exam] PRIMARY KEY CLUSTERED 
(
	[ExamID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_SystemUser]    Script Date: 01/24/2008 15:13:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_SystemUser](
	[SystemUserID] [uniqueidentifier] NOT NULL,
	[SystemUserName] [varchar](200) NOT NULL,
	[SystemUserPassword] [varchar](200) NOT NULL,
	[DeleteTime] [datetime] NULL,
	[EmailAddress] [varchar](100) NOT NULL,
 CONSTRAINT [PK_EX_SystemUser] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_Question]    Script Date: 01/24/2008 15:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_Question](
	[QuestionID] [uniqueidentifier] NOT NULL,
	[QuestionText] [varchar](1000) NOT NULL,
	[QuestionCreatorID] [uniqueidentifier] NOT NULL,
	[QuestionDefaultMark] [float] NOT NULL,
	[QuestionTypeID] [int] NOT NULL,
	[QuestionCategoryID] [int] NOT NULL,
	[QuestionPossibleAnswerTime] [float] NOT NULL,
	[QuestionLabelID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EX_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_CandidateForExam]    Script Date: 01/24/2008 15:12:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_CandidateForExam](
	[CandidateID] [varchar](200) NOT NULL,
	[ExamID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EX_CandidateForExam] PRIMARY KEY CLUSTERED 
(
	[CandidateID] ASC,
	[ExamID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_QuestionGeneration]    Script Date: 01/24/2008 15:13:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EX_QuestionGeneration](
	[ExamID] [uniqueidentifier] NOT NULL,
	[QuestionID] [uniqueidentifier] NOT NULL,
	[SetupQuestionMark] [float] NOT NULL,
	[GeneratorID] [uniqueidentifier] NOT NULL,
	[GenerationTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EX_QuestionGeneration] PRIMARY KEY CLUSTERED 
(
	[ExamID] ASC,
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EX_Objective]    Script Date: 01/24/2008 15:13:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_Objective](
	[ObjectiveQuestionID] [uniqueidentifier] NOT NULL,
	[ObjectiveAnswer] [varchar](1000) NOT NULL,
	[ObjectiveAnswerIsValid] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_CandidateExam]    Script Date: 01/24/2008 15:12:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_CandidateExam](
	[ExamID] [uniqueidentifier] NOT NULL,
	[QuestionID] [uniqueidentifier] NOT NULL,
	[CandidateID] [varchar](200) NOT NULL,
	[AnswerStringOrBits] [varchar](4000) NOT NULL,
	[ObtainMark] [float] NULL,
	[AnswerAttachmentPath] [varchar](400) NULL,
 CONSTRAINT [PK_EX_CandidateExam] PRIMARY KEY CLUSTERED 
(
	[ExamID] ASC,
	[QuestionID] ASC,
	[CandidateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_LevelUpdate]    Script Date: 01/24/2008 15:12:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SP_LevelUpdate]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_CandidateUpdate]    Script Date: 01/24/2008 15:12:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SP_CandidateUpdate]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_CandidateSetup]    Script Date: 01/24/2008 15:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CandidateSetup]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_ExamDelete]    Script Date: 01/24/2008 15:12:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Procedure for exam delete
CREATE PROCEDURE [dbo].[SP_ExamDelete]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_SystemUserEntry]    Script Date: 01/24/2008 15:12:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SystemUserEntry]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_SystemUserModification]    Script Date: 01/24/2008 15:12:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SP_SystemUserModification]
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
GO
/****** Object:  ForeignKey [FK_EX_CandidateExam_EX_CandidateForExam]    Script Date: 01/24/2008 15:12:50 ******/
ALTER TABLE [dbo].[EX_CandidateExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateExam_EX_CandidateForExam] FOREIGN KEY([CandidateID], [ExamID])
REFERENCES [dbo].[EX_CandidateForExam] ([CandidateID], [ExamID])
GO
ALTER TABLE [dbo].[EX_CandidateExam] CHECK CONSTRAINT [FK_EX_CandidateExam_EX_CandidateForExam]
GO
/****** Object:  ForeignKey [FK_EX_CandidateExam_EX_QuestionGeneration]    Script Date: 01/24/2008 15:12:50 ******/
ALTER TABLE [dbo].[EX_CandidateExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateExam_EX_QuestionGeneration] FOREIGN KEY([ExamID], [QuestionID])
REFERENCES [dbo].[EX_QuestionGeneration] ([ExamID], [QuestionID])
GO
ALTER TABLE [dbo].[EX_CandidateExam] CHECK CONSTRAINT [FK_EX_CandidateExam_EX_QuestionGeneration]
GO
/****** Object:  ForeignKey [FK_EX_CandidateForExam_EX_Candidate1]    Script Date: 01/24/2008 15:12:51 ******/
ALTER TABLE [dbo].[EX_CandidateForExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateForExam_EX_Candidate1] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[EX_Candidate] ([CompositeCandidateID])
GO
ALTER TABLE [dbo].[EX_CandidateForExam] CHECK CONSTRAINT [FK_EX_CandidateForExam_EX_Candidate1]
GO
/****** Object:  ForeignKey [FK_EX_CandidateForExam_EX_Exam1]    Script Date: 01/24/2008 15:12:52 ******/
ALTER TABLE [dbo].[EX_CandidateForExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateForExam_EX_Exam1] FOREIGN KEY([ExamID])
REFERENCES [dbo].[EX_Exam] ([ExamID])
GO
ALTER TABLE [dbo].[EX_CandidateForExam] CHECK CONSTRAINT [FK_EX_CandidateForExam_EX_Exam1]
GO
/****** Object:  ForeignKey [FK_EX_Objective_EX_Question]    Script Date: 01/24/2008 15:13:00 ******/
ALTER TABLE [dbo].[EX_Objective]  WITH CHECK ADD  CONSTRAINT [FK_EX_Objective_EX_Question] FOREIGN KEY([ObjectiveQuestionID])
REFERENCES [dbo].[EX_Question] ([QuestionID])
GO
ALTER TABLE [dbo].[EX_Objective] CHECK CONSTRAINT [FK_EX_Objective_EX_Question]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_Category]    Script Date: 01/24/2008 15:13:04 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_Category] FOREIGN KEY([QuestionCategoryID])
REFERENCES [dbo].[EX_Category] ([CategoryID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_Category]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_Label]    Script Date: 01/24/2008 15:13:04 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_Label] FOREIGN KEY([QuestionLabelID])
REFERENCES [dbo].[EX_Label] ([LabelID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_Label]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_QuestionType]    Script Date: 01/24/2008 15:13:04 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_QuestionType] FOREIGN KEY([QuestionTypeID])
REFERENCES [dbo].[EX_QuestionType] ([TypeID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_QuestionType]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_SystemUser]    Script Date: 01/24/2008 15:13:05 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_SystemUser] FOREIGN KEY([QuestionCreatorID])
REFERENCES [dbo].[EX_SystemUser] ([SystemUserID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_SystemUser]
GO
/****** Object:  ForeignKey [FK_EX_QuestionGeneration_EX_Exam]    Script Date: 01/24/2008 15:13:08 ******/
ALTER TABLE [dbo].[EX_QuestionGeneration]  WITH CHECK ADD  CONSTRAINT [FK_EX_QuestionGeneration_EX_Exam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[EX_Exam] ([ExamID])
GO
ALTER TABLE [dbo].[EX_QuestionGeneration] CHECK CONSTRAINT [FK_EX_QuestionGeneration_EX_Exam]
GO
/****** Object:  ForeignKey [FK_EX_QuestionGeneration_EX_Question]    Script Date: 01/24/2008 15:13:08 ******/
ALTER TABLE [dbo].[EX_QuestionGeneration]  WITH CHECK ADD  CONSTRAINT [FK_EX_QuestionGeneration_EX_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[EX_Question] ([QuestionID])
GO
ALTER TABLE [dbo].[EX_QuestionGeneration] CHECK CONSTRAINT [FK_EX_QuestionGeneration_EX_Question]
GO

insert into EX_SystemUser(SystemUserID,SystemUserName,SystemUserPassword,EmailAddress) values(newid(),'administrator','administrator','rr@pyxisnet.com')
insert into EX_QuestionType(TypeName) values('Objective')
insert into EX_QuestionType(TypeName) values('Descriptive')