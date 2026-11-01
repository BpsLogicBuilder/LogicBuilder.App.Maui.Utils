using LogicBuilder.App.Maui.Forms.Parameters.Navigation;
using LogicBuilder.Attributes;

namespace LogicBuilder.App.Maui.Utils.Interfaces
{
    public interface IActions
    {
        [AlsoKnownAs("SetupNavigationMenu")]
        void UpdateNavigationBar(NavigationBarParameters navBar);
    }
}
