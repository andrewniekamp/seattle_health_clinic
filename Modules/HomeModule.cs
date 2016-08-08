using Nancy;
using System;
using System.Collections.Generic;

namespace SeattleHealthClinic
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      //landing page
      Get["/"] = _ => {
        return View["landing.cshtml"];
      };

      //home view, must pass through an employee object in each view!
      Post["/home_view"] = _ => {
        return View["index.cshtml"];
      };
    }
  }
}
