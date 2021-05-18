using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCT.FitApp.Mobile.Service
{
    public class Response
    {
        public int total_hits { get; set; }
        public double max_score { get; set; }
        public List<Hit> hits { get; set; }
    }
}
