using BlazorWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Interfaces
{
    public interface IRegistry
    {
        string OldHtml { get; set; }
        string NewHtml { get; set; }

    }
}
