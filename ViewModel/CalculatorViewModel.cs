using Microsoft.Xaml.Behaviors.Core;
using NoteApplication.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NoteApplication.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public NumberButtonCalcCommand NumberButtonCalcCommand { get; set; }

        public CalculatorViewModel()
        {
            NumberButtonCalcCommand = new(this);

        }

        internal void NumberClick(Button button)
        {

        }


    }
}
