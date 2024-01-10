using Cherrez_EjemploAPI.Model;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static Cherrez_EjemploAPI.Model.AC_Weather;
using static System.Net.WebRequestMethods;

namespace Cherrez_EjemploAPI.View;

public partial class AC_ClimaActual : ContentPage
{
	public AC_ClimaActual()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		string latitud = lat.Text;
		string longitud = lon.Text;

		if (Connectivity.NetworkAccess == NetworkAccess.Internet) 
		{
			using (var client = new HttpClient())
            {
				string url = $"https://api.openweathermap.org/data/2.5/weather?lat=" + latitud + "&lon=" + longitud + "&appid=45a3596e8cf096a3ea27dffa47ff4402";
					var response = await client.GetAsync(url);
				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();
                    var clima = JsonConvert.DeserializeObject<Rootobject>(json);

					weatherLabel.Text = clima.weather[0].main;
					cityLabel.Text = clima.name;
					countryLabel.Text = clima.sys.country;
				}

			}
		}
    }
}