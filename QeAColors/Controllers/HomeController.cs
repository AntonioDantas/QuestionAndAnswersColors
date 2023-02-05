using QeAColors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QeAColors.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["player"] == null)
            {
                Session["player"] = new Player();
            }
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Results()
        {
            if (Session["player"] == null)
            {
                Response.Redirect("Index");
            }
            var results = (Player)Session["player"];
            return View(results.Results);
        }

        public ActionResult New()
        {
            if (Session["player"] == null)
            {
                Response.Redirect("Index");
            }

            if (Session["Questions"] == null)
            {
                Session["Questions"] = Controllers.GameController.LoadQuestions();
            }

            var questions = (List<Question>)Session["Questions"];
            var player = (Player)Session["Player"];
            if (player.CurrentQuestion > questions.Count - 1)
            {
                player.Results.Add(new Result());
            }
            else
            {
                player.Results[player.Results.Count-1] = new Result();
            }
            player.CurrentQuestion = 0;
            Session["Player"] = player;
            return RedirectToAction("../Game/Index");
        }
    }
}