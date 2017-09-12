using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace stackoverflow.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentID {get; set;}
        public string Body {get; set;}
        public string UserId {get; set;}
        public DateTime PostDate {get; set;}
        public int QuestionID {get; set;}
        public int AnswerID {get; set;}

        public ApplicationUser ApplicationUser {get; set;}
        public QuestionModel QuestionModel {get; set;}
        public AnswerModel AnswerModel {get; set;}

        public CommentModel()
        {
            
        }
    }
}