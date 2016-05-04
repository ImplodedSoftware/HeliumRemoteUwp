using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Classes;
using Uwp.SharedResources.Helpers;
using Uwp.SharedResources.Interfaces;
using Uwp.SharedResources.Types;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Uwp.SharedResources.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LettersPage : Page
    {
        private ViewParameters _parameters;
        private readonly ILetterFacadeVm _vm;

        public LettersPage()
        {
            InitializeComponent();
            _vm = SharedRepository.Instance.Repository.LetterFacadeVm;
            DataContext = _vm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _parameters = (ViewParameters) e.Parameter;
            _vm.ViewType = _parameters.ViewType;

            var tit = TranslationHelper.GetString(_parameters.ViewType.ToString());
            if (_parameters.ViewType == UwpViewTypes.LabelsByLetter)
                tit = string.Format("{0}: {1}", TranslationHelper.GetString("LabelLetters"), _parameters.Letter);
            SharedRepository.Instance.Repository.SharedApp.UpdatePageTitle(tit);
        }

        private async void LettersPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _vm.Populate(_parameters);
        }
    }
}