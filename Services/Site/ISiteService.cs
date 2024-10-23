using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnnuaireEntreprise.Models;

namespace AnnuaireEntreprise.Services.Site
{
    public interface ISiteService
    {
        List<Models.Site> GetAllSites();
        void AddSite(Models.Site site);
        void UpdateSite(Models.Site site);
        void DeleteSite(Models.Site site);
    }
}
