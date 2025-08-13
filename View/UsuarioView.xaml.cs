using AppBindingCommands.ViewModels;

namespace AppBindingCommands.View;

public partial class UsuarioView : ContentPage
{
	public UsuarioView()
	{
		InitializeComponent();
		BindingContext = new UsuarioViewModel();
	}
}