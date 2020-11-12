using DataSaving;
using MonopolyPreUnity;
using MonopolyPreUnity.Components.SystemRequest.HSInput;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Initialization;
using siof.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace GameInterface
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties
        string _inputText = "";
        public string InputText
        {
            get => _inputText;
            set
            {
                if (value != _inputText)
                {
                    _inputText = value;
                    RaisePropertyChanged(nameof(InputText));
                }
            }
        }
        #endregion

        #region Context
        Context _context;
        public Context Context
        {
            get => _context;
            set
            {
                if (value != _context)
                {
                    _context = value;
                    RaisePropertyChanged(nameof(Context));
                }
            }
        }
        #endregion

        #region SystemBag
        SystemsBag SysBag { get; set; }
        #endregion

        #region Commands
        public ICommand SendInputCommand { get; set; }
        public ICommand SaveOutputLog { get; set; }
        #endregion

        #region backgroundWorker
        BackgroundWorker _backgroundWorker { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            // initialize
            Context = MockContext.CreateMockDataSmallTest();
            SysBag = new SystemsBag(SystemArrays.GetAllSystems(DiContainer.CreateDiContainer(Context)));

            SendInputCommand = new RelayCommand(x =>
            {
                lock (Context)
                    Context.InputString = InputText;
                InputText = "";
            }, x => InputText.Length > 0 && Context.ContainsComponentInterface<IHSRequest>());
            SaveOutputLog = new RelayCommand(x => Context.SaveOutputLog());

            _backgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
            _backgroundWorker.DoWork += (x, y) => MonopolyEntry.RunSystemsContinuous(SysBag);
            _backgroundWorker.RunWorkerAsync();
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
