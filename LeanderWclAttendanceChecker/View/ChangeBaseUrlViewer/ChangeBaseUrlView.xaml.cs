using LeanderWclAttendanceChecker.Model;
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

namespace LeanderWclAttendanceChecker.View.ChangeBaseUrlViewer
{
    /// <summary>
    /// Interaktionslogik für ChangeBaseUrlView.xaml
    /// </summary>
    public partial class ChangeBaseUrlView : Window
    {
        private ModelService _modelService;

        private bool _buttonClicked;
        public bool ButtonClicked
        {
            get { return _buttonClicked; }
        }

        private string _newBaseUrl;
        public string NewBaseUrl
        {
            get { return _newBaseUrl; }
        }

        public ChangeBaseUrlView(ModelService modelService)
        {
            InitializeComponent();

            _modelService = modelService;
            _buttonClicked = false;
        }

        private void ButtonChangeBaseUrl_Click(object sender, RoutedEventArgs e)
        {
            _newBaseUrl = TextBoxNewBaseUrl.Text;
            _buttonClicked = true;
            this.Close();
        }
    }
}
