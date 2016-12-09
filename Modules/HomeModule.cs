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
      Get["/stylist/{id}"] = parameters => {
        Stylist currentStylist = Stylist.Find(parameters.id);
        return View["stylist.cshtml", currentStylist];
      };
      Get["/client/add"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["new-client-form.cshtml", allStylists];
      };
      // Get["/client/add/{id}"] = parameters => {
      //   List<Stylist> allStylists = Stylist.GetAll();
      //   return View["new-client-form.cshtml", allStylists];
      // }
      Post["/client/add"] = _ => {
        Client newClient = new Client(Request.Form["clientName"], Request.Form["clientStylistId"]);
        newClient.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
    }
  }
}
