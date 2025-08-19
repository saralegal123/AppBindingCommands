using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppBindingCommands.ViewModels
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        //quando criamos um command é MUITO importante chamar ele no codigo pra meio qu deixar salvo
        public UsuarioViewModel()
        {
            ShowMessageCommand = new Command(ShowMessage);
            CountCommand = new Command(async () => await CountCharacters());
            CleanCommand = new Command(async () => await CleanConfirmation());
            OptionCommand = new Command(async() => await ShowOptions());   
        }
       
        public event PropertyChangedEventHandler? PropertyChanged;

        //metodo que notifica quando propriedades tem qualquer tipo de alteração
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?. Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //CTRL + R + E = cria a propriedade
        private string name = string.Empty; //atributo

        //propriedade
        //entao quando esse trem for alterado vamos ser notificados
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));
            } 
        }

        //propriedade que vai ser visualizada na tela
        public string DisplayName => $"Nome digitado: {Name}";

        private string displayMessage = string.Empty;
        public string DisplayMessage 
        { 
            get => displayMessage; 
            set 
            {
                if (displayMessage == null)
                    return;

                displayMessage = value;
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public ICommand ShowMessageCommand { get; }

        //metodo de mostrar as infos adicionadas (nome e data)
        public void ShowMessage()
        {
            DateTime data = DateTime.Now;
            DisplayMessage = $"Boa Noite, {Name}. Hoje é {data}";
        }

        public async Task CountCharacters()
        {
            string nameLenght =
                string.Format("Seu nome tem {0} letras", name.Length);

            await Application.Current
                .MainPage.DisplayAlert("Informação", nameLenght, "OK");
        }

        public ICommand CountCommand { get; }

       public async Task ShowOptions()
        {
            string result = await Application.Current.MainPage
                .DisplayActionSheet("Selecione uma opção: ", "", "Cancelar", "Limpar", "Contar Caracteres", "Exibir Saudação");
            
            if (result != null)
            {
                if(result.Equals("Limpar"))
                    await CleanConfirmation();
                if (result.Equals("Contar Caracteres"))
                    await CountCharacters();
                if (result.Equals("Exibir Saudação"))
                    ShowMessage();
            }
        }
        public ICommand OptionCommand { get; }

        public async Task CleanConfirmation()
        {
            if (await Application.Current.MainPage
                .DisplayAlert("Confirmação", "Confirmar limpeza dos dados?", "Yes", "No"))
            {
                Name = string.Empty;
                DisplayMessage = string.Empty;
                OnPropertyChanged(Name);
                OnPropertyChanged(DisplayMessage);

                await Application.Current.MainPage
                    .DisplayAlert("Informação", "Limpeza realizada com sucesso", "Ok");
            }
        }
        public ICommand CleanCommand { get; }
    }
}
