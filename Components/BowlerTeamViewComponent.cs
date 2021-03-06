using Assignment10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Components
{
    public class BowlerTeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;
        public BowlerTeamViewComponent (BowlingLeagueContext ctx)
        {
            context = ctx;
        }
        public IViewComponentResult Invoke()
        {
            //highlight selected team on left nav bar
            ViewBag.SelectedTeam = RouteData?.Values["bowlerteam"]; 
            //return list of team names 
            return View(context.Teams
                          //.Select(x => x.TeamName)
                          .Distinct()
                          .OrderBy(x => x)
                          //.ToList()
                          );
        }
    }
}
