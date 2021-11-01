using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Project_Staff
{
    [XmlType(TypeName = "Post")]
    public class PostFileDto
    { 
        public string Title { get; set; }
        public string Devision { get; set; }
        public decimal Salary { get; set; }
        public double Bet { get; set; }


        public static PostFileDto Map(Post post)
        {
            return new PostFileDto()
            {
                Title = post.Position.Title,
                Devision = post.Position.Devision,
                Salary = post.Position.Salary,
                Bet = post.Bet
            };
        }
        public static Post Map(PostFileDto post)
        {
            return new Post(
                post.Title,
                post.Devision,
                post.Salary,
                post.Bet
            );
        }
    }
}
