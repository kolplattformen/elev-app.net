﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Skolplattformen.ElevApp.ApiInterface.Models;
using Skolplattformen.ElevApp.Data;


namespace Skolplattformen.ElevApp.ViewModels
{
    public partial class TeachersViewModel : ObservableObject
    {
        private readonly SkolplattformenService _skolplattformenService;
        [ObservableProperty] private bool isLoading;

        [ObservableProperty] private ObservableCollection<Teacher> items;

        public TeachersViewModel(SkolplattformenService skolplattformenService)
        {
            _skolplattformenService = skolplattformenService;
            items = new ObservableCollection<Teacher>();

            //Task.Run(LoadData);
        }

        private async Task LoadData()
        {
            if (IsLoading) return;
            IsLoading = true;
            var teachers = await _skolplattformenService.GetTeachersAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                items.Clear();
                foreach (var teacher in teachers.OrderBy(t => t.Lastname))
                {
                    items.Add(teacher);
                }

                NotifyScrollChangeAction();
            });
            IsLoading = false;
        }

        public Action NotifyScrollChangeAction;
        public Task OnActivated()
        {
            return LoadData();
        }
    }

}
