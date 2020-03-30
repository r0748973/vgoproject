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
using System.Windows.Threading;

namespace ChronometerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            this.Chronometer = new Chronometer();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(250);
            timer.Tick += (o, s) => Chronometer.Tick();
            timer.Start();
        }

        public Chronometer Chronometer { get; }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            Chronometer.Start();
        }

        private void OnPause(object sender, RoutedEventArgs e)
        {
            Chronometer.Pause();
        }
    }
}
