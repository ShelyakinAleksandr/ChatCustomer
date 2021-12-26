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

           // LoadMesseges();
        }

        private void BtnSendMessege_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               Messege messege = InteractionServer.SendMesseges("Пользовател", DateTime.Now, TxtBxMessege.Text);

                if (null != messege)
                    LstBxChat.Items.Add(messege);
                else
                    TxtBxMessege.ToolTip = "Введите текст сообщения";
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void TxtBxMessege_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && TxtBxMessege.CaretIndex == 0)
            {
                e.Handled = true;
            }
        }

        void LoadMesseges()
        {
            try
            {
                foreach (Messege messege in InteractionServer.LoadMesseges(Convert.ToBoolean(ChckBxFilter.IsChecked),
                                                                            DtPckrStart.SelectedDate,
                                                                            DtPckrEnd.SelectedDate))
                    LstBxChat.Items.Add(messege);

                BtnConectionServer.IsEnabled = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
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

        private void BtnFindMessage_Click(object sender, RoutedEventArgs e)
        {
            LstBxChat.Items.Clear();

            foreach (Messege messege in InteractionServer.LoadMesseges(Convert.ToBoolean(ChckBxFilter.IsChecked),
                                                                           DtPckrStart.SelectedDate,
                                                                           DtPckrEnd.SelectedDate))
                LstBxChat.Items.Add(messege);
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

        private void BtnConectionServer_Click_1(object sender, RoutedEventArgs e)
        {
            LoadMesseges();
        }
    }
}
