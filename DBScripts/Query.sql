select ExamDateWithTime,ExamDuration from EX_Exam where ExamID in (select ExamID from EX_Candidate where EX_Candidate.CompositeCandidateID='rakib_1' and EX_Candidate.CandidatePassword='rakib_1')
select ExamDateWithTime,ExamDuration from EX_Exam where ExamID in (select ExamID from EX_Candidate where EX_Candidate.CompositeCandidateID='rakib_1' and EX_Candidate.CandidatePassword='rakib_1')
select EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath,EX_Candidate.CandidateIDInt,EX_Candidate.CandidateID from EX_Candidate,EX_Exam where EX_Exam.ExamID in (select ExamID from EX_Candidate where CompositeCandidateID='rakib_1' and CandidatePassword='rakib_1')
select ExamID from EX_Candidate where CompositeCandidateID='rakib_1' and CandidatePassword='rakib_1'
select EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Candidate.ExamID,EX_Candidate.CandidateID from EX_Candidate,EX_Exam where EX_Exam.ExamID=EX_Candidate.ExamID and EX_Exam.ExamID in (select ExamID from EX_Candidate where CompositeCandidateID='rakib_1' and CandidatePassword='rakib_1') 
select EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath,EX_Candidate.CandidateIDInt,EX_Candidate.CandidateID from EX_Candidate inner join EX_Exam on EX_Exam.ExamID=EX_Candidate.ExamID and EX_Candidate.CompositeCandidateID='rakib_1' and EX_Candidate.CandidatePassword='rakib_1'

select EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath,EX_Candidate.CandidateIDInt,EX_Candidate.CandidateID from EX_Candidate inner join EX_Exam on EX_Exam.ExamID=EX_Candidate.ExamID and EX_Candidate.CompositeCandidateID='rakib_5' and EX_Candidate.CandidatePassword='rakib_5'

select EX_Category.CategoryName from EX_Category inner join EX_Question on EX_Category.CategoryID=EX_Question.QuestionCategoryID inner join  EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID and EX_QuestionGeneration.ExamID='672fc917-f8d0-4387-8dc5-a187f04e09cd'
select distinct EX_Category.CategoryID, EX_Category.CategoryName from EX_Category inner join EX_Question on EX_Category.CategoryID=EX_Question.QuestionCategoryID inner join EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID and EX_QuestionGeneration.ExamID='672fc917-f8d0-4387-8dc5-a187f04e09cd'



--<special
--for type
select distinct EX_QuestionType.TypeID, EX_QuestionType.TypeName from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID=EX_Question.QuestionTypeID inner join  EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='993fee17-962f-49d3-81c9-3c5117ae16f5' and EX_Question.QuestionCategoryID ='4' order by EX_QuestionType.TypeID asc

--for category
select distinct EX_Category.CategoryID, EX_Category.CategoryName from EX_Category 
inner join EX_Question on EX_Category.CategoryID=EX_Question.QuestionCategoryID 
inner join  EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID 
where EX_QuestionGeneration.ExamID='993fee17-962f-49d3-81c9-3c5117ae16f5' order by EX_Category.CategoryID asc

--for number of questions in a category of a type by M
select count(EX_Question.QuestionID) from EX_QuestionType 
inner join EX_Question on EX_QuestionType.TypeID=EX_Question.QuestionTypeID 
inner join  EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID 
where EX_QuestionGeneration.ExamID='993fee17-962f-49d3-81c9-3c5117ae16f5' 
and EX_Question.QuestionCategoryID ='3' 
and EX_Question.QuestionTypeID='1'

--for number of questions in a category of a type by R
select count(EX_Question.QuestionID) as TotalQuestions from EX_Question inner join  EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='993fee17-962f-49d3-81c9-3c5117ae16f5' and EX_Question.QuestionCategoryID ='3' and EX_Question.QuestionTypeID='1'


select distinct EX_Category.CategoryID, EX_Category.CategoryName from EX_Category,EX_Question,EX_QuestionGeneration 
where EX_Category.CategoryID=EX_Question.QuestionCategoryID 
and EX_QuestionGeneration.QuestionID=EX_Question.QuestionID 
and EX_QuestionGeneration.ExamID='993fee17-962f-49d3-81c9-3c5117ae16f5' order by EX_Category.CategoryID asc


