using LogicBuilder.App.Maui.Business.Settings.Screen;
using LogicBuilder.App.Maui.Utils.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace LogicBuilder.App.Maui.Utils.Tests
{
    public class TerminateTest
    {
        public TerminateTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public async Task CallTerminateFlow()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            await flowManager.Start("callterminateflow");

            //assert
            Assert.Equal("Contoso with MyUniqueValue appended.", flowManager.FlowDataCache.NavigationBar.BrandText);
        }

        #region Helpers
        [MemberNotNull(nameof(serviceProvider))]
        private void Initialize()
        {
            serviceProvider = new ServiceCollection()
                .AddAutoMapperServices()
                .AddLogging()
                .AddFlowServices()
                .AddRulesCache()
                .AddSingleton<IAppLogger, AppLogger>()
                .AddTransient<IScopedFlowManagerService, ScopedFlowManagerService>()
                .AddAppUtilsServices()
                .BuildServiceProvider();
        }
        #endregion Helpers
    }
}
