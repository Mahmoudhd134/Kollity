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
            select newid(),id, doctor_id, cast(1 as bit), getdate(), cast(1 as bit)
            from KollityServicesDb.services.Course c
            where c.doctor_id is not null
            union all
            select newid(),ca.course_id, ca.assistant_id, cast(0 as bit), getdate(), cast(1 as bit)
            from KollityServicesDb.services.CourseAssistant ca

            insert into KollityReportingDb.reporting.CourseStudent (id,course_id, student_id,is_currently_assigned,assigning_date)
            select newid(),course_id, student_id,cast(1 as bit),getdate()
            from KollityServicesDb.services.StudentCourse
            -- migration of course data ended


            --migrate rooms data
            insert into KollityReportingDb.reporting.Room (id, name, course_id, doctor_id, is_deleted)
            select id, name, course_id, doctor_id, cast(0 as bit)
            from KollityServicesDb.services.Room;

            insert into KollityReportingDb.reporting.RoomUser(room_id, user_id)
            select room_id, user_id
            from KollityServicesDb.services.UserRoom
            where join_request_accepted = 1
            -- migration of rooms data ended

            -- migrate exam data
            insert into KollityReportingDb.reporting.Exam (id,
                                                           exam_id,
                                                           question_id,
                                                           option_id,
                                                           name,
                                                           start_date,
                                                           end_date,
                                                           creation_date,
                                                           doctor_id,
                                                           room_id,
                                                           question_text,
                                                           question_open_for_seconds,
                                                           question_degree,
                                                           [option],
                                                           is_right_option)
            select newid(),
                   E.id,
                   EQ.id,
                   EQO.id,
                   E.name,
                   E.start_date,
                   E.end_date,
                   E.creation_date,
                   R.doctor_id,
                   E.room_id,
                   EQ.question,
                   EQ.open_for_seconds,
                   EQ.degree,
                   EQO.[option],
                   EQO.is_right_option
            from KollityServicesDb.services.Exam E
                     left join KollityServicesDb.services.Room R on R.id = E.room_id
                     left join KollityServicesDb.services.ExamQuestion EQ on E.id = EQ.exam_id
                     left join KollityServicesDb.services.ExamQuestionOption EQO on EQ.id = EQO.exam_question_id

            insert into KollityReportingDb.reporting.ExamAnswer (student_id, option_id, request_time, submit_time)
            select EA.student_id, E.id, EA.request_time, EA.submit_time
            from KollityServicesDb.services.ExamAnswer EA
                     inner join KollityReportingDb.reporting.Exam E
                                on E.option_id = EA.exam_question_option_id and E.question_id = EA.exam_question_id
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

            insert into KollityReportingDb.reporting.AssignmentAnswer (assignment_id, student_id, degree, group_id)
            select AA.assignment_id, AA.student_id, AA.degree, null
            from KollityServicesDb.services.AssignmentAnswer AA
            where AA.student_id is not null
            union all
            select AAD.assignment_id, AAD.student_id, AAD.degree, G.id
            from KollityServicesDb.services.AssignmentAnswerDegree AAD
                     inner join KollityServicesDb.services.AssignmentGroup G on G.id = AAD.group_id

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
from KollityReportingDb.reporting.[User]

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

    SELECT [t0].[ExamId], [t0].[Name], [t0].[StartDate], [t0].[EndDate], [t0].[RoomName], [t0].[RoomDoctor], [t0].[CourseName], [t0].[CourseDoctor], COUNT(*) AS [QuestionsCount], COALESCE(SUM(CAST([t0].[QuestionDegree] AS int)), 0) AS [TotalDegree]
    FROM (
             SELECT DISTINCT [e].[exam_id] AS [ExamId], [e].[name] AS [Name], [e].[start_date] AS [StartDate], [e].[end_date] AS [EndDate], [r].[name] AS [RoomName], [t].[user_name] AS [RoomDoctor], [c].[name] AS [CourseName], (
                 SELECT TOP(1) [t1].[user_name]
                 FROM [reporting].[CourseDoctorAndAssistant] AS [c0]
                          INNER JOIN (
                     SELECT [u0].[id], [u0].[email], [u0].[full_name_in_arabic], [u0].[is_deleted], [u0].[profile_image], [u0].[type], [u0].[user_name], [u0].[doctor_type]
                     FROM [reporting].[User] AS [u0]
                     WHERE [u0].[type] = N'Doctor'
                 ) AS [t1] ON [c0].[doctor_id] = [t1].[id]
                 WHERE [c].[id] = [c0].[course_id] AND [c0].[is_doctor] = CAST(1 AS bit) AND [c0].[is_currently_assigned] = CAST(1 AS bit)) AS [CourseDoctor], [e].[question_id] AS [QuestionId], [e].[question_degree] AS [QuestionDegree]
             FROM [reporting].[Exam] AS [e]
                      INNER JOIN [reporting].[Room] AS [r] ON [e].[room_id] = [r].[id]
                      INNER JOIN (
                 SELECT [u].[id], [u].[user_name]
                 FROM [reporting].[User] AS [u]
                 WHERE [u].[type] = N'Doctor'
             ) AS [t] ON [r].[doctor_id] = [t].[id]
                      INNER JOIN [reporting].[Course] AS [c] ON [r].[course_id] = [c].[id]
         ) AS [t0]
    GROUP BY [t0].[ExamId], [t0].[Name], [t0].[StartDate], [t0].[EndDate], [t0].[RoomName], [t0].[RoomDoctor], [t0].[CourseName], [t0].[CourseDoctor]