select distinct EX_QuestionType.TypeID, EX_QuestionType.TypeName from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID=EX_Question.QuestionTypeID where EX_Question.QuestionCategoryID='3' order by EX_QuestionType.TypeID asc

select count(EX_QuestionType.TypeID), EX_QuestionType.TypeID, EX_QuestionType.TypeName from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID=EX_Question.QuestionTypeID group by EX_QuestionType.TypeID having EX_Question.QuestionCategoryID='3' order by EX_QuestionType.TypeID asc





--22-11-07
select count(EX_Question.QuestionID) as TotalQuestions from EX_Question inner join EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='993fee17-962f-49d3-81c9-3c5117ae16f5' and EX_Question.QuestionCategoryID ='3' and EX_Question.QuestionTypeID='0'
select count(EX_Question.QuestionID) as TotalQuestions from EX_Question inner join EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='993fee17-962f-49d3-81c9-3c5117ae16f5' and EX_Question.QuestionCategoryID ='3' and EX_Question.QuestionTypeID='1'

--special/>



--Select the questions from EX_Question for a prticular categoryID for a particular Exam in which the candidate is involved
--select * from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.ExamID='672fc917-f8d0-4387-8dc5-a187f04e09cd' and EX_QuestionGeneration.QuestionID=EX_Question.QuestionID and EX_Question.QuestionCategoryID='7ba87523-8855-4851-a3be-1ab5483b91d8'

select EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionCreatorID,EX_Question.QuestionDefaultMark,EX_Question.QuestionCategoryID,EX_Question.QuestionTypeName,EX_Question.QuestionIsUsed from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID and EX_QuestionGeneration.ExamID='' and EX_Question.QuestionCategoryID=''

select EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID

select EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID and  EX_Question.QuestionCategoryID='1' and EX_Question.QuestionTypeID='0'
select ObjectiveAnswer,ObjectiveAnswerIsValid from EX_Objective where ObjectiveQuestionID='a69e4321-1031-4903-bdb6-23f3eb174afc'

insert into EX_SystemUser(SystemUserID,SystemUserName,SystemUserPassword) values(newid(),'administrator','Pass@123')

select EX_QuestionGeneration.ExamID,EX_QuestionGeneration.QuestionID,EX_QuestionGeneration.GeneratorID,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID and EX_QuestionGeneration.ExamID='bcc657ed-b568-4deb-973e-a0da3f7ec8a8' and EX_Question.QuestionTypeID='0' and EX_Question.QuestionCategoryID='1'
select EX_QuestionGeneration.ExamID,EX_QuestionGeneration.QuestionID,EX_QuestionGeneration.GeneratorID,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID
--sInsert = "DECLARE @rows int SET @rows = (SELECT  max(CandidateIDInt) from EX_Candidate) if @rows is null insert into EX_Candidate(ExamID,CompositeCandidateID,CandidatePassword,Name,LastResult,LastInstitution,LastPassingYear,CvPath,CandidateID) values('" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "','" + oCandidate.CandidateName + "_1','" + oCandidate.CandidateName + "_1','" + oCandidate.CandidateName + "','" + oCandidate.CandidateLastResult + "','" + oCandidate.CandiadteLastInstitution + "','" + oCandidate.CandidateLastPassingYear + "','" + oCandidate.CandidateCvPath + "','" + oCandidate.CandidateID + "')  else insert into EX_Candidate(ExamID,CompositeCandidateID,CandidatePassword,Name,LastResult,LastInstitution,LastPassingYear,CvPath,CandidateID) values('" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "','" + oCandidate.CandidateName + "_'" + "+Convert(varchar(50),@rows+1),'" + oCandidate.CandidateName + "_'" + "+Convert(varchar(50),@rows+1),'" + oCandidate.CandidateName + "','" + oCandidate.CandidateLastResult + "','" + oCandidate.CandiadteLastInstitution + "','" + oCandidate.CandidateLastPassingYear + "','" + oCandidate.CandidateCvPath + "','" + oCandidate.CandidateID + "')";     //values('abc' + Convert(varchar(50),@rows+1))";
select EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID and  EX_Question.QuestionCategoryID='1' and EX_Question.QuestionTypeID='0'




select EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID and  EX_Question.QuestionCategoryID='1' and EX_Question.QuestionTypeID='0'
select EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID
select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID and  EX_Question.QuestionCategoryID='1' and EX_Question.QuestionTypeID='0'

select EX_QuestionGeneration.ExamID,EX_QuestionGeneration.QuestionID,EX_QuestionGeneration.GeneratorID,EX_Question.QuestionText,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID and EX_QuestionGeneration.ExamID='a5f5169f-124b-478e-aa49-737860965439' and EX_Question.QuestionTypeID='0' and EX_Question.QuestionCategoryID='1'

delete from EX_QuestionGeneration where ExamID='a5f5169f-124b-478e-aa49-737860965439' and QuestionID='adb115d6-4700-4f92-a3ba-8f5b41128366' and GeneratorID='f4ed9c8d-2261-4347-857d-9e0bd4941a38'

select SystemUserID,SystemUserName,SystemUserPassword,DeleteTime from EX_SystemUser where DeleteTime is NULL

select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID where EX_Question.QuestionCategoryID='3' and EX_Question.QuestionTypeID='0'

select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionCategoryID='3' and EX_Question.QuestionID=EX_QuestionGeneration.QuestionID  


select EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath from EX_Candidate inner join EX_Exam on EX_Exam.ExamID=EX_Candidate.ExamID where EX_Candidate.CompositeCandidateID='hira633309936619310676' and EX_Candidate.CandidatePassword='hira633309936619310676' and EX_Candidate.CompositeCandidateID not in (select EX_CandidateExam.CandidateID from EX_Candidate inner join EX_CandidateExam on EX_Candidate.ExamID=EX_CandidateExam.ExamID where EX_Candidate.CompositeCandidateID='hira633309936619310676')

select EX_CandidateExam.CandidateID from EX_Candidate inner join EX_CandidateExam on EX_Candidate.ExamID=EX_CandidateExam.ExamID where EX_Candidate.CompositeCandidateID='hira633309936619310676'

select EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionCreatorID,EX_Question.QuestionDefaultMark,EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='993fee17-962f-49d3-81c9-3c5117ae16f5' order by EX_Question.QuestionCategoryID, EX_Question.QuestionTypeID asc



--<select candidates if, the system user generate any questions for a particular Exam
select EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,CvPath
from EX_Candidate inner join 
EX_CandidateExam on EX_Candidate.CompositeCandidateID = EX_CandidateExam.CandidateID where 
EX_Candidate.ExamID='' and EX_Candidate.ExamID in 
(select EX_QuestionGeneration.ExamID from EX_QuestionGeneration where 
EX_QuestionGeneration.ExamID='' and EX_QuestionGeneration.GeneratorID='')

--<select candidates if, the system user=admin
select EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,CvPath
from EX_Candidate inner join 
EX_CandidateExam on EX_Candidate.CompositeCandidateID = EX_CandidateExam.CandidateID where 
EX_Candidate.ExamID='' and EX_Candidate.ExamID in 
(select EX_QuestionGeneration.ExamID from EX_QuestionGeneration where 
EX_QuestionGeneration.ExamID='')

select distinct EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,CvPath from EX_Candidate inner join EX_CandidateExam on EX_Candidate.CompositeCandidateID = EX_CandidateExam.CandidateID where EX_Candidate.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5' and EX_Candidate.ExamID in (select EX_QuestionGeneration.ExamID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5')

--<select Questions 
select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='' and EX_QuestionGeneration.GeneratorID=''

--<by R, CandidateID='' will be from Hyper Link.. Select Questions for a candidate which are setup by a particular user...
select EX_CandidateExam.AnswerStringOrBits,EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID
from EX_CandidateExam inner join EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID
inner join EX_Question on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID
where EX_CandidateExam.CandidateID='' and EX_CandidateExam.ExamID='' and EX_CandidateExam.QuestionID in 
(select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='' 
and EX_QuestionGeneration.GeneratorID='') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc

--next by R
--<by R, CandidateID='' will be from Hyper Link.. Select Questions for a candidate which are setup by a particular user...
select distinct EX_CandidateExam.AnswerStringOrBits,EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID
from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID
inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID
where EX_CandidateExam.CandidateID='' and EX_CandidateExam.ExamID='' and EX_CandidateExam.QuestionID in 
(select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='' 
and EX_QuestionGeneration.GeneratorID='') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc

