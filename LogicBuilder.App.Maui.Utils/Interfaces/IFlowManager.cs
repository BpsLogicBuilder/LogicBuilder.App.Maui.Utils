using LogicBuilder.App.Maui.Business.Cache.Interfaces;
using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.App.Maui.Business.Settings;
using LogicBuilder.RulesDirector;
using System;
using System.Threading.Tasks;

namespace LogicBuilder.App.Maui.Utils.Interfaces
{
    public interface IFlowManager
    {
        Progress Progress { get; }
        DirectorBase Director { get; }
        IFlowDataCache FlowDataCache { get; }
        IFlowActivity FlowActivity { get; }
        IRulesCache RulesCache { get; }
        IServiceProvider ServiceProvider { get; }

        Task<FlowSettings> Start(string module);
        Task<FlowSettings> Next(CommandButtonRequest request);
        Task<FlowSettings> NewFlowStart(NewFlowRequest request);
        void FlowComplete();
        void Terminate();
        void SetCurrentBusinessBackupData();
    }
}
