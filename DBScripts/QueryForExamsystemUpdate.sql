
select distinct EX_CandidateForExam.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID inner join EX_CandidateExam on EX_CandidateForExam.ExamID=EX_CandidateExam.ExamID where EX_CandidateExam.ExamID='513e30ca-e9c0-497b-a553-2241fff0523a' and EX_CandidateExam.ExamID in (select EX_QuestionGeneration.ExamID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='513e30ca-e9c0-497b-a553-2241fff0523a')
select EX_CandidateForExam.ExamID,CompositeCandidateID,CandidatePassword,Name,LastResult,LastInstitution,LastPassingYear,CvPath,EmailAddress from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID where EX_CandidateForExam.ExamID='513e30ca-e9c0-497b-a553-2241fff0523a'

insert into EX_CandidateForExam(CandidateID,ExamID) values('hira633335702953882200','4f872043-dccb-445f-8c56-c17b8ab37716') where 'hira633335702953882200' not in (select EX_CandidateForExam.CandidateID from EX_CandidateForExam where EX_CandidateForExam.CandidateID='hira633335702953882200' and EX_CandidateForExam.ExamID='4f872043-dccb-445f-8c56-c17b8ab37716')
select EX_CandidateForExam.CandidateID from EX_CandidateForExam where EX_CandidateForExam.CandidateID='hira633335702953882200' and EX_CandidateForExam.ExamID='4f872043-dccb-445f-8c56-c17b8ab37716'

insert into EX_CandidateForExam(CandidateID,ExamID) select 'hira633335702953882200' as CName,'4f872043-dccb-445f-8c56-c17b8ab37716' as ENme where CName not in (select EX_CandidateForExam.CandidateID from EX_CandidateForExam where EX_CandidateForExam.CandidateID='hira633335702953882200' and EX_CandidateForExam.ExamID='4f872043-dccb-445f-8c56-c17b8ab37716')

if not exists(select EX_CandidateForExam.CandidateID from EX_CandidateForExam where EX_CandidateForExam.CandidateID='hira633335702953882200' and EX_CandidateForExam.ExamID='4f872043-dccb-445f-8c56-c17b8ab37716') insert into EX_CandidateForExam(CandidateID,ExamID) values('hira633335702953882200','4f872043-dccb-445f-8c56-c17b8ab37716')

update EX_Category set CategoryName='C B' where CategoryID='9' and CategoryName not in (select CategoryName from EX_Category where CategoryName='C B')

if not exists(select CategoryName from EX_Category where CategoryName='C B') update EX_Category set CategoryName='C B' where CategoryID='8'

select EX_Exam.ExamID,EX_Exam.ExamName,EX_Exam.ExamTotalMarks,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamConstraint from EX_CandidateForExam inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID where EX_CandidateForExam.CandidateID='rakib633335965850782544'

alter table EX_Candidate add CandidatePicture image;
alter table EX_Candidate drop COLUMN  LastResultRnge
alter table EX_Candidate add LastResultRange float not null;
alter table EX_Candidate add LastResultType varchar(100) not null;



delete from EX_Objective

if not exists(select EmailAddress from EX_Candidate where EmailAddress=@Email) 
insert into EX_Candidate(CompositeCandidateID,CandidatePassword,Name,LastResult,
LastInstitution,LastPassingYear,CvPath,EmailAddress,LastResultRange,LastResultTypeName,CandidatePicturePath) 
values(@CompositeCandidateID,@CandidatePassword,@Name,@LastResult,
@LastInstitution,@LastPassingYear,@CvPath,@EmailAddress,@LastResultRange,@LastResultTypeName,@CandidatePicturePath)

