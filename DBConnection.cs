using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Microsoft.Win32;
using System.Windows;

namespace SilverWPF
{
    public class DBConnection
    {
        public DataTable dtDoljnost = new DataTable("Doljnost");
        public DataTable dtNomer = new DataTable("Nomer");
        public DataTable dtOtbor = new DataTable("Otbor");
        public DataTable dtGrafik = new DataTable("Grafik");

        public static SqlConnection connection = new SqlConnection("Data Source = LAPTOP-V355LHT6\\MYSERVER1; " +
                                                                    " Initial Catalog = SILVER; Persist Security Info = true;" +
                                                                    " User ID = sa; Password = \"london689\"");


        public static string
            qrOtbor = "SELECT [ID_Otbor], [Familiya], [Imya],[Otchestvo], [Pasport]," +
            " [Opit], [Login],[Password],[Nazvanie],[Naimenovanie],[dbo].[Otbor].[ID_Grafik],[dbo].[Otbor].[ID_Doljnost] FROM [dbo].[Otbor] " +
            "INNER JOIN [dbo].[Grafik] ON [dbo].[Otbor].[ID_Grafik] = [dbo].[Grafik].[ID_Grafik]" +
            "INNER JOIN [dbo].[Doljnost] ON [dbo].[Otbor].[ID_Doljnost] = [dbo].[Doljnost].[ID_Doljnost]",

            qrDoljnost = "SELECT [ID_Doljnost], [Naimenovanie], " +
            "[Oklad]  FROM [dbo].[Doljnost]",

            qrNomer = "SELECT [ID_Nomer], [Nom], [Status], [Klass], [Familiya], " +
            " [dbo].[Nomer].[ID_Otbor] FROM [dbo].[Nomer]" +
            " INNER JOIN [dbo].[Otbor] ON [dbo].[Nomer].[ID_Otbor] = [dbo].[Otbor].[ID_Otbor]",

             qrGrafik = "SELECT [ID_Grafik],[Nazvanie], [Nachalo], " +
            "[Konec] FROM [dbo].[Grafik]";



        private SqlCommand command = new SqlCommand("", connection);
        public static Int32 IDrecord, IDuser;
        private void dtFill(DataTable table, string query)
        {
            command.CommandText = query;
            connection.Open();
            table.Load(command.ExecuteReader());
            connection.Close();
        }

        public void OtborFill()
        {
            dtFill(dtOtbor, qrOtbor);
        }



        public void DoljnostFill()
        {
            dtFill(dtDoljnost, qrDoljnost);
        }
      

        public void NomerFill()
        {
            dtFill(dtNomer, qrNomer);
        }
        public void GrafikFill()
        {
            dtFill(dtGrafik, qrGrafik);
        }
        bool startup = true;
        private void SystemChek()
        {
            int Major = Environment.OSVersion.Version.Major;
            int Minor = Environment.OSVersion.Version.Minor;
            if ((Major >= 6) && (Minor >= 0))
            {
                RegistryKey registrySQL =
                Registry.LocalMachine.
                OpenSubKey(@"SOFTWARE\MICROSOFT\Microsoft SQL Server");
                if (registrySQL == null)
                {
                    MessageBox.Show("Запуск системы не возможен, " +
                    "в системе отсутсвует Microsoft SQL Server ",
                    "Даниел");
                    startup = false;
                }
                else
                {
                    try
                    {
                        DBConnection.connection.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Не возможно подключиться к источнику данных", "Даниел");
                        startup = false;
                    }
                    finally
                    {
                        DBConnection.connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Данная операционная система не предназначена для запуска приложения", "Даниел");
                startup = false;
            }

            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://google.ru");
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                object p = MessageBox.Show("Нет соединения с интернетом", "Даниел");
            }
        }
    }


}