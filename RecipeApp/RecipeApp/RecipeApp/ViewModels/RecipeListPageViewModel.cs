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

namespace RecipeApp.ViewModels
{
    public class RecipeListPageViewModel : ViewModelBase
    {
        private IApiService _apiService;
        private ObservableCollection<Meal> _meals;

        public DelegateCommand RefreshCommand { get; private set; }
        public DelegateCommand<Meal> MealClickedCommand { get; private set; }

        public ObservableCollection<Meal> Meals
        {
            get { return _meals; }
            set { SetProperty(ref _meals, value); }
        }
        Category selectedCategory;
        public RecipeListPageViewModel(INavigationService navigationService,
            IApiService apiService)
            : base(navigationService)
        {
            Title = "Recipe List";
            _apiService = apiService;
            Meals = new ObservableCollection<Meal>();
            RefreshCommand = new DelegateCommand(async () => await Refresh());
            MealClickedCommand = new DelegateCommand<Meal>(async (data) => await NavigateToMealDetail(data));
        }

        private async Task NavigateToMealDetail(Meal meal)
        {
            try
            {
                if (IsBusy || meal is null) return;
                IsBusy = true;
                var navParameters = new NavigationParameters
            {
                { "selectedMeal", meal }
            };
                await NavigationService.NavigateAsync("MealPage", navParameters);
                IsBusy = false;
            }
           catch (Exception ex)
            {

            }
        }

        private async Task Refresh()
        {
            if (IsBusy) return;
            IsBusy = true;
            if(selectedCategory != null)
                await LoadMeals();
            IsBusy = false;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                var category = parameters.GetValue<Category>("selectedCategory");
                if (category != null)
                {
                    selectedCategory = category;
                    _= LoadMeals();
                }
            }
        }

        private async Task LoadMeals()
        {
            UserDialogs.Instance.ShowLoading();
            Meals = new ObservableCollection<Meal>();
            var response = await _apiService.GetMeals(selectedCategory.StrCategory);
            if (response?.Success ?? false)
            {
                ThreadingHelpers.InvokeOnMainThread(() =>
                {
                    Meals = new ObservableCollection<Meal>(response.Data.Meals);
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
