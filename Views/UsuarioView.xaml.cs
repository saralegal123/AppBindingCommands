using AppBindingCommands.ViewModels;

namespace AppBindingCommands.View;

public partial class UsuarioView : ContentPage
{
	private UsuarioViewModel viewModel;
	public UsuarioView()
	{
		InitializeComponent();
		BindingContext = new UsuarioViewModel();
		BindingContext = viewModel;
	}
}