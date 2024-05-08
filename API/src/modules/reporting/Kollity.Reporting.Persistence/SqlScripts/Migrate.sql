begin
    begin transaction migrate_data
        begin try
            -- migrate user data
            insert into KollityReportingDb.reporting.[User](id,
                                                            user_name,
                                                            email,
                                                            full_name_in_arabic,
                                                            profile_image,
                                                            is_deleted,
                                                            type,
                                                            doctor_type,
                                                            code)
            select id,
                   user_name,
                   email,
                   full_name_in_arabic,
                   profile_image,
                   cast(0 as bit),
                   type,
                   case
                       when user_name like '%Doctor%' then 1
                       when user_name like '%Assistant%' then 2
                       end as type,
                   code
            from KollityServicesDb.services.[User]
            -- migration of user data ended


-- migrate course data
            insert into KollityReportingDb.reporting.Course(id, department, code, hours, name, is_deleted)
            select id, department, code, hours, name, cast(0 as bit)
            from KollityServicesDb.services.Course

            insert into KollityReportingDb.reporting.CourseDoctorAndAssistant(id,
                                                                              course_id,
                                                                              doctor_id,
                                                                              is_doctor,
                                                                              assigning_date,
                                                                              is_currently_assigned)
            select newid(), id, doctor_id, cast(1 as bit), getdate(), cast(1 as bit)
            from KollityServicesDb.services.Course c
            where c.doctor_id is not null
            union all
            select newid(), ca.course_id, ca.assistant_id, cast(0 as bit), getdate(), cast(1 as bit)
            from KollityServicesDb.services.CourseAssistant ca

            insert into KollityReportingDb.reporting.CourseStudent (id, course_id, student_id, is_currently_assigned, assigning_date)
            select newid(), course_id, student_id, cast(1 as bit), getdate()
            from KollityServicesDb.services.StudentCourse
            -- migration of course data ended


            --migrate rooms data
            insert into KollityReportingDb.reporting.Room (id, name, course_id, doctor_id, is_deleted, created_at)
            select id, name, course_id, doctor_id, cast(0 as bit), getdate()
            from KollityServicesDb.services.Room;

            insert into KollityReportingDb.reporting.RoomUser(room_id, user_id)
            select room_id, user_id
            from KollityServicesDb.services.UserRoom
            where join_request_accepted = 1
            -- migration of rooms data ended

            -- migrate exam data
            insert into KollityReportingDb.reporting.Exam (id, name, start_date, end_date, creation_date, room_id)
            select e.id, name, start_date, end_date, creation_date, room_id
            from KollityServicesDb.services.Exam E

            insert into KollityReportingDb.reporting.ExamQuestion (id, exam_id, question, open_for_seconds, degree)
            select EQ.id, exam_id, question, open_for_seconds, degree
            from KollityServicesDb.services.ExamQuestion EQ

            insert into KollityReportingDb.reporting.ExamQuestionOption (id, exam_question_id, [option], is_right_option)
            select EQO.id, exam_question_id, [option], is_right_option
            from KollityServicesDb.services.ExamQuestionOption EQO

            insert into KollityReportingDb.reporting.ExamAnswer (id, student_id, exam_id, exam_question_id,
                                                                 exam_question_option_id, request_time, submit_time,
                                                                 room_id)
            select EA.id,
                   student_id,
                   exam_id,
                   exam_question_id,
                   exam_question_option_id,
                   request_time,
                   submit_time,
                   E.room_id
            from KollityServicesDb.services.ExamAnswer EA
                     left join KollityServicesDB.services.Exam E on E.id = EA.exam_id
            where EA.exam_question_option_id is not null and EA.submit_time is not null and EA.student_id is not null
            -- migration of exam data ended

            -- migrate assignment data
            insert into KollityReportingDb.reporting.Assignment (id, name, description, mode, created_date,
                                                                 last_update_date,
                                                                 open_until_date, room_id, doctor_id, is_deleted)
            select id,
                   name,
                   description,
                   mode,
                   created_date,
                   last_update_date,
                   open_until_date,
                   room_id,
                   doctor_id,
                   cast(0 as bit)
            from KollityServicesDb.services.Assigment

            insert into KollityReportingDb.reporting.AssignmentGroup (id, code, room_id, student_id)
            select AG.id, AG.code, AG.room_id, AGS.student_id
            from KollityServicesDb.services.AssignmentGroup AG
                     inner join KollityServicesDb.services.AssignmentGroupStudent AGS on AG.id = AGS.assignment_group_id

            insert into KollityReportingDb.reporting.AssignmentAnswer (assignment_id, student_id, degree, group_id, room_id)
            select AA.assignment_id, AA.student_id, AA.degree, null, A.room_id
            from KollityServicesDb.services.AssignmentAnswer AA
                     left join KollityServicesDb.services.Assigment A on A.id = AA.assignment_id
            where AA.student_id is not null
            union all
            select AAD.assignment_id, AAD.student_id, AAD.degree, G.id, A.room_id
            from KollityServicesDb.services.AssignmentAnswerDegree AAD
                     inner join KollityServicesDb.services.AssignmentGroup G on G.id = AAD.group_id
                     left join KollityServicesDb.services.Assigment A on A.id = AAD.assignment_id

            -- migration of assignment data ended

            commit transaction migrate_data
        end try
        begin catch
            select error_message(), error_line()
            rollback transaction migrate_data
        end catch
end

