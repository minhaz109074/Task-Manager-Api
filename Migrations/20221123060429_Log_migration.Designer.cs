﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Task_Manager_Api.Repositories;

#nullable disable

namespace TaskManagerApi.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20221123060429_Log_migration")]
    partial class Logmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Task_Manager_Api.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("RequestArriveTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("RequestLeaveTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RequestMethod")
                        .HasColumnType("text");

                    b.Property<string>("RequestPath")
                        .HasColumnType("text");

                    b.Property<int?>("StatusCode")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Task_Manager_Api.Models.Person", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Designation")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Name");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Task_Manager_Api.Models.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<string>("TaskAssignedTo")
                        .HasColumnType("text");

                    b.Property<string>("TaskRequestedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("TaskAssignedTo");

                    b.HasIndex("TaskRequestedBy");

                    b.ToTable("TaskList");
                });

            modelBuilder.Entity("Task_Manager_Api.Models.TaskItem", b =>
                {
                    b.HasOne("Task_Manager_Api.Models.Person", null)
                        .WithMany("TaskAssign")
                        .HasForeignKey("TaskAssignedTo")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Task_Manager_Api.Models.Person", null)
                        .WithMany("TaskRuqest")
                        .HasForeignKey("TaskRequestedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Task_Manager_Api.Models.Person", b =>
                {
                    b.Navigation("TaskAssign");

                    b.Navigation("TaskRuqest");
                });
#pragma warning restore 612, 618
        }
    }
}
