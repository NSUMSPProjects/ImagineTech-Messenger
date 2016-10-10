using MessengerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace MessengerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IMessengerService Server;
        public static DuplexChannelFactory<IMessengerService> _channelFactory;

        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IMessengerService>(
                new ClientCallBack(), "MessengerServiceEndPoint");
            Server = _channelFactory.CreateChannel();
        }

        public void TakeMessage(string message, string userName)
        {
            TextDisplayTextBox.Text += userName + ": " + message + "\n";
            TextDisplayTextBox.ScrollToEnd();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageTextBox.Text.Length == 0)
                return;

            Server.SendMessageToAll(MessageTextBox.Text, UserNameTextBox.Text);
            TakeMessage(MessageTextBox.Text, "You");
            MessageTextBox.Text = ""; // empty for the next message
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            int returnValue = Server.Login(UserNameTextBox.Text);
            if (returnValue == 1)
            {
                MessageBox.Show("You are already logged in. Try again.");
            }
            else if (returnValue == 0)
            {
                MessageBox.Show("You logged in!");
                WelcomeLabel.Content = "Welcome, " + UserNameTextBox.Text + "!";
                UserNameTextBox.IsEnabled = false;
                LoginButton.IsEnabled = false;
            }
        }
    }
}
