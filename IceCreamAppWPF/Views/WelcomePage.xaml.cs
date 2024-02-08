using CommunityToolkit.Mvvm.Input;
using IceCreamAppWPF.ViewModels;
using System.Windows.Controls;

namespace IceCreamAppWPF.Views
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {

        public WelcomePage(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
