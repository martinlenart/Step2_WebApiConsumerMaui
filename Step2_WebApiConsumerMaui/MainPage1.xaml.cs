using Step2_WebApiConsumerConsole.Models;
using Step2_WebApiConsumerConsole.Services;

namespace Step2_WebApiConsumerMaui;

public partial class MainPage1 : ContentPage
{
    public WebApiID webApi { get; set; }

    string _serviceUri = "https://localhost:7249";
    FriendsHttpService _service;

    int count = 0;

    public MainPage1()
	{
		InitializeComponent();

        _service = new FriendsHttpService(new Uri(_serviceUri));

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //This is making the first load of data
        MainThread.BeginInvokeOnMainThread(async () => { await RunService(); });
    }

    private async Task RunService()
    {
        try
        {
            webApi = await _service.GetInfoAsync();

            //Should better be done in constructor and then implement INotifyPropertyChange
            this.BindingContext = webApi;

            lblConnectionStatus.Text = $"Connection established to {_serviceUri}";
        }
        catch
        {
            lblConnectionStatus.Text = "Could not connect";
            webApi = new WebApiID();
        }
    }
}


