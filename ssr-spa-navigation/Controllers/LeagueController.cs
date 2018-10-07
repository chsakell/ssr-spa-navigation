using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ssr_spa_navigation.Infrastructure;
using ssr_spa_navigation.Infrastructure.Repositories;

namespace ssr_spa_navigation.Controllers
{
    public class LeagueController : BaseController
    {
        private readonly IContentRepository _contentRepository;


        public LeagueController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        [ServiceFilter(typeof(LiveStoiximaStructureResult))]
        [ServiceFilter(typeof(DefaultStructureResult))]
        public IActionResult Details(string sport, int id)
        {
            var sportLeague =  _contentRepository.GetSports().SelectMany(s => s.Leagues).First(l => Int32.Parse(l.Id.Split("-")[l.Id.Split("-").Length - 1]) == id);

            return ResolveResult(new { league = sportLeague });
        }
    }
}
