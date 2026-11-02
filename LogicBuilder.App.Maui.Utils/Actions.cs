using AutoMapper;
using LogicBuilder.App.Maui.Business.Cache.Interfaces;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;
using LogicBuilder.App.Maui.Forms.Parameters.Navigation;
using LogicBuilder.App.Maui.Utils.Interfaces;

namespace LogicBuilder.App.Maui.Utils
{
    public class Actions(IFlowDataCache flowDataCache, IMapper mapper) : IActions
    {
        #region Fields
        private readonly IFlowDataCache flowDataCache = flowDataCache;
        private readonly IMapper mapper = mapper;
        #endregion Fields

        public void UpdateNavigationBar(NavigationBarParameters navBar)
        {
            this.flowDataCache.NavigationBar = mapper.Map<NavigationBarDescriptor>(navBar);
        }
    }
}
