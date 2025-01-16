using System.Text.Json;
using System.Text;
using ReactionsApp.Helpers;
using ReactionsApp.Models;

namespace ReactionsApp;

public partial class LeaderboardPage : ContentPage
{
	public LeaderboardPage()
	{
		InitializeComponent();
        gamePicker.Items.Add("Starting Lights");
        gamePicker.Items.Add("Random Points");
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        gamePicker.SelectedIndex = 0;
    }

    private void GenerateTable<T>(List<T> items)
    {
        table.ColumnDefinitions.Clear();
        table.Clear();

        var properties = typeof(T).GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            table.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            table.Add(new Label() { Text = properties[i].Name }, i, 0);
        }

        for (int i = 0; i < items.Count; i++)
        {
            for (int j = 0; j < properties.Length; j++)
            {
                table.Add(new Label() { Text = properties[j].GetValue(items[i]).ToString() }, j, i + 1);
            }
        }
    }

    private async void OnGameSelected(object sender, EventArgs e)
    {
        string selectedGame = gamePicker.SelectedItem as string;

        if (selectedGame == "Starting Lights")
        {
            try
            {

                // Send a POST request
                var response = await HttpClientWrapper.GetAuthorizedAsync("/api/startinglightsgameresult", Preferences.Get("token", ""));
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonSerializer.Deserialize<List<StartingLightsGameResultModel>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    GenerateTable(responseData);
                }
                else
                {
                    var responseData = content;

                }
            }
            catch (Exception ex)
            {

            }
        }
        else if (selectedGame == "Random Points")
        {
            try
            {

                // Send a POST request
                var response = await HttpClientWrapper.GetAuthorizedAsync("/api/randompointsgameresult", Preferences.Get("token", ""));
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonSerializer.Deserialize<List<RandomPointsGameResultModel>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    GenerateTable(responseData);
                }
                else
                {
                    var responseData = content;

                }
            }
            catch (Exception ex)
            {

            }
        }


        // Re-generate the table with filtered data
        //GenerateTable(filteredData);
    }
}