using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace ChatCustomer.Model
{
    /// <summary>
    /// Класс модели отправленного сообщения
    /// </summary>
    class Messege : INotifyPropertyChanged
    {
        private DateTime dateTimeMessege;
        private string nameUser;
        private string messegesText;

        /// <summary>
        /// Время отправки
        /// </summary>
        public DateTime DateTimeMessege
        {
            get { return dateTimeMessege; }
            set
            {
                dateTimeMessege = value;
                OnPropertyChanged("DateTimeMessege");
            }
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string NameUser
        {
            get { return nameUser; }
            set
            {
                nameUser = value;
                OnPropertyChanged("NameUser");
            }
        }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string MessegeText
        {
            get { return messegesText; }
            set 
            {
                messegesText = value;
                OnPropertyChanged("MessegesText");
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
