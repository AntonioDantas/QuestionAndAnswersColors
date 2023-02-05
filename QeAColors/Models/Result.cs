using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QeAColors.Models
{
    public class Result
    {
        public Result()
        {
            DateStart = DateTime.Now;
            DateEnd = DateTime.MaxValue;
            Answers = new List<Color>();
            Questions = new List<Question>();
        }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<Color> Answers { get; set; } = new List<Color>();
        public int CountResult {
            get => Questions.Select((x, i) => new { c = x.Color, index = i })
                            .Count(y => y.c == this.Answers.ToArray()[y.index]);
        }
    }
}