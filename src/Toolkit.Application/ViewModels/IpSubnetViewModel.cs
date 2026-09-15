using System;
using System.Windows.Input;
using Engineering.Core.Calculators;
using Toolkit.Application.ViewModels.Base;

namespace Toolkit.Application.ViewModels
{
    public class IpSubnetViewModel : BaseViewModel
    {
        private string _ipAddress = "192.168.1.0";
        public string IpAddress { get => _ipAddress; set => SetProperty(ref _ipAddress, value); }

        private int _cidr = 24;
        public int Cidr { get => _cidr; set => SetProperty(ref _cidr, value); }

        private IpSubnetCalculator.SubnetInfo? _result;
        public IpSubnetCalculator.SubnetInfo? Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private string _error = "";
        public string Error { get => _error; set => SetProperty(ref _error, value); }

        public ICommand CalculateCommand { get; }
        public ICommand ClearCommand { get; }

        public IpSubnetViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(Reset);
            Calculate();
        }

        private void Calculate()
        {
            try
            {
                Result = IpSubnetCalculator.Calculate(IpAddress, Cidr);
                Error = "";
            }
            catch (Exception ex) { Error = ex.Message; Result = null; }
        }

        private void Reset()
        {
            IpAddress = "192.168.1.0";
            Cidr = 24;
            Calculate();
        }
    }
}