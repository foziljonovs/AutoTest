﻿// <auto-generated />
using System;
using AutoTest.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AutoTest.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AutoTest.Domain.Entities.Files.TestFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FileName")
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.Property<string>("FilePath")
                        .HasColumnType("text")
                        .HasColumnName("file_path");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint")
                        .HasColumnName("test_id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("file_type");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("TestFiles");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.Option", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsChange")
                        .HasColumnType("boolean")
                        .HasColumnName("is_change");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean")
                        .HasColumnName("is_correct");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint")
                        .HasColumnName("question_id");

                    b.Property<string>("Text")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Problem")
                        .HasColumnType("text")
                        .HasColumnName("problem");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint")
                        .HasColumnName("test_id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.QuestionSolution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CustomAnswer")
                        .HasColumnType("text")
                        .HasColumnName("custom_answer");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean")
                        .HasColumnName("is_correct");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint")
                        .HasColumnName("question_id");

                    b.Property<int>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("score");

                    b.Property<long?>("SelectedOptionId")
                        .HasColumnType("bigint")
                        .HasColumnName("selected_option_id");

                    b.Property<long>("TestSolutionId")
                        .HasColumnType("bigint")
                        .HasColumnName("test_solution_id");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TestSolutionId");

                    b.ToTable("QuestionSolutions");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.Test", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<int>("Level")
                        .HasColumnType("integer")
                        .HasColumnName("level");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.TestSolution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("finished_at");

                    b.Property<int>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("score");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("started_at");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint")
                        .HasColumnName("test_id");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("TestSolutions");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.Topic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Users.SavedTest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint")
                        .HasColumnName("test_id");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.HasIndex("UserId");

                    b.ToTable("SavedTests");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Firstname")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("Lastname")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Salt")
                        .HasColumnType("text")
                        .HasColumnName("salt");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Users.UserTestSolution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("TestSolutionId")
                        .HasColumnType("bigint")
                        .HasColumnName("test_solution_id");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("TestSolutionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTestSolutions");
                });

            modelBuilder.Entity("TestTopic", b =>
                {
                    b.Property<long>("TestsId")
                        .HasColumnType("bigint");

                    b.Property<long>("TopicsId")
                        .HasColumnType("bigint");

                    b.HasKey("TestsId", "TopicsId");

                    b.HasIndex("TopicsId");

                    b.ToTable("TestTopic");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Files.TestFile", b =>
                {
                    b.HasOne("AutoTest.Domain.Entities.Tests.Test", "Test")
                        .WithMany("Files")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.Option", b =>
                {
                    b.HasOne("AutoTest.Domain.Entities.Tests.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.Question", b =>
                {
                    b.HasOne("AutoTest.Domain.Entities.Tests.Test", "Test")
                        .WithMany("Question")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.QuestionSolution", b =>
                {
                    b.HasOne("AutoTest.Domain.Entities.Tests.Question", "Question")
                        .WithMany("QuestionSolutions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoTest.Domain.Entities.Tests.TestSolution", "TestSolution")
                        .WithMany("QuestionSolutions")
                        .HasForeignKey("TestSolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("TestSolution");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.Test", b =>
                {
                    b.HasOne("AutoTest.Domain.Entities.Users.User", "User")
                        .WithMany("Tests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.TestSolution", b =>
                {
                    b.HasOne("AutoTest.Domain.Entities.Tests.Test", "Test")
                        .WithMany("TestSolutions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Users.SavedTest", b =>
                {
                    b.HasOne("AutoTest.Domain.Entities.Tests.Test", "Test")
                        .WithMany("SavedTests")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoTest.Domain.Entities.Users.User", "User")
                        .WithMany("SavedTests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Users.UserTestSolution", b =>
                {
                    b.HasOne("AutoTest.Domain.Entities.Tests.TestSolution", "TestSolution")
                        .WithMany()
                        .HasForeignKey("TestSolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoTest.Domain.Entities.Users.User", "User")
                        .WithMany("UserTestSolutions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestSolution");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestTopic", b =>
                {
                    b.HasOne("AutoTest.Domain.Entities.Tests.Test", null)
                        .WithMany()
                        .HasForeignKey("TestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoTest.Domain.Entities.Tests.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.Question", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("QuestionSolutions");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.Test", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Question");

                    b.Navigation("SavedTests");

                    b.Navigation("TestSolutions");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Tests.TestSolution", b =>
                {
                    b.Navigation("QuestionSolutions");
                });

            modelBuilder.Entity("AutoTest.Domain.Entities.Users.User", b =>
                {
                    b.Navigation("SavedTests");

                    b.Navigation("Tests");

                    b.Navigation("UserTestSolutions");
                });
#pragma warning restore 612, 618
        }
    }
}
