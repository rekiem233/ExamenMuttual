using ExamenMuttual.Shared.Abstraction;
using ExamenMuttual.Shared.Models.SQLite;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenMuttual.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Users : ContentPage
    {
        private readonly ISQLite _sQLiteHelper = null;
        public Users()
        {
            _sQLiteHelper = App.ServiceProvider.GetService<ISQLite>();

            InitializeComponent();
        }
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Usuarios person = new Shared.Models.SQLite.Usuarios()
                {
                    Name = txtName.Text,
                    Email = txtCorreo.Text
                };

                await _sQLiteHelper.SaveItemAsync(person);
                txtName.Text = string.Empty;
                txtCorreo.Text = string.Empty;
                await DisplayAlert("Success", "Usuario agregado", "OK");
                var personList = await _sQLiteHelper.GetItemsAsync();
                if (personList != null)
                {
                    lstPersons.ItemsSource = personList;
                }
            }
            else
            {
                await DisplayAlert("Required", "Ingrese nombre!", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void BtnRetrieve_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPersonId.Text))
            {
                //Get Person  
                var person = await _sQLiteHelper.GetItemAsync(Convert.ToInt32(txtPersonId.Text));
                if (person != null)
                {
                    txtName.Text = person.Name;
                    await DisplayAlert("Success", "Usuario : " + person.Name, "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Ingrese Usuario Id", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPersonId.Text))
            {
                Usuarios person = new Usuarios()
                {
                    PersonID = Convert.ToInt32(txtPersonId.Text),
                    Name = txtName.Text
                };

                //Update Person  
                await _sQLiteHelper.SaveItemAsync(person);

                txtPersonId.Text = string.Empty;
                txtName.Text = string.Empty;
                await DisplayAlert("Success", "Usuario actualizado", "OK");
                //Get All Persons  
                var personList = await _sQLiteHelper.GetItemsAsync();
                if (personList != null)
                {
                    lstPersons.ItemsSource = personList;
                }

            }
            else
            {
                await DisplayAlert("Required", "Ingrese usuario id", "OK");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPersonId.Text))
            {
                //Get Person  
                var person = await _sQLiteHelper.GetItemAsync(Convert.ToInt32(txtPersonId.Text));
                if (person != null)
                {
                    //Delete Person  
                    await _sQLiteHelper.DeleteItemAsync(person);
                    txtPersonId.Text = string.Empty;
                    await DisplayAlert("Success", "Usuario  Eliminado", "OK");

                    //Get All Persons  
                    var personList = await _sQLiteHelper.GetItemsAsync();
                    if (personList != null)
                    {
                        lstPersons.ItemsSource = personList;
                    }
                }
            }
            else
            {
                await DisplayAlert("Required", "Ingrese usuario id", "OK");
            }
        }
    }
}
