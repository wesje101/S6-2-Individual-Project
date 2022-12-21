﻿// <auto-generated />
using System;
using ChatService.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatService.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20221219121824_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatService.Models.ChatMessage", b =>
                {
                    b.Property<int>("_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("_id"));

                    b.Property<int>("_ChatRoom_id")
                        .HasColumnType("integer");

                    b.Property<string>("_message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("_sender_id")
                        .HasColumnType("integer");

                    b.HasKey("_id");

                    b.HasIndex("_ChatRoom_id");

                    b.HasIndex("_sender_id");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("ChatService.Models.ChatRoom", b =>
                {
                    b.Property<int>("_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("_id"));

                    b.Property<string>("_roomName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.HasKey("_id");

                    b.ToTable("ChatRooms");
                });

            modelBuilder.Entity("ChatService.Models.User", b =>
                {
                    b.Property<int>("_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("_id"));

                    b.Property<int?>("ChatRoom_id")
                        .HasColumnType("integer");

                    b.Property<string>("_name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.HasKey("_id");

                    b.HasIndex("ChatRoom_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatService.Models.ChatMessage", b =>
                {
                    b.HasOne("ChatService.Models.ChatRoom", "_ChatRoom")
                        .WithMany("_messages")
                        .HasForeignKey("_ChatRoom_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatService.Models.User", "_sender")
                        .WithMany()
                        .HasForeignKey("_sender_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_ChatRoom");

                    b.Navigation("_sender");
                });

            modelBuilder.Entity("ChatService.Models.User", b =>
                {
                    b.HasOne("ChatService.Models.ChatRoom", null)
                        .WithMany("_participants")
                        .HasForeignKey("ChatRoom_id");
                });

            modelBuilder.Entity("ChatService.Models.ChatRoom", b =>
                {
                    b.Navigation("_messages");

                    b.Navigation("_participants");
                });
#pragma warning restore 612, 618
        }
    }
}
