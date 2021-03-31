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
        //set context
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        //                                         pass in pageNum
        public IActionResult Index(long? bowlerteamid, string bowlerteam, int pageNum = 0)
        {
            //pagination # items per page var
            int pageSize = 5;

            return View(new IndexViewModel
            {
                //pagination
                Bowlers = (context.Bowlers
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
                    //counts total num of bowlers or num on selected team
                    TotalNumItems = (bowlerteamid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == bowlerteamid).Count())
                },
                //set team name here
                TeamCategory = bowlerteam
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
