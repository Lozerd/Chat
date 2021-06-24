using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool active = false;
        ServiceChat.ServiceChatClient client;
        int Id;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!active)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                Id = client.Connect(tbUsername.Text, tbPassword.Text);
                tbUsername.IsEnabled = false;
                btnConnectDisconnect.Content = "Disconnect";
                active = true;
            }
        }

        void DisconnectUser()
        {
            if (active)
            {
                client.Disconnect(Id);
                client = null;
                tbUsername.IsEnabled = true;
                tbPassword.IsEnabled = true;
                btnConnectDisconnect.Content = "Connect";
                active = false;
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (active)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MessageCallback(string message)
        {
            lbChat.Items.Add(message);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMessage(tbInput.Text, Id);
                    tbInput.Text = "";
                }
            }
        }
    }
}
