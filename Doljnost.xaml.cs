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
    /// Логика взаимодействия для Doljnost.xaml
    /// </summary>
    public partial class Doljnost : Window
    {
        public Doljnost()
        {
            InitializeComponent();
        }


        private string QR = "";

        DBProcedures procedure = new DBProcedures();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrDoljnost;
            dgFill(QR);
        }

        private void dgFill(string qr)
        {
            DBConnection DBConnection = new DBConnection();
            DBConnection.qrDoljnost = qr;
            DBConnection.DoljnostFill();
            dgDoljnost.ItemsSource = DBConnection.dtDoljnost.DefaultView;
            dgDoljnost.Columns[0].Visibility = Visibility.Collapsed;

        }



        private void dgDoljnost_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Naimenovanie"):
                    e.Column.Header = "Наименование";
                    break;
                case ("Oklad"):
                    e.Column.Header = "Оклад";
                    break;

            }
        }

        private void btInsert_Click(object sender, RoutedEventArgs e)
        {
            procedure.spDoljnost_Insert(tbNaimenovanie.Text, Convert.ToInt32(tbOklad.Text));
            dgFill(QR);
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            procedure.spDoljnost_Update(DBConnection.IDrecord, tbNaimenovanie.Text, Convert.ToInt32(tbOklad.Text));

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
                        procedure.spDoljnost_Delete(DBConnection.IDrecord);
                        dgFill(QR);
                        break;
                    }

            }

        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgDoljnost.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[2].ToString().Equals(tbSearch.Text))
                {
                    dgDoljnost.SelectedItem = dataRow;
                }
            }
        }

        private void dgDoljnost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView drv = dgDoljnost.SelectedCells[0].Item as DataRowView;
            if (drv != null)
            {
                DBConnection.IDrecord = Convert.ToInt32(drv[0]);
            }
        }

        private void btFilter_Click(object sender, RoutedEventArgs e)
        {
            string newQR = QR + " where [Naimenovanie] like '%" + tbSearch.Text + "%' " +
                                "or" + "[Oklad] like '%" + tbSearch.Text + "%'";
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
                dgDoljnost.FontSize = 12;
                btSearch.FontSize = 12;
                
            }
            else
            {
                //Средний размер
                if ((X >= 800 || Y >= 600) && (X < 1280 || Y < 1024))
                {
                    tbSearch.FontSize = 30;
                    btDelete.FontSize = 30;
                    dgDoljnost.FontSize = 30;
                    btSearch.FontSize = 30;
                }
                else
                {
                    //Большой размер
                    if ((X >= 1280 || Y >= 1024) && (X < 1680 || Y < 1024))
                    {
                        tbSearch.FontSize = 50;
                        btDelete.FontSize = 50;
                        dgDoljnost.FontSize = 50;
                        btSearch.FontSize = 50;


                    }
                    else
                    {
                        //FUll HD
                        if ((X >= 1680 || Y >= 1024))
                        {
                            tbSearch.FontSize = 70;
                            btDelete.FontSize = 70;
                            dgDoljnost.FontSize = 70;
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

        



    
    