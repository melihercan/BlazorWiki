using Microsoft.AspNetCore.Components;
using BlazorWiki.Data;
using BlazorWiki.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BlazorWiki.Models;

namespace BlazorWiki.Services
{
    public class Repository : IRepository
    {
        ApplicationDbContext _db;

        public Repository()
        {
            _db = new ApplicationDbContext();
            _db.WikiPages.Include(_ => _.WikiContents).ToList();
        }

        public async Task<IEnumerable<WikiPage>> GetAllAsync()
        {
            return await _db.WikiPages.ToListAsync();
        }

        public async Task<WikiPage> GetAsync(int wikiPageId)
        {
            return await _db.WikiPages.FindAsync(wikiPageId);
        }

        public async Task AddAsync(WikiPage wikiPage)
        {
            _db.Add(wikiPage);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int wikiPageId, WikiContent wikiContent)
        {
            var page = await _db.WikiPages.FindAsync(wikiPageId);
            if (page != null)
            {
                page.WikiContents.Add(wikiContent);
                await _db.SaveChangesAsync();
            }
        }

        public async Task CleanupContentsAsync(int wikiPageId)
        {
            var page = await _db.WikiPages.FindAsync(wikiPageId);
            if (page != null)
            {
                foreach(var content in page.WikiContents)
                {
                    _db.WikiContents.Remove(content);
                }
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int wikiPageId)
        {
            var page = await _db.WikiPages.FindAsync(wikiPageId);
            if (page != null)
            {
                foreach (var content in page.WikiContents)
                {
                    _db.WikiContents.Remove(content);
                }
                _db.WikiPages.Remove(page);
                await _db.SaveChangesAsync();
            }
        }
    }
}
