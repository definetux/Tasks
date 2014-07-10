using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FootballMatches.Models;
using FootballMatches.Models.ViewModels;

namespace FootballMatches.Controllers
{
    public class TeamsApiController : ApiController
    {
        private readonly EFDbContext _context;

        public TeamsApiController()
        {
            _context = new EFDbContext();
        }

        // GET: api/Teams/5
        public List<TeamViewModel> Get(int id)
        {
            var match = _context.Matches.FirstOrDefault(m => m.MatchId == id);
            if (match == null)
                return null;

            var teams = match.Teams;

            return teams.Select(team => new TeamViewModel()
            {
                City = team.City,
                Name = team.Name,
                Place = team.Place,
                TeamId = team.TeamId,
                MatchId = team.MatchId
            }).ToList();
        }

        public TeamViewModel Get(int id, int id1)
        {
            var match = _context.Matches.FirstOrDefault(m => m.MatchId == id);
            if (match == null)
                return null;

            var teams = match.Teams;

            var team = teams.FirstOrDefault(t => t.TeamId == id1);

            return new TeamViewModel()
            {
                TeamId = team.TeamId,
                Name = team.Name,
                City = team.City,
                Place = team.Place,
                MatchId = team.MatchId
            };
        }

        // POST: api/Teams
        public void Post(int id, TeamViewModel teamViewModel)
        {
            if (teamViewModel.City == null ||
                teamViewModel.Name == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Unable to find any results") };
                throw new HttpResponseException(response);
            }

            var matches = _context.Matches.FirstOrDefault(m => m.MatchId == id);

            if (matches.Teams.Count == 2)
                return;

            var team = new Team()
            {
                City = teamViewModel.City,
                Name = teamViewModel.Name,
                Place = teamViewModel.Place,
                MatchId = teamViewModel.MatchId
            };

            matches.Teams.Add(team);

            _context.SaveChanges();
        }

        // PUT: api/Teams/5
        public void Put(int id1, TeamViewModel teamsViewModel)
        {
            var match = _context.Matches.FirstOrDefault(m => m.MatchId == teamsViewModel.MatchId);

            var teams = match.Teams;

            var team = teams.FirstOrDefault(t => t.TeamId == teamsViewModel.TeamId);

            team.Name = teamsViewModel.Name;
            team.City = teamsViewModel.City;
            team.Place = teamsViewModel.Place;
            _context.SaveChanges();
        }

        // DELETE: api/Teams/5
        public void Delete(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.TeamId == id);

            _context.Teams.Remove(team);

            _context.SaveChanges();
        }
    }
}
