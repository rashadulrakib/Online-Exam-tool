use master
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'DBExamSystem')
DROP DATABASE DBExamSystem
use master
create database DBExamSystem

USE DBExamSystem
GO
/****** Object:  Table [dbo].[EX_Exam]    Script Date: 11/28/2007 14:23:47 ******/
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
/****** Object:  Table [dbo].[EX_SystemUser]    Script Date: 11/28/2007 14:23:59 ******/
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
 CONSTRAINT [PK_EX_SystemUser] PRIMARY KEY CLUSTERED 
(
	[SystemUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_Category]    Script Date: 11/28/2007 14:23:43 ******/
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
/****** Object:  Table [dbo].[EX_QuestionType]    Script Date: 11/28/2007 14:23:57 ******/
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
/****** Object:  Table [dbo].[EX_CandidateExam]    Script Date: 11/28/2007 14:23:42 ******/
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
/****** Object:  Table [dbo].[EX_QuestionGeneration]    Script Date: 11/28/2007 14:23:55 ******/
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
/****** Object:  Table [dbo].[EX_Candidate]    Script Date: 11/28/2007 14:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EX_Candidate](
	[ExamID] [uniqueidentifier] NOT NULL,
	[CompositeCandidateID] [varchar](200) NOT NULL,
	[CandidatePassword] [varchar](200) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[LastResult] [float] NOT NULL,
	[LastInstitution] [varchar](50) NOT NULL,
	[LastPassingYear] [int] NOT NULL,
	[CvPath] [varchar](400) NOT NULL,
 CONSTRAINT [PK_EX_Candidate] PRIMARY KEY CLUSTERED 
(
	[CompositeCandidateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EX_Objective]    Script Date: 11/28/2007 14:23:48 ******/
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
/****** Object:  Table [dbo].[EX_Question]    Script Date: 11/28/2007 14:23:52 ******/
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
 CONSTRAINT [PK_EX_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_EX_Candidate_EX_Exam1]    Script Date: 11/28/2007 14:23:39 ******/
ALTER TABLE [dbo].[EX_Candidate]  WITH CHECK ADD  CONSTRAINT [FK_EX_Candidate_EX_Exam1] FOREIGN KEY([ExamID])
REFERENCES [dbo].[EX_Exam] ([ExamID])
GO
ALTER TABLE [dbo].[EX_Candidate] CHECK CONSTRAINT [FK_EX_Candidate_EX_Exam1]
GO
/****** Object:  ForeignKey [FK_EX_CandidateExam_EX_Candidate]    Script Date: 11/28/2007 14:23:42 ******/
ALTER TABLE [dbo].[EX_CandidateExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateExam_EX_Candidate] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[EX_Candidate] ([CompositeCandidateID])
GO
ALTER TABLE [dbo].[EX_CandidateExam] CHECK CONSTRAINT [FK_EX_CandidateExam_EX_Candidate]
GO
/****** Object:  ForeignKey [FK_EX_CandidateExam_EX_QuestionGeneration]    Script Date: 11/28/2007 14:23:42 ******/
ALTER TABLE [dbo].[EX_CandidateExam]  WITH CHECK ADD  CONSTRAINT [FK_EX_CandidateExam_EX_QuestionGeneration] FOREIGN KEY([ExamID], [QuestionID])
REFERENCES [dbo].[EX_QuestionGeneration] ([ExamID], [QuestionID])
GO
ALTER TABLE [dbo].[EX_CandidateExam] CHECK CONSTRAINT [FK_EX_CandidateExam_EX_QuestionGeneration]
GO
/****** Object:  ForeignKey [FK_EX_Objective_EX_Question]    Script Date: 11/28/2007 14:23:49 ******/
ALTER TABLE [dbo].[EX_Objective]  WITH CHECK ADD  CONSTRAINT [FK_EX_Objective_EX_Question] FOREIGN KEY([ObjectiveQuestionID])
REFERENCES [dbo].[EX_Question] ([QuestionID])
GO
ALTER TABLE [dbo].[EX_Objective] CHECK CONSTRAINT [FK_EX_Objective_EX_Question]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_Category]    Script Date: 11/28/2007 14:23:52 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_Category] FOREIGN KEY([QuestionCategoryID])
REFERENCES [dbo].[EX_Category] ([CategoryID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_Category]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_QuestionType]    Script Date: 11/28/2007 14:23:52 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_QuestionType] FOREIGN KEY([QuestionTypeID])
REFERENCES [dbo].[EX_QuestionType] ([TypeID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_QuestionType]
GO
/****** Object:  ForeignKey [FK_EX_Question_EX_SystemUser]    Script Date: 11/28/2007 14:23:52 ******/
ALTER TABLE [dbo].[EX_Question]  WITH CHECK ADD  CONSTRAINT [FK_EX_Question_EX_SystemUser] FOREIGN KEY([QuestionCreatorID])
REFERENCES [dbo].[EX_SystemUser] ([SystemUserID])
GO
ALTER TABLE [dbo].[EX_Question] CHECK CONSTRAINT [FK_EX_Question_EX_SystemUser]
GO
/****** Object:  ForeignKey [FK_EX_QuestionGeneration_EX_Exam]    Script Date: 11/28/2007 14:23:56 ******/
ALTER TABLE [dbo].[EX_QuestionGeneration]  WITH CHECK ADD  CONSTRAINT [FK_EX_QuestionGeneration_EX_Exam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[EX_Exam] ([ExamID])
GO
ALTER TABLE [dbo].[EX_QuestionGeneration] CHECK CONSTRAINT [FK_EX_QuestionGeneration_EX_Exam]
GO
/****** Object:  ForeignKey [FK_EX_QuestionGeneration_EX_Question]    Script Date: 11/28/2007 14:23:56 ******/
ALTER TABLE [dbo].[EX_QuestionGeneration]  WITH CHECK ADD  CONSTRAINT [FK_EX_QuestionGeneration_EX_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[EX_Question] ([QuestionID])
GO
ALTER TABLE [dbo].[EX_QuestionGeneration] CHECK CONSTRAINT [FK_EX_QuestionGeneration_EX_Question]
GO

insert into EX_SystemUser(SystemUserID,SystemUserName,SystemUserPassword) values(newid(),'administrator','administrator')
insert into EX_QuestionType(TypeName) values('Objective')
insert into EX_QuestionType(TypeName) values('Descriptive')
