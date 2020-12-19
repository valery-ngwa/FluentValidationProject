using ContactModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ContactModule
{
    public class ViewAModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainRegion", typeof(ViewA));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
