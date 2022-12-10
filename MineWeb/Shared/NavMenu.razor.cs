using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MineWeb.Pages;

namespace MineWeb.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        [Inject]
        public IStringLocalizer<NavMenu> Localizer { get; set; }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}
