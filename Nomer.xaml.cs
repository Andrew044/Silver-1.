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
using word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using SilverWPF;

namespace SilverWPF
{
    /// <summary>
    /// Логика взаимодействия для Nomer.xaml
    /// </summary>
    public partial class Nomer 
    {
        public Nomer()
        {
            InitializeComponent();
        }

        private string QR = "";

        DBProcedures procedure = new DBProcedures();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrNomer;
            dgFill(QR);
            cbFill();
        }

        private void dgFill(string qr)
        {
            DBConnection DBConnection = new DBConnection();
            DBConnection.qrNomer = qr;
            DBConnection.NomerFill();
            dgNomer.ItemsSource = DBConnection.dtNomer.DefaultView;
            dgNomer.Columns[0].Visibility = Visibility.Collapsed;
            dgNomer.Columns[5].Visibility = Visibility.Collapsed;

        }

        private void cbFill()
        {
            DBConnection DBConnection = new DBConnection();
            DBConnection.OtborFill();
            cbFamiliya.ItemsSource = DBConnection.dtOtbor.DefaultView;
            cbFamiliya.SelectedValuePath = "ID_Otbor";
            cbFamiliya.DisplayMemberPath = "Familiya";

        }


        private void dgNomer_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Nom"):
                    e.Column.Header = "Номер";
                    break;
                case ("Status"):
                    e.Column.Header = "Статус";
                    break;
                case ("Klass"):
                    e.Column.Header = "Класс";
                    break;
                case ("Familiya"):
                    e.Column.Header = "Сотрудник";
                    break;

            }
        }

        private void btInsert_Click(object sender, RoutedEventArgs e)
        {
            procedure.spNomer_Insert(Convert.ToInt32(tbNom.Text), tbStatus.Text,tbKlass.Text, Convert.ToInt32(cbFamiliya.SelectedValue));
            dgFill(QR);
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            procedure.spNomer_Update(DBConnection.IDrecord, Convert.ToInt32(tbNom.Text), tbStatus.Text, tbKlass.Text, Convert.ToInt32(cbFamiliya.SelectedValue));

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
                        procedure.spNomer_Delete(DBConnection.IDrecord);
                        dgFill(QR);
                        break;
                    }

            }
        } 


private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgNomer.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[2].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[3].ToString().Equals(tbSearch.Text) ||
                    dataRow.Row.ItemArray[4].ToString().Equals(tbSearch.Text))
                {
                    dgNomer.SelectedItem = dataRow;
                }
            }
        }

        private void DgNomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView drv = dgNomer.SelectedCells[0].Item as DataRowView;
            if (drv != null)
            {
                DBConnection.IDrecord = Convert.ToInt32(drv[0]);
            }
        }

        private void BtFilter_Click(object sender, RoutedEventArgs e)
        {
            string newQR = QR + " where [Nom] like '%" + tbSearch.Text + "%' " +
                                "or" + "[Status] like '%" + tbSearch.Text + "%'" +
                                "or" + "[Klass] like '%" + tbSearch.Text + "%'" +
                                "or" + "[Familiya] like '%" + tbSearch.Text + "%'";
            dgFill(newQR);
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            dgFill(QR);
            tbSearch.Clear();
        }


        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            Table table = new Table();
            table.Show();
            Close();
            
        }
        private void ChangeSize(int X, int Y)
        {
            //Малый размер
            if ((X >= 0 || Y >= 0) && (X < 800 || Y < 600))
            {
                dgNomer.FontSize = 8;
                btInsert.FontSize = 8;
                btDelete.FontSize = 8;
                btUpdate.FontSize = 8;
                tbSearch.FontSize = 10;
                btSearch.FontSize = 8;
                btFilter.FontSize = 8;
                btCancel.FontSize = 8;
                btBack.FontSize = 8;
                cbFamiliya.FontSize = 8;
            }
            else
            {
                //Средний размер
                if ((X >= 800 || Y >= 600) && (X < 1280 || Y < 1024))
                {
                    dgNomer.FontSize = 12;
                    btInsert.FontSize = 10;
                    btDelete.FontSize = 10;
                    btUpdate.FontSize = 10;
                    tbSearch.FontSize = 12;
                    btSearch.FontSize = 10;
                    btFilter.FontSize = 10;
                    btCancel.FontSize = 10;
                    btBack.FontSize = 10;
                    cbFamiliya.FontSize = 10;
                }
                else
                {
                    //Большой размер
                    if ((X >= 1280 || Y >= 1024) && (X < 1680 || Y < 1024))
                    {
                        dgNomer.FontSize = 14;
                        btInsert.FontSize = 14;
                        btDelete.FontSize = 14;
                        btUpdate.FontSize = 14;
                        tbSearch.FontSize = 16;
                        btSearch.FontSize = 14;
                        btFilter.FontSize = 14;
                        btCancel.FontSize = 14;
                        btBack.FontSize = 14;
                        cbFamiliya.FontSize = 16;
                    }
                    else
                    {
                        //FUll HD
                        if ((X >= 1680 || Y >= 1024))
                        {
                            dgNomer.FontSize = 22;
                            btInsert.FontSize = 22;
                            btDelete.FontSize = 22;
                            btUpdate.FontSize = 22;
                            tbSearch.FontSize = 24;
                            btSearch.FontSize = 22;
                            btFilter.FontSize = 22;
                            btCancel.FontSize = 22;
                            btBack.FontSize = 22;
                            cbFamiliya.FontSize = 22;
                        }
                    }
                }
            }
        }
        //Изменение размера окна
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize((int)e.NewSize.Width, (int)e.NewSize.Height);
        }

        private void BtExport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

     

