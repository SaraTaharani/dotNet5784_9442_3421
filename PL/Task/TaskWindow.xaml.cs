using BO;
using PL.Engineer;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Task? CurrentTask
        {
            get { return (BO.Task?)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
DependencyProperty.Register("CurrentTask", typeof(BO.Task),
typeof(TaskWindow), new PropertyMetadata(null));
        public TaskWindow(int id=0)
        {
            InitializeComponent();
            if (id == 0)
            {
                CurrentTask = new BO.Task() { Id=0,Description="", Alias=""};
            }
            else
            {
                try
                {
                    CurrentTask = s_bl.Task.Read(id)!;
                }
                catch (BlDoesNotExistException ex)
                {
                    CurrentTask = null;
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
