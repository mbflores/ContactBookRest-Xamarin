using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ContactBookXam.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ContactBookXam
{
    public partial class MainPage : ContentPage
    {
        private const string url = "http://contactbookrest.azurewebsites.net/api/contacts";
        private HttpClient _client = new HttpClient();
        string sContentType = "application/json";
        private ObservableCollection<Contact> _contacts= new ObservableCollection<Contact>();
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            var post = _contacts = await GetContact();
            _contacts = new ObservableCollection<Contact>(post);
            LstView.ItemsSource = _contacts;
            base.OnAppearing();
        }

        private async void LstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = (Contact)e.SelectedItem;
            var page = new ContactForm(selected);
            page.SaveBtn.Clicked += async (s, x) =>
            {
                selected.FirstName = page.FirstEntry.Text.ToString();
                selected.LastName = page.LastEntry.Text.ToString();

                var select = JsonConvert.SerializeObject(selected);
                await _client.PutAsync(url + "/" + selected.Id, new StringContent(select, Encoding.UTF8, sContentType));
                await Navigation.PopAsync();
            };
            await Navigation.PushAsync(page);
        }

        private async void LstView_Refreshing(object sender, EventArgs e)
        {
            var post = _contacts = await GetContact();
            _contacts = new ObservableCollection<Contact>(post);
            LstView.ItemsSource = _contacts;
            LstView.EndRefresh();

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var page = new ContactForm();
            page.SaveBtn.Clicked += async (s, x) =>
            {
                var contact = new Contact
                {
                    FirstName = page.FirstEntry.Text.ToString(),
                    LastName = page.LastEntry.Text.ToString()
                };


                var select = JsonConvert.SerializeObject(contact);
                await _client.PostAsync(url, new StringContent(select, Encoding.UTF8, sContentType));
                await Navigation.PopAsync();
            };
            await Navigation.PushAsync(page);
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var selected = (sender as MenuItem).CommandParameter as Contact;
            _contacts.Remove(selected);
            await _client.DeleteAsync(url + "/" + selected.Id);
        }

        private async Task<ObservableCollection<Contact>> GetContact()
        {
            var result = Task.Run(async () =>
            {
                var get = await _client.GetStringAsync(url);
                var post = JsonConvert.DeserializeObject<List<Contact>>(get);
                return new ObservableCollection<Contact>(post);
            });
            return await result;
        }
    }
}
