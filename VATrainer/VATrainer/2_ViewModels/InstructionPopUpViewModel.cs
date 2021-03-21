﻿using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;

namespace VATrainer.ViewModels
{
    public class InstructionPopUpViewModel : BindableBase
    {
        private readonly ISettings _settings;

        public InstructionPopUpViewModel(ISettings settings)
        {
            _settings = settings;
            OkCommand = new DelegateCommand(OkCommandExecuted);
        }

        public DelegateCommand OkCommand { get; }

        public bool DontShowAgain
        {
            get => false;
            set => _settings.DisplayInstruction = !value;
        }

        private async void OkCommandExecuted()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
