using Assignment10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(long? bowlerteamid)
        {
            //var blah = "%oooword%"; //for string interpolation>>replace bowlerSearch w/ blah below
            //or could pass in a parameter to change search var each time
                //index(string bowlerSearch) example

            return View(context.Bowlers
                //.FromSqlRaw("SELECT * FROM Bowlers WHERE BowlerFirstName LIKE \"%A%\" ORDER BY BowlerFirstName")
                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId LIKE {teamSearch}")
                .FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {bowlerteamid} OR {bowlerteamid} IS NULL")
                //.Where(x => x.BowlerFirstName.Contains("A"))
                //.OrderBy(x => x.BowlerId)
                .ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
