using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using RecipeApp.Helpers;
using RecipeApp.Models;
using RecipeApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

namespace RecipeApp.ViewModels
{
    public class MealPageViewModel : ViewModelBase
    {
        private IApiService _apiService;
        private Meal _meal;
        private bool _dataLoaded = false;
        public DelegateCommand LinkClickedCommand { get; private set; }
        public Meal Meal
        {
            get { return _meal; }
            set { SetProperty(ref _meal, value); }
        }
        public bool DataLoaded
        {
            get { return _dataLoaded; }
            set { SetProperty(ref _dataLoaded, value); }
        }
        public MealPageViewModel(INavigationService navigationService,
            IApiService apiService)
            : base(navigationService)
        {
            Title = "Meal";
            _apiService = apiService;
            LinkClickedCommand = new DelegateCommand(async() => await Browser.OpenAsync(Meal.StrYoutube, BrowserLaunchMode.SystemPreferred));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetNavigationMode() == Prism.Navigation.NavigationMode.New)
            {
                var meal = parameters.GetValue<Meal>("selectedMeal");
                if (meal != null)
                {
                    _ = LoadMealDetail(meal);
                }
            }
        }

        private async Task LoadMealDetail(Meal meal)
        {
            UserDialogs.Instance.ShowLoading();
            var response = await _apiService.GetMealDetails(meal.IdMeal);
            if (response?.Success ?? false)
            {
                ThreadingHelpers.InvokeOnMainThread(() =>
                {
                    Meal = response.Data.Meals[0];
                    DataLoaded = true;
                });
            }
            else
            {
                UserDialogs.Instance.Alert(response?.ErrorMessage ?? Constants.Global_Error, "Error");
            }
            UserDialogs.Instance.HideLoading();
        }
    }
}
