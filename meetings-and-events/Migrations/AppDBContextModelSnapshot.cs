﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using meetings_and_events.Data;

namespace meetings_and_events.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("meetings_and_events.Models.Place", b =>
                {
                    b.Property<int>("id_place")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("create_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<int>("id_user")
                        .HasColumnType("integer");

                    b.Property<string>("image")
                        .HasColumnType("text");

                    b.Property<bool>("multi_time")
                        .HasColumnType("boolean");

                    b.Property<string>("title")
                        .HasColumnType("text");

                    b.HasKey("id_place");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_address", b =>
                {
                    b.Property<int>("id_address")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("city")
                        .HasColumnType("text");

                    b.Property<string>("country")
                        .HasColumnType("text");

                    b.Property<int>("id_place")
                        .HasColumnType("integer");

                    b.Property<string>("number")
                        .HasColumnType("text");

                    b.Property<string>("street")
                        .HasColumnType("text");

                    b.HasKey("id_address");

                    b.HasIndex("id_place")
                        .IsUnique();

                    b.ToTable("Place_address");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_comments", b =>
                {
                    b.Property<int>("id_comment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Placeid_place")
                        .HasColumnType("integer");

                    b.Property<int?>("Usersid_user")
                        .HasColumnType("integer");

                    b.Property<string>("comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("comment_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("id_place")
                        .HasColumnType("integer");

                    b.Property<int>("id_user")
                        .HasColumnType("integer");

                    b.HasKey("id_comment");

                    b.HasIndex("Placeid_place");

                    b.HasIndex("Usersid_user");

                    b.ToTable("Place_comments");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_data_multitime", b =>
                {
                    b.Property<int>("id_data")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("day_week")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("end_date")
                        .HasColumnType("interval");

                    b.Property<int>("id_place")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("start_date")
                        .HasColumnType("interval");

                    b.HasKey("id_data");

                    b.HasIndex("id_place")
                        .IsUnique();

                    b.ToTable("Place_data_multitime");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_data_onetime", b =>
                {
                    b.Property<int>("id_data")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("end_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("id_place")
                        .HasColumnType("integer");

                    b.Property<DateTime>("start_date")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id_data");

                    b.HasIndex("id_place")
                        .IsUnique();

                    b.ToTable("Place_data_onetime");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_rate", b =>
                {
                    b.Property<int>("id_rate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Placeid_place")
                        .HasColumnType("integer");

                    b.Property<int?>("Usersid_user")
                        .HasColumnType("integer");

                    b.Property<int>("id_place")
                        .HasColumnType("integer");

                    b.Property<int>("id_user")
                        .HasColumnType("integer");

                    b.Property<bool>("like")
                        .HasColumnType("boolean");

                    b.HasKey("id_rate");

                    b.HasIndex("Placeid_place");

                    b.HasIndex("Usersid_user");

                    b.ToTable("Place_rate");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_special_close", b =>
                {
                    b.Property<int>("id_data_closetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("id_place")
                        .HasColumnType("integer");

                    b.HasKey("id_data_closetime");

                    b.HasIndex("id_place");

                    b.ToTable("Place_special_close");
                });

            modelBuilder.Entity("meetings_and_events.Models.User_follow", b =>
                {
                    b.Property<int>("id_follow")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Placeid_place")
                        .HasColumnType("integer");

                    b.Property<int?>("Usersid_user")
                        .HasColumnType("integer");

                    b.Property<int>("id_place")
                        .HasColumnType("integer");

                    b.Property<int>("id_user")
                        .HasColumnType("integer");

                    b.HasKey("id_follow");

                    b.HasIndex("Placeid_place");

                    b.HasIndex("Usersid_user");

                    b.ToTable("Users_follow");
                });

            modelBuilder.Entity("meetings_and_events.Models.User_join", b =>
                {
                    b.Property<int>("id_join")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Placeid_place")
                        .HasColumnType("integer");

                    b.Property<int?>("Usersid_user")
                        .HasColumnType("integer");

                    b.Property<int>("id_place")
                        .HasColumnType("integer");

                    b.Property<int>("id_user")
                        .HasColumnType("integer");

                    b.HasKey("id_join");

                    b.HasIndex("Placeid_place");

                    b.HasIndex("Usersid_user");

                    b.ToTable("User_join");
                });

            modelBuilder.Entity("meetings_and_events.Models.Users", b =>
                {
                    b.Property<int>("id_user")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("create_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .HasColumnType("text");

                    b.HasKey("id_user");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_address", b =>
                {
                    b.HasOne("meetings_and_events.Models.Place", "Place")
                        .WithOne("Place_address")
                        .HasForeignKey("meetings_and_events.Models.Place_address", "id_place")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_comments", b =>
                {
                    b.HasOne("meetings_and_events.Models.Place", null)
                        .WithMany("Place_comments")
                        .HasForeignKey("Placeid_place");

                    b.HasOne("meetings_and_events.Models.Users", null)
                        .WithMany("Place_comments")
                        .HasForeignKey("Usersid_user");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_data_multitime", b =>
                {
                    b.HasOne("meetings_and_events.Models.Place", "Place")
                        .WithOne("Place_data_multitime")
                        .HasForeignKey("meetings_and_events.Models.Place_data_multitime", "id_place")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_data_onetime", b =>
                {
                    b.HasOne("meetings_and_events.Models.Place", "Place")
                        .WithOne("Place_data_onetime")
                        .HasForeignKey("meetings_and_events.Models.Place_data_onetime", "id_place")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_rate", b =>
                {
                    b.HasOne("meetings_and_events.Models.Place", null)
                        .WithMany("Place_rate")
                        .HasForeignKey("Placeid_place");

                    b.HasOne("meetings_and_events.Models.Users", null)
                        .WithMany("Place_rate")
                        .HasForeignKey("Usersid_user");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place_special_close", b =>
                {
                    b.HasOne("meetings_and_events.Models.Place", "Place")
                        .WithMany("Place_special_close")
                        .HasForeignKey("id_place")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("meetings_and_events.Models.User_follow", b =>
                {
                    b.HasOne("meetings_and_events.Models.Place", null)
                        .WithMany("User_follow")
                        .HasForeignKey("Placeid_place");

                    b.HasOne("meetings_and_events.Models.Users", null)
                        .WithMany("User_follow")
                        .HasForeignKey("Usersid_user");
                });

            modelBuilder.Entity("meetings_and_events.Models.User_join", b =>
                {
                    b.HasOne("meetings_and_events.Models.Place", null)
                        .WithMany("User_join")
                        .HasForeignKey("Placeid_place");

                    b.HasOne("meetings_and_events.Models.Users", null)
                        .WithMany("User_join")
                        .HasForeignKey("Usersid_user");
                });

            modelBuilder.Entity("meetings_and_events.Models.Place", b =>
                {
                    b.Navigation("Place_address");

                    b.Navigation("Place_comments");

                    b.Navigation("Place_data_multitime");

                    b.Navigation("Place_data_onetime");

                    b.Navigation("Place_rate");

                    b.Navigation("Place_special_close");

                    b.Navigation("User_follow");

                    b.Navigation("User_join");
                });

            modelBuilder.Entity("meetings_and_events.Models.Users", b =>
                {
                    b.Navigation("Place_comments");

                    b.Navigation("Place_rate");

                    b.Navigation("User_follow");

                    b.Navigation("User_join");
                });
#pragma warning restore 612, 618
        }
    }
}
