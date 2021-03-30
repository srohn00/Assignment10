using Assignment10.Models;
using Assignment10.Models.ViewModels;
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

        //                                         pass in pageNum
        public IActionResult Index(long? bowlerteamid, int pageNum = 0)
        {
            //var blah = "%oooword%"; //for string interpolation>>replace bowlerSearch w/ blah below
            //or could pass in a parameter to change search var each time
            //index(string bowlerSearch) example

            //pagination # items per page var
            int pageSize = 5;
            //pagination current page #
            //int pageNum = 1;

            return View(new IndexViewModel
            {
                Bowlers = (context.Bowlers
                //.FromSqlRaw("SELECT * FROM Bowlers WHERE BowlerFirstName LIKE \"%A%\" ORDER BY BowlerFirstName")
                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId LIKE {teamSearch}")

                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {bowlerteamid} OR {bowlerteamid} IS NULL")
                .Where(m => m.TeamId == bowlerteamid || bowlerteamid == null)
                .OrderBy(m => m.BowlerFirstName)
                //pagination
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage=pageSize,
                    CurrentPage = pageNum,
                    //counts total num of bowlers or num o selected team
                    TotalNumItems = (bowlerteamid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == bowlerteamid).Count())
                }
            });
                
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
