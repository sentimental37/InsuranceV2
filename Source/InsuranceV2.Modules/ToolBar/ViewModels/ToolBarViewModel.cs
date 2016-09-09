﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using InsuranceV2.Application.Services;
using InsuranceV2.Common.Events;
using InsuranceV2.Common.MVVM;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using InsuranceV2.Common.Logging;

namespace InsuranceV2.Modules.ToolBar.ViewModels
{
    public class ToolBarViewModel : BindableBase, INotifyPropertyChanged
    {
        private readonly ILogger<ToolBarViewModel> _logger;
        private readonly INavigationAppService _navigationAppService;
        private readonly IEventAggregator _eventAggregator;

        private string _addCommandParameter;

        private Visibility _toolBarVisibility;
        private Visibility _informationButtonVisibility;
        private Visibility _exportButtonVisibility;
        private Visibility _previousButtonVisibility;
        private Visibility _homeButtonVisibility;
        private Visibility _nextButtonVisibility;
        private Visibility _trashButtonVisibility;
        private Visibility _editButtonVisibility;
        private Visibility _addButtonVisibility;

        public ToolBarViewModel(
            ILogger<ToolBarViewModel> logger, 
            INavigationAppService navigationAppService, 
            IEventAggregator eventAggregator)
        {
            _logger = logger;
            _logger.Debug("Created ToolBarViewModel!");

            _navigationAppService = navigationAppService;
            _eventAggregator = eventAggregator;

            NavigateCommand = new DelegateCommand<string>(NavigateExecute);
            NavigateToNextCommand = new DelegateCommand(NavigateNextExecute);
            NavigateToPreviousCommand = new DelegateCommand(NavigatePreviousExecute);
            ExportCommand = new DelegateCommand(ExportExecute);

            _addCommandParameter = ContentNames.InsureeAddOrEditView;

            _toolBarVisibility = Visibility.Collapsed;
            _informationButtonVisibility = Visibility.Collapsed;
            _exportButtonVisibility = Visibility.Collapsed;
            _previousButtonVisibility = Visibility.Collapsed;
            _homeButtonVisibility = Visibility.Collapsed;
            _nextButtonVisibility = Visibility.Collapsed;
            _trashButtonVisibility = Visibility.Collapsed;
            _editButtonVisibility = Visibility.Collapsed;
            _addButtonVisibility = Visibility.Collapsed;

            SubscribeEvents();
        }

        #region Execution

        private void NavigateExecute(string uri)
        {
            _navigationAppService.NavigateTo(uri);
        }

        private void NavigatePreviousExecute()
        {
            _navigationAppService.GoBackward();
        }

        private void NavigateNextExecute()
        {
            _navigationAppService.GoForward();
        }

        private void ExportExecute()
        {
            _logger.Error("Export is not implemented yet!");
        }

        #endregion

        #region Commands

        public ICommand NavigateCommand { get; }

        public ICommand NavigateToNextCommand { get; }

        public ICommand NavigateToPreviousCommand { get; }

        public ICommand ExportCommand { get; }

        public string AddCommandParameter
        {
            get { return _addCommandParameter; }
            set { _addCommandParameter = value; }
        }

        #endregion

        #region Visibility

        public Visibility ToolBarVisibility
        {
            get { return _toolBarVisibility; }
            set
            {
                _toolBarVisibility = value;
                RaisePropertyChanged("ToolBarVisibility");
            }
        }

        public Visibility InformationButtonVisibility
        {
            get { return _informationButtonVisibility; }
            set
            {
                _informationButtonVisibility = value;
                RaisePropertyChanged("InformationButtonVisibility");
            }
        }

        public Visibility ExportButtonVisibility
        {
            get { return _exportButtonVisibility; }
            set
            {
                _exportButtonVisibility = value;
                RaisePropertyChanged("ExportButtonVisibility");
            }
        }

        public Visibility PreviousButtonVisibility
        {
            get { return _previousButtonVisibility; }
            set
            {
                _previousButtonVisibility = value;
                RaisePropertyChanged("PreviousButtonVisibility");
            }
        }

        public Visibility HomeButtonVisibility
        {
            get { return _homeButtonVisibility; }
            set
            {
                _homeButtonVisibility = value;
                RaisePropertyChanged("HomeButtonVisibility");
            }
        }

        public Visibility NextButtonVisibility
        {
            get { return _nextButtonVisibility; }
            set
            {
                _nextButtonVisibility = value;
                RaisePropertyChanged("NextButtonVisibility");
            }
        }

