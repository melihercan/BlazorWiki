using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Pages
{
    public partial class UploadFiles : ComponentBase
    {
        [Inject]
        private IConfiguration _configuration { get; set; }

        private List<string> _logs = new List<string>();

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
        private async Task FilesSelected(IFileListEntry[] files)
        {
            foreach(var file in files)
            {
                var filesPath = _configuration.GetValue<string>("WikiPaths:Files");
                var path = filesPath + file.Name;

                using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    await file.Data.CopyToAsync(fileStream);
                }

                _logs.Add($"- {file.Name} has been uploaded @ {DateTime.Now}");
            }
        }
    }
}
