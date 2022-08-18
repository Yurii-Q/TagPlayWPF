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

namespace TagPlayWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            field = new TagPlay.RandomField();
            InitButtons();
            ActivateNeedButton();
        }

        //Fields
        private const int rang = 4;
        private MyButton[,] buttons;
        private TagPlay.RandomField field;


        //Functions
        private void InitButtons()
        {
            buttons = new MyButton[rang, rang];

            for (int i = 0; i < rang; i++)
                for (int j = 0; j < rang; j++)
                {
                    buttons[i, j] = new MyButton();
                    buttons[i, j].I = i;
                    buttons[i, j].J = j;
                    buttons[i, j].Content = field.getNumber(i,j).ToString();
                    buttons[i, j].Style = (Style)Resources["btnTag"];
                    buttons[i, j].IsEnabled = false;
                    Grid.SetColumn(buttons[i, j], j);
                    Grid.SetRow(buttons[i, j], i);
                    MainGrid.Children.Add(buttons[i, j]);
                }
            buttons[field.indexI0, field.indexJ0].Content = "";
        }

        private void ActivateNeedButton()
        {
            int i0 = field.indexI0;
            int j0 = field.indexJ0;
            if (i0 + 1 < 4)             
                buttons[i0 + 1, j0].IsEnabled = true;
            if(i0 -1 > -1)
                buttons[i0 - 1, j0].IsEnabled = true;
            if (j0 + 1 < 4)
                buttons[i0, j0 + 1].IsEnabled = true;
            if (j0 - 1 > -1)
                buttons[i0, j0 - 1].IsEnabled = true; 
        }

        private void DisableAllButton()
        {
            for (int i = 0; i < rang; i++)
                for (int j = 0; j < rang; j++)
                    buttons[i, j].IsEnabled = false;
        }

        private void DisplayField()
        {
            for (int i = 0; i < rang; i++)
                for (int j = 0; j < rang; j++)
                {
                    buttons[i, j].Content = field.getNumber(i, j).ToString();
                }
            buttons[field.indexI0, field.indexJ0].Content = "";
        }

        //Handlers
        private void btnTag_Click(object sender, RoutedEventArgs e)
        {
            var button = e.Source as MyButton;

            field.swapNumberByI(button.I);
            field.swapNumberByJ(button.J);
            DisableAllButton();
            ActivateNeedButton();
            DisplayField();
            if(field.Check())
            {
                DisableAllButton();
                MessageBox.Show("You Win!", "You Win!");
            }
            e.Handled = true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            field.Reset();
            DisableAllButton();
            ActivateNeedButton();
            DisplayField();
            e.Handled = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
            e.Handled = true;
        }

        private void r_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R)
                this.btnReset_Click(sender, e);
        }        
    }
    internal class MyButton : Button
    {
        public int I { get; set; } = -1;
        public int J { get; set; } = -1;
    }
}