end

-- {
-- "studentId": "63d471c5-07d0-4df0-3079-08dc2f249cd2",
-- "userName": "MahmoudStudent21",
-- "fullName": "this is my student21'th full name",
-- "code": "21",
-- "profileImage": null,
-- "roomId": "ee7ae979-3ded-4b0b-056b-08dc2f249fcd",
-- "roomName": "this is room for assistant -413e0a64-3478-4428-3298-08dc2f249cd2",
-- "exams": [
--     {
--       "id": "5bbb36a8-7625-4260-6979-08dc3fc4ce4f",
--       "name": "string",
--       "startDate": "2024-03-08T23:09:13.522Z",
--       "endDate": "2024-03-30T23:10:13.522Z",
--       "totalDegree": 0,
--       "numberOfQuestions": 0,
--       "studentDegree": 0
--     },
--     {
--       "id": "180bc292-f7f7-4a2e-c0ec-08dc3fcce821",
--       "name": "this is new exam",
--       "startDate": "2024-03-09T23:35:06.808Z",
--       "endDate": "2024-03-30T15:24:06.808Z",
--       "totalDegree": 8,
--       "numberOfQuestions": 4,
--       "studentDegree": 1
--     },
--     {
--       "id": "ad6b00f4-4b38-49c7-f6b6-08dc45714d9e",
--       "name": "string",
--       "startDate": "2024-03-16T04:26:43.347Z",
--       "endDate": "2024-03-16T05:26:43.347Z",
--       "totalDegree": 0,
--       "numberOfQuestions": 0,
--       "studentDegree": 0
--     },
--     {
--       "id": "b6b57e26-d365-4b9b-b273-08dc528e81e1",
--       "name": "string",
--       "startDate": "2024-04-01T20:58:24.761Z",
--       "endDate": "2024-04-02T20:58:24.761Z",
--       "totalDegree": 0,
--       "numberOfQuestions": 0,
--       "studentDegree": 0
--     },
--     {
--       "id": "fbbfed20-b9bd-45cf-b274-08dc528e81e1",
--       "name": "string",
--       "startDate": "2024-04-01T20:58:24.761Z",
--       "endDate": "2024-04-02T20:58:24.761Z",
--       "totalDegree": 0,
--       "numberOfQuestions": 0,
--       "studentDegree": 0
--     },
--     {
--       "id": "c5c2ddd6-56f7-4e78-b275-08dc528e81e1",
--       "name": "string",
--       "startDate": "2024-04-01T20:58:24.761Z",
--       "endDate": "2024-04-02T20:58:24.761Z",
--       "totalDegree": 0,
--       "numberOfQuestions": 0,
--       "studentDegree": 0
--     },
--     {
--       "id": "709c554a-9469-459b-c3ae-08dc528eaf85",
--       "name": "string",
--       "startDate": "2024-04-01T20:58:24.761Z",
--       "endDate": "2024-04-02T20:58:24.761Z",
--       "totalDegree": 0,
--       "numberOfQuestions": 0,
--       "studentDegree": 0
--     },
--     {
--       "id": "e91b4e94-a0ec-4104-c3af-08dc528eaf85",
--       "name": "string",
--       "startDate": "2024-04-01T20:58:24.761Z",
--       "endDate": "2024-04-02T20:58:24.761Z",
--       "totalDegree": 0,
--       "numberOfQuestions": 0,
--       "studentDegree": 0
--     },
--     {
--       "id": "c86c1cfe-d972-471e-45ad-08dc528f0421",
--       "name": "string",
--       "startDate": "2024-04-01T20:58:24.761Z",
--       "endDate": "2024-04-02T20:58:24.761Z",
--       "totalDegree": 0,
--       "numberOfQuestions": 0,
--       "studentDegree": 0
--     },
--     {
--       "id": "d51439a3-c515-40bd-9432-08dc54f39179",
--       "name": "string",
--       "startDate": "2024-04-04T22:06:06.99Z",
--       "endDate": "2024-11-04T22:06:06.99Z",
--       "totalDegree": 0,
--       "numberOfQuestions": 0,
--       "studentDegree": 0
--     },
--     {
--       "id": "6fb38265-256c-47c1-3e5b-08dc5e76f7fe",
--       "name": "aaa",
--       "startDate": "2024-04-17T00:42:15.179Z",
--       "endDate": "2024-04-17T01:40:15.179Z",
--       "totalDegree": 22,
--       "numberOfQuestions": 2,
--       "studentDegree": 22
--     }
--   ],
-- "assignments": [
--     {
--       "id": "d07df928-8edd-4425-e6f1-08dc34a0cb9d",
--       "name": "indi",
--       "mode": 1,
--       "startDate": "2024-02-23T18:53:59.5107065Z",
--       "endDate": "2024-09-01T00:00:00Z",
--       "submitDate": "2024-03-05T22:16:32.0451882Z",
--       "studentDegree": 16
--     },
--     {
--       "id": "9cca005c-2708-4d68-e6f2-08dc34a0cb9d",
--       "name": "group",
--       "mode": 2,
--       "startDate": "2024-02-23T18:54:05.178822Z",
--       "endDate": "2025-01-01T00:00:00Z",
--       "submitDate": "2024-04-17T00:12:48.4810519Z",
--       "studentDegree": 19
--     },
--     {
--       "id": "ad2672f6-081d-4671-d766-08dc3fbd350c",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-08T22:15:05.0784936Z",
--       "endDate": "2024-03-08T22:15:01.697Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "4a794b40-399d-440a-8f49-08dc4217576b",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:05:19.790813Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "a34bf9e8-900d-40b5-8f4a-08dc4217576b",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:05:47.3389592Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "7d689849-1469-4397-8f4b-08dc4217576b",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:06:16.9081674Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "8c2f6164-c340-4ffa-8f4c-08dc4217576b",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:06:43.9921311Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "5ad032a7-753c-42ee-8f4d-08dc4217576b",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:06:54.3546941Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "af78e8dc-7abe-409e-8f4e-08dc4217576b",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:07:18.5192685Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "6d2f2caf-cd37-4d78-8f4f-08dc4217576b",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:07:49.4219523Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "91b2b913-83f2-4746-8f50-08dc4217576b",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:08:04.8957185Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "a100c112-e15c-49a3-8340-08dc421a6ec9",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:27:27.4561296Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "ac078ab9-fdeb-4506-8341-08dc421a6ec9",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:27:45.5555578Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "47ada139-256c-49ec-8342-08dc421a6ec9",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:28:18.6295531Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "1bd9d6bc-f4a7-4c7c-c15d-08dc421a9deb",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:28:46.5479095Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "f6519eab-11fa-4f2b-c15e-08dc421a9deb",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:28:56.4321857Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "dd3fb506-3861-4c7f-c15f-08dc421a9deb",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:29:46.9964359Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "50c990d4-d669-493d-27da-08dc421d1a27",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:46:33.9490106Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "b4206ab4-fc6e-4c98-0de8-08dc421d4740",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:47:49.5787524Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "4e63f657-b5be-48f4-65ca-08dc421dece8",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T22:52:27.5527559Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "fa0847af-38dc-42ab-6618-08dc421f51ab",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T23:02:26.0786978Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "ac0b8931-5318-44df-6619-08dc421f51ab",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-11T23:02:43.6951613Z",
--       "endDate": "2024-03-11T22:05:17.193Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "f59e0838-4314-4df1-8064-08dc44650232",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-14T20:26:19.8899929Z",
--       "endDate": "2024-03-14T20:26:17.565Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "1b9d699c-affa-4269-d75f-08dc44654967",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-14T20:28:19.3093449Z",
--       "endDate": "2024-03-14T20:26:17.565Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "0f986f9f-1f55-4ca7-02cc-08dc4465b167",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-14T20:31:13.7977738Z",
--       "endDate": "2024-03-14T20:26:17.565Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "9f52828e-fc86-42cb-9e3c-08dc4465c45e",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-14T20:31:45.5941307Z",
--       "endDate": "2024-03-14T20:26:17.565Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "76d58b05-734d-4c20-8116-08dc44911b1c",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-15T01:41:59.532473Z",
--       "endDate": "2024-03-15T01:41:56.924Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "293b1dc3-bb33-48df-c771-08dc44930926",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-15T01:55:48.3811182Z",
--       "endDate": "2024-03-15T01:41:56.924Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "271da3ef-9d11-45e5-8ce4-08dc44977e92",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-15T02:27:43.3637614Z",
--       "endDate": "2024-03-15T01:41:56.924Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "4ac995fa-27a8-4242-f126-08dc44988159",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-15T02:34:57.5225341Z",
--       "endDate": "2024-03-15T01:41:56.924Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "67ba83bf-5a71-455a-3aa3-08dc456064a0",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T02:25:48.701577Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "ae67b9a6-3cf8-410b-190f-08dc4570c107",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:22:55.6555645Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "aceef994-7ffb-46cd-5c07-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:10.4551449Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "80bc09ed-4d71-4037-5c08-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:11.5333862Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "1dad8730-3725-4af6-5c09-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:12.096403Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "1446658a-c0f8-440e-5c0a-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:12.457095Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "52e3ca26-a899-4950-5c0b-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:12.7314928Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "1d2a2f24-4df1-438c-5c0c-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:12.9384311Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "d6cc5f2e-a5a2-4af6-5c0d-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:13.3036048Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "60b7803e-79a7-41c9-5c0e-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:13.5186184Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "89baf210-4f4f-4d0d-5c0f-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:13.7187594Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "1abff23b-5fb4-400a-5c10-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:13.9660734Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "533bfb8a-8e4d-4ede-5c11-08dc457329d1",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-16T04:40:14.1356837Z",
--       "endDate": "2024-03-16T02:25:28.956Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "66c65a7e-7189-4881-69ff-08dc4621e05c",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-17T01:30:49.1530026Z",
--       "endDate": "2024-03-17T01:30:46.274Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "84e11c5a-53d9-4e5a-6a00-08dc4621e05c",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-17T01:31:12.7479879Z",
--       "endDate": "2024-03-17T01:30:46.274Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "366bef23-282c-4f8e-6a01-08dc4621e05c",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-17T01:32:06.657608Z",
--       "endDate": "2024-03-17T01:30:46.274Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "bc49ad20-40e9-4eb8-4c24-08dc46226561",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-17T01:34:32.3276209Z",
--       "endDate": "2024-03-17T01:30:46.274Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "883b1e1a-179b-4dce-4c25-08dc46226561",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-17T01:34:50.5697972Z",
--       "endDate": "2024-03-17T01:30:46.274Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "ac244193-d52c-4347-4c26-08dc46226561",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-17T01:36:16.999745Z",
--       "endDate": "2024-03-17T01:30:46.274Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "dc98b6b5-154a-4eed-d11b-08dc4627a53d",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-17T02:12:06.9632854Z",
--       "endDate": "2024-03-17T01:30:46.274Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "f03439d8-53c8-4f33-d11c-08dc4627a53d",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-17T02:12:27.0848503Z",
--       "endDate": "2024-03-17T01:30:46.274Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "65e86305-28ac-4d43-981e-08dc5063b9ad",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-30T02:47:22.6590117Z",
--       "endDate": "2024-03-30T02:46:32.739Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "3adc009d-40d4-471e-981f-08dc5063b9ad",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-03-30T02:48:56.0777284Z",
--       "endDate": "2024-03-30T02:46:32.739Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     },
--     {
--       "id": "285b7e6d-1181-44d5-b546-08dc528e6efe",
--       "name": "string",
--       "mode": 1,
--       "startDate": "2024-04-01T20:58:08.0609388Z",
--       "endDate": "2024-04-01T20:57:50.44Z",
--       "submitDate": "0001-01-01T00:00:00Z",
--       "studentDegree": 0
--     }
--   ],
-- "courseId": "e971ce05-a6b2-4a66-556b-08dc2f249d91",
-- "courseName": "Licensed Frozen Tuna"
-- }