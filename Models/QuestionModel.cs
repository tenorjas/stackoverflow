using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace stackoverflow.Models
{
    public class QuestionModel
    {
        [Key]
        public int QuestionID {get; set;}
        public string Title {get; set;}
        public string Body {get; set;}
        public string UserId {get; set;}
        public DateTime PostDate {get; set;} = DateTime.Now;
        public int VoteCount {get; set;}
        public ICollection<AnswerModel> Answers {get; set;} = new HashSet<AnswerModel>();

        public ApplicationUser ApplicationUser {get; set;}

        public QuestionModel()
        {
            
        }
    }
}