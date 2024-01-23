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

        // Using a DependencyProperty as the backing store for CurrentCourse.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(AddUpdateEngineer), new PropertyMetadata(null));
        public EngineerWindow(int id = 0)
        {

            InitializeComponent();
            try
            {
                CurrentEngineer = (id != 0) ? s_bl.Engineer.Read(id)! : new BO.Engineer() { Id = 0, Name = "", EMail = "", Level = BO.EngineerExperience.None };
            }
            
             
          
            //BO.Engineer? CurrentEngineer =  new BO.Engineer()
            //{
            //    Id = 0,
            //    Name = "",
            //    Email = "",
            //    Level = BO.EngineerExperience.All
            //};

            if (id!=0)
            {
                try
                {
                    if (s_bl.Engineer.Read(id) == null)
                    {
                        throw new BO.BlDoesNotExistException("this engineer dosnt exist");
                    }
                    else
                        CurrentEngineer = s_bl.Engineer.Read(id)!;
                }
                catch (BlDoesNotExistException ex) {
                    MessageBox.Show(ex.Message, "error in update engineer");
                }
            }
            //else
            //{
            //  engineer =  s_bl.Engineer.Read(id);
            //}
        }
  

        private void EngineerLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

      
    }
}
