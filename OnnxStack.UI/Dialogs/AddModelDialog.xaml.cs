﻿using Microsoft.Extensions.Logging;
using OnnxStack.Core;
using OnnxStack.StableDiffusion.Enums;
using OnnxStack.UI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace OnnxStack.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for AddModelDialog.xaml
    /// </summary>
    public partial class AddModelDialog : Window, INotifyPropertyChanged
    {
        private readonly ILogger<AddModelDialog> _logger;

        private string _textResult;
        private string _errorMessage;
        private List<string> _invalidOptions;
        private DiffuserPipelineType _pipelineType;

        public AddModelDialog(ILogger<AddModelDialog> logger)
        {
            _logger = logger;
            WindowCloseCommand = new AsyncRelayCommand(WindowClose);
            WindowRestoreCommand = new AsyncRelayCommand(WindowRestore);
            WindowMinimizeCommand = new AsyncRelayCommand(WindowMinimize);
            WindowMaximizeCommand = new AsyncRelayCommand(WindowMaximize);
            SaveCommand = new AsyncRelayCommand(Save, CanExecuteSave);
            CancelCommand = new AsyncRelayCommand(Cancel, CanExecuteCancel);
            InitializeComponent();
            ErrorMessage = string.Empty;
        }
        public AsyncRelayCommand WindowMinimizeCommand { get; }
        public AsyncRelayCommand WindowRestoreCommand { get; }
        public AsyncRelayCommand WindowMaximizeCommand { get; }
        public AsyncRelayCommand WindowCloseCommand { get; }
        public AsyncRelayCommand SaveCommand { get; }
        public AsyncRelayCommand CancelCommand { get; }

        public DiffuserPipelineType PipelineType
        {
            get { return _pipelineType; }
            set { _pipelineType = value; NotifyPropertyChanged(); }
        }


        public string TextResult
        {
            get { return _textResult; }
            set { _textResult = value; NotifyPropertyChanged(); ErrorMessage = string.Empty; }
        }

        public List<string> InvalidOptions
        {
            get { return _invalidOptions; }
            set { _invalidOptions = value; NotifyPropertyChanged(); }
        }


        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; NotifyPropertyChanged(); }
        }


        public bool ShowDialog(string title, List<string> invalidOptions = null)
        {
            Title = title;
            InvalidOptions = invalidOptions;
            return ShowDialog() ?? false;
        }


        private Task Save()
        {
            var result = TextResult.Trim();
            if (!InvalidOptions.IsNullOrEmpty() && InvalidOptions.Contains(result))
            {
                ErrorMessage = $"{result} is an invalid option";
                return Task.CompletedTask;
            }

            _textResult = result;
            DialogResult = true;
            return Task.CompletedTask;
        }

        private bool CanExecuteSave()
        {
            var result = TextResult?.Trim() ?? string.Empty;
            return result.Length > 2 && result.Length <= 24;
        }

        private Task Cancel()
        {
            DialogResult = false;
            return Task.CompletedTask;
        }

        private bool CanExecuteCancel()
        {
            return true;
        }

        #region BaseWindow

        private Task WindowClose()
        {
            Close();
            return Task.CompletedTask;
        }

        private Task WindowRestore()
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
            return Task.CompletedTask;
        }

        private Task WindowMinimize()
        {
            WindowState = WindowState.Minimized;
            return Task.CompletedTask;
        }

        private Task WindowMaximize()
        {
            WindowState = WindowState.Maximized;
            return Task.CompletedTask;
        }

        private void OnContentRendered(object sender, EventArgs e)
        {
            InvalidateVisual();
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
