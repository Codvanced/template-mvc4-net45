using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DryIoc;
using DryIoc.Mvc;
using System.Reflection;
using NAME_REPLACE.Binding;
using IOC.FW.Core.Abstraction.Container.Binding;
using IOC.FW.ContainerManager.DryIoc;

namespace NAME_REPLACE.WebMvcApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var adapter = new DryIocAdapter();
            
            var binders = new IBinding[]{
                new BusinessBinder(),
                new DaoBinder(),
                new SharedBinder(),
                new FrameworkBinder(),
            };

            foreach (var binder in binders)
                binder.SetBinding(adapter);

            var containerWithMvc = adapter._container.WithMvc();
            var ioc = new DryIocDependencyResolver(containerWithMvc);
            
            DependencyResolver.SetResolver(ioc);
        }
    }
}