--sInsert = "DECLARE @rows int SET @rows = (SELECT  max(CandidateIDInt) from EX_Candidate) if @rows is null insert into EX_Candidate(ExamID,CompositeCandidateID,CandidatePassword,Name,LastResult,LastInstitution,LastPassingYear,CvPath,CandidateID) values('" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "','" + oCandidate.CandidateName + "_1','" + oCandidate.CandidateName + "_1','" + oCandidate.CandidateName + "','" + oCandidate.CandidateLastResult + "','" + oCandidate.CandiadteLastInstitution + "','" + oCandidate.CandidateLastPassingYear + "','" + oCandidate.CandidateCvPath + "','" + oCandidate.CandidateID + "')  else insert into EX_Candidate(ExamID,CompositeCandidateID,CandidatePassword,Name,LastResult,LastInstitution,LastPassingYear,CvPath,CandidateID) values('" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "','" + oCandidate.CandidateName + "_'" + "+Convert(varchar(50),@rows+1),'" + oCandidate.CandidateName + "_'" + "+Convert(varchar(50),@rows+1),'" + oCandidate.CandidateName + "','" + oCandidate.CandidateLastResult + "','" + oCandidate.CandiadteLastInstitution + "','" + oCandidate.CandidateLastPassingYear + "','" + oCandidate.CandidateCvPath + "','" + oCandidate.CandidateID + "')";     //values('abc' + Convert(varchar(50),@rows+1))";

