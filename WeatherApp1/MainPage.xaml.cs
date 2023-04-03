namespace WeatherApp1;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class MainPage : ContentPage
{
	City MyCity;
	List<City> data;

	CityViewModel viewmodel;

	public MainPage()
	{
		InitializeComponent();
		LoadCities();
	}

	public async void LoadCities()
	{
		string json = await SecureStorage.GetAsync("City");
		if (!string.IsNullOrEmpty(json))
		{
			data = JsonConvert.DeserializeObject<List<City>>(json);
			foreach(var track in data)
			{
				LanguagePicker.Items.Add(track.Name);
			}
		}
	}

    private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
		string name = LanguagePicker.SelectedItem.ToString();
		foreach(var track in data)
		{
			if(name == track.Name)
			{
				DisplayAlert("Уведомление", track.Name + " lat:" + track.Latitude.ToString()
					+ " lon " + track.Longitude.ToString(), "OK");

				this.BindingContext = new CityViewModel
				{
                    Name = track.Name,
					Latitude = track.Latitude,
					Longitude = track.Longitude,
					Time = "",
					Temperature = 0,
					Windspeed = 0,
				};
			}
		}
    }
}

