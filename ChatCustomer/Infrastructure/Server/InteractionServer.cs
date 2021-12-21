using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using SysJson = System.Text.Json;
using System.Windows;
using ChatCustomer.Model;
using Newtonsoft.Json;

namespace ChatCustomer.Infrastructure.Server
{
    class InteractionServer
    {
        public List<Messege> LoadMesseges(bool filterDate, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                FilterMassege filterMassege = new FilterMassege();
                filterMassege.Filter = filterDate;
                filterMassege.DateStart = Convert.ToDateTime(startDate);
                filterMassege.DateEnd = Convert.ToDateTime(endDate);

                List<Messege> messeges = new List<Messege>();

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:19028/ChatUsers/GetMessage");
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

        public Messege SendMesseges(string userName, DateTime dateTimeMessege, string textMessege)
        {
            try
            {
                if (textMessege != "")
                {
                    Messege messege = new Messege();

                    messege.DateTimeMessege = dateTimeMessege;
                    messege.NameUser = userName;
                    messege.Messeges = textMessege;

                    var httpWebRequest = WebRequest.Create("http://localhost:19028/ChatUsers/SendMessege");
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
