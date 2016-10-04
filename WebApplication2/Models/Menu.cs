using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2;

namespace WebApplication2.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public bool IsTopBar { get; set; }
        public ICollection<MenuItem> Items { get; set; }
        
        public Menu()
        {
            Items = new List<MenuItem>();
        }
    }
}