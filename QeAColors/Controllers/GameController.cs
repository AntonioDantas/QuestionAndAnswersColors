using System.Text.Json;
using QeAColors.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QeAColors.Controllers
{
    public class GameController : Controller
    {
        // GET: Game Question
        public ActionResult Index()
        {
            if (Session["Player"] == null)
            {
                return RedirectToAction("~/Home");
            }

            if (Session["Questions"] == null)
            {
                Session["Questions"] = LoadQuestions();
            }

            var questions = (List<Question>)Session["Questions"];
            var player = (Player)Session["Player"];

            if (player.CurrentQuestion > questions.Count - 1)
            {
                Response.Redirect("~/Results");
                return null;
            }

            return View(questions[player.CurrentQuestion]);
        }

        // POST: Game Next
        [HttpPost]
        [Route("Next")]
        public ActionResult Next(int awnser)
        {
            try
            {
                var questions = (List<Question>)Session["Questions"];
                var player = (Player)Session["Player"];
                var currentQuestion = questions[player.CurrentQuestion];
                var currentAwnser = (Color)(awnser);

                if (player.Results == null)
                {
                    player.Results = new List<Result>();
                }
                if (player.Results.Count() == 0)
                {
                    player.Results.Add(new Result());
                }

                var lastResult = player.Results.LastOrDefault();
                lastResult.Questions.Add(currentQuestion);
                lastResult.Answers.Add(currentAwnser);
                if (player.CurrentQuestion == questions.Count() - 1)
                {
                    lastResult.DateEnd = DateTime.Now;
                }
                player.CurrentQuestion++;
                player.Results[player.Results.Count - 1] = lastResult;
                Session["player"] = player;

                return (lastResult.DateEnd == DateTime.MaxValue) ? RedirectToAction("../Game") : RedirectToAction("../Home/Results");
            }
            catch
            {
                return RedirectToAction("../");
            }
        }

        public static IEnumerable<Question> LoadQuestions()
        {
            string jsonString = System.IO.File.ReadAllText("C:\\Game\\Questions.json");
            return JsonSerializer.Deserialize<List<Question>>(jsonString);
        }

    }
}
