using LeanderWclAttendanceChecker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace LeanderWclAttendanceChecker.View.CreateNewPlayerViewer
{
    /// <summary>
    /// Interaktionslogik für CreateNewPlayerView.xaml
    /// </summary>
    public partial class CreateNewPlayerView : Window
    {
        ModelService _modelService;

        private bool _buttonClicked;

        public bool ButtonClicked
        {
            get { return _buttonClicked; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public CreateNewPlayerView(ModelService modelService)
        {
            InitializeComponent();

            _modelService = modelService;
            _buttonClicked = false;
        }

        private void ButtonCreateNewPlayer_Click(object sender, RoutedEventArgs e)
        {
            _name = TextBoxNewPlayerName.Text;
            _buttonClicked = true;
            this.Close();
        }
    }
}
