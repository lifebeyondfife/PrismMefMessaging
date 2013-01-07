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
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace ModuleB
{
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[ModuleExport(typeof(ModuleB))]
	public class ModuleB : IModule
	{
		[Import]
		public IRegionManager RegionManager { get; set; }

		public void Initialize()
		{
			this.RegionManager.RegisterViewWithRegion("RegionB", typeof(ReceiveGrid));
		}
	}
}