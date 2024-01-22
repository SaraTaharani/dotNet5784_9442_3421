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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerWindow(int id = 0)
        {
         //   int Id = 0;
            BO.Engineer? engineer=null;
            InitializeComponent();
            if (id==0)
            {
                id= s_bl.Engineer.Create(engineer!);
            }
            else
            {
              engineer=  s_bl.Engineer.Read(id);
            }
        }
        public static readonly DependencyProperty EngineerProperty =
       DependencyProperty.Register("CurrentEngineer", typeof(IEnumerable<BO.Engineer>),
       typeof(EngineerListWindow), new PropertyMetadata(null));
    }
}
