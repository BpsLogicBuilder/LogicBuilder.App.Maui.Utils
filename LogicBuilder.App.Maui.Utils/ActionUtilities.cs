using LogicBuilder.App.Maui.Forms.Parameters.Navigation;
using LogicBuilder.App.Maui.Utils.Interfaces;

namespace LogicBuilder.App.Maui.Utils
{
    public static class ActionUtilities
    {
        public static void UpdateNavigationBar(IActions actions, NavigationBarParameters navBar)
        {
            actions.UpdateNavigationBar(navBar);
        }
    }
}
