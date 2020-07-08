using Microsoft.AspNetCore.Components;
using BlazorWiki.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Pages
{
    public partial class Diff : ComponentBase
    {
        [Inject]
        IRegistry _registry { get; set; }


    }
}
