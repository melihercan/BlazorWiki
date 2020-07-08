using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Models
{
    public class WikiPage
    {
        public int WikiPageId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Link { get; set; }

        public List<WikiContent> WikiContents { get; set; } = new List<WikiContent>(); 
    }
}
