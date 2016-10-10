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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Server.Test("Hello World");
        }
    }
}
