﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using BlazorWiki.Data;

namespace BlazorWiki.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200521081010_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("BlazorWiki.Models.WikiContent", b =>
                {
                    b.Property<int>("WikiContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChangeDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("Markdown")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("WikiPageId")
                        .HasColumnType("INTEGER");

                    b.HasKey("WikiContentId");

                    b.HasIndex("WikiPageId");

                    b.ToTable("WikiContents");
                });

            modelBuilder.Entity("BlazorWiki.Models.WikiPage", b =>
                {
                    b.Property<int>("WikiPageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("WikiPageId");

                    b.ToTable("WikiPages");
                });

            modelBuilder.Entity("BlazorWiki.Models.WikiContent", b =>
                {
                    b.HasOne("BlazorWiki.Models.WikiPage", "WikiPage")
                        .WithMany("WikiContents")
                        .HasForeignKey("WikiPageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
