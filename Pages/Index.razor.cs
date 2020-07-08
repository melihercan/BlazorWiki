using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BlazorWiki.Interfaces;
using BlazorWiki.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BlazorWiki.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private IConfiguration _configuration { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private IRepository _repository { get; set; }

        private IEnumerable<WikiPage> _wikiPages;

        private string _searchString { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _wikiPages = await _repository.GetAllAsync();
            await base.OnInitializedAsync();
        }

        private void PageSelected(MouseEventArgs e, WikiPage wikiPage)
        {
            _navigationManager.NavigateTo(wikiPage.Link, true);
        }

        private void EditSelected(MouseEventArgs e, WikiPage wikiPage)
        {
            _navigationManager.NavigateTo($"edit/{wikiPage.WikiPageId}");
        }

        private async void EditAbsentSelected(MouseEventArgs e, WikiPage wikiPage)
        {
            // Cleanup database content entries.
            await _repository.CleanupContentsAsync(wikiPage.WikiPageId);

            _navigationManager.NavigateTo($"edit/{wikiPage.WikiPageId}");
        }


        private void HistorySelected(MouseEventArgs e, WikiPage wikiPage)
        {
            _navigationManager.NavigateTo($"history/{wikiPage.WikiPageId}");
        }

        private void SearchSelected(MouseEventArgs e)
        {
            if (_searchString != null)
            {
                _navigationManager.NavigateTo($"search/{_searchString}");
            }
        }

        private bool FileExists(string _wikiPageTitle)
        {
            var file = _wikiPageTitle + ".html";
            var pagesPath = _configuration.GetValue<string>("WikiPaths:Pages");
            var path = pagesPath + file;
            return File.Exists(path);


        }

    }
}
