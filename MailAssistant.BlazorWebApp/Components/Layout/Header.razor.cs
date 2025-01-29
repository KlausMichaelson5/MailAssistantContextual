namespace MailAssistant.BlazorWebApp.Components.Layout
{
    public partial class Header
    {

        private string selectedTab = "";

        protected override void OnInitialized()
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