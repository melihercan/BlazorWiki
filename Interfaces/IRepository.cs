using BlazorWiki.Data;
using BlazorWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWiki.Interfaces
{
    public interface IRepository
    {

        Task<IEnumerable<WikiPage>> GetAllAsync();

        Task<WikiPage> GetAsync(int wikiPageId);

        Task AddAsync(WikiPage wikiPage);

        Task UpdateAsync(int wikiPageId, WikiContent wikiContent);

        Task CleanupContentsAsync(int wikiPageId);

        Task DeleteAsync(int wikiPageId);
    }
}
