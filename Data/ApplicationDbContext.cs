﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using stackoverflow.Models;

namespace stackoverflow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AnswerModel> AnswerModel {get; set;}
        public DbSet<ApplicationUser> ApplicationUser {get; set;}
        public DbSet<CommentModel> CommentModel {get; set;}
        public DbSet<QtieModel> QtieModel {get; set;}
        public DbSet<QuestionModel> QuestionModel {get; set;}
        public DbSet<TagModel> TagModel {get; set;}
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
