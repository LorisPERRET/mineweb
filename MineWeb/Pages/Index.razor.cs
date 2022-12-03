using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MineWeb.Pages;

namespace MineWeb.Pages
{
    public partial class Index
    {
        [Inject]
        public IStringLocalizer<Index> Localizer { get; set; }
    }
}
