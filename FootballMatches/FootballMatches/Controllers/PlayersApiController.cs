using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FootballMatches.Models;
using FootballMatches.Models.ViewModels;

namespace FootballMatches.Controllers
{
    public class PlayersApiController : ApiController
    {
        private readonly EFDbContext _context;

        public PlayersApiController()
        {
            _context = new EFDbContext();
        }

        // GET: api/PlayersApi/5
        public List<PlayerViewModel> Get(int id)
        {
            var team = _context.Teams.FirstOrDefault(m => m.TeamId == id);
            if (team == null)
                return null;

            var players = team.Players;

            return players.Select(p => new PlayerViewModel()
            {
                LastName = p.LastName,
                FirstName = p.FirstName,
                Number = p.Number,
                Position = p.Position,
                PlayerId = p.PlayerId,
                TeamId = p.TeamId
            }).ToList();
        }

        public PlayerViewModel Get(int id, int id1)
        {
            var team = _context.Teams.FirstOrDefault(m => m.TeamId == id);
            if (team == null)
                return null;

            var players = team.Players;

            var player = players.FirstOrDefault(t => t.PlayerId == id1);

            return new PlayerViewModel()
            {
                PlayerId = player.PlayerId,
                LastName = player.LastName,
                FirstName = player.FirstName,
                Number = player.Number,
                Position = player.Position,
                TeamId = player.TeamId
            };
        }

        // POST: api/PlayersApi
        public void Post(int id, PlayerViewModel playerViewModel)
        {
            if (playerViewModel.LastName == null ||
                playerViewModel.FirstName == null ||
                playerViewModel.Position == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Unable to find any results") };
                throw new HttpResponseException(response);
            }

            var teams = _context.Teams.FirstOrDefault(m => m.TeamId == id);

            var player = new Player()
            {
                LastName = playerViewModel.LastName,
                FirstName = playerViewModel.FirstName,
                Number = playerViewModel.Number,
                Position = playerViewModel.Position,
                TeamId = playerViewModel.TeamId
            };

            teams.Players.Add(player);

            _context.SaveChanges();
        }

        // PUT: api/PlayersApi/5
        public void Put(int id1, PlayerViewModel playerViewModel)
        {
            if (playerViewModel.LastName == null ||
                playerViewModel.FirstName == null ||
                playerViewModel.Position == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Unable to find any results") };
                throw new HttpResponseException(response);
            }

            var team = _context.Teams.FirstOrDefault(m => m.TeamId == playerViewModel.TeamId);

            var players = team.Players;

            var player = players.FirstOrDefault(t => t.PlayerId == playerViewModel.PlayerId);

            player.LastName = playerViewModel.LastName;
            player.FirstName = playerViewModel.FirstName;
            player.Number = playerViewModel.Number;
            player.Position = playerViewModel.Position;
            _context.SaveChanges();
        }

        // DELETE: api/PlayersApi/5
        public void Delete(int id)
        {
            var player = _context.Players.FirstOrDefault(t => t.PlayerId == id);

            _context.Players.Remove(player);

            _context.SaveChanges();
        }
    }
}
