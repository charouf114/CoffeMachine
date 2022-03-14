using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Coffe.Service.Common;
using Newtonsoft.Json;

namespace CoffeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceClient ServiceClient { get; set; }
        List<DrinkTypeWrapper> DrinkTypes { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            this.ServiceClient = new ServiceClient(new System.Net.Http.HttpClient());
            var drinktypeValues = Enum.GetValues(typeof(DrinkType));
            DrinkTypes = new List<DrinkTypeWrapper>();
            foreach (var value in drinktypeValues)
            {
                DrinkTypes.Add(new DrinkTypeWrapper() { Value = value.ToString() });
            }
            cmb_DrinkType.ItemsSource = DrinkTypes;
            cmb_DrinkType.SelectedItem = DrinkTypes[0];
            TB_Message.Visibility = Visibility.Hidden;
        }

        private async void GetLatestCoffe(object sender, RoutedEventArgs e)
        {
            var BadgeId = TB_Badge.Text;
            if (!string.IsNullOrWhiteSpace(BadgeId))
            {
                Drink CoffeRequest = new Drink(BadgeId, DrinkType.Coffe, 0, false);
                var result = await GetLatestCoffe(CoffeRequest);
                if(result != null)
                {
                    cmb_DrinkType.SelectedItem = DrinkTypes.FirstOrDefault( c => c.Value == result.DrinkType.ToString());
                    TB_SucarCount.Text = result.SucreCount.ToString();
                    CB_HasMug.IsChecked = result.HasMug;
                    TB_Badge.Text = result.BadgeId;
                    TB_Message.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("An Error Occured");
                }
            }
            else
            {
                MessageBox.Show("Please enter your Badge identifier");
            }

        }

        private async void RequestCoffe(object sender, RoutedEventArgs e)
        {
            var drinkTypeWrapper = (DrinkTypeWrapper)cmb_DrinkType.SelectedItem;
            var drinkType = drinkTypeWrapper is null ? null : drinkTypeWrapper.getCoffeType();
            var sucarCountAsStr = TB_SucarCount.Text;
            var hasMug = CB_HasMug.IsChecked;
            var BadgeId = TB_Badge.Text;

            List<string> errorList = new List<string>();
            if(!drinkType.HasValue)
            {
                errorList.Add("Please enter a drink Type");
            }

            if (!int.TryParse(sucarCountAsStr, out int sucarCount))
            {
                errorList.Add("Please enter the quantity of the sucar");
            }

            if (!hasMug.HasValue)
            {
                errorList.Add("Please enter if you have a mug or Not");
            }

            if (string.IsNullOrWhiteSpace(BadgeId))
            {
                errorList.Add("Please enter your Badge identifier");
            }

            if (errorList.Count == 0)
            {
                Drink CoffeRequest = new Drink(BadgeId, drinkType.Value, sucarCount, hasMug.Value);
                var result = await RequestCoffe(CoffeRequest);
                if(result == null)
                {
                    MessageBox.Show("An Error Occured");
                }

                TB_Message.Visibility = Visibility.Hidden;
                MessageBox.Show("Your Coffe is Ready, Drink Well");
            }
            else
            {
                MessageBox.Show(string.Join('\r', errorList));
            }
        }

        private async Task<Drink> RequestCoffe(Drink coffeRequest)
        {
            var url = ServiceClient.BuildUrl("AddCoffe");
            return await CallService(coffeRequest, url);
        }

        private async Task<Drink> GetLatestCoffe(Drink coffeRequest)
        {
            var url = ServiceClient.BuildUrl("LastCoffe");
            return await CallService(coffeRequest, url);
        }

        private async Task<Drink> CallService(Drink coffeRequest, string url)
        {
            string result = await ServiceClient.PostAsync(coffeRequest, url);

            Drink coffeRequestResult = null;
            if (!string.IsNullOrWhiteSpace(result))
            {
                coffeRequestResult = JsonConvert.DeserializeObject<Drink>(result);
            }
            return coffeRequestResult;
        }

        
    }
}
