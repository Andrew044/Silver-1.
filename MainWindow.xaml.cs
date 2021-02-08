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
using Microsoft.Win32;
using System.Management;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.IO;

namespace SilverWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKylobytes);
        bool startup = true;
        public MainWindow()
        {
            InitializeComponent();
            SystemCheck();
            switch (startup)
            { 
                 case true:
                    InitializeComponent();
                tbLogin.Clear();
                tbPassword.Clear();
                ChangeSize((int)Width, (int)Height);
                break;
                case false:
                    Close();
                break;
            }
            ChangeSize((int)Width, (int)Height);
        }
        private void SystemCheck()
        {
            int Major = Environment.OSVersion.Version.Major;
            int Minor = Environment.OSVersion.Version.Minor;
            if ((Major >= 6) && (Minor >= 0))
            {
                RegistryKey registrySQL =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                RegistryKey registryNET =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework");
                RegistryKey registryWord =
                Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\16.0\Word");
                RegistryKey registryEdge =
                Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\MicrosoftEdge");
                RegistryKey registryChrome =
                Registry.CurrentUser.OpenSubKey(@"Software\Google\Chrome");
                RegistryKey registryExcel =
                    Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Excel");

                if (registrySQL == null)
                {
                    MessageBox.Show("Программа не может быть запущенна, так как в системе отсутствует Microsoft SQL Server");
                    startup = false;
                }
                else if (registryNET == null)
                {
                    MessageBox.Show("Программа не может быть запущенна, так как в системе отсутствует .NETFramework");
                    startup = false;
                }
                //else if (registryWord == null)
                //{
                //    MessageBox.Show("Программа не может быть запущена, так как  в системе отсутствует Microsoft Word");
                //    startup = false;
                //}
                //else if (registryExcel == null)
                //{
                //    MessageBox.Show("Программа не может быть запущена, так как в системе отсутствует Microsoft Excel");
                //    startup = false;
                //}
                else if (registryEdge == null)
                {
                    MessageBox.Show("Программа не может быть запущена, так как в системе отсутствует Microsoft Edge");
                    startup = false;
                }
                else if (registryChrome == null)
                {
                    MessageBox.Show("Программа не может быть запущена, так как в системе отсутствует брауер Google Chrome");
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
                        MessageBox.Show("Не возможно подключиться к источнику данных");
                        startup = false;
                    }
                    finally
                    {
                        DBConnection.connection.Close();
                    }
                }

                RegistryKey freckey = Registry.LocalMachine;
                freckey = freckey.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0", false);
                string str = freckey.GetValue("~MHz").ToString();
                //MessageBox.Show(String.Format("Частота Мгц: {0}", str));
                if (Convert.ToInt32(str) <= 1000)
                {
                    MessageBox.Show(String.Format("Очень низкая тактовая частота процессора: {0}", str));
                    startup = false;
                }
                double free = 0;
                double a = 0;
                string Vol = "";
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo MyDriveInfo in allDrives)
                {
                    if (MyDriveInfo.IsReady == true)
                    {
                        free = MyDriveInfo.AvailableFreeSpace;
                        a = (free / 1024) / 1024;
                        Vol += MyDriveInfo.Name + ": " + a.ToString("#.##") + Environment.NewLine;
                    }
                }
                if (a < 1000)
                {
                    MessageBox.Show("Недостаточно памяти на жёстком диске");
                    startup = false;
                }
            }
            else
            {
                MessageBox.Show("Невозможно запустить приложение на данной Операционной системе.");
            }
        }
        void GetSystemInfo()
        {
            long memKb;
            GetPhysicallyInstalledSystemMemory(out memKb);
            if ((memKb / 1024 / 1024) < 2)
            {
                MessageBox.Show(String.Format("Недостаточно Оперативной  памяти: ", memKb / 1024 / 1024));
                startup = false;
            }
            if (IsConnectedToInternet() == false)
            {
                MessageBox.Show(String.Format("Отсутствует подключение к интернету"));
                startup = false;
            }


        }

        private void btEnter_Click(object sender, RoutedEventArgs e)
        {
            DBProcedures procedures = new DBProcedures();
            DBConnection.IDuser = procedures.Authorization(tbLogin.Text, tbPassword.Password);
            switch (DBConnection.IDuser)
            {
                case (0):
                    tbLogin.Clear();
                    tbPassword.Clear();
                    MessageBox.Show("Неверный логин или пароль. " +
                        "\n Введите повторно.", "Сильвер",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                default:
                    Table windowTablitsi = new Table();
                    windowTablitsi.Show();
                    Close();
                    break;
            }
        }
        //Изменение размера шрифта в зависимости от разрешения экрана
        private void ChangeSize(int Y, int X)
        {
            //Малый размер
            if ((X >= 0 || Y >= 0) && (X < 800 || Y < 600))
            {

                tbLogin.FontSize = 12;
                tbPassword.FontSize = 12;
                btEnter.FontSize = 12;

            }
            else
            {
                //Средний размер
                if ((X >= 800 || Y >= 600) && (X < 1280 || Y < 1024))
                {
                    tbLogin.FontSize = 24;
                    tbPassword.FontSize = 24;
                    btEnter.FontSize = 24;

                }
                else
                {
                    //Большой размер
                    if ((X >= 1280 || Y >= 1024) && (X < 1680 || Y < 1024))
                    {
                        tbLogin.FontSize = 50;
                        tbPassword.FontSize = 50;
                        btEnter.FontSize = 50;

                    }
                    else
                    {
                        //FUll HD
                        if ((X >= 1680 || Y >= 1024))
                        {
                            tbLogin.FontSize = 63;
                            tbPassword.FontSize = 33;
                            btEnter.FontSize = 63;

                        }
                    }
                }
            }
        }
        //Изменение размера окна
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize((int)e.NewSize.Width, (int)e.NewSize.Height);
        }
    }
}



