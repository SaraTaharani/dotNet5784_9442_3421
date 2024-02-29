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
using System.ComponentModel;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    { 
    public int STATE;
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

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
                CurrentTask = new BO.Task() {
                    Id = 0,
                    Description = "",
                    Alias = "",
                    DependenciesList = null,
                    CreatedAtDate = DateTime.Now,
                    Status = BO.Status.Unscheduled,
                    Milestone = null,
                    BaselineStartDate = null,
                    StartDate = null,
                    ScheduledStartDate = null,
                    ForecastDate = null,
                    DeadlineDate = null,
                    CompleteDate = null,
                    Deliverables = null,
                    Remarks = null,
                    Engineer = new BO.EngineerInTask() { Id = 0, Name=""},
                    CopmlexityLevel= 0,
                };
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
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChooseEngineer_Click(object sender, RoutedEventArgs e)
        {
            EngineerListWindow dialogWindow = new EngineerListWindow(-1);
            dialogWindow.ShowDialog();
            BO.Engineer dataFromDialog = dialogWindow.DataFromDialog;
            BO.EngineerInTask selectedEngineer=new BO.EngineerInTask() { Id=dataFromDialog.Id , Name=dataFromDialog.Name};
            CurrentTask = new BO.Task()
            {
                Id = 0,
                Description = "",
                Alias = "",
                DependenciesList = null,
                CreatedAtDate = DateTime.Now,
                Status = BO.Status.Unscheduled,
                Milestone = null,
                BaselineStartDate = null,
                StartDate = null,
                ScheduledStartDate = null,
                ForecastDate = null,
                DeadlineDate = null,
                CompleteDate = null,
                Deliverables = null,
                Remarks = null,
                Engineer = selectedEngineer,
                CopmlexityLevel = dataFromDialog.Level,
            };
            //CurrentTask!.Engineer= selectedEngineer;
            //CurrentTask!.CopmlexityLevel = dataFromDialog.Level;
           // itemNameTextBox is an instance of a TextBox
           //BindingExpression be = .GetBindingExpression(TextBox.TextProperty);
           // be.UpdateSource();
        }
    }
}
