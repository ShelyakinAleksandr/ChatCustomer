using System;
using System.Windows;
using System.Net;
using System.IO;
using ChatCustomer.ViewModel;
using ChatCustomer.Model;
using System.Windows.Input;
using ChatCustomer.Infrastructure.Server;

namespace ChatCustomer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InteractionServer InteractionServer = new InteractionServer();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BtnFindMessage.IsEnabled = false;
            LblDate.IsEnabled = false;
            RdBtnDateEquals.IsEnabled = false;
            RdBtnDateEquals.IsChecked = false;
            RdBtnDateRange.IsEnabled = false;
            DtPckrStart.IsEnabled = false;
            DtPckrEnd.IsEnabled = false;

        }

        private void TxtBxMessege_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && TxtBxMessege.CaretIndex == 0)
            {
                e.Handled = true;
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ChckBxFilter.IsChecked == true)
                {
                    BtnFindMessage.IsEnabled = true;
                    LblDate.IsEnabled = true;
                    RdBtnDateEquals.IsEnabled = true;
                    RdBtnDateEquals.IsChecked = true;
                    RdBtnDateRange.IsEnabled = true;
                    DtPckrStart.IsEnabled = true;
                }
                else
                {
                    BtnFindMessage.IsEnabled = false;
                    LblDate.IsEnabled = false;
                    RdBtnDateEquals.IsEnabled = false;
                    RdBtnDateEquals.IsChecked = false;
                    RdBtnDateRange.IsEnabled = false;
                    DtPckrStart.IsEnabled = false;
                    DtPckrEnd.IsEnabled = false;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void RdBtnDateEquals_Checked(object sender, RoutedEventArgs e)
        {
            DtPckrEnd.IsEnabled = false;
            DtPckrEnd.SelectedDate = null;
        }

        private void RdBtnDateRange_Checked(object sender, RoutedEventArgs e)
        {
            DtPckrEnd.IsEnabled = true;
        }
    }
}
