using System;
using System.Collections.Generic;
using System.Text;

namespace ChatCustomer.Model
{
    /// <summary>
    /// Класс модели отправленного сообщения
    /// </summary>
    class Messege
    {
        /// <summary>
        /// Время отправки
        /// </summary>
        public DateTime DateTimeMessege { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string NameUser { get; set; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Messeges { get; set; }
    }
}
