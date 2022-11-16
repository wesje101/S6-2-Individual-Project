﻿// <auto-generated />
using System;
using AccountService.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AccountService.Migrations
{
    [DbContext(typeof(AccountContext))]
    [Migration("20221111093432_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccountService.Models.Account", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int?>("Accountid")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.HasKey("id");

                    b.HasIndex("Accountid");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("AccountService.Models.Account", b =>
                {
                    b.HasOne("AccountService.Models.Account", null)
                        .WithMany("FriendList")
                        .HasForeignKey("Accountid");
                });

            modelBuilder.Entity("AccountService.Models.Account", b =>
                {
                    b.Navigation("FriendList");
                });
#pragma warning restore 612, 618
        }
    }
}