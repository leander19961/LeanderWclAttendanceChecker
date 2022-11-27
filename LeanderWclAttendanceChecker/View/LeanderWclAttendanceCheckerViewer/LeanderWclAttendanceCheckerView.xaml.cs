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
using LeanderWclAttendanceChecker.IO;
using LeanderWclAttendanceChecker.Model;
using LeanderWclAttendanceChecker.Properties;
using LeanderWclAttendanceChecker.Resource;
using LeanderWclAttendanceChecker.View.ChangeBaseUrlViewer;
using LeanderWclAttendanceChecker.View.CreateNewPlayerViewer;
using LeanderWclAttendanceChecker.View.PickPlayerViewer;
using static LeanderWclAttendanceChecker.Resource.Constants;

namespace LeanderWclAttendanceChecker.View.LeanderWclAttendanceCheckerViewer
{
    /// <summary>
    /// Interaktionslogik für LeanderWclAttendanceCheckerView.xaml
    /// </summary>
    public partial class LeanderWclAttendanceCheckerView : Window
    {
        private ModelService _modelService;

        Task _backgroundWorker;

        public LeanderWclAttendanceCheckerView()
        {
            InitializeComponent();

            IOManager.CheckDirectories();

            _modelService = new ModelService(this, Settings.Default.baseUrl);

            IOManager.LoadPlayers(_modelService);

            BindListViewPlayers();
            BindListViewCharacters();
        }

        private void BindListViewCharacters()
        {
            ObservableCollection<Character> characters = _modelService.GetCharactersObservable();

            ListViewCharacters.ItemsSource = null;
            ListViewCharacters.ItemsSource = characters;
        }

        private void BindListViewPlayers()
        {
            ObservableCollection<Player> players = _modelService.GetPlayersObservable();

            ListViewPlayers.ItemsSource = null;
            ListViewPlayers.ItemsSource = players;
        }

        private void RefreshListViews()
        {
            ListViewPlayers.Items.Refresh();
            ListViewCharacters.Items.Refresh();
        }

        private void ButtonCreateNewPlayer_Click(object sender, RoutedEventArgs e)
        {
            CreateNewPlayerView createNewPlayerView = ViewManager.ShowCreateNewPlayerView(this, _modelService);

            if (createNewPlayerView.ButtonClicked)
            {
                string name = createNewPlayerView.Name;
                _modelService.CreateNewPlayer(name);
            }

            BindListViewPlayers();
            this.Activate();
        }

        private void ButtonDeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            if ((TabControlMainView.SelectedContent == ListViewPlayers))
            {
                Player player = ListViewPlayers.SelectedItem as Player;
                _modelService.DeletePlayer(player);
            }

            BindListViewPlayers();
            RefreshListViews();
            this.Activate();
        }

        private void ButtonGetWclData_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarBackgroundWorker.IsIndeterminate = true;

            DateTime? startDate = DatePickerStartTime.SelectedDate;
            DateTime? endDate = DatePickerEndTime.SelectedDate;

            _backgroundWorker = Task.Factory.StartNew(() => _modelService.GetCharacterAttendance(startDate, endDate));
            _backgroundWorker.GetAwaiter().OnCompleted(() => BackgroundWorkerCompleted());
        }

        private void ButtonCalcAttandance_Click(object sender, RoutedEventArgs e)
        {
            _modelService.GetPlayerAttendance();

            RefreshListViews();
        }

        private void ButtonBindCharacter_Click(object sender, RoutedEventArgs e)
        {
            if ((TabControlMainView.SelectedContent == ListViewCharacters))
            {
                Character character = ListViewCharacters.SelectedItem as Character;

                PickPlayerView pickPlayerView = ViewManager.ShowPickPlayerView(this, _modelService);

                if (pickPlayerView.SelectedPlayer != null && pickPlayerView.ButtonClicked)
                {
                    character.SetPlayer(pickPlayerView.SelectedPlayer);
                }
            }

            RefreshListViews();
            this.Activate();
        }

        private void ButtonUndBindCharacter_Click(object sender, RoutedEventArgs e)
        {
            if ((TabControlMainView.SelectedContent == ListViewCharacters))
            {
                Character character = ListViewCharacters.SelectedItem as Character;

                character.SetPlayer(null);
            }

            RefreshListViews();
        }

        private void ButtonDataExport_Click(object sender, RoutedEventArgs e)
        {
            ExcelExport.WriteOutPutFile(_modelService.Players);
        }


        private void ButtonChangeBaseUrl_Click(object sender, RoutedEventArgs e)
        {
            ChangeBaseUrlView changeBaseUrlView = ViewManager.ShowChangeBaseUrlView(this, _modelService);

            if (changeBaseUrlView.ButtonClicked)
            {
                _modelService.BaseUrl = changeBaseUrlView.NewBaseUrl;
            }
            this.Activate();
        }

        private void CheckBoxTime_Click(object sender, RoutedEventArgs e)
        {
            _modelService.UseStartTime = (bool) CheckBoxUseStartTime.IsChecked;
            _modelService.UseEndTime = (bool) CheckBoxUseEndTime.IsChecked;
        }

        public void BackgroundWorkerCompleted()
        {
            ProgressBarBackgroundWorker.IsIndeterminate = false;

            BindListViewPlayers();
            BindListViewCharacters();
            RefreshListViews();
        }

        private void Application_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            settings["baseUrl"] = _modelService.BaseUrl;
            settings["useStartTime"] = _modelService.UseStartTime.ToString();
            settings["useEndTime"] = _modelService.UseEndTime.ToString();

            IOManager.SavePlayers(_modelService.Players);

            SettingsIO.SaveSettings(settings);
        }
    }
}