--<Select Choices in Database by R
select distinct EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,EX_Objective.ObjectiveAnswerIsValid
from EX_Objective inner join EX_Question on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID
inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID
inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID
where EX_CandidateExam.QuestionID='976bccc5-fddd-4543-9853-6acc00ec59e7' and 
EX_CandidateExam.CandidateID='rakib633313390299563826' and 
EX_CandidateExam.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5' and EX_CandidateExam.QuestionID in 
(select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5' and EX_QuestionGeneration.GeneratorID='')




--<by M, CandidateID='' will be from Hyper Link.. Select Questions for a candidate which are setup by a particular user...
select EX_CandidateExam.AnswerStringOrBits
from EX_CandidateExam inner join EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID 
where EX_CandidateExam.CandidateID='' and EX_CandidateExam.ExamID='' and EX_CandidateExam.QuestionID in 
(select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID=EX_CandidateExam.ExamID 
and EX_QuestionGeneration.GeneratorID='')






--<if the user is admin then 
select EX_CandidateExam.AnswerStringOrBits, EX_CandidateExam.ObtainMark from EX_CandidateExam inner join 
EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID 
where EX_CandidateExam.CandidateID='' and EX_CandidateExam.ExamID='' and EX_CandidateExam.QuestionID 
in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='')

--next
select EX_CandidateExam.AnswerStringOrBits,EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID
from EX_CandidateExam inner join EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID
inner join EX_Question on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID
where EX_CandidateExam.CandidateID='rakib633313390299563826' and EX_CandidateExam.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5' and EX_CandidateExam.QuestionID in 
(select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc

--nextnext
select distinct EX_CandidateExam.AnswerStringOrBits,EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID
from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID
inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID
where EX_CandidateExam.CandidateID='rakib633313390299563826' and EX_CandidateExam.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5' and EX_CandidateExam.QuestionID in 
(select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc

--Choices
select distinct EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,EX_Objective.ObjectiveAnswerIsValid
from EX_Objective inner join EX_Question on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID
inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID
inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID
where EX_CandidateExam.QuestionID='976bccc5-fddd-4543-9853-6acc00ec59e7' and 
EX_CandidateExam.CandidateID='rakib633313390299563826' and 
EX_CandidateExam.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5' and EX_CandidateExam.QuestionID in 
(select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5')

--choices
select distinct EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_Question on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.QuestionID='976bccc5-fddd-4543-9853-6acc00ec59e7' and EX_CandidateExam.CandidateID='rakib633313390299563826' and EX_CandidateExam.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5' and EX_QuestionGeneration.GeneratorID='edbc3e01-6b78-4248-9a2a-3c97635fd407')

--select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark, EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='rakib633313390299563826' and EX_CandidateExam.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='cda67cd0-ee03-46df-a505-0dbaa1ccbea5') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc



--select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark, EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='hira633317550861605888' and EX_CandidateExam.ExamID='3b8ee455-abab-447d-a5e7-7071159aa348' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='3b8ee455-abab-447d-a5e7-7071159aa348') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc
--select EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_Question on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.QuestionID='b6f15a11-8f21-4a7b-b77e-5a4daae7cb13' and EX_CandidateExam.CandidateID='hira633317550861605888' and EX_CandidateExam.ExamID='3b8ee455-abab-447d-a5e7-7071159aa348' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='3b8ee455-abab-447d-a5e7-7071159aa348')

--select EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_Question on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.QuestionID='b6f15a11-8f21-4a7b-b77e-5a4daae7cb13' and EX_CandidateExam.CandidateID='hira633317550861605888' and EX_CandidateExam.ExamID='3b8ee455-abab-447d-a5e7-7071159aa348' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='3b8ee455-abab-447d-a5e7-7071159aa348' and EX_QuestionGeneration.GeneratorID='edbc3e01-6b78-4248-9a2a-3c97635fd407')