select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks,
EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,
EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath 
from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID 
inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com' 
and EX_Candidate.CandidatePassword='ra@123' and EX_Candidate.CompositeCandidateID 
not in (select EX_CandidateExam.CandidateID from EX_Candidate inner join EX_CandidateForExam on 
EX_Candidate.CompositeCandidateID =EX_CandidateForExam.CandidateID inner join EX_CandidateExam on 
EX_CandidateForExam.ExamID=EX_CandidateExam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com')



select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks,
EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,
EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath 
from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID 
inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com' and 
EX_Candidate.CandidatePassword='ra@123' and EX_Candidate.CompositeCandidateID not in 
(select EX_CandidateExam.CandidateID from EX_Candidate inner join EX_CandidateForExam on 
EX_Candidate.CompositeCandidateID =EX_CandidateForExam.CandidateID inner join EX_CandidateExam on 
EX_CandidateForExam.ExamID=EX_CandidateExam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com')

--by milton for candidate login
select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks,
EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,
EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath 
from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID 
inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com' and 
EX_Candidate.CandidatePassword='ra@123' and EX_CandidateForExam.CandidateID not in 
(select EX_CandidateExam.CandidateID from EX_CandidateExam where  
EX_CandidateExam.ExamID=EX_CandidateForExam.ExamID and EX_CandidateExam.CandidateID=EX_CandidateForExam.CandidateID)
--end

---by rakib for candidate login
select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks,
EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,
EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath 
from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID 
inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com' and 
EX_Candidate.CandidatePassword='ra@123' and EX_CandidateForExam.CandidateID not in 
(select EX_CandidateExam.CandidateID from EX_CandidateExam inner join EX_CandidateForExam 
on EX_CandidateExam.CandidateID=EX_CandidateForExam.CandidateID
 where EX_CandidateExam.ExamID=EX_CandidateForExam.ExamID )
--end

 select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks, EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword, EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com' and EX_Candidate.CandidatePassword='ra@123' and EX_CandidateForExam.CandidateID not in (select EX_CandidateExam.CandidateID from EX_CandidateExam where EX_CandidateExam.ExamID=EX_CandidateForExam.ExamID and EX_CandidateExam.CandidateID=EX_CandidateForExam.CandidateID)



select distinct EX_CandidateForExam.ExamID,EX_Candidate.CompositeCandidateID,
EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,
EX_Candidate.LastPassingYear,EX_Candidate.CvPath from 
EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID 
inner join EX_CandidateExam on EX_CandidateForExam.ExamID=EX_CandidateExam.ExamID and 
EX_CandidateForExam.CandidateID=EX_CandidateExam.CandidateID
where EX_CandidateExam.ExamID='202c8a5e-df56-4fe4-a1cb-64d7c1820ef4' 
and EX_CandidateExam.ExamID in 
(select EX_QuestionGeneration.ExamID 
from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='202c8a5e-df56-4fe4-a1cb-64d7c1820ef4')

select SystemUserName from EX_SystemUser where DeleteTime is NULL and SystemUserName='rakib'

 select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks, EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword, EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com' and EX_Candidate.CandidatePassword='ra@123' and EX_CandidateForExam.CandidateID not in (select EX_CandidateExam.CandidateID from EX_CandidateExam where EX_CandidateExam.ExamID=EX_CandidateForExam.ExamID and EX_CandidateExam.CandidateID=EX_CandidateForExam.CandidateID)



update EX_Candidate set CandidatePassword='hi@123' ,Name='hira' ,
LastResult='3' ,LastInstitution='ku' ,LastPassingYear='2006' ,
CvPath='Test 31_1_08\hira633352297661361212_Cv of Rakib.doc' ,
EmailAddress=
(	
	case 
		when 
			not exists(select EmailAddress from EX_Candidate where EmailAddress='hira_cse02@yahoo.com')
		then
			'hira_cse02@yahoo.com'
		else
			'hira_cse02@yahoo.com'
	end
),LastResultRange='4' ,LastResultTypeName='CGPA' ,CandidatePicturePath='' 
where CompositeCandidateID='hira633352297661361212'

update EX_Label set 
LabelName=
(
	case 
		when
			not exists(select LabelName from EX_Label where LabelName='a')
		then
			'a'
	end
) ,
LabelPrerequisite=''
where LabelID=''


update EX_SystemUser set 
SystemUserName=
(
	case 
		when
			not exists(select SystemUserName from EX_SystemUser where SystemUserName='a')
		then
			'a'
		else
			'a'
	end
),SystemUserPassword='a'
,EmailAddress=
(
	case 
		when
			not exists(select EmailAddress from EX_SystemUser where EmailAddress='a')
		then
			'a'
		else
			'a'
	end
)
where SystemUserID='a' and DeleteTime is NULL



delete from EX_CandidateForExam where EX_CandidateForExam.ExamID='6de1ca63-22bc-40ff-b989-922aa4425f79' 
and EX_CandidateForExam.CandidateID='rakib633352226400892462' and EX_CandidateForExam.CandidateID 
not in (select EX_CandidateExam.CandidateID from EX_CandidateExam where 
EX_CandidateExam.ExamID='6de1ca63-22bc-40ff-b989-922aa4425f79' and EX_CandidateExam.CandidateID='rakib633352226400892462') ; 
delete from EX_Candidate where CompositeCandidateID='rakib633352226400892462' and CompositeCandidateID 
not in (select CandidateID from EX_CandidateForExam where CandidateID='rakib633352226400892462')


select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark,EX_CandidateExam.AnswerAttachmentPath, 
EX_Question.QuestionID,EX_Question.QuestionText,EX_QuestionGeneration.SetupQuestionMark,
EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID 
from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID 
inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID 
where EX_CandidateExam.CandidateID='hira633352297661361212' and 
EX_CandidateExam.ExamID='324b5505-63c6-481a-8f54-9e68744a155b' and 
EX_CandidateExam.QuestionID 
in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration 
where EX_QuestionGeneration.ExamID='324b5505-63c6-481a-8f54-9e68744a155b') 
order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc


select EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,EX_Objective.ObjectiveAnswerIsValid 
from EX_Objective inner join EX_Question on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID 
inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID 
inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID 
where EX_CandidateExam.QuestionID='098d3202-3218-48ee-b07b-0276a1af86da' 
and EX_CandidateExam.CandidateID='hira633352297661361212' 
and EX_CandidateExam.ExamID='324b5505-63c6-481a-8f54-9e68744a155b' 
and EX_CandidateExam.QuestionID in 
(select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration 
where EX_QuestionGeneration.ExamID='324b5505-63c6-481a-8f54-9e68744a155b')


select distinct EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_Question on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.QuestionID='098d3202-3218-48ee-b07b-0276a1af86da' and EX_CandidateExam.CandidateID='hira633352297661361212' and EX_CandidateExam.ExamID='324b5505-63c6-481a-8f54-9e68744a155b' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='324b5505-63c6-481a-8f54-9e68744a155b')

select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark,EX_CandidateExam.AnswerAttachmentPath, EX_Question.QuestionID,EX_Question.QuestionText,EX_QuestionGeneration.SetupQuestionMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='hira633352297661361212' and EX_CandidateExam.ExamID='324b5505-63c6-481a-8f54-9e68744a155b' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='324b5505-63c6-481a-8f54-9e68744a155b') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc

