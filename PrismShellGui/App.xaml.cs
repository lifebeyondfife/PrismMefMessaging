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
using System.Windows;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;

namespace PrismShellGui
{
	public partial class App : Application
	{
		private IEventAggregator EventAggregator { get; set; }
		private CompositionContainer Container { get; set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			CreateEventAggregator();

			new Bootstrapper().Run();
		}

		private void CreateEventAggregator()
		{
			this.Container = new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
			this.EventAggregator = new EventAggregator();

			var compositionBatch = new CompositionBatch();
			compositionBatch.AddExportedValue(this.EventAggregator);

			this.Container.Compose(compositionBatch);
		}
	}
}
