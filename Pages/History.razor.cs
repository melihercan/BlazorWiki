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
    public partial class History : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private IRegistry _registry { get; set; }

        [Inject]
        private IRepository _repository { get; set; }

        [Parameter]
        public int? Id { get; set; }

        private WikiPage _wikiPage;
        private List<WikiContent> _wikiContents;


        protected async override Task OnInitializedAsync()
        {
            _wikiPage = await _repository.GetAsync((int)Id);
            if (_wikiPage != null)
            {
                _wikiContents = new List<WikiContent>(_wikiPage.WikiContents);
                _wikiContents.Reverse();
            }
            await base.OnInitializedAsync();
        }

        private void DiffSelected(MouseEventArgs e, WikiContent instance)
        {
            int index = _wikiContents.FindIndex(_ => _.Timestamp == instance.Timestamp);
            if(index == -1)
            {
                //// TODO: EROOR POPUP
            }

            _registry.NewHtml = Markdown.ToHtml(instance.Markdown);
            _registry.OldHtml = Markdown.ToHtml(index == _wikiContents.Count-1 ? string.Empty : _wikiContents.ElementAt(index + 1).Markdown);

            _navigationManager.NavigateTo("/diff");
        }
    }
}
