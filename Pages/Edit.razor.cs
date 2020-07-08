using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using BlazorWiki.Interfaces;
using BlazorWiki.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Pages
{
    public partial class Edit : ComponentBase
    {
        [Inject] 
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private IConfiguration _configuration { get; set; }

        [Inject]
        private IRegistry _registry { get; set; }

        [Inject]
        private IRepository _repository { get; set; }


        [Parameter]
        public int? Id { get; set; }

        public string Body { get; set; } = string.Empty;
        public string Preview => Markdown.ToHtml(Body);

        private WikiPage _wikiPage = new WikiPage { /*WikiContents = new List<WikiContent> { }*/ };
        private WikiContent _wikiContent = new WikiContent{};

        private const string HtmlTop =
        @"
            <!doctype html>
            <html lang=""en"">
                <head>
                    <meta charset = ""utf-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
                    <title>Wiki page</title>
                    <link href=""css/site.css"" rel=""stylesheet"">
                </head>
                <body>
        ";
        private const string HtmlBottom =
        @"
               </body>
            </html>
        ";


        protected async override Task OnInitializedAsync()
        {
            if( Id != null)
            {
                //_wikiPage = _registry.WikiPages.FirstOrDefault(_ => _.WikiPageId == Id);
                _wikiPage = await _repository.GetAsync((int)Id);
                if(_wikiPage != null)
                {
                    if (_wikiPage.WikiContents.Count != 0)
                    {
                        var currentWikiContent = _wikiPage.WikiContents
                            .OrderBy(_ => _.Timestamp)
                            .Last();
                        Body = currentWikiContent.Markdown;
                    }
                }
                else
                {
                    Id = null;
                }
            }
            await base.OnInitializedAsync();
        }

        private async void OnValidSubmit()
        {
            // Check if already exists.
            if (_repository.GetAsync(_wikiPage.WikiPageId) != null)
            {

                var file = _wikiPage.Title + ".html";
                var pagesPath = _configuration.GetValue<string>("WikiPaths:Pages");
                var pagesLink = _configuration.GetValue<string>("WikiLinks:Pages");

                var path = pagesPath + file;
                var content = HtmlTop + Preview + HtmlBottom;
                File.WriteAllText(path, content);

                _wikiContent.Timestamp = DateTime.Now;
                _wikiContent.Markdown = Body;

                if (Id == null)
                {
                    // New page.
                    _wikiPage.Link = pagesLink + file;
                    await _repository.AddAsync(_wikiPage);
                }
                await _repository.UpdateAsync(_wikiPage.WikiPageId, _wikiContent);
            }
            else
            {
                //// TODO: SHOW ERROR POPUP
            }

            _navigationManager.NavigateTo("/");
        }
    }
}
