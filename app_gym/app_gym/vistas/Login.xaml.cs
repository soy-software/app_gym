﻿using app_gym.Helpers;
using app_gym.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = app_gym.modelos.Device;

namespace app_gym.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private ResFull _res = new ResFull();
        public Login()
        {
            InitializeComponent();
        }
        async private void Acceder(object sender, EventArgs args)
        {
            if (validarCampos())
            {
                var usuario = await _res.Get<Usuario>("login-app/" + txtEmail.Text + "/" + txtPass.Text);
                if (usuario.email != null)
                {
                    bool estado = false;
                    if (switch_recodar.IsToggled == true)
                    {
                        estado = true;
                    }

                    Settings.IsEmailIn = usuario.email;
                    Settings.IsPerfilIn = usuario.perfil;
                    Settings.IsUserdIn = usuario.name;
                    Settings.IsIdIn = usuario.id;
                    Settings.IsLoggedIn = estado;
                    Application.Current.MainPage = new NavigationPage(new Inicio());
                    await Navigation.PushAsync(new Inicio());
                    await DisplayAlert("MENSAJE", "Bienvenido " + Settings.IsEmailIn, "OK");

                }
                else
                {
                    await DisplayAlert("MENSAJE", "No existe usuario con esa información", "OK");
                }
            }
            else
            {
                await DisplayAlert("MENSAJE", "Complete información", "OK");
            }
        }

        public bool validarCampos()
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                return false;
            }
            return true;
        }

        [Obsolete]
        private async void AccederFacebook(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacebookProfilePage());
        }

        private async void resetearPassword(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri(ResFull.urlbase+"password/reset"), BrowserLaunchMode.SystemPreferred);

        }
    }
}