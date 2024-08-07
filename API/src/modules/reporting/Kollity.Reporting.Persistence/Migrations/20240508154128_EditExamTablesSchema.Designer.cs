﻿// <auto-generated />
using System;
using Kollity.Reporting.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kollity.Reporting.Persistence.Migrations
{
    [DbContext(typeof(ReportingDbContext))]
    [Migration("20240508154128_EditExamTablesSchema")]
    partial class EditExamTablesSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("reporting")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Kollity.Reporting.Domain.AssignmentModels.Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<byte>("Degree")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)20)
                        .HasColumnName("degree");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4095)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("doctor_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_update_date");

                    b.Property<int>("Mode")
                        .HasColumnType("int")
                        .HasColumnName("mode");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasColumnName("name");

                    b.Property<DateTime>("OpenUntilDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("open_until_date");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("room_id");

                    b.HasKey("Id")
                        .HasName("pk_assignment");

                    b.HasIndex("DoctorId")
                        .HasDatabaseName("ix_assignment_doctor_id");

                    b.HasIndex("RoomId")
                        .HasDatabaseName("ix_assignment_room_id");

                    b.ToTable("Assignment", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.AssignmentModels.AssignmentAnswer", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("student_id");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("assignment_id");

                    b.Property<int?>("Degree")
                        .HasColumnType("int")
                        .HasColumnName("degree");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("group_id");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("room_id");

                    b.HasKey("StudentId", "AssignmentId")
                        .HasName("pk_assignment_answer");

                    b.HasIndex("AssignmentId")
                        .HasDatabaseName("ix_assignment_answer_assignment_id");

                    b.HasIndex("RoomId")
                        .HasDatabaseName("ix_assignment_answer_room_id");

                    b.HasIndex("GroupId", "StudentId")
                        .HasDatabaseName("ix_assignment_answer_group_id_student_id");

                    b.ToTable("AssignmentAnswer", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.AssignmentModels.AssignmentGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("student_id");

                    b.Property<int>("Code")
                        .HasColumnType("int")
                        .HasColumnName("code");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("room_id");

                    b.HasKey("Id", "StudentId")
                        .HasName("pk_assignment_group");

                    b.HasIndex("Code")
                        .HasDatabaseName("ix_assignment_group_code");

                    b.HasIndex("RoomId")
                        .HasDatabaseName("ix_assignment_group_room_id");

                    b.HasIndex("StudentId")
                        .HasDatabaseName("ix_assignment_group_student_id");

                    b.ToTable("AssignmentGroup", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.CourseModels.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("Code")
                        .HasColumnType("int")
                        .HasColumnName("code");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("department");

                    b.Property<int>("Hours")
                        .HasColumnType("int")
                        .HasColumnName("hours");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_course");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_course_code");

                    b.ToTable("Course", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.CourseModels.CourseDoctorAndAssistants", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("AssigningDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("assigning_date");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("course_id");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("doctor_id");

                    b.Property<bool>("IsCurrentlyAssigned")
                        .HasColumnType("bit")
                        .HasColumnName("is_currently_assigned");

                    b.Property<bool>("IsDoctor")
                        .HasColumnType("bit")
                        .HasColumnName("is_doctor");

                    b.HasKey("Id")
                        .HasName("pk_course_doctor_and_assistant");

                    b.HasIndex("DoctorId")
                        .HasDatabaseName("ix_course_doctor_and_assistant_doctor_id");

                    b.HasIndex("CourseId", "DoctorId")
                        .HasDatabaseName("ix_course_doctor_and_assistant_course_id_doctor_id");

                    b.ToTable("CourseDoctorAndAssistant", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.CourseModels.CourseStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("AssigningDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("assigning_date");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("course_id");

                    b.Property<bool>("IsCurrentlyAssigned")
                        .HasColumnType("bit")
                        .HasColumnName("is_currently_assigned");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("student_id");

                    b.HasKey("Id")
                        .HasName("pk_course_student");

                    b.HasIndex("StudentId")
                        .HasDatabaseName("ix_course_student_student_id");

                    b.HasIndex("CourseId", "StudentId")
                        .HasDatabaseName("ix_course_student_course_id_student_id");

                    b.ToTable("CourseStudent", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.Exam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("creation_date");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_updated_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("room_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.HasKey("Id")
                        .HasName("pk_exam");

                    b.HasIndex("RoomId")
                        .HasDatabaseName("ix_exam_room_id");

                    b.ToTable("Exam", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.ExamAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ExamId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("exam_id");

                    b.Property<Guid>("ExamQuestionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("exam_question_id");

                    b.Property<Guid>("ExamQuestionOptionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("exam_question_option_id");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("request_time");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("room_id");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("student_id");

                    b.Property<DateTime>("SubmitTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("submit_time");

                    b.HasKey("Id")
                        .HasName("pk_exam_answer");

                    b.HasIndex("ExamId")
                        .HasDatabaseName("ix_exam_answer_exam_id");

                    b.HasIndex("ExamQuestionId")
                        .HasDatabaseName("ix_exam_answer_exam_question_id");

                    b.HasIndex("ExamQuestionOptionId")
                        .HasDatabaseName("ix_exam_answer_exam_question_option_id");

                    b.HasIndex("RoomId")
                        .HasDatabaseName("ix_exam_answer_room_id");

                    b.HasIndex("StudentId", "ExamQuestionId")
                        .IsUnique()
                        .HasDatabaseName("ix_exam_answer_student_id_exam_question_id");

                    b.ToTable("ExamAnswer", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.ExamQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<byte>("Degree")
                        .HasColumnType("tinyint")
                        .HasColumnName("degree");

                    b.Property<Guid>("ExamId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("exam_id");

                    b.Property<int>("OpenForSeconds")
                        .HasColumnType("int")
                        .HasColumnName("open_for_seconds");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)")
                        .HasColumnName("question");

                    b.HasKey("Id")
                        .HasName("pk_exam_question");

                    b.HasIndex("ExamId")
                        .HasDatabaseName("ix_exam_question_exam_id");

                    b.ToTable("ExamQuestion", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.ExamQuestionOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ExamQuestionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("exam_question_id");

                    b.Property<bool>("IsRightOption")
                        .HasColumnType("bit")
                        .HasColumnName("is_right_option");

                    b.Property<string>("Option")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)")
                        .HasColumnName("option");

                    b.HasKey("Id")
                        .HasName("pk_exam_question_option");

                    b.HasIndex("ExamQuestionId")
                        .HasDatabaseName("ix_exam_question_option_exam_question_id");

                    b.ToTable("ExamQuestionOption", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.RoomModels.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("course_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("doctor_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(227)
                        .HasColumnType("nvarchar(227)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_room");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("ix_room_course_id");

                    b.HasIndex("DoctorId")
                        .HasDatabaseName("ix_room_doctor_id");

                    b.ToTable("Room", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.RoomModels.RoomUser", b =>
                {
                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("room_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("RoomId", "UserId")
                        .HasName("pk_room_user");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_room_user_user_id");

                    b.ToTable("RoomUser", "reporting");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.UserModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("FullNameInArabic")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("full_name_in_arabic");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("ProfileImage")
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)")
                        .HasColumnName("profile_image");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("type");

                    b.Property<string>("UserName")
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_user_email")
                        .HasFilter("[email] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasDatabaseName("ix_user_user_name")
                        .HasFilter("[user_name] IS NOT NULL");

                    b.ToTable("User", "reporting");

                    b.HasDiscriminator<string>("Type").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.UserModels.Doctor", b =>
                {
                    b.HasBaseType("Kollity.Reporting.Domain.UserModels.User");

                    b.Property<int>("DoctorType")
                        .HasColumnType("int")
                        .HasColumnName("doctor_type");

                    b.ToTable("User", "reporting");

                    b.HasDiscriminator().HasValue("Doctor");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b26c556f-d543-4a2a-b15a-49fba7751ffa"),
                            Email = "nassermahmoud571@gmail.com",
                            FullNameInArabic = "Mahmoud Ahmed Nasser Mahmoud",
                            IsDeleted = false,
                            UserName = "Mahmoudhd134",
                            DoctorType = 1
                        });
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.UserModels.Student", b =>
                {
                    b.HasBaseType("Kollity.Reporting.Domain.UserModels.User");

                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("code");

                    b.ToTable("User", "reporting");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.AssignmentModels.Assignment", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.UserModels.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_assignment_doctors_doctor_id");

                    b.HasOne("Kollity.Reporting.Domain.RoomModels.Room", "Room")
                        .WithMany("Assignments")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_assignment_rooms_room_id");

                    b.Navigation("Doctor");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.AssignmentModels.AssignmentAnswer", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.AssignmentModels.Assignment", "Assignment")
                        .WithMany("AssignmentsAnswers")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_assignment_answer_assignments_assignment_id");

                    b.HasOne("Kollity.Reporting.Domain.RoomModels.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("fk_assignment_answer_rooms_room_id");

                    b.HasOne("Kollity.Reporting.Domain.UserModels.Student", "Student")
                        .WithMany("AssignmentAnswers")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_assignment_answer_students_student_id");

                    b.HasOne("Kollity.Reporting.Domain.AssignmentModels.AssignmentGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId", "StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("fk_assignment_answer_assignment_groups_group_id_student_id");

                    b.Navigation("Assignment");

                    b.Navigation("Group");

                    b.Navigation("Room");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.AssignmentModels.AssignmentGroup", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.RoomModels.Room", "Room")
                        .WithMany("AssignmentGroups")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_assignment_group_rooms_room_id");

                    b.HasOne("Kollity.Reporting.Domain.UserModels.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_assignment_group_students_student_id");

                    b.Navigation("Room");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.CourseModels.CourseDoctorAndAssistants", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.CourseModels.Course", "Course")
                        .WithMany("DoctorsAndAssistants")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_course_doctor_and_assistant_course_course_id");

                    b.HasOne("Kollity.Reporting.Domain.UserModels.Doctor", "Doctor")
                        .WithMany("DoctorsAndAssistants")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_course_doctor_and_assistant_doctors_doctor_id");

                    b.Navigation("Course");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.CourseModels.CourseStudent", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.CourseModels.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_course_student_course_course_id");

                    b.HasOne("Kollity.Reporting.Domain.UserModels.Student", "Student")
                        .WithMany("Courses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_course_student_students_student_id");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.Exam", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.RoomModels.Room", "Room")
                        .WithMany("Exams")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_exam_rooms_room_id");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.ExamAnswer", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.ExamModels.Exam", "Exam")
                        .WithMany("Answers")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_exam_answer_exams_exam_id");

                    b.HasOne("Kollity.Reporting.Domain.ExamModels.ExamQuestion", "ExamQuestion")
                        .WithMany("ExamAnswers")
                        .HasForeignKey("ExamQuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_exam_answer_exam_questions_exam_question_id");

                    b.HasOne("Kollity.Reporting.Domain.ExamModels.ExamQuestionOption", "ExamQuestionOption")
                        .WithMany("ExamAnswers")
                        .HasForeignKey("ExamQuestionOptionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_exam_answer_exams_question_options_exam_question_option_id");

                    b.HasOne("Kollity.Reporting.Domain.RoomModels.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_exam_answer_rooms_room_id");

                    b.HasOne("Kollity.Reporting.Domain.UserModels.Student", "Student")
                        .WithMany("ExamAnswers")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_exam_answer_students_student_id");

                    b.Navigation("Exam");

                    b.Navigation("ExamQuestion");

                    b.Navigation("ExamQuestionOption");

                    b.Navigation("Room");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.ExamQuestion", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.ExamModels.Exam", "Exam")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_exam_question_exam_exam_id");

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.ExamQuestionOption", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.ExamModels.ExamQuestion", "ExamQuestion")
                        .WithMany("ExamQuestionOptions")
                        .HasForeignKey("ExamQuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_exam_question_option_exam_question_exam_question_id");

                    b.Navigation("ExamQuestion");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.RoomModels.Room", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.CourseModels.Course", "Course")
                        .WithMany("Rooms")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_room_course_course_id");

                    b.HasOne("Kollity.Reporting.Domain.UserModels.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_room_doctors_doctor_id");

                    b.Navigation("Course");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.RoomModels.RoomUser", b =>
                {
                    b.HasOne("Kollity.Reporting.Domain.RoomModels.Room", "Room")
                        .WithMany("RoomUsers")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_room_user_room_room_id");

                    b.HasOne("Kollity.Reporting.Domain.UserModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_room_user_users_user_id");

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.AssignmentModels.Assignment", b =>
                {
                    b.Navigation("AssignmentsAnswers");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.CourseModels.Course", b =>
                {
                    b.Navigation("DoctorsAndAssistants");

                    b.Navigation("Rooms");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.Exam", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("ExamQuestions");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.ExamQuestion", b =>
                {
                    b.Navigation("ExamAnswers");

                    b.Navigation("ExamQuestionOptions");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.ExamModels.ExamQuestionOption", b =>
                {
                    b.Navigation("ExamAnswers");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.RoomModels.Room", b =>
                {
                    b.Navigation("AssignmentGroups");

                    b.Navigation("Assignments");

                    b.Navigation("Exams");

                    b.Navigation("RoomUsers");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.UserModels.Doctor", b =>
                {
                    b.Navigation("DoctorsAndAssistants");
                });

            modelBuilder.Entity("Kollity.Reporting.Domain.UserModels.Student", b =>
                {
                    b.Navigation("AssignmentAnswers");

                    b.Navigation("Courses");

                    b.Navigation("ExamAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
