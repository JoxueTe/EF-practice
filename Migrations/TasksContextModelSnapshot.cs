﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using projectef;

#nullable disable

namespace projectef.Migrations
{
    [DbContext(typeof(TasksContext))]
    partial class TasksContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("projectef.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("CategoryId");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d5d"),
                            Name = "Physical Activity",
                            Weight = 7
                        },
                        new
                        {
                            CategoryId = new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d56"),
                            Name = "Study",
                            Weight = 10
                        });
                });

            modelBuilder.Entity("projectef.Models.Task", b =>
                {
                    b.Property<Guid>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("TaskPriority")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("TaskId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Task", (string)null);

                    b.HasData(
                        new
                        {
                            TaskId = new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d10"),
                            CategoryId = new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d5d"),
                            CreationDate = new DateTime(2023, 10, 24, 19, 28, 13, 717, DateTimeKind.Utc).AddTicks(3885),
                            TaskPriority = 1,
                            Title = "Excercise"
                        },
                        new
                        {
                            TaskId = new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d11"),
                            CategoryId = new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d56"),
                            CreationDate = new DateTime(2023, 10, 24, 19, 28, 13, 717, DateTimeKind.Utc).AddTicks(3894),
                            TaskPriority = 2,
                            Title = "Blazor Course"
                        });
                });

            modelBuilder.Entity("projectef.Models.Task", b =>
                {
                    b.HasOne("projectef.Models.Category", "Category")
                        .WithMany("Tasks")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("projectef.Models.Category", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
