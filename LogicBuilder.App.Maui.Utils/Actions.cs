using AutoMapper;
using LogicBuilder.App.Maui.Business.Cache;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;
using LogicBuilder.App.Maui.Forms.Parameters.Navigation;
using LogicBuilder.App.Maui.Utils.Interfaces;

namespace LogicBuilder.App.Maui.Utils
{
    public class Actions(FlowDataCache flowDataCache, IMapper mapper) : IActions
    {
        #region Fields
        private readonly FlowDataCache flowDataCache = flowDataCache;
        private readonly IMapper mapper = mapper;
        #endregion Fields

        public void UpdateNavigationBar(NavigationBarParameters navBar)
        {
            this.flowDataCache.NavigationBar = mapper.Map<NavigationBarDescriptor>(navBar);
        }
    }
}
