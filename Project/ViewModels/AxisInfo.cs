using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using APAS.Plugin.AutoTouchByMEQ.Classes;
using APAS.ServiceContract.Wcf;

namespace APAS.Plugin.AutoTouchByMEQ.ViewModels
{
    public class AxisInfo : INotifyPropertyChanged
    {
        #region Variables

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ISystemService _service;

        private int _speed = 100;
        private double _distance;

        #endregion

        #region Ctors

        public AxisInfo(ISystemService service, string lmc1, string axis1, string lmc2, string axis2,
            string ccwButtonCaption, string cwButtonCaption, bool isSwapAxis1Dir = false, bool isSwapAxis2Dir = false)
        {
            _service = service;

            this.Lmc1 = lmc1;
            this.Axis1 = axis1;
            
            this.Lmc2 = lmc2;
            this.Axis2 = axis2;

            this.CcwButtonCaption = ccwButtonCaption;
            this.CwButtonCaption = cwButtonCaption;
            this.IsSwapAxis1Direction = isSwapAxis1Dir;
            this.IsSwapAxis2Direction = isSwapAxis2Dir;
        }

        #endregion

        #region Properties

        public string Lmc1 { get; }

        public string Axis1{ get; }

        public string Lmc2{ get; }

        public string Axis2 { get; }

        public string CcwButtonCaption { get; } = "-";

        public string CwButtonCaption { get; } = "+";

        public bool IsSwapAxis1Direction { get; } = false;

        public bool IsSwapAxis2Direction { get; } = false;

        public int Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                OnPropertyChange();
            }
        }

        public double Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Methods

        void OnPropertyChange([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion

        #region Commands

        public ICommand CcwMoveCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    try
                    {
                        _service.__SSC_MoveAxis(Lmc1, Axis1, SSC_MoveMode.REL, Speed,
                            (IsSwapAxis1Direction ? 1d : -1d) * Distance);
                        _service.__SSC_MoveAxis(Lmc2, Axis2, SSC_MoveMode.REL, Speed,
                            (IsSwapAxis2Direction ? 1d : -1d) * Distance);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                });
            }
        }

        public ICommand CwMoveCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    try
                    {
                        _service.__SSC_MoveAxis(Lmc1, Axis1, SSC_MoveMode.REL, Speed,
                            (IsSwapAxis1Direction ? -1d : 1d) * Distance);
                        _service.__SSC_MoveAxis(Lmc2, Axis2, SSC_MoveMode.REL, Speed,
                            (IsSwapAxis2Direction ? -1d : 1d) * Distance);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        #endregion
    }
}
