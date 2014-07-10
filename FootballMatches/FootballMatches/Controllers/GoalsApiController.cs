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
    public class GoalsApiController : ApiController
    {
        private readonly EFDbContext _context;

        public GoalsApiController()
        {
            _context = new EFDbContext();
        }

        // GET: api/Teams/5
        public List<GoalViewModel> Get(int id)
        {
            var team = _context.Teams.FirstOrDefault(m => m.TeamId == id);
            if (team == null)
                return null;

            var goals = team.Goals;

            return goals.Select(p => new GoalViewModel()
            {
                LastName = p.LastName,
                Minute = p.Minute,
                GoalId = p.GoalId,
                TeamId = p.TeamId
            }).ToList();
        }

        public GoalViewModel Get(int id, int id1)
        {
            var team = _context.Teams.FirstOrDefault(m => m.TeamId == id);
            if (team == null)
                return null;

            var goals = team.Goals;

            var goal = goals.FirstOrDefault(t => t.GoalId == id1);

            return new GoalViewModel()
            {
                GoalId = goal.GoalId,
                LastName = goal.LastName,
                Minute = goal.Minute,
                TeamId = goal.TeamId
            };
        }

        // POST: api/GoalsApi
        public void Post(int id, GoalViewModel goalViewModel)
        {
            if (goalViewModel.LastName == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Unable to find any results") };
                throw new HttpResponseException(response);
            }

            var teams = _context.Teams.FirstOrDefault(m => m.TeamId == id);

            var goal = new Goal()
            {
                LastName = goalViewModel.LastName,
                Minute = goalViewModel.Minute,
                TeamId = goalViewModel.TeamId
            };

            teams.Goals.Add(goal);

            _context.SaveChanges();
        }

        // PUT: api/GoalsApi/5
        public void Put(int id1, GoalViewModel goalViewModel)
        {
            if (goalViewModel.LastName == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Unable to find any results") };
                throw new HttpResponseException(response);
            }

            var team = _context.Teams.FirstOrDefault(m => m.TeamId == goalViewModel.TeamId);

            var goals = team.Goals;

            var goal = goals.FirstOrDefault(t => t.GoalId == goalViewModel.GoalId);

            goal.LastName = goalViewModel.LastName;
            goal.Minute = goalViewModel.Minute;
            _context.SaveChanges();
        }

        // DELETE: api/GoalsApi/5
        public void Delete(int id)
        {
            var goal = _context.Goals.FirstOrDefault(t => t.GoalId == id);

            _context.Goals.Remove(goal);

            _context.SaveChanges();
        }
    }
}
