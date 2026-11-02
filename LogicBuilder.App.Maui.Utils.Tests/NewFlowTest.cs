using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.App.Maui.Business.Settings;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.ListForm;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;
using LogicBuilder.App.Maui.Forms.Configuration.SearchForm;
using LogicBuilder.App.Maui.Forms.Configuration.TextForm;
using LogicBuilder.App.Maui.Utils.Interfaces;
using LogicBuilder.Forms.Parameters.Expressions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace LogicBuilder.App.Maui.Utils.Tests
{
    public class NewFlowTest
    {
        public NewFlowTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public async Task SatrtFlowWithHomeTarget_StopsAtHomeScreen()
        {
            //arrange
            using IScopedFlowManagerService flowManagerService = serviceProvider.GetRequiredService<IScopedFlowManagerService>();

            //act
            var result = await flowManagerService.FlowManager.Start("home");

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<TextFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.TextPage, result.ScreenSettings.ViewType);
            Assert.Equal("Home", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task SatrtFlowWithInvalidTarget_StopsAtExceptionScreen()
        {
            //arrange
            using IScopedFlowManagerService flowManagerService = serviceProvider.GetRequiredService<IScopedFlowManagerService>();

            //act
            var result = await flowManagerService.FlowManager.Start("invalid");

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<TextFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.TextPage, result.ScreenSettings.ViewType);
            Assert.Equal("Exception", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task FlowWithHomeTarget_StopsAtHomeScreen()
        {
            //arrange
            using IScopedFlowManagerService flowManagerService = serviceProvider.GetRequiredService<IScopedFlowManagerService>();

            //act
            var result = await flowManagerService.FlowManager.NewFlowStart(new NewFlowRequest { InitialModuleName = "home"});

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<TextFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.TextPage, result.ScreenSettings.ViewType);
            Assert.Equal("Home", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task FlowWithInvalidTarget_StopsAtExceptionScreen()
        {
            //arrange
            using IScopedFlowManagerService flowManagerService = serviceProvider.GetRequiredService<IScopedFlowManagerService>();

            //act
            var result = await flowManagerService.FlowManager.NewFlowStart(new NewFlowRequest { InitialModuleName = "invalid" });

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<TextFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.TextPage, result.ScreenSettings.ViewType);
            Assert.Equal("Exception", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task FlowWithAboutTarget_StopsAtAboutScreen()
        {
            //arrange
            using IScopedFlowManagerService flowManagerService = serviceProvider.GetRequiredService<IScopedFlowManagerService>();

            //act
            var result = await flowManagerService.FlowManager.NewFlowStart(new NewFlowRequest { InitialModuleName = "about" });

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<ListFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.ListPage, result.ScreenSettings.ViewType);
            Assert.Equal("About", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task FlowWithCoursesTarget_StopsAtCoursesSearchForm()
        {
            //arrange
            using IScopedFlowManagerService flowManagerService = serviceProvider.GetRequiredService<IScopedFlowManagerService>();

            //act
            var result = await flowManagerService.FlowManager.NewFlowStart(new NewFlowRequest { InitialModuleName = "courses" });

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<SearchFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.SearchPage, result.ScreenSettings.ViewType);
            Assert.Equal("Courses", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task FlowWithStudentsTarget_CanVisitStudentsAddScreen()
        {
            //arrange
            FlowSettings? initialFlowSettings;
            using (IScopedFlowManagerService flowManagerService1 = serviceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                initialFlowSettings = await flowManagerService1.FlowManager.NewFlowStart(new NewFlowRequest { InitialModuleName = "students" });
            }

            using IScopedFlowManagerService flowManagerService2 = serviceProvider.GetRequiredService<IScopedFlowManagerService>();
            ((Director)flowManagerService2.FlowManager.Director).FlowState = initialFlowSettings!.FlowState;
            flowManagerService2.FlowManager.FlowDataCache.PersistentKeys = [.. initialFlowSettings.FlowDataCache.PersistentKeys];
            flowManagerService2.FlowManager.FlowDataCache.Items = new Dictionary<string, object>(initialFlowSettings.FlowDataCache.Items);
            flowManagerService2.FlowManager.FlowDataCache.NavigationBar = new NavigationBarDescriptor
            (
                brandText: initialFlowSettings.FlowDataCache.NavigationBar.BrandText,
                currentModule: initialFlowSettings.FlowDataCache.NavigationBar.CurrentModule,
                menuItems: [.. initialFlowSettings.FlowDataCache.NavigationBar.MenuItems]
            );

            //act
            var result = await flowManagerService2.FlowManager.Next(new CommandButtonRequest { NewSelection = "students_ADD" });

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<DataFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.EditForm, result.ScreenSettings.ViewType);
            Assert.Equal("Add Student", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task FlowWithDepartmentsTarget_CanVisitDepartmentsDetailScreen()
        {
            //arrange
            FlowSettings? initialFlowSettings;
            using (IScopedFlowManagerService flowManagerService1 = serviceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                initialFlowSettings = await flowManagerService1.FlowManager.NewFlowStart(new NewFlowRequest { InitialModuleName = "departments" });
            }

            using IScopedFlowManagerService flowManagerService2 = serviceProvider.GetRequiredService<IScopedFlowManagerService>();
            ((Director)flowManagerService2.FlowManager.Director).FlowState = initialFlowSettings!.FlowState;
            flowManagerService2.FlowManager.FlowDataCache.PersistentKeys = [.. initialFlowSettings.FlowDataCache.PersistentKeys];
            flowManagerService2.FlowManager.FlowDataCache.Items = new Dictionary<string, object>(initialFlowSettings.FlowDataCache.Items);
            flowManagerService2.FlowManager.FlowDataCache.NavigationBar = new NavigationBarDescriptor
            (
                brandText: initialFlowSettings.FlowDataCache.NavigationBar.BrandText,
                currentModule: initialFlowSettings.FlowDataCache.NavigationBar.CurrentModule,
                menuItems: [.. initialFlowSettings.FlowDataCache.NavigationBar.MenuItems]
            );
            flowManagerService2.FlowManager.FlowDataCache.Items[typeof(DepartmentModel).FullName!] = new DepartmentModel { DepartmentID = 111, Name = "Physics", Budget = 1000000, InstructorID = 1 };

            //act
            var result = await flowManagerService2.FlowManager.Next(new CommandButtonRequest { NewSelection = "departments_DETAIL" });

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<DataFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.DetailForm, result.ScreenSettings.ViewType);
            Assert.Equal("Departments", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task FlowWithInsructorsTarget_CanVisitInsructorsEditScreen()
        {
            //arrange
            FlowSettings? initialFlowSettings;
            using (IScopedFlowManagerService flowManagerService1 = serviceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                initialFlowSettings = await flowManagerService1.FlowManager.NewFlowStart(new NewFlowRequest { InitialModuleName = "instructors" });
            }

            using IScopedFlowManagerService flowManagerService2 = serviceProvider.GetRequiredService<IScopedFlowManagerService>();
            ((Director)flowManagerService2.FlowManager.Director).FlowState = initialFlowSettings!.FlowState;
            flowManagerService2.FlowManager.FlowDataCache.PersistentKeys = [.. initialFlowSettings.FlowDataCache.PersistentKeys];
            flowManagerService2.FlowManager.FlowDataCache.Items = new Dictionary<string, object>(initialFlowSettings.FlowDataCache.Items);
            flowManagerService2.FlowManager.FlowDataCache.NavigationBar = new NavigationBarDescriptor
            (
                brandText: initialFlowSettings.FlowDataCache.NavigationBar.BrandText,
                currentModule: initialFlowSettings.FlowDataCache.NavigationBar.CurrentModule,
                menuItems: [.. initialFlowSettings.FlowDataCache.NavigationBar.MenuItems]
            );
            flowManagerService2.FlowManager.FlowDataCache.Items[typeof(InstructorModel).FullName!] = new InstructorModel { ID = 111, FirstName = "John", LastName = "Smith", HireDate = new DateTime(2026, 11, 11, 0, 0, 0, DateTimeKind.Unspecified) };

            //act
            var result = await flowManagerService2.FlowManager.Next(new CommandButtonRequest { NewSelection = "instructors_EDIT" });

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<DataFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.EditForm, result.ScreenSettings.ViewType);
            Assert.Equal("Edit Instructor", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task FlowWithInsructorsTarget_WithMissingModelForEditScreen_StopsAtExceptionScreenScreen()
        {
            //arrange
            FlowSettings? initialFlowSettings;
            using (IScopedFlowManagerService flowManagerService1 = serviceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                initialFlowSettings = await flowManagerService1.FlowManager.NewFlowStart(new NewFlowRequest { InitialModuleName = "instructors" });
            }

            using IScopedFlowManagerService flowManagerService2 = serviceProvider.GetRequiredService<IScopedFlowManagerService>();
            ((Director)flowManagerService2.FlowManager.Director).FlowState = initialFlowSettings!.FlowState;
            flowManagerService2.FlowManager.FlowDataCache.PersistentKeys = [.. initialFlowSettings.FlowDataCache.PersistentKeys];
            flowManagerService2.FlowManager.FlowDataCache.Items = new Dictionary<string, object>(initialFlowSettings.FlowDataCache.Items);
            flowManagerService2.FlowManager.FlowDataCache.NavigationBar = new NavigationBarDescriptor
            (
                brandText: initialFlowSettings.FlowDataCache.NavigationBar.BrandText,
                currentModule: initialFlowSettings.FlowDataCache.NavigationBar.CurrentModule,
                menuItems: [.. initialFlowSettings.FlowDataCache.NavigationBar.MenuItems]
            );

            //act
            var result = await flowManagerService2.FlowManager.Next(new CommandButtonRequest { NewSelection = "instructors_EDIT" });

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<TextFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.TextPage, result.ScreenSettings.ViewType);
            Assert.Equal("Exception", screenSettings.Settings.Title);
        }

        [Fact]
        public async Task TransientFlow_Succeeds()
        {
            //arrange
            using IScopedFlowManagerService flowManagerService = serviceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.SetFlowDataCacheItem
            (
                "SkipCount",
                2
            );

            flowManagerService.SetFlowDataCacheItem
            (
                "SearchText",
                ""
            );

            //act
            var result = await flowManagerService.FlowManager.NewFlowStart(new NewFlowRequest { InitialModuleName = "paging_selector_students" });

            //assert
            Assert.Null(result.ScreenSettings);
            Assert.NotNull(flowManagerService.GetFlowDataCacheItem(typeof(SelectorLambdaOperatorParameters).FullName!));
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