        public Visibility TrashButtonVisibility
        {
            get { return _trashButtonVisibility; }
            set
            {
                _trashButtonVisibility = value;
                RaisePropertyChanged("TrashButtonVisibility");
            }
        }

        public Visibility EditButtonVisibility
        {
            get { return _editButtonVisibility; }
            set
            {
                _editButtonVisibility = value;
                RaisePropertyChanged("EditButtonVisibility");
            }
        }

        public Visibility AddButtonVisibility
        {
            get { return _addButtonVisibility; }
            set
            {
                _addButtonVisibility = value;
                RaisePropertyChanged("AddButtonVisibility");
            }
        }

        #endregion

        #region Events

        public new event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<NavigationChangedEvent>().Subscribe(OnNavigationChanged, true);
            _eventAggregator.GetEvent<NavigationCanGoBackEvent>().Subscribe(OnCanGoBackChanged, true);
            _eventAggregator.GetEvent<NavigationCanGoForwardEvent>().Subscribe(OnCanGoForwardChanged, true);
        }

        private void OnNavigationChanged(string newRegion)
        {
            switch (newRegion)
            {
                case ContentNames.StartupView:
                    {
                        ToolBarVisibility = Visibility.Collapsed;
                        break;
                    }
                case ContentNames.InsureeListView:
                    {
                        ToolBarVisibility = Visibility.Visible;

                        ExportButtonVisibility = Visibility.Visible;
                        InformationButtonVisibility = Visibility.Visible;

                        HomeButtonVisibility = Visibility.Collapsed;

                        TrashButtonVisibility = Visibility.Collapsed;
                        EditButtonVisibility = Visibility.Collapsed;
                        AddButtonVisibility = Visibility.Visible;
                        
                        break;
                    }
                case ContentNames.InsureeDetailsView:
                    {
                        ToolBarVisibility = Visibility.Visible;

                        ExportButtonVisibility = Visibility.Visible;
                        InformationButtonVisibility = Visibility.Visible;

                        HomeButtonVisibility = Visibility.Visible;

                        TrashButtonVisibility = Visibility.Visible;
                        EditButtonVisibility = Visibility.Visible;
                        AddButtonVisibility = Visibility.Visible;

                        break;
                    }
                case ContentNames.InsureeAddOrEditView:
                    {
                        ToolBarVisibility = Visibility.Visible;

                        ExportButtonVisibility = Visibility.Visible;
                        InformationButtonVisibility = Visibility.Visible;

                        HomeButtonVisibility = Visibility.Visible;

                        TrashButtonVisibility = Visibility.Collapsed;
                        EditButtonVisibility = Visibility.Collapsed;
                        AddButtonVisibility = Visibility.Collapsed;

                        break;
                    }
                case ContentNames.SettingsView:
                case ContentNames.InformationView:
                    {
                        ToolBarVisibility = Visibility.Visible;

                        ExportButtonVisibility = Visibility.Collapsed;
                        InformationButtonVisibility = Visibility.Collapsed;

                        HomeButtonVisibility = Visibility.Visible;

                        TrashButtonVisibility = Visibility.Collapsed;
                        EditButtonVisibility = Visibility.Collapsed;
                        AddButtonVisibility = Visibility.Collapsed;

                        break;
                    }
                default:
                    {
                        throw new NotSupportedException(String.Format("Region {0} not supported!", newRegion));
                    }
            }
        }

        private void OnCanGoBackChanged(bool canGoBack)
        {
            if (canGoBack)
            {
                PreviousButtonVisibility = Visibility.Visible;
            }
            else
            {
                PreviousButtonVisibility = Visibility.Collapsed;
            }
        }

        private void OnCanGoForwardChanged(bool canGoForward)
        {
            if (canGoForward)
            {
                PreviousButtonVisibility = Visibility.Visible;
            }
            else
            {
                NextButtonVisibility = Visibility.Collapsed;
            }
        }

        //TODO: implement functionality to unsubscribe!
        private void UnSubscribeEvents()
        {
            _eventAggregator.GetEvent<NavigationChangedEvent>().Unsubscribe(OnNavigationChanged);
            _eventAggregator.GetEvent<NavigationCanGoBackEvent>().Unsubscribe(OnCanGoBackChanged);
            _eventAggregator.GetEvent<NavigationCanGoForwardEvent>().Unsubscribe(OnCanGoForwardChanged);
        }

        #endregion
    }
}