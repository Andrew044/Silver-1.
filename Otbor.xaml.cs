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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using SilverWPF;

namespace SilverWPF
{
    /// <summary>
    /// Логика взаимодействия для Otbor.xaml
    /// </summary>
    public partial class Otbor : Window
    {
        public Otbor()
        {
            InitializeComponent();
        }

        private string QR = "";

        DBProcedures procedure = new DBProcedures();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrOtbor;
            dgFill(QR);
            cbFill();
        }

        private void dgFill(string qr)
        {
            DBConnection DBConnection = new DBConnection();
            DBConnection.qrOtbor = qr;
            DBConnection.OtborFill();
            dgOtbor.ItemsSource = DBConnection.dtOtbor.DefaultView;
            dgOtbor.Columns[0].Visibility = Visibility.Collapsed;
            dgOtbor.Columns[10].Visibility = Visibility.Collapsed;
            dgOtbor.Columns[11].Visibility = Visibility.Collapsed;

        }

        private void cbFill()
        {
            DBConnection DBConnection = new DBConnection();
            DBConnection.GrafikFill();
            cbNazvanie.ItemsSource = DBConnection.dtGrafik.DefaultView;
            cbNazvanie.SelectedValuePath = "ID_Grafik";
            cbNazvanie.DisplayMemberPath = "Nazvanie";

            DBConnection.DoljnostFill();
            cbNaimenovanie.ItemsSource = DBConnection.dtDoljnost.DefaultView;
            cbNaimenovanie.SelectedValuePath = "ID_Doljnost";
            cbNaimenovanie.DisplayMemberPath = "Naimenovanie";
        }


        private void dgOtbor_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Familiya"):
                    e.Column.Header = "Фамилия";
                    break;
                case ("Imya"):
                    e.Column.Header = "Имя";
                    break;
                case ("Otchestvo"):
                    e.Column.Header = "Отчество";
                    break;
                case ("Pasport"):
                    e.Column.Header = "Паспорт";
                    break;
                case ("Opit"):
                    e.Column.Header = "Опыт";
                    break;
                case ("Login"):
                    e.Column.Header = "Логин";
                    break;
                case ("Password"):
                    e.Column.Header = "Пароль";
                    break;
                case ("Nazvanie"):
                    e.Column.Header = "График";
                    break;
                case ("Naimenovanie"):
                    e.Column.Header = "Должность";
                    break;

            }
        }

        private void btInsert_Click(object sender, RoutedEventArgs e)
        {
            procedure.spOtbor_Insert(tbFamiliya.Text, tbImya.Text, tbOtchestvo.Text, tbPasport.Text, Convert.ToInt32(tbOpit.Text), tbLogin.Text, tbPassword.Text, Convert.ToInt32(cbNaimenovanie.SelectedValue), Convert.ToInt32(cbNazvanie.SelectedValue));
            dgFill(QR);
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            procedure.spOtbor_Update(DBConnection.IDrecord, tbFamiliya.Text, tbImya.Text, tbOtchestvo.Text, tbPasport.Text, Convert.ToInt32(tbOpit.Text), tbLogin.Text, tbPassword.Text, Convert.ToInt32(cbNaimenovanie.SelectedValue), Convert.ToInt32(cbNazvanie.SelectedValue));

            dgFill(QR);
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Уверены?",
                "Удалить", MessageBoxButton.YesNo,
                MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    {
                        procedure.spOtbor_Delete(DBConnection.IDrecord);
                        dgFill(QR);
                        break;
                    }

            }
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgOtbor.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[2].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[3].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[4].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[5].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[6].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[7].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[8].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[9].ToString().Equals(tbSearch.Text))
                {
                    dgOtbor.SelectedItem = dataRow;
                }
            }

        }

        private void dgOtbor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView drv = dgOtbor.SelectedCells[0].Item as DataRowView;
            if (drv != null)
            {
                DBConnection.IDrecord = Convert.ToInt32(drv[0]);
            }
        }

        private void btFilter_Click(object sender, RoutedEventArgs e)
        {
            string newQR = QR + " where [Familiya] like '%" + tbSearch.Text + "%' " +
                                "or" + "[Imya] like '%" + tbSearch.Text + "%'" +
                                "or" + "[Otchestvo] like '%" + tbSearch.Text + "%'" +
                                "or" + "[Pasport] like '%" + tbSearch.Text + "%'" +
                                "or" + "[Opit] like '%" + tbSearch.Text + "%'" +
                                "or" + "[Login] like '%" + tbSearch.Text + "%'" +
                                "or" + "[Password] like '%" + tbSearch.Text + "%'" +
                                "or" + "[Nazvanie] like '%" + tbSearch.Text + "%'" +
                                "or" + "[Naimenovanie] like '%" + tbSearch.Text + "%'";
            dgFill(newQR);
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            dgFill(QR);
            tbSearch.Clear();
        }


        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            Table table = new Table();
            table.Show();
            Close();
        }



        //Изменение размера шрифта в зависимости от разрешения экрана
        private void ChangeSize(int Y, int X)
        {
            //Малый размер
            if ((X >= 0 || Y >= 0) && (X < 800 || Y < 600))
            {

                
                tbSearch.FontSize = 12;
                btDelete.FontSize = 12;
                dgOtbor.FontSize = 12;
                btSearch.FontSize = 12;
                
            }
            else
            {
                //Средний размер
                if ((X >= 800 || Y >= 600) && (X < 1280 || Y < 1024))
                {
                    tbSearch.FontSize = 30;
                    btDelete.FontSize = 30;
                    dgOtbor.FontSize = 30;
                    btSearch.FontSize = 30;

                }
                else
                {
                    //Большой размер
                    if ((X >= 1280 || Y >= 1024) && (X < 1680 || Y < 1024))
                    {
                        tbSearch.FontSize = 50;
                        btDelete.FontSize = 50;
                        dgOtbor.FontSize = 50;
                        btSearch.FontSize = 50;


                    }
                    else
                    {
                        //FUll HD
                        if ((X >= 1680 || Y >= 1024))
                        {
                            tbSearch.FontSize = 70;
                            btDelete.FontSize = 70;
                            dgOtbor.FontSize = 70;
                            btSearch.FontSize = 70;


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


