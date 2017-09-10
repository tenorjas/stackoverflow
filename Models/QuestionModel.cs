using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace stackoverflow.Models
{
    public class QuestionModel
    {
        public int QuestionID {get; set;}
        public string Title {get; set;}
        public string Body {get; set;}
        public int UserId {get; set;}
        public DateTime PostDate {get; set;}
        public int VoteCount {get; set;}

        public ApplicationUser ApplicationUser {get; set;}

        public QuestionModel()
        {
            
        }
    }
}