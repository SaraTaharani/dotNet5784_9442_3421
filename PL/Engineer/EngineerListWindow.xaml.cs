using BO;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    
    public partial class EngineerListWindow : Window
    {
        public BO.Engineer DataFromDialog { get; private set; }
        public int Id { get; set; }
        public int STATE;
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience experience { get; set; } = BO.EngineerExperience.All;
      

        public EngineerListWindow(int id=0)
        {
            Id = id;
            if (id == 0)
                STATE = 0;
            else
                STATE = 1;
            InitializeComponent();
            
            EngineerList =new ObservableCollection<BO.Engineer>(s_bl.Engineer.ReadAll()!) ;
            Activated += EngineerListWindow_Activated!;
        }

        private void EngineerListWindow_Activated(object sender, EventArgs e)
        {
            // Execute the query and update the list
            var temp = experience == BO.EngineerExperience.All ? s_bl?.Engineer.ReadAll() :
                s_bl?.Engineer.ReadAll(item => item.Level == experience);
            EngineerList = (temp == null) ? new() : new(temp!);
        }
        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }
        public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>),
        typeof(EngineerListWindow), new PropertyMetadata(null));

        private void EngineerLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                experience = (BO.EngineerExperience)comboBox.SelectedItem;
                var temp = experience == BO.EngineerExperience.All ? s_bl?.Engineer.ReadAll() :
                    s_bl?.Engineer.ReadAll(item => item.Level == experience);
                EngineerList = (temp == null) ? new() : new(temp!);
            }
        }

        private void BtnAddChoose_Click(object sender, RoutedEventArgs e)
        {
            if(STATE==0)
            {
                new EngineerWindow().ShowDialog();
                CollectionViewSource.GetDefaultView(EngineerList).Refresh();
            }
            else
            {
                DialogResult = true;
            }
            this.Close();
        }
        private void UpdateEngineer(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? engineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            BO.Engineer? engineer = s_bl.Engineer.Read(engineerInList!.Id);
            new EngineerWindow(engineer!.Id).ShowDialog();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                BO.Engineer? selectedEngineer = (sender as ListView)?.SelectedItem as BO.Engineer;
                DataFromDialog = selectedEngineer!;
        }
    }
}
