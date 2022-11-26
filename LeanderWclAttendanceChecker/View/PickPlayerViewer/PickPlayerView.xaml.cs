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

namespace LeanderWclAttendanceChecker.View.PickPlayerViewer
{
    /// <summary>
    /// Interaktionslogik für PickPlayerView.xaml
    /// </summary>
    public partial class PickPlayerView : Window
    {
        private ModelService _modelService;

        private bool _buttonClicked;
        public bool ButtonClicked
        {
            get { return _buttonClicked; }
        }

        private Player _selectedPlayer;
        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
        }

        public PickPlayerView(ModelService modelService)
        {
            InitializeComponent();

            _modelService = modelService;
            _buttonClicked = false;

            ComboBoxPlayers.ItemsSource = _modelService.GetPlayersObservable();
        }

        private void ButtonBindSelectedPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxPlayers.SelectedItem != null)
            {
                _selectedPlayer = (Player)ComboBoxPlayers.SelectedItem;
                _buttonClicked = true;
                this.Close();
            }
        }
    }
}