select distinct EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_Question on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.QuestionID='098d3202-3218-48ee-b07b-0276a1af86da' and EX_CandidateExam.CandidateID='hira633352297661361212' and EX_CandidateExam.ExamID='324b5505-63c6-481a-8f54-9e68744a155b' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='324b5505-63c6-481a-8f54-9e68744a155b')

select distinct EX_CandidateExam.CandidateID,EX_Candidate.Name from EX_Candidate inner join EX_CandidateExam on EX_Candidate.CompositeCandidateID=EX_CandidateExam.CandidateID where EX_CandidateExam.ExamID='324b5505-63c6-481a-8f54-9e68744a155b'

select sum(EX_CandidateExam.ObtainMark) as AvgTypeMark, EX_QuestionType.TypeID,EX_QuestionType.TypeName 
from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID = EX_Question.QuestionTypeID 
inner join EX_QuestionGeneration on EX_Question.QuestionID= EX_QuestionGeneration.QuestionID 
inner join EX_CandidateExam on EX_QuestionGeneration.QuestionID = EX_CandidateExam.QuestionID 
group by EX_CandidateExam.ExamID,EX_CandidateExam.CandidateID,EX_Question.QuestionCategoryID,EX_QuestionType.TypeID,
EX_QuestionType.TypeName having EX_CandidateExam.ExamID='324b5505-63c6-481a-8f54-9e68744a155b' 
and EX_CandidateExam.CandidateID='hira633352297661361212' 
and EX_Question.QuestionCategoryID='2' and EX_QuestionType.TypeID='0'

select EX_CandidateExam.ObtainMark, EX_QuestionType.TypeID,EX_QuestionType.TypeName 
from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID = EX_Question.QuestionTypeID 
inner join EX_QuestionGeneration on EX_Question.QuestionID= EX_QuestionGeneration.QuestionID 
inner join EX_CandidateExam on EX_QuestionGeneration.QuestionID = EX_CandidateExam.QuestionID 
group by EX_CandidateExam.ExamID,EX_CandidateExam.CandidateID,EX_Question.QuestionCategoryID,
EX_QuestionType.TypeID,EX_QuestionType.TypeName having EX_CandidateExam.ExamID='324b5505-63c6-481a-8f54-9e68744a155b'
and EX_CandidateExam.CandidateID='hira633352297661361212' and EX_Question.QuestionCategoryID='2' and EX_QuestionType.TypeID='0'

select sum(EX_CandidateExam.ObtainMark) as AvgTypeMark from EX_CandidateExam 
where EX_CandidateExam.ExamID='324b5505-63c6-481a-8f54-9e68744a155b'
and EX_CandidateExam.CandidateID='hira633352297661361212'


select getdate() as currentTime

select convert(varchar, getdate(), 101) as currentTime

select SystemUserID,SystemUserName,SystemUserPassword,EmailAddress from EX_SystemUser where SystemUserName='administrator' and SystemUserPassword='administrator' and DeleteTime is NULL

update EX_Label set LabelName= 
( case 
	when 
		not exists (select LabelName from EX_Label where LabelName='Level 2') 
	then 'Level 2'
 
  end 
), 
LabelPrerequisite='Description of Level 1' 
where LabelID='a968abbc-7d1d-47b8-b7f9-7070f00eb240'

if not exists(select LabelName from EX_Label where LabelName='Level 2')
update EX_Label set 
LabelName='Level 2',LabelPrerequisite='Description of Level 1' 
where LabelID='a968abbc-7d1d-47b8-b7f9-7070f00eb240'


select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks, 
EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.EmailAddress ,EX_Candidate.CandidatePassword, 
EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,
EX_Candidate.CvPath from EX_Candidate inner join EX_CandidateForExam on 
EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID inner join EX_Exam on 
EX_CandidateForExam.ExamID=EX_Exam.ExamID 
where EX_Candidate.EmailAddress='rr@pyxisnet.com' and EX_Candidate.CandidatePassword='ra@123' and 
EX_CandidateForExam.CandidateID not in (select EX_CandidateExam.CandidateID from 
EX_CandidateExam where EX_CandidateExam.ExamID=EX_CandidateForExam.ExamID and 
EX_CandidateExam.CandidateID=EX_CandidateForExam.CandidateID)

 select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks, EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword, EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com' and EX_Candidate.CandidatePassword='ra@123' and EX_CandidateForExam.CandidateID not in (select EX_CandidateExam.CandidateID from EX_CandidateExam where EX_CandidateExam.ExamID=EX_CandidateForExam.ExamID and EX_CandidateExam.CandidateID=EX_CandidateForExam.CandidateID)

 select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks, EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword, EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID where EX_Candidate.EmailAddress='rr@pyxisnet.com' and EX_Candidate.CandidatePassword='ra@123' and EX_CandidateForExam.CandidateID not in (select EX_CandidateExam.CandidateID from EX_CandidateExam where EX_CandidateExam.ExamID=EX_CandidateForExam.ExamID and EX_CandidateExam.CandidateID=EX_CandidateForExam.CandidateID)

