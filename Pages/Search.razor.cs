using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorWiki.Interfaces;
using BlazorWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Pages
{
    public partial class Search : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private IRepository _repository { get; set; }

        [Parameter]
        public string SearchString { get; set; }

        private List<WikiPage> _wikiPages = new List<WikiPage>();


        protected async override Task OnInitializedAsync()
        {
            var pages = await _repository.GetAllAsync();
            foreach(var page in pages)
            {
                var content = page.WikiContents
                    .OrderBy(_ => _.Timestamp)
                    .Last();
                var text = Markdown.ToPlainText(content.Markdown);
                if(text.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                {
                    _wikiPages.Add(page);
                }
            }

            await base.OnInitializedAsync();
        }

        private void PageSelected(MouseEventArgs e, WikiPage wikiPage)
        {
            _navigationManager.NavigateTo(wikiPage.Link, true);
        }

    }
}