--select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath from EX_Candidate inner join EX_Exam on EX_Exam.ExamID=EX_Candidate.ExamID where EX_Candidate.CompositeCandidateID='hira633317550861605888' and EX_Candidate.CandidatePassword='hira633317550861605888' and EX_Candidate.CompositeCandidateID not in (select EX_CandidateExam.CandidateID from EX_Candidate inner join EX_CandidateExam on EX_Candidate.ExamID=EX_CandidateExam.ExamID where EX_Candidate.CompositeCandidateID='hira633317550861605888')

--insert into EX_Question(QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID) values('37dfb4eb-74ec-41d7-97ea-84b195c7952b','What is true?','fb53ad59-8384-4eca-a7f6-49006eab4e4e','1','1','0')




<--for Result View Header
select distinct EX_Category.CategoryID, EX_Category.CategoryName from EX_Category inner join EX_Question on EX_Category.CategoryID=EX_Question.QuestionCategoryID inner join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID inner join EX_Exam on EX_QuestionGeneration.ExamID=EX_Exam.ExamID where EX_Exam.ExamID=''

select distinct EX_QuestionType.TypeID, EX_QuestionType.TypeName from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID=EX_Question.QuestionTypeID inner join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID inner join EX_Exam on EX_QuestionGeneration.ExamID= EX_Exam.ExamID where EX_Exam.ExamID='' and EX_Question.QuestionCategoryID=''


--select all
select avg(EX_CandidateExam.ObtainMark) as AvgTypeMark, EX_QuestionType.TypeID,EX_Question.QuestionCategoryID,EX_CandidateExam.CandidateID,EX_Candidate.Name 
from EX_Candidate inner join EX_CandidateExam on EX_Candidate.CompositeCandidateID=EX_CandidateExam.CandidateID 
inner join EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID 
inner join EX_Question on EX_QuestionGeneration.QuestionID = EX_Question.QuestionID 
inner join EX_QuestionType on EX_Question.QuestionTypeID=EX_QuestionType.TypeID 
group by EX_CandidateExam.ExamID,EX_CandidateExam.CandidateID,EX_Candidate.Name,EX_Question.QuestionCategoryID,EX_QuestionType.TypeID 
having EX_CandidateExam.ExamID='16482033-083a-4dfc-8ca7-dbea36703246'
order by EX_CandidateExam.CandidateID,EX_Candidate.Name,EX_Question.QuestionCategoryID,EX_QuestionType.TypeID asc

--Select Particular type Avg Mark for an Exam,a Candidate, a Category
select avg(EX_CandidateExam.ObtainMark) as AvgTypeMark, EX_QuestionType.TypeID,EX_Question.QuestionCategoryID,EX_CandidateExam.CandidateID,EX_Candidate.Name 
from EX_Candidate inner join EX_CandidateExam on EX_Candidate.CompositeCandidateID=EX_CandidateExam.CandidateID 
inner join EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID 
inner join EX_Question on EX_QuestionGeneration.QuestionID = EX_Question.QuestionID 
inner join EX_QuestionType on EX_Question.QuestionTypeID=EX_QuestionType.TypeID 
group by EX_CandidateExam.ExamID,EX_CandidateExam.CandidateID,EX_Candidate.Name,EX_Question.QuestionCategoryID,EX_QuestionType.TypeID 
having EX_CandidateExam.ExamID='16482033-083a-4dfc-8ca7-dbea36703246' and EX_CandidateExam.CandidateID='hira633319237917375822' and EX_Question.QuestionCategoryID='3'
order by EX_CandidateExam.CandidateID,EX_Candidate.Name,EX_Question.QuestionCategoryID,EX_QuestionType.TypeID asc





--Select Particular Avg of types for an Exam,a Candidate, a Category
select avg(EX_CandidateExam.ObtainMark) as AvgTypeMark, EX_QuestionType.TypeID,EX_QuestionType.TypeName
from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID = EX_Question.QuestionTypeID
inner join EX_QuestionGeneration on EX_Question.QuestionID= EX_QuestionGeneration.QuestionID
inner join EX_CandidateExam on EX_QuestionGeneration.QuestionID = EX_CandidateExam.QuestionID
group by EX_CandidateExam.ExamID,EX_CandidateExam.CandidateID,EX_Question.QuestionCategoryID,EX_QuestionType.TypeID,EX_QuestionType.TypeName
having EX_CandidateExam.ExamID='16482033-083a-4dfc-8ca7-dbea36703246' and EX_CandidateExam.CandidateID='hira633319237917375822'
and EX_Question.QuestionCategoryID='3' and EX_QuestionType.TypeID='1'

