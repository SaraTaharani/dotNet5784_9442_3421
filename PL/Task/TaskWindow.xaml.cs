using BO;
using DO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int STATE;
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BO.Task? CurrentTask
        {
            get { return (BO.Task?)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
DependencyProperty.Register("CurrentTask", typeof(BO.Task),
typeof(TaskWindow), new PropertyMetadata(null));
        public TaskWindow(int id = 0)
        {
            InitializeComponent();
            if (id == 0)
            {
                STATE = 0;
                CurrentTask = new BO.Task() { Id = 0, Description = "", Alias = "", Status = BO.Status.All };
            }
            else
            {
                STATE = 1;
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
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (STATE != 0)
                {
                    s_bl.Task.Update(CurrentTask!);
                }
                else
                {
                    s_bl.Task.Create(CurrentTask!);
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void btnChooseEngineer_Click(object sender, RoutedEventArgs e)
        {
            EngineerListWindow dialogWindow = new EngineerListWindow(-1);
            dialogWindow.ShowDialog();
            BO.Engineer dataFromDialog = dialogWindow.DataFromDialog;
            BO.EngineerInTask selectedEngineer=new EngineerInTask() { Id=dataFromDialog.Id , Name=dataFromDialog.Name};
            CurrentTask!.Engineer = selectedEngineer;
            OnPropertyChanged("Engineer");
            // itemNameTextBox is an instance of a TextBox
            //BindingExpression be = .GetBindingExpression(TextBox.TextProperty);
            //be.UpdateSource();
        }
    }
}
