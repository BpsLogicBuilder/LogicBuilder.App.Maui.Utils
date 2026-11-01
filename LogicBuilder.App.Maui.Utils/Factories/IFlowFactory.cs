using LogicBuilder.App.Maui.Utils.Interfaces;
using LogicBuilder.RulesDirector;

namespace LogicBuilder.App.Maui.Utils.Factories
{
    public interface IFlowFactory
    {
        DirectorBase GetDirector(IFlowManager flowManager);
        IFlowActivity GetFlowActivity(IFlowManager flowManager);
    }
}
