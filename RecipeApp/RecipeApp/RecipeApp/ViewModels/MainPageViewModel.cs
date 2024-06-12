using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using RecipeApp.Helpers;
using RecipeApp.Models;
using RecipeApp.Services;
using RecipeApp.Services.Interface;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RecipeApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IApiService _apiService;
        private ObservableCollection<Category> _categories;

        public DelegateCommand RefreshCommand { get; private set; }
        public DelegateCommand<Category> CategoryClickedCommand { get; private set; }

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        public MainPageViewModel(INavigationService navigationService,
            IApiService apiService)
            : base(navigationService)
        {
            Title = "Recipe Category";
            _apiService = apiService;
            Categories = new ObservableCollection<Category>();
            RefreshCommand = new DelegateCommand(async () => await Refresh());
            CategoryClickedCommand = new DelegateCommand<Category>(async (data) => await NavigateToRecipeList(data));
        }

        private async Task NavigateToRecipeList(Category category)
        {
            if(IsBusy || category is null) return;
            IsBusy = true;
            var navParameters = new NavigationParameters
            {
                { "selectedCategory", category }
            };
            await NavigationService.NavigateAsync("RecipeListPage", navParameters);
            IsBusy = false;
        }

        private async Task Refresh()
        {
            if (IsBusy) return;
            IsBusy = true;
            await LoadCategories();
            IsBusy = false;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if(parameters.GetNavigationMode() == NavigationMode.New)
            {
                _= LoadCategories();
            }
        }

        private async Task LoadCategories()
        {
            //UserDialogs.Instance.ShowLoading();
            Categories = new ObservableCollection<Category>();
            var response = await _apiService.GetCategories();
            if (response?.Success ?? false)
            {
                ThreadingHelpers.InvokeOnMainThread(() =>
                {
                    Categories = new ObservableCollection<Category>(response.Data.Categories);
                });
            }
            else
            {
                UserDialogs.Instance.Alert(response?.ErrorMessage ?? Constants.Global_Error, "Error");
            }
            //UserDialogs.Instance.HideLoading();
        }
    }
}
