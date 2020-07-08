using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Models
{
    public class WikiContent
    {
        public int WikiContentId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChangeDescription { get; set; }
        public string Markdown { get; set; }

        public int WikiPageId { get; set; }
        public WikiPage WikiPage { get; set; }
    }
}
