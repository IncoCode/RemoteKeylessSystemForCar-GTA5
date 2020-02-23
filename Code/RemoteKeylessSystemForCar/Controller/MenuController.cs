using GTA;
using NativeUI;
using RemoteKeylessSystemForCar.Collection;
using RemoteKeylessSystemForCar.Model;
using System;
using System.Collections.Generic;

namespace RemoteKeylessSystemForCar.Controller
{
    internal enum OnOffItem
    {
        On,
        Off
    }

    internal class MenuController
    {
        private readonly MenuPool _menuPool;
        private UIMenu _mainMenu;
        private readonly SmartLocksCollection _locksCollection;

        public MenuController( SmartLocksCollection locksCollection )
        {
            this._menuPool = new MenuPool();
            this._locksCollection = locksCollection;
        }

        public void Tick()
        {
            this._menuPool.ProcessMenus();
        }

        private UIMenu GetPropertiesMenu( Vehicle vehicle )
        {
            var menu = new UIMenu( "SmartLock Properties", "" );
            var propertiesItems = new List<UIMenuListItem>();

            var onOffItems = Enum.GetValues( typeof( OnOffItem ) );

            //propertiesItems.Add()

            return menu;
        }

        private void CreateMenu( Vehicle vehicle )
        {
            //bool isSmartLockInstalled = this._smartLockController.IsSmartLockInstalled( vehicle );

            //var installSmartLockBtn = new UIMenuItem( "Install SmartLock" );
            //installSmartLockBtn.Activated += ( UIMenu sender, UIMenuItem selectedItem ) =>
            //{
            //    this._smartLockController.AddVehicle( vehicle, new VehicleSmartLockProperties() );
            //    GTA.UI.Notification.Show( "SmartLock installed" );
            //    // ToDo: refresh menu
            //    // Decrease money
            //};

            //var uninstallSmartLockBtn = new UIMenuItem( "Uninstall SmartLock" );
            //uninstallSmartLockBtn.Activated += ( UIMenu sender, UIMenuItem selectedItem ) =>
            //{
            //    GTA.UI.Notification.Show( "SmartLock uninstalled (DUMMY)" );
            //};

            //var smartLockPropertiesBtn = new UIMenuItem( "Lock Properties" );
            //smartLockPropertiesBtn.Activated += ( UIMenu sender, UIMenuItem selectedItem ) =>
            //{
            //    GTA.UI.Notification.Show( "Open Properties" );
            //};

            //var menu = new UIMenu( "SmartLock Menu", "" );
            //menu.Visible = false;


            //menu.AddItem( !isSmartLockInstalled ? installSmartLockBtn : uninstallSmartLockBtn );
            //if ( isSmartLockInstalled )
            //{
            //    menu.AddItem( smartLockPropertiesBtn );
            //}

            //this._mainMenu = menu;
            //this._menuPool.Add( menu );
        }

        public void ShowMenu( Vehicle vehicle )
        {
            this.CreateMenu( vehicle );
            this._mainMenu.Visible = true;
        }
    }
}
