namespace TimeTracker.MauiBlazor.Services;

public record DialogService : IDialogService
{
    public async Task<bool> ConfirmAsync(string title, string message, string accept, string cancel) =>
        await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);

    public async Task AlertAsync(string title, string message, string cancel) =>
        await Application.Current.MainPage.DisplayAlert(title, message, cancel);
}