delete
from KollityReportingDb.reporting.[ExamAnswer]
delete
from KollityReportingDb.reporting.[ExamQuestionOption]
delete
from KollityReportingDb.reporting.[ExamQuestion]
delete
from KollityReportingDb.reporting.[Exam]
delete
from KollityReportingDb.reporting.AssignmentAnswer
delete
from KollityReportingDb.reporting.AssignmentGroup
delete
from KollityReportingDb.reporting.Assignment
delete
from KollityReportingDb.reporting.[RoomUser]
delete
from KollityReportingDb.reporting.[Room]
delete
from KollityReportingDb.reporting.[CourseDoctorAndAssistant]
delete
from KollityReportingDb.reporting.[CourseStudent]
delete
from KollityReportingDb.reporting.[Course]
delete
from KollityReportingDb.reporting.[User]

select *
from KollityReportingDb.reporting.[ExamAnswer]

select E.name, E.question_text, E.question_degree, E.[option], E.is_right_option
from KollityReportingDb.reporting.[ExamAnswer]
         left join reporting.Exam E on E.id = ExamAnswer.option_id

select *
from KollityReportingDb.reporting.[Exam]
order by exam_id, question_id, option_id

select *
from KollityReportingDb.reporting.[AssignmentGroup]

select *
from KollityReportingDb.reporting.[AssignmentAnswer]

select *
from KollityReportingDb.reporting.__EFMigrationsHistory


begin
    declare @user_id uniqueidentifier = '63d471c5-07d0-4df0-3079-08dc2f249cd2'
    declare @room_id uniqueidentifier = 'ee7ae979-3ded-4b0b-056b-08dc2f249fcd'

    select T.exam_id              as ExamId,
           T.name                 as Name,
           T.start_date           as StartDate,
           T.end_date             as EndDate,
           R.name                 as RoomName,
           A.user_name            as RoomDoctor,
           C.name                 as CourseName,
           count(T.question_id)   as QuestionsCount,
           sum(T.question_degree) as TotalDegree,
           SD.user_degree         as UserDegree
    from (select distinct E.exam_id,
                          E.name,
                          E.start_date,
                          E.end_date,
                          E.question_id,
                          E.question_degree,
                          E.room_id
          from reporting.Exam E
          where E.room_id = @room_id) as T
             left join reporting.Room R on R.id = T.room_id
             left join reporting.[User] A on A.id = R.doctor_id and A.type = 'Doctor' and A.doctor_type = 2
             left join reporting.Course C on C.id = R.course_id
             left join (select E1.exam_id,
                               sum(case
                                       when E1.is_right_option = 0 then 0
                                       when E1.is_right_option = 1 then E1.question_degree
                                   end) as user_degree
                        from reporting.ExamAnswer EA1
                                 left join reporting.Exam E1 on E1.id = EA1.option_id
                        where EA1.student_id = @user_id
                        group by E1.exam_id) SD on SD.exam_id = T.exam_id
    group by T.exam_id, T.name, T.start_date, T.end_date, R.name, R.doctor_id, C.name, A.user_name, SD.user_degree

    SELECT [t0].[ExamId],
           [t0].[Name],
           [t0].[StartDate],
           [t0].[EndDate],
           [t0].[RoomName],
           [t0].[RoomDoctor],
           [t0].[CourseName],
           [t0].[CourseDoctor],
           COUNT(*)                                             AS [QuestionsCount],
           COALESCE(SUM(CAST([t0].[QuestionDegree] AS int)), 0) AS [TotalDegree]
    FROM (SELECT DISTINCT [e].[exam_id]                                         AS [ExamId],
                          [e].[name]                                            AS [Name],
                          [e].[start_date]                                      AS [StartDate],
                          [e].[end_date]                                        AS [EndDate],
                          [r].[name]                                            AS [RoomName],
                          [t].[user_name]                                       AS [RoomDoctor],
                          [c].[name]                                            AS [CourseName],
                          (SELECT TOP (1) [t1].[user_name]
                           FROM [reporting].[CourseDoctorAndAssistant] AS [c0]
                                    INNER JOIN (SELECT [u0].[id],
                                                       [u0].[email],
                                                       [u0].[full_name_in_arabic],
                                                       [u0].[is_deleted],
                                                       [u0].[profile_image],
                                                       [u0].[type],
                                                       [u0].[user_name],
                                                       [u0].[doctor_type]
                                                FROM [reporting].[User] AS [u0]
                                                WHERE [u0].[type] = N'Doctor') AS [t1] ON [c0].[doctor_id] = [t1].[id]
                           WHERE [c].[id] = [c0].[course_id]
                             AND [c0].[is_doctor] = CAST(1 AS bit)
                             AND [c0].[is_currently_assigned] = CAST(1 AS bit)) AS [CourseDoctor],
                          [e].[question_id]                                     AS [QuestionId],
                          [e].[question_degree]                                 AS [QuestionDegree]
          FROM [reporting].[Exam] AS [e]
                   INNER JOIN [reporting].[Room] AS [r] ON [e].[room_id] = [r].[id]
                   INNER JOIN (SELECT [u].[id], [u].[user_name]
                               FROM [reporting].[User] AS [u]
                               WHERE [u].[type] = N'Doctor') AS [t] ON [r].[doctor_id] = [t].[id]
                   INNER JOIN [reporting].[Course] AS [c] ON [r].[course_id] = [c].[id]) AS [t0]
    GROUP BY [t0].[ExamId], [t0].[Name], [t0].[StartDate], [t0].[EndDate], [t0].[RoomName], [t0].[RoomDoctor],
             [t0].[CourseName], [t0].[CourseDoctor]


end