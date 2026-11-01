using LogicBuilder.App.Maui.Business.Cache;
using LogicBuilder.App.Maui.Business.Cache.Interfaces;
using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Maui.Utils.Interfaces;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable IDE0130 // Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended namespace for service registrations
{
    public static class MauiUtilsServiceRegistrations
    {
        public static IServiceCollection AddMauiUtilsServices(this IServiceCollection services)
            => services
                .AddScoped<IFlowManager, FlowManager>()
                .AddScoped<FlowDataCache>()
                .AddScoped<IFlowDataCache, FlowDataCache>()
                .AddScoped<ScreenData>()
                .AddScoped<IScreenData, ScreenData>()
                .AddScoped<Progress>()
                .AddScoped<IDialogFunctions, DialogFunctions>()
                .AddScoped<IActions, Actions>();
    }
}
