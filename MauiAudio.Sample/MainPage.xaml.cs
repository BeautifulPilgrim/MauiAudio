namespace MauiAudio.Sample;

public partial class MainPage : ContentPage
{
	int count = 0;
	MainPageViewModel viewModel=>BindingContext as MainPageViewModel;
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		viewModel.timer.Start();
    }
    protected override void OnDisappearing()
    {
        viewModel.timer.Stop();
        base.OnDisappearing();
    }
}

