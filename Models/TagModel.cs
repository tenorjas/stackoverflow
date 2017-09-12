using System;
using System.ComponentModel.DataAnnotations;

namespace stackoverflow.Models
{
    public class TagModel
    {
        [Key]
        public int TagID {get; set;}
        public string TagName {get; set;}

        public TagModel()
        {
            
        }
    }
}