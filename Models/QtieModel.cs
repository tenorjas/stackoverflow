using System;
using System.ComponentModel.DataAnnotations;

namespace stackoverflow.Models
{
    public class QtieModel
    {
        [Key]
        public int QtieID {get; set;}
        public int QuestionID {get; set;}
        public int TagID {get; set;}

        public QuestionModel QuestionModel {get; set;}
        public TagModel TagModel {get; set;}

        public QtieModel()
        {
            
        }
    }
}