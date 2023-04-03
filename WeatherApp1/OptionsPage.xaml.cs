using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherApp1;

public partial class OptionsPage : ContentPage
{
	public OptionsPage()
	{
		City MyCity;
        CityViewModel viewModel;
        InitializeComponent();
	}

    public async void OnButtonAddClick(object sender,EventArgs e)
	{
		var p = EntryCity.Text.ToString();
		string json = await SecureStorage.GetAsync(p);
		if (!string.IsNullOrEmpty(json))
		{
			List<City> data = JsonConvert.DeserializeObject<List<City>>(json);
			foreach(var track in data)
			{
				LabelInfo.Text += Environment.NewLine + track.Name + track.Latitude.ToString() +
					track.Longitude.ToString();
			}
		}
		else
		{
			await SecureStorage.SetAsync("name2", "Sergey");
		}
		
	}
	private void OnButtonClear(object sender, EventArgs e)
	{
		SecureStorage.RemoveAll();
		DisplayAlert("Уведомление", "Список городов очищен!", "ОК");
		List<City> cities;
		City city1 = new City();
		city1.Name = "Москва";
		city1.Latitude = 55.75;
		city1.Longitude = 37.62;
        City city2 = new City();
        city2.Name = "Санкт-Петербург";
        city2.Latitude = 59.94;
        city2.Longitude = 30.31;
        City city3 = new City();
        city3.Name = "Сочи";
        city3.Latitude = 43.60	;
        city3.Longitude = 39.73;
		cities = new List<City> {city1, city2, city3};
		string data = JsonConvert.SerializeObject(cities);
		SecureStorage.SetAsync("City", data);
		DisplayAlert("Уведомение", "Добавлены 3 новых города", "Ок");
    }
    private void EntryCityChanged(object sender, TextChangedEventArgs e)
    {
        this.BindingContext = new CityViewModel
        {
            Name = EntryCity.Text.ToString(),
            Latitude = 0,
            Longitude = 0,
            Time = "",
            Temperature = 0,
            Windspeed = 0,
        };
    }

}