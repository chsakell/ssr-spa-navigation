using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ssr_spa_navigation.Models;

namespace ssr_spa_navigation.Infrastructure.Repositories
{
    public interface IContentRepository
    {
        string GetHeaderContent(string content);
        string GetFooterContent();
        List<Sport> GetSports();
        List<Offer> GetOffers();
        string GetLiveBet();
    }
}
