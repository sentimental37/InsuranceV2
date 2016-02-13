﻿using Prism.Modularity;
using Prism.Regions;
using System;
using InsuranceV2.Common.MVVM;
using Main.Views;

namespace Main
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof (MainView));
        }
    }
}