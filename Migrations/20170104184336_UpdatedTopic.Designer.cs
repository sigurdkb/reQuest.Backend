using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using reQuest.Backend.Entities;

namespace reQuest.Backend.Migrations
{
    [DbContext(typeof(reQuestDbContext))]
    [Migration("20170104184336_UpdatedTopic")]
    partial class UpdatedTopic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("reQuest.Backend.Entities.Competency", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("PlayerId");

                    b.Property<double>("Score");

                    b.Property<string>("TopicId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TopicId");

                    b.ToTable("Competencies");
                });

            modelBuilder.Entity("reQuest.Backend.Entities.Player", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("ExternalId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("QuestId");

                    b.Property<string>("QuestId1");

                    b.Property<string>("TeamId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("QuestId");

                    b.HasIndex("QuestId1");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("reQuest.Backend.Entities.Quest", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("OwnerId");

                    b.Property<TimeSpan>("Timeout");

                    b.Property<string>("Title");

                    b.Property<string>("TopicId");

                    b.Property<string>("WinnerId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TopicId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Quests");
                });

            modelBuilder.Entity("reQuest.Backend.Entities.Team", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("reQuest.Backend.Entities.Topic", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<string>("ExternalId");

                    b.Property<bool>("Locked");

                    b.Property<string>("ShortName");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("reQuest.Backend.Entities.Competency", b =>
                {
                    b.HasOne("reQuest.Backend.Entities.Player")
                        .WithMany("Competencies")
                        .HasForeignKey("PlayerId");

                    b.HasOne("reQuest.Backend.Entities.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("reQuest.Backend.Entities.Player", b =>
                {
                    b.HasOne("reQuest.Backend.Entities.Quest")
                        .WithMany("ActivePlayers")
                        .HasForeignKey("QuestId");

                    b.HasOne("reQuest.Backend.Entities.Quest")
                        .WithMany("PassivePlayers")
                        .HasForeignKey("QuestId1");

                    b.HasOne("reQuest.Backend.Entities.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("reQuest.Backend.Entities.Quest", b =>
                {
                    b.HasOne("reQuest.Backend.Entities.Player", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("reQuest.Backend.Entities.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.HasOne("reQuest.Backend.Entities.Player", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");
                });
        }
    }
}
