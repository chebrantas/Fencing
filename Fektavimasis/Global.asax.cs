using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Fektavimasis.Models;

namespace Fektavimasis
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //naudojama refreshinti DB strukturai jei Modeliai yra pasikeite
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ParticipantsDB>());

            //su seedu uzpildome db kad butu su kuo dirbti
            //Database.SetInitializer(new MusicStoreDBInitializer());
            //System.Data.Entity.Database.SetInitializer(new MvcMusicStore5.Models.SampleData());


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
