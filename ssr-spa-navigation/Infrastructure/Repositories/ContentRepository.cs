using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ssr_spa_navigation.Models;

namespace ssr_spa_navigation.Infrastructure.Repositories
{
    public class ContentRepository : IContentRepository
    {
        public string GetFooterContent()
        {
            return "Our betting company proudly supports Sports and Fair Play";
        }

        public string GetHeaderContent(string content)
        {
            return content;
        }

        public List<Sport> GetSports()
        {
            var sports = new List<Sport>();

            var soccer = new Sport() { Id = "SOCCER-FOOT", DisplayName = "Football" };
            var superLeague = new League() { Title = "Superleague", Type = "Greece", Id = "Super-League-Greece-1636", Image = "superleague.png"};
            var championsLeague = new League() { Title = "Champions League - Matches", Type = "Champions League", Id = "Champions-League-Matches-Champions-League-182748", Image = "champions-league.png" };
            var europaLeague = new League() { Title = "Europa League - Matches", Type = "Europa League", Id = "Europa-League-Matches-Europa-League-182761", Image = "europa-league.png" };
            soccer.Leagues.Add(superLeague);
            soccer.Leagues.Add(championsLeague);
            soccer.Leagues.Add(europaLeague);

            var basket = new Sport() { Id = "Basketball-Bask", DisplayName = "Basket" };
            var odds = new League() { Title = "Enhanced odds", Type = "Enhanced odds", Id = "Enhanced-Odds-Enhanced-Odds-189061", Image = "enhanced-odds.png" };
            var basketLeague = new League() { Title = "Basket League", Type = "Greece", Id = "Basket-League-Greece-11472", Image = "basket-league.png"};
            var euroLeague = new League() { Title = "Euroleague", Type = "Euroleague", Id = "Euroleague-Euroleague-16717", Image = "euroleague.png"};
            basket.Leagues.Add(odds);
            basket.Leagues.Add(basketLeague);
            basket.Leagues.Add(euroLeague);

            sports.Add(soccer);
            sports.Add(basket);

            return sports;
        }

        public List<Offer> GetOffers()
        {
            return new List<Offer>()
            {
                new Offer() { Title = "0% Margin*", Image = "zero-margin.png", Name = "0-ganiota-1234"},
                new Offer() { Title = "Acca Bonus*", Image = "acca-bonus.png", Name = "prim-sto-paroli-3235"},
                new Offer() { Title = "Acca Insurance*", Image = "acca-insurance.png", Name = "paroligo-322"},
                new Offer() { Title = "Request a Bet*",  Image = "request-a-bet.png", Name = "pesto-ki-egine-1134" },
                new Offer() { Title = "2 Goals Up*", Image = "2-goals-up.png", Name = "duo-kai-tameio-3884"},
                new Offer() { Title = "Bore Draw Refund*", Image = "bore-draw-refund.png", Name = "leukh-epistrofi-9382" }
            };
        }

        public string GetLiveBet()
        {
            return "live-bet.png";
        }
    }
}
