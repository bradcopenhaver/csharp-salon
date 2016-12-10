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
      Post["/client/add"] = _ => {
        Client newClient = new Client(Request.Form["clientName"], Request.Form["clientStylistId"]);
        newClient.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["index.cshtml", allStylists];
      };
      Get["/client/{id}"] = parameters => {
        Client currentClient = Client.Find(parameters.id);
        return View["client.cshtml", currentClient];
      };
      Get["stylist/edit/{id}"] = parameters => {
        Stylist currentStylist = Stylist.Find(parameters.id);
        return View["edit-stylist-form.cshtml", currentStylist];
      };
      Patch["stylist/edit/{id}"] = parameters => {
        Stylist currentStylist = Stylist.Find(parameters.id);
        currentStylist.Edit(Request.Form["stylistName"]);
        return View["stylist.cshtml", currentStylist];
      };
      Get["client/edit/{id}"] = parameters => {
        Client currentClient = Client.Find(parameters.id);
        return View["edit-client-form.cshtml", currentClient];
      };
      Patch["client/edit/{id}"] = parameters => {
        Client currentClient = Client.Find(parameters.id);
        currentClient.Edit(Request.Form["clientName"], currentClient.GetStylistId());
        return View["client.cshtml", currentClient];
      };
    }
  }
}
