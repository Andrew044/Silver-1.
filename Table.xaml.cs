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

namespace SilverWPF
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : Window
    {
        public Table()
        {
            InitializeComponent();
        }

        private void btOtrbor_Click(object sender, RoutedEventArgs e)
        {
            Otbor otbor = new Otbor();
            otbor.Show();
            Close();
        }

        private void btDoljnost_Click(object sender, RoutedEventArgs e)
        {
            Doljnost doljnost = new Doljnost();
            doljnost.Show();
            Close();
        }

        private void btNomer_Click(object sender, RoutedEventArgs e)
        {
            Nomer nomer = new Nomer();
            nomer.Show();
            Close();
        }

        private void btGrafik_Click(object sender, RoutedEventArgs e)
        {
            Grafik grafik = new Grafik();
            grafik.Show();
            Close();
        }

        //Изменение размера шрифта в зависимости от разрешения экрана
        private void ChangeSize(int Y, int X)
        {
            //Малый размер
            if ((X >= 0 || Y >= 0) && (X < 800 || Y < 600))
            {
                btGrafik.FontSize = 12;
                btOtrbor.FontSize = 12;
                btDoljnost.FontSize = 12;
                btNomer.FontSize = 12;


            }
            else
            {
                //Средний размер
                if ((X >= 800 || Y >= 600) && (X < 1280 || Y < 1024))
                {
                    btGrafik.FontSize = 30;
                    btOtrbor.FontSize = 30;
                    btDoljnost.FontSize = 30;
                    btNomer.FontSize = 30;
                }
                else
                {
                    //Большой размер
                    if ((X >= 1280 || Y >= 1024) && (X < 1680 || Y < 1024))
                    {
                        btGrafik.FontSize = 60;
                        btOtrbor.FontSize = 60;
                        btDoljnost.FontSize = 60;
                        btNomer.FontSize = 60;

                    }
                    else
                    {
                        //FUll HD
                        if ((X >= 1680 || Y >= 1024))
                        {
                            btGrafik.FontSize = 80;
                            btOtrbor.FontSize = 80;
                            btDoljnost.FontSize = 80;
                            btNomer.FontSize = 80;
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

