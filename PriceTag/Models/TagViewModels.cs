using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriceTag.Models
{
    public class CreateTagVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}