using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Engineer? CurrentEngineer
        {
            get { return (BO.Engineer?)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer),
typeof(EngineerWindow), new PropertyMetadata(null));

        public EngineerWindow(int id = 0)
        {
            InitializeComponent();
            if (id == 0)
            {
                CurrentEngineer = new BO.Engineer() { Id=0, Name="",Email="" , Level=BO.EngineerExperience.All, Cost=0, Task=null};
            }
            else
            {
                try
                {
                    CurrentEngineer = s_bl.Engineer.Read(id)!;
                }
                catch(BlDoesNotExistException ex) {
                    CurrentEngineer = null;
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void EngineerLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
