﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Site
    {
        public int SiteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TemplateId { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        // public bool Iscomplited { get; set; }
        //  public string MenuType { get; set; }
        // public string Url { get; set; }
        //  public string Tags { get; set; }
        //   public double Rate { get; set; }
        //   public bool HasComments { get; set; }
        public DateTime CreationTime { get; set; }
        public string UserId { get; set; }
       public virtual ICollection<Page> Pages { get; set; }
       // public virtual ICollection<Comment> Comments { get; set; }

        public Site()
        {
            Pages = new List<Page>();
           // Comments = new List<Comment>();
        }
    }
}