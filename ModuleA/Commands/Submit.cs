/*
  Copyright © Iain McDonald 2012
  
  This file is part of PrismMefMessaging.

    PrismMefMessaging is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    PrismMefMessaging is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with PrismMefMessaging.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Linq;
using System.Windows.Input;
using PrismModels;

namespace ModuleA
{
	public class Submit : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private SendGridViewModel ViewModel { get; set; }

		public Submit(SendGridViewModel viewModel)
		{
			this.ViewModel = viewModel;
		}

		public bool CanExecute(object parameter)
		{
			return this.ViewModel.SendDataTable.Any();
		}

		public void Execute(object parameter)
		{
			var copiedRows = this.ViewModel.SendDataTable.Select(mdt => (MyDataType) mdt.Clone());

			this.ViewModel.EventAggregator.GetEvent<MyDataTypeEvent>().Publish(copiedRows);
		}
	}
}
