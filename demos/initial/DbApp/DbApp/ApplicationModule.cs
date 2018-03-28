using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DbApp
{
    public class ApplicationModule : IModule
    {
        public ApplicationModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public void Initialize()
        {
            _container.RegisterType<Services.ICustomersService, Services.CustomersService>();
            _regionManager.RegisterViewWithRegion(MainRegionName, typeof(Views.MainView));
        }

        public static string MainRegionName => "Main_Region";
    }
}
