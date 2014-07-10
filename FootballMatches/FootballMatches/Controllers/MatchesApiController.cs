using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using FootballMatches.Models;
using FootballMatches.Models.Concrete;
using FootballMatches.Models.ViewModels;

namespace FootballMatches.Controllers
{
    public class MatchesApiController : ApiController
    {
        private readonly EFDbContext _context;

        public MatchesApiController()
        {
            _context = new EFDbContext();
        }

        public IEnumerable<MatchViewModel> Get()
        {
            var m = _context.Matches;

            return m.Select(item => new MatchViewModel()
            {
                City = item.City, Stadium = item.Stadium, MatchId = item.MatchId
            }).ToList();
        }

        // GET: api/Matches/5
        public MatchViewModel Get(int id)
        {
            var match = _context.Matches.FirstOrDefault(m => m.MatchId == id);
            if (match == null)
                return null;
            return new MatchViewModel()
            {
                MatchId = match.MatchId,
                City = match.City,
                Stadium = match.Stadium
            };
        }

        // POST: api/Matches
        public void Post(MatchViewModel matchViewModel)
        {
            if (matchViewModel.City == null || matchViewModel.Stadium == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                { Content = new StringContent("Unable to find any results") };
                throw new HttpResponseException(response);
            }
                

            Match match = new Match()
            {
                City = matchViewModel.City,
                Stadium = matchViewModel.Stadium
            };

            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        // PUT: api/Matches/5
        public void Put(MatchViewModel matchViewModel)
        {
            if (matchViewModel.City == null || matchViewModel.Stadium == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Unable to find any results") };
                throw new HttpResponseException(response);
            }

            var match = _context.Matches.FirstOrDefault(m => m.MatchId == matchViewModel.MatchId);
            match.City = matchViewModel.City;
            match.Stadium = matchViewModel.Stadium;
            _context.SaveChanges();
        }

        // DELETE: api/Matches/5
        public void Delete(int id)
        {
            var match = _context.Matches.FirstOrDefault(m => m.MatchId == id);
            _context.Matches.Remove(match);
            _context.SaveChanges();
        }
    }
}
