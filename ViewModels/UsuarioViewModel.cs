using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                if (displayMessage == value)
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
    }
}
