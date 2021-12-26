using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using SysJson = System.Text.Json;
using System.Windows;
using ChatCustomer.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ChatCustomer.Infrastructure.Server
{
    /// <summary>
    /// Класс отвечающий за взаимодействие с сервером
    /// </summary>
    class InteractionServer
    {
        string urlServer = "http://localhost:19028";

        /// <summary>
        /// Загрузка сообщений с сервера
        /// </summary>
        /// <param name="filterDate"> Нужна ли сортировка по дате. true нажна, false не нужна  </param>
        /// <param name="startDate"> С какой даты нужно найти сообщение </param>
        /// <param name="endDate"> По какую дату нужно найти сообщение. При передаче null будут получены сообщения чья дата равна startDate </param>
        /// <returns></returns>
        public List<Messege> LoadMesseges(bool filterDate, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                FilterMassege filterMassege = new FilterMassege();
                filterMassege.Filter = filterDate;
                filterMassege.DateStart = Convert.ToDateTime(startDate);
                filterMassege.DateEnd = Convert.ToDateTime(endDate);

                List<Messege> messeges = new List<Messege>();

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlServer+"/ChatUsers/GetMessage");
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = SysJson.JsonSerializer.Serialize(filterMassege);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    //ответ от сервера
                    var result = streamReader.ReadToEnd();

                    //Сериализация
                    messeges = JsonConvert.DeserializeObject<List<Messege>>(result);
                }
                return messeges;

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            return null;
        }

        /// <summary>
        /// Перегружаемый метод для загрузки всех сообщений без фильтра
        /// </summary>
        /// <returns></returns>
        public List<Messege> LoadMesseges()
        {
            try
            {
                FilterMassege filterMassege = new FilterMassege();
                filterMassege.Filter = false;
                filterMassege.DateStart = new DateTime();
                filterMassege.DateEnd = new DateTime();

                List<Messege> messeges = new List<Messege>();

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlServer + "/ChatUsers/GetMessage");
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = SysJson.JsonSerializer.Serialize(filterMassege);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    //ответ от сервера
                    var result = streamReader.ReadToEnd();

                    //Сериализация
                    messeges = JsonConvert.DeserializeObject<List<Messege>>(result);
                }
                return messeges;

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            return null;
        }

        /// <summary>
        /// Отправка сообщений на сервер
        /// </summary>
        /// <param name="userName"> Имя пользователя</param>
        /// <param name="dateTimeMessege"> Текущая дата и время </param>
        /// <param name="textMessege"> Текст сообщения. Если текст сообщения пустой, сообщение не отправиться </param>
        /// <returns></returns>
        public Messege SendMesseges(string userName, DateTime dateTimeMessege, string textMessege)
        {
            try
            {
                if (textMessege != "")
                {
                    Messege messege = new Messege();

                    messege.DateTimeMessege = dateTimeMessege;
                    messege.NameUser = userName;
                    messege.MessegeText = textMessege;

                    var httpWebRequest = WebRequest.Create(urlServer+"/ChatUsers/SendMessege");
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/json";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = SysJson.JsonSerializer.Serialize(messege);

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                        return messege;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            return null;
        }
    }
}
