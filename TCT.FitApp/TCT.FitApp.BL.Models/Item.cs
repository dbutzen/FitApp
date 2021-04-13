﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TCT.FitApp.BL.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid Type { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        

    }
}