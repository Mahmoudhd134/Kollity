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

            insert into KollityReportingDb.reporting.CourseDoctorAndAssistant(course_id,
                                                           doctor_id,
                                                           is_doctor,
                                                           assigning_date,
                                                           is_currently_assigned)
            select id, doctor_id, cast(1 as bit), getdate(), cast(1 as bit)
            from KollityServicesDb.services.Course c
            where c.doctor_id is not null
            union all
            select ca.course_id, ca.assistant_id, cast(0 as bit), getdate(), cast(1 as bit)
            from KollityServicesDb.services.CourseAssistant ca

            insert into KollityReportingDb.reporting.CourseStudent (course_id, student_id)
            select course_id, student_id
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
            insert into KollityReportingDb.reporting.Assignment (id, name, description, mode, created_date, last_update_date,
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

select *
from KollityReportingDb.reporting.[Exam]
order by exam_id, question_id, option_id

select *
from KollityReportingDb.reporting.[AssignmentGroup]

select *
from KollityReportingDb.reporting.[AssignmentAnswer]

select *
from KollityReportingDb.reporting.__EFMigrationsHistory


declare @user_id uniqueidentifier = '63d471c5-07d0-4df0-3079-08dc2f249cd2'
declare @room_id uniqueidentifier = 'ee7ae979-3ded-4b0b-056b-08dc2f249fcd'

select *
from reporting.Exam E
inner join reporting.RoomUser RU on E.room_id = RU.room_id and RU.room_id = @room_id and RU.user_id = @user_id

