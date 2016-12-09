using Nancy;
using System.Collections.Generic;
using System;
using HairSalon.Objects;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
      Get["/stylist/add"] = _ => {
        return View["new-stylist-form.cshtml"];
      };
      Post["/stylist/add"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylistName"]);
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
    }
  }
}
