using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QeAColors.Models
{
    public class Player
    {
        public int CurrentQuestion { get; set; } = 0;
        public List<Result> Results { get; set; } = new List<Result>();
    }
}