--Select Particular Categories for an Exam,a Candidate,
select distinct EX_Category.CategoryName,EX_Question.QuestionCategoryID 
from EX_Category inner join EX_Question on EX_Category.CategoryID=EX_Question.QuestionCategoryID
inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID
inner join EX_CandidateExam on EX_QuestionGeneration.QuestionID = EX_CandidateExam.QuestionID
where EX_CandidateExam.ExamID='16482033-083a-4dfc-8ca7-dbea36703246' and EX_CandidateExam.CandidateID='hira633319237917375822'

--Select Particular Candidates for an Exam
select distinct EX_CandidateExam.CandidateID,EX_Candidate.Name 
from EX_Candidate inner join EX_CandidateExam on EX_Candidate.CompositeCandidateID=EX_CandidateExam.CandidateID 
where EX_CandidateExam.ExamID='16482033-083a-4dfc-8ca7-dbea36703246'




-->


select sum(SetupQuestionMark) from EX_QuestionGeneration where ExamID=''
select sum(EX_Question.QuestionPossibleAnswerTime) from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID where EX_QuestionGeneration.ExamID =''

--insert into EX_Question(QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,QuestionPossibleAnswerTime) values('769ea6b6-82a6-4213-af96-ee7490d57814','IQ Question A','70b2e0f6-85f8-4eaf-984a-85f852216d2c','5','1','0','5')

--delete from EX_Category where CategoryID='4' and CategoryID not in (select QuestionCategoryID from EX_Question where QuestionCategoryID='4')
--select QuestionCategoryID from EX_Question where QuestionCategoryID='4'

select EX_QuestionGeneration.ExamID,EX_QuestionGeneration.QuestionID,EX_QuestionGeneration.GeneratorID,EX_Question.QuestionText,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID,EX_Question.QuestionPossibleAnswerTime from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='4899b7e1-5fce-4c1c-99e9-ed9c56d5dc78' and EX_Question.QuestionTypeID='0' and EX_Question.QuestionCategoryID='2'

select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark,EX_CandidateExam.AnswerAttachmentPath, EX_Question.QuestionID,EX_Question.QuestionText,EX_QuestionGeneration.SetupQuestionMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='hira633324671441933892' and EX_CandidateExam.ExamID='dcc40e8a-d90a-498c-a159-45a1613dcd88' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='dcc40e8a-d90a-498c-a159-45a1613dcd88') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc

select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,
EX_QuestionGeneration.SetupQuestionMark,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,
QuestionTypeID,EX_Question.QuestionPossibleAnswerTime 
from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID 
where EX_Question.QuestionCategoryID='1' and EX_Question.QuestionTypeID='1'
and EX_QuestionGeneration.ExamID='00d9ac36-8b70-43f2-b585-ef153aa315f9'

--
select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,
QuestionTypeID,QuestionPossibleAnswerTime from EX_Question where QuestionCategoryID='1' and QuestionTypeID='1'

select EX_QuestionGeneration.QuestionID as GeneratedQuestionID,EX_QuestionGeneration.SetupQuestionMark from EX_QuestionGeneration where EX_QuestionGeneration.QuestionID='AE9283B7-5CD3-4546-BFBD-B9F2CC07C0FA' and EX_QuestionGeneration.ExamID='00d9ac36-8b70-43f2-b585-ef153aa315f9'
--

select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,
EX_QuestionGeneration.SetupQuestionMark,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,
EX_Question.QuestionPossibleAnswerTime from 
EX_Question
inner join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID 
where EX_Question.QuestionCategoryID='1' and EX_Question.QuestionTypeID='1' and EX_QuestionGeneration.ExamID<>'aeed22e9-647c-4873-80e6-b8afc43a869d'

select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,QuestionPossibleAnswerTime from EX_Question where QuestionCategoryID='1' and QuestionTypeID='1'
select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,QuestionPossibleAnswerTime from EX_Question where QuestionCategoryID='2' and QuestionTypeID='1'