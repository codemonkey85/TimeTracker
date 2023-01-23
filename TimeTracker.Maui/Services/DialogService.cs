namespace TimeTracker.MauiBlazor.Services;

public record DialogService : IDialogService
{
    private static Page MainPage => Application.Current.MainPage;

    public async Task<bool> ConfirmAsync(string title, string message, string accept, string cancel) =>
        await MainPage.DisplayAlert(title, message, accept, cancel);

    public async Task AlertAsync(string title, string message, string cancel) =>
        await MainPage.DisplayAlert(title, message, cancel);
}
