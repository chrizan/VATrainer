﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VATrainer.DataLayer;

namespace VATrainer.Migrations
{
    [DbContext(typeof(VATrainerContext))]
    partial class VATrainerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("VATrainer.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("VATrainer.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("VATrainer.Models.ArticleAnswer", b =>
                {
                    b.Property<int>("ArticleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnswerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArticleId", "AnswerId");

                    b.HasIndex("AnswerId");

                    b.ToTable("ArticleAnswer");
                });

            modelBuilder.Entity("VATrainer.Models.ArticleQuestion", b =>
                {
                    b.Property<int>("ArticleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArticleId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("ArticleQuestion");
                });

            modelBuilder.Entity("VATrainer.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ThemeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ThemeId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("VATrainer.Models.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Theme");
                });

            modelBuilder.Entity("VATrainer.Models.Answer", b =>
                {
                    b.HasOne("VATrainer.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("VATrainer.Models.ArticleAnswer", b =>
                {
                    b.HasOne("VATrainer.Models.Answer", "Answer")
                        .WithMany("ArticleAnswers")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VATrainer.Models.Article", "Article")
                        .WithMany("ArticleAnswers")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("Article");
                });

            modelBuilder.Entity("VATrainer.Models.ArticleQuestion", b =>
                {
                    b.HasOne("VATrainer.Models.Article", "Article")
                        .WithMany("ArticleQuestions")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VATrainer.Models.Question", "Question")
                        .WithMany("ArticleQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("VATrainer.Models.Question", b =>
                {
                    b.HasOne("VATrainer.Models.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("VATrainer.Models.Answer", b =>
                {
                    b.Navigation("ArticleAnswers");
                });

            modelBuilder.Entity("VATrainer.Models.Article", b =>
                {
                    b.Navigation("ArticleAnswers");

                    b.Navigation("ArticleQuestions");
                });

            modelBuilder.Entity("VATrainer.Models.Question", b =>
                {
                    b.Navigation("ArticleQuestions");
                });
#pragma warning restore 612, 618
        }
    }
}
