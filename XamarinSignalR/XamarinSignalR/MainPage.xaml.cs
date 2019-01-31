using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRLibrary.Models;
using Xamarin.Forms;

namespace XamarinSignalR
{
    public partial class MainPage : ContentPage
    {
        HubConnection hub;

        List<MyMessage> myMessages;

        public MainPage()
        {
            InitializeComponent();

            sendButton.Clicked += SendButton_Clicked;

            myMessages = new List<MyMessage>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            hub = new HubConnectionBuilder().WithUrl("https://codebangkoksignalr.azurewebsites.net/chathub").Build();

            hub.On("ReceiveMessage", (MyMessage myMessage) => {
                Device.BeginInvokeOnMainThread(() => {
                    myMessages.Add(myMessage);
                    chatListView.ItemsSource = null;
                    chatListView.ItemsSource = myMessages;
                });
            });

             await hub.StartAsync();
        }

        async void SendButton_Clicked(object sender, EventArgs e)
        {
            var myMessage = new MyMessage
            {
                User = userEntry.Text,
                Message = messageEntry.Text
            };
            await hub.InvokeAsync("SendMessage", myMessage);
            messageEntry.Text = "";
        }

    }
}
