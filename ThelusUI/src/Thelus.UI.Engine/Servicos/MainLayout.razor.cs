using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using Thelus.UI.Engine.Servicos;

namespace Thelus.UI.Engine.Layouts;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [Inject] protected IJSRuntime JS { get; set; } = default!;
    [Inject] protected LayoutStateService LayoutState { get; set; } = default!;

    protected override void OnInitialized()
    {
        // Se inscreve para escutar mudanças no serviço de layout
        LayoutState.OnStateChanged += StateHasChanged;
    }

    protected async Task ToggleTheme()
    {
        await LayoutState.ToggleThemeAsync(JS);
    }

    public void Dispose()
    {
        LayoutState.OnStateChanged -= StateHasChanged;
    }
}