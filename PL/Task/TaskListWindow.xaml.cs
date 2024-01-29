using DalApi;
using PL.Task;
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
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    /// 
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status status { get; set; } = BO.Status.All;
        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }
        public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>),
        typeof(TaskListWindow), new PropertyMetadata(null));
        public TaskListWindow()
        {
            InitializeComponent();
            TaskList = from BO.Task boTask in s_bl.Task.ReadAll()
                       select new BO.TaskInList()
                       {
                           Id = boTask.Id,
                           Description = boTask.Description!,
                           Alias = boTask.Alias!,
                           Status = boTask.Status
                       };
        }

        private void StatusTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (status == BO.Status.All) ?
              from BO.Task boTask in s_bl.Task.ReadAll()
              select new BO.TaskInList()
              {
                  Id = boTask.Id,
                  Description = boTask.Description!,
                  Alias = boTask.Alias!,
                  Status = boTask.Status
              }!
              :
              from BO.Task boTask in s_bl.Task.ReadAll(item=>item.Status==status)
              select new BO.TaskInList()
              {
                  Id = boTask.Id,
                  Description = boTask.Description!,
                  Alias = boTask.Alias!,
                  Status = boTask.Status
              };
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
        }

        private void UpdateTask(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? taskInList = (sender as ListView)?.SelectedItem as BO.TaskInList;
            BO.Task? task= s_bl.Task.Read(taskInList!.Id);
            new TaskWindow(task!.Id).ShowDialog();
        }
    }
}
