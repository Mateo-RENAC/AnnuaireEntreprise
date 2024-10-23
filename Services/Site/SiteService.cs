using AnnuaireEntreprise.Data;
using AnnuaireEntreprise.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnnuaireEntreprise.Services.Site
{
    public class SitesService : ISiteService
    {
        private readonly AnnuaireContext _context;

        public SitesService(AnnuaireContext context)
        {
            _context = context;
        }

        public List<Models.Site> GetAllSites()
        {
            return _context.Sites.ToList();
        }

        public void AddSite(Models.Site site)
        {
            _context.Sites.Add(site);
            _context.SaveChanges();
        }

        public void UpdateSite(Models.Site site)
        {
            _context.Sites.Update(site);
            _context.SaveChanges();
        }

        public void DeleteSite(Models.Site site)
        {
            _context.Sites.Remove(site);
            _context.SaveChanges();
        }
    }
}
