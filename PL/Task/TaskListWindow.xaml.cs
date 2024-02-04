using DalApi;
using PL.Engineer;
using PL.Task;
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
        public ObservableCollection<BO.Task> TaskList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }
        public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>),
        typeof(TaskListWindow), new PropertyMetadata(null));
        public TaskListWindow()
        {
            InitializeComponent();

            TaskList = new ObservableCollection<BO.Task>(s_bl.Task.ReadAll()!);
            Activated += TaskListWindow_Activated!;
        }

        private void TaskListWindow_Activated(object sender, EventArgs e)
        {
            // Execute the query and update the list
            var temp = status == BO.Status.All ? s_bl?.Task.ReadAll() :
                 s_bl?.Task.ReadAll(item => item.Status == status);
            TaskList = (temp == null) ? new() : new(temp!);
        }

        private void StatusTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                status = (BO.Status)comboBox.SelectedItem;
                var temp = status == BO.Status.All ? s_bl?.Task.ReadAll() :
                    s_bl?.Task.ReadAll(item => item.Status == status);
                TaskList = (temp == null) ? new() : new(temp!);
            }
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
            CollectionViewSource.GetDefaultView(TaskList).Refresh();
        }

        private void UpdateTask(object sender, MouseButtonEventArgs e)
        {
            BO.Task? taskInList = (sender as ListView)?.SelectedItem as BO.Task;
            BO.Task? task = s_bl.Task.Read(taskInList!.Id);
            new TaskWindow(task!.Id).ShowDialog();
        }
    }
}
