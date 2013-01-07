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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using PrismModels;

namespace ModuleB
{
	[Export]
	public class ReceiveGridViewModel : DependencyObject, IPartImportsSatisfiedNotification, INotifyPropertyChanged
	{

		private IEventAggregator EventAggregator { get; set; }
		public IReceiveGridModel Model { get; set; }
		private SubscriptionToken SubscriptionToken { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<MyDataType> ReceiveDataTableProperty { get; set; }
		public ObservableCollection<MyDataType> ReceiveDataTable
		{
			get
			{
				return this.ReceiveDataTableProperty;
			}
			set
			{
				OnPropertyChanged("ReceiveDataTableProperty");
			}
		}

		public void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		[ImportingConstructor]
		public ReceiveGridViewModel(IReceiveGridModel model, IEventAggregator eventAggregator)
		{
			this.Model = model;
			this.EventAggregator = eventAggregator;
			this.ReceiveDataTableProperty = new ObservableCollection<MyDataType>();
		}

		public void OnImportsSatisfied()
		{
			if (this.EventAggregator == null)
				return;

			var myDataTypeEvent = this.EventAggregator.GetEvent<MyDataTypeEvent>();

			if (this.SubscriptionToken != null)
				myDataTypeEvent.Unsubscribe(this.SubscriptionToken);

			this.SubscriptionToken = myDataTypeEvent.Subscribe(OnMyDataTypeEvent, ThreadOption.UIThread, false);
		}

		private void OnMyDataTypeEvent(IEnumerable<MyDataType> myDataTypeEnumerable)
		{
			foreach (var myDataType in myDataTypeEnumerable)
				this.ReceiveDataTable.Add(myDataType);
		}
	}
}
