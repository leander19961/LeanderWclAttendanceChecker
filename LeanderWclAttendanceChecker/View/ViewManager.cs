using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LeanderWclAttendanceChecker.Model;
using LeanderWclAttendanceChecker.View.ChangeBaseUrlViewer;
using LeanderWclAttendanceChecker.View.CreateNewPlayerViewer;
using LeanderWclAttendanceChecker.View.PickPlayerViewer;

namespace LeanderWclAttendanceChecker.View
{
    public static class ViewManager
    {
        public static PickPlayerView ShowPickPlayerView(Window owner, ModelService modelService)
        {
            PickPlayerView pickPlayerView = new PickPlayerView(modelService)
            {
                Owner = owner
            };

            pickPlayerView.ShowDialog();
            pickPlayerView.Activate();

            return pickPlayerView;
        }

        public static ChangeBaseUrlView ShowChangeBaseUrlView(Window owner, ModelService modelService)
        {
            ChangeBaseUrlView changeBaseUrlView = new ChangeBaseUrlView(modelService)
            {
                Owner = owner
            };

            changeBaseUrlView.ShowDialog();
            changeBaseUrlView.Activate();

            return changeBaseUrlView;
        }

        public static CreateNewPlayerView ShowCreateNewPlayerView(Window owner, ModelService modelService)
        {
            CreateNewPlayerView createNewPlayerView = new CreateNewPlayerView(modelService)
            {
                Owner = owner
            };

            createNewPlayerView.ShowDialog();
            createNewPlayerView.Activate();

            return createNewPlayerView;
        }
    }
}
