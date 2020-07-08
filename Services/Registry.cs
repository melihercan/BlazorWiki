using BlazorWiki.Interfaces;
using BlazorWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Services
{
    public class Registry : IRegistry
    {
        public string OldHtml { get; set; }
        public string NewHtml { get; set; }
    }
}
