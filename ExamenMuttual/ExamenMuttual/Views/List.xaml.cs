using ExamenMuttual.Shared;
using ExamenMuttual.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenMuttual.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List : ContentPage
    {
        private readonly HttpClient HttpClient = null;
        public List()
        {

            HttpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient("query");
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            var _result = HttpClient.GetAsync("https://randomuser.me/api/?results=50").GetAwaiter().GetResult();
            List<ListItems> vs = new List<ListItems>();
            string _json = _result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var _response = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(_json);
            foreach (var item in _response.results)
            {
                vs.Add( new ListItems() { Nombre = string.Concat(item.name.first, " ", item.name.last), Email = item.email, Pic = item.picture.thumbnail });
            }
            ListControl.ItemsSource = vs;

        }
    }
}