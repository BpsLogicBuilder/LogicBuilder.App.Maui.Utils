using LogicBuilder.App.Maui.Business.Cache;
using LogicBuilder.App.Maui.Business.Cache.Interfaces;
using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Maui.Utils.Interfaces;
using LogicBuilder.RulesDirector;

#pragma warning disable IDE0130 // Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended namespace for service registrations
{
    public static class MauiUtilsServiceRegistrations
    {
        public static IServiceCollection AddMauiUtilsServices(this IServiceCollection services)
            => services
                .AddTransient<IFlowManager, FlowManager>()
                .AddScoped<IFlowDataCache, FlowDataCache>()
                .AddScoped<IScreenData, ScreenData>()
                .AddScoped<Progress>()
                .AddTransient<IDialogFunctions, DialogFunctions>()
                .AddTransient<IActions, Actions>();
    }
}
