﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Models.ViewModels
{
    public class IndexViewModel
    {
        //connect multiple models
        public List<Bowlers> Bowlers { get; set; }
        public PageNumberingInfo PageNumberingInfo { get; set; }
        public string TeamCategory { get; set; }
    }
}
