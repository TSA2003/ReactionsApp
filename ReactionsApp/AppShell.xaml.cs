﻿using Microsoft.Maui.Controls;

namespace ReactionsApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("id", "");
            Preferences.Set("username", "");
            Preferences.Set("email", "");
            Preferences.Set("token", "");
            Navigation.PushAsync(new LoginPage());
        }
    }
}
