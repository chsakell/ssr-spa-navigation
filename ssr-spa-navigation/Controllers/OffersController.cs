using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ssr_spa_navigation.Infrastructure;
using ssr_spa_navigation.Infrastructure.Repositories;
using ssr_spa_navigation.Models;
using ssr_spa_navigation.ViewModels;

namespace ssr_spa_navigation.Controllers
{
    public class OffersController : BaseController
    {
        readonly List<Offer> _offers;

        public OffersController(IContentRepository contentRepository)
        {
            _offers = contentRepository.GetOffers();
        }

        [ServiceFilter(typeof(DefaultStructureResult))]
        public IActionResult Index()
        {
            var result = new OffersVM {Offers = _offers};
            return ResolveResult(result);
        }

        /// <summary>
        /// Example of how to set two different root properties on the store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(NoSidebarStructureResult))]
        public IActionResult Details(int id)
        {
            var offer = _offers.First(o => Int32.Parse(o.Name.Split("-")[o.Name.Split("-").Length - 1]) == id);

            var article = new Article()
            {
                Id = id,
                Title = "Article for " + offer.Title + " offer",
            };

            var result = new ArticleDetailsVM
            {
                Article = article,
                Betofday = "betofday.png"
            };

            return ResolveResult(result);
        }
    }
}