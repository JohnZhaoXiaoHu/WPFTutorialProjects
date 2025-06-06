﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zhaoxi.SCADAConfigration.Base
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            OnExecute?.Invoke(parameter);
        }
        Action<object?> OnExecute;
        public Command(Action<object?> action)
        {
            OnExecute = action;
        }
    }
}
