using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.App.Maui.Utils.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LogicBuilder.App.Maui.Utils.Tests
{
    internal class ScopedFlowManagerService : IScopedFlowManagerService
    {
        private readonly IServiceScope scope;

        public ScopedFlowManagerService(IServiceScopeFactory serviceScopeFactory, IAppLogger appLogger)
        {
            this.appLogger = appLogger;
            scope = serviceScopeFactory.CreateScope();
            FlowManager = scope.ServiceProvider.GetRequiredService<IFlowManager>();
        }

        private readonly IAppLogger appLogger;

        public IFlowManager FlowManager { get; }

        public async Task Start()
        {
            DateTime dt = DateTime.Now;
            await this.FlowManager.Start("home");
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"Start (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
        }

        public async Task NewFlowStart(NewFlowRequest request)
        {
            DateTime dt = DateTime.Now;
            await this.FlowManager.NewFlowStart(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"NavStart (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
        }

        public async Task RunFlow(NewFlowRequest request)
        {
            DateTime dt = DateTime.Now;
            await this.FlowManager.NewFlowStart(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"RunFlow (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
        }

        public async Task Next(CommandButtonRequest request)
        {
            DateTime dt = DateTime.Now;
            await this.FlowManager.Next(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"Next (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
                scope?.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SetFlowDataCacheItem(string key, object value)
        {
            if (this.FlowManager.FlowDataCache.Items == null)
                return;

            this.FlowManager.FlowDataCache.Items[key] = value;
        }

        public object? GetFlowDataCacheItem(string key)
        {
            if (this.FlowManager.FlowDataCache.Items == null)
                return null;

            return this.FlowManager.FlowDataCache.Items.TryGetValue(key, out object? value) ? value : null;
        }

        public void CopyFlowItems()
        {
        }

        public void CopyPersistentFlowItems()
        {
        }
    }
}
