using System.ComponentModel;
using ChatCustomer.Model;
using ChatCustomer.Infrastructure.Server;
using System.Runtime.CompilerServices;
using ChatCustomer.Comm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChatCustomer.ViewModel
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        InteractionServer InteractionServer = new InteractionServer();

        public ObservableCollection<Messege> AllMesseges { get; set; }

        private RelayCommand addCommand;
        private RelayCommand loadCommand;

        public ApplicationViewModel()
        {
            //Загрузка Сооющений

            AllMesseges = new ObservableCollection<Messege>();

            foreach (Messege messege in InteractionServer.LoadMesseges())
                AllMesseges.Add(messege);
        }

        public RelayCommand AddMessege
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        string mes = obj as string;

                        if (mes != "" || mes != null)
                        {
                            Messege messege = InteractionServer.SendMesseges("Пользователь",
                                                            DateTime.Now,
                                                            mes
                                                            );

                            if (messege != null)
                                AllMesseges.Add(messege);
                        }
                        
                    }));
            }
        }

        public RelayCommand LoadMessege
        {
            get
            {
                return loadCommand ??
                    (loadCommand = new RelayCommand(obj =>
                    {
                        foreach (Messege messege in InteractionServer.LoadMesseges())
                            AllMesseges.Add(messege);
                    }));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
