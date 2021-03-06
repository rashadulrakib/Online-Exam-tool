use master
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'DBExamSystemUpdate')
DROP DATABASE DBExamSystemUpdate
use master
create database DBExamSystemUpdate

USE [DBExamSystemUpdate]
GO
/****** Object:  Table [dbo].[EX_SystemUser]    Script Date: 12/12/2007 15:14:25 ******/
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
	[EmailAddress] [varchar](100) NULL,
 CONSTRAINT [PK_EX_SystemUser] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_Candidate]    Script Date: 12/12/2007 15:13:58 ******/
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
	[LastInstitution] [varchar](50) NOT NULL,
	[LastPassingYear] [int] NOT NULL,
	[CvPath] [varchar](400) NOT NULL,
	[EmailAddress] [varchar](100) NULL,
 CONSTRAINT [PK_EX_Candidate] PRIMARY KEY CLUSTERED 
(
	[CompositeCandidateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_Category]    Script Date: 12/12/2007 15:14:06 ******/
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
/****** Object:  Table [dbo].[EX_QuestionType]    Script Date: 12/12/2007 15:14:22 ******/
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
/****** Object:  Table [dbo].[EX_Exam]    Script Date: 12/12/2007 15:14:09 ******/
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
/****** Object:  Table [dbo].[EX_Question]    Script Date: 12/12/2007 15:14:15 ******/
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
 CONSTRAINT [PK_EX_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_QuestionGeneration]    Script Date: 12/12/2007 15:14:19 ******/
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
/****** Object:  Table [dbo].[EX_Objective]    Script Date: 12/12/2007 15:14:11 ******/
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
/****** Object:  Table [dbo].[EX_CandidateExam]    Script Date: 12/12/2007 15:14:01 ******/
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
/****** Object:  Table [dbo].[EX_CandidateForExam]    Script Date: 12/12/2007 15:14:04 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ExamDelete]    Script Date: 12/12/2007 15:13:53 ******/
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
/****** Object:  ForeignKey [FK_EX_CandidateExam_EX_CandidateForExam]    Script Date: 12/12/2007 15:14:02 ******/
ALTER TABLE [dbo].[EX_CandidateExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateExam_EX_CandidateForExam] FOREIGN KEY([CandidateID], [ExamID])
REFERENCES [dbo].[EX_CandidateForExam] ([CandidateID], [ExamID])
GO
ALTER TABLE [dbo].[EX_CandidateExam] CHECK CONSTRAINT [FK_EX_CandidateExam_EX_CandidateForExam]
GO
/****** Object:  ForeignKey [FK_EX_CandidateExam_EX_QuestionGeneration]    Script Date: 12/12/2007 15:14:02 ******/
ALTER TABLE [dbo].[EX_CandidateExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateExam_EX_QuestionGeneration] FOREIGN KEY([ExamID], [QuestionID])
REFERENCES [dbo].[EX_QuestionGeneration] ([ExamID], [QuestionID])
GO
ALTER TABLE [dbo].[EX_CandidateExam] CHECK CONSTRAINT [FK_EX_CandidateExam_EX_QuestionGeneration]
GO
/****** Object:  ForeignKey [FK_EX_CandidateForExam_EX_Candidate1]    Script Date: 12/12/2007 15:14:04 ******/
ALTER TABLE [dbo].[EX_CandidateForExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateForExam_EX_Candidate1] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[EX_Candidate] ([CompositeCandidateID])
GO
ALTER TABLE [dbo].[EX_CandidateForExam] CHECK CONSTRAINT [FK_EX_CandidateForExam_EX_Candidate1]
GO
/****** Object:  ForeignKey [FK_EX_CandidateForExam_EX_Exam1]    Script Date: 12/12/2007 15:14:04 ******/
ALTER TABLE [dbo].[EX_CandidateForExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateForExam_EX_Exam1] FOREIGN KEY([ExamID])
REFERENCES [dbo].[EX_Exam] ([ExamID])
GO
ALTER TABLE [dbo].[EX_CandidateForExam] CHECK CONSTRAINT [FK_EX_CandidateForExam_EX_Exam1]
GO
/****** Object:  ForeignKey [FK_EX_Objective_EX_Question]    Script Date: 12/12/2007 15:14:11 ******/
ALTER TABLE [dbo].[EX_Objective]  WITH CHECK ADD  CONSTRAINT [FK_EX_Objective_EX_Question] FOREIGN KEY([ObjectiveQuestionID])
REFERENCES [dbo].[EX_Question] ([QuestionID])
GO
ALTER TABLE [dbo].[EX_Objective] CHECK CONSTRAINT [FK_EX_Objective_EX_Question]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_Category]    Script Date: 12/12/2007 15:14:16 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_Category] FOREIGN KEY([QuestionCategoryID])
REFERENCES [dbo].[EX_Category] ([CategoryID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_Category]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_QuestionType]    Script Date: 12/12/2007 15:14:16 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_QuestionType] FOREIGN KEY([QuestionTypeID])
REFERENCES [dbo].[EX_QuestionType] ([TypeID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_QuestionType]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_SystemUser]    Script Date: 12/12/2007 15:14:16 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_SystemUser] FOREIGN KEY([QuestionCreatorID])
REFERENCES [dbo].[EX_SystemUser] ([SystemUserID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_SystemUser]
GO
/****** Object:  ForeignKey [FK_EX_QuestionGeneration_EX_Exam]    Script Date: 12/12/2007 15:14:20 ******/
ALTER TABLE [dbo].[EX_QuestionGeneration]  WITH CHECK ADD  CONSTRAINT [FK_EX_QuestionGeneration_EX_Exam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[EX_Exam] ([ExamID])
GO
ALTER TABLE [dbo].[EX_QuestionGeneration] CHECK CONSTRAINT [FK_EX_QuestionGeneration_EX_Exam]
GO
/****** Object:  ForeignKey [FK_EX_QuestionGeneration_EX_Question]    Script Date: 12/12/2007 15:14:20 ******/
ALTER TABLE [dbo].[EX_QuestionGeneration]  WITH CHECK ADD  CONSTRAINT [FK_EX_QuestionGeneration_EX_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[EX_Question] ([QuestionID])
GO
ALTER TABLE [dbo].[EX_QuestionGeneration] CHECK CONSTRAINT [FK_EX_QuestionGeneration_EX_Question]
GO

insert into EX_SystemUser(SystemUserID,SystemUserName,SystemUserPassword,EmailAddress) values(newid(),'administrator','administrator','rr@pyxisnet.com')
insert into EX_QuestionType(TypeName) values('Objective')
insert into EX_QuestionType(TypeName) values('Descriptive')