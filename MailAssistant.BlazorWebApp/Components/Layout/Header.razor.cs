using Microsoft.AspNetCore.Components.Routing;

namespace MailAssistant.BlazorWebApp.Components.Layout
{
    public partial class Header
    {

        private string selectedTab = "";

        protected override void OnInitialized()
        {
            Navigation.LocationChanged += OnLocationChanged;
            UpdateSelectedTab();
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            UpdateSelectedTab();
            StateHasChanged();
        }

        private void UpdateSelectedTab()
        {
            var currentPage = new Uri(Navigation.Uri).Segments.Last().Trim('/');
            if (currentPage == "") currentPage = "home";
            selectedTab = currentPage;
        }

        private void GoToHome()
        {
            selectedTab = "home";
            Navigation.NavigateTo("/");
        }
        private void GoToChatbot()
        {
            selectedTab = "Chatbot";
            Navigation.NavigateTo("/Chatbot");
        }
    }
}