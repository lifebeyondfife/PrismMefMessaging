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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using PrismModels;

namespace ModuleA
{
	[Export]
	public class SendGridViewModel : DependencyObject, INotifyPropertyChanged
	{
		public IEventAggregator EventAggregator { get; set; }
		public ISendGridModel Model { get; set; }
		public ICommand SubmitCommand { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<MyDataType> SendDataTableProperty { get; set; }
		public ObservableCollection<MyDataType> SendDataTable
		{
			get
			{
				return this.SendDataTableProperty;
			}
			set
			{
				OnPropertyChanged("SendDataTableProperty");
			}
		}

		public void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		[ImportingConstructor]
		public SendGridViewModel(ISendGridModel model, IEventAggregator eventAggregator)
		{
			this.Model = model;
			this.EventAggregator = eventAggregator;
			this.SubmitCommand = new Submit(this);
			this.SendDataTableProperty = new ObservableCollection<MyDataType>();

			this.SendDataTable.Add(new MyDataType { Age = 65, Name = "Genghis Khan", FavouriteTrooper = SuperTroopers.Mac });
		}
	}
}