select ExamID,ExamName,ExamTotalMarks,ExamDateWithTime,ExamDuration,ExamConstraint from EX_Exam order by ExamDateWithTime desc,ExamName asc

select distinct EX_QuestionGeneration.ExamID, EX_QuestionGeneration.SetupQuestionMark, 
EX_QuestionGeneration.GeneratorID, EX_Question.QuestionID, EX_Question.QuestionText,EX_Question.QuestionTypeID, 
EX_Question.QuestionPossibleAnswerTime, EX_Label.LabelID, EX_Label.LabelName, EX_Category.CategoryID,
EX_Category.CategoryName from EX_QuestionGeneration,EX_Question,EX_Category,EX_Label 
where EX_Question.QuestionLabelID=EX_Label.LabelID and EX_Question.QuestionCategoryID=EX_Category.CategoryID 
and EX_Question.QuestionID=EX_QuestionGeneration.QuestionID and 
EX_QuestionGeneration.ExamID='ff2607e1-1ec3-400f-9bf4-519cca2474d7' 
order by EX_Category.CategoryName,EX_Label.LabelName,EX_Question.QuestionTypeID asc


select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark,
EX_CandidateExam.AnswerAttachmentPath, EX_Question.QuestionID, 
EX_Question.QuestionText,EX_QuestionGeneration.SetupQuestionMark, 
EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from 
EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID
inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID 
where EX_CandidateExam.CandidateID='hira633371297142862500' 
and EX_QuestionGeneration.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be' and
EX_CandidateExam.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be' 
and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID 
from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be') 
order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc


select distinct EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer, 
EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_CandidateExam on 
EX_CandidateExam.QuestionID=EX_Objective.ObjectiveQuestionID 
where EX_CandidateExam.QuestionID='bd452c3f-95d5-4be9-b910-117e6035fa60' 
and EX_CandidateExam.CandidateID='hira633371297142862500' 
and EX_CandidateExam.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be' 
and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration 
where EX_QuestionGeneration.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be')

select distinct EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer, 
EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join 
EX_CandidateExam on EX_CandidateExam.QuestionID=EX_Objective.ObjectiveQuestionID 
where EX_CandidateExam.QuestionID='a56a85de-db36-4a58-8936-91a0dcf1a8a6' 
and EX_CandidateExam.CandidateID='hira633371297142862500' 
and EX_CandidateExam.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be' 
and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID 
from EX_QuestionGeneration where 
EX_QuestionGeneration.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be')

select EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer, 
EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_CandidateExam 
on EX_CandidateExam.QuestionID=EX_Objective.ObjectiveQuestionID 
where EX_CandidateExam.QuestionID='a56a85de-db36-4a58-8936-91a0dcf1a8a6' 
and EX_CandidateExam.CandidateID='hira633371297142862500' and 
EX_CandidateExam.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be' and EX_CandidateExam.QuestionID 
in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration 
where EX_QuestionGeneration.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be')


------Get the TotalSetupMarkOfTypeOfCategory
select sum(EX_QuestionGeneration.SetupQuestionMark) as TotalSetupMarkOfTypeOfCategory
from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID
inner join EX_QuestionType on EX_QuestionType.TypeID = EX_Question.QuestionTypeID
where EX_QuestionGeneration.ExamID='fa0b62b8-f2cc-48b0-95c7-c384687080be'
and EX_Question.QuestionCategoryID='4'
and EX_QuestionType.TypeID='0'
-----end TotalSetupMarkOfTypeOfCategory