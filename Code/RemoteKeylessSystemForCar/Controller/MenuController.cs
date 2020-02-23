using GTA;
using NativeUI;
using RemoteKeylessSystemForCar.Collection;
using RemoteKeylessSystemForCar.Model;
using System.Collections.Generic;
using System.Linq;

namespace RemoteKeylessSystemForCar.Controller
{
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

        private bool OnOffToBool( int index )
        {
            return index == 0;
        }

        private int BoolToIndex( bool enabled )
        {
            return enabled ? 0 : 1;
        }

        private UIMenu GetPropertiesMenu( Vehicle vehicle )
        {
            VehicleSmartLock vehicleLock = this._locksCollection.GetVehicleSmartLock( vehicle );
            VehicleSmartLockProperties properties = vehicleLock.Properties;

            var menu = new UIMenu( "SmartLock Properties", "" );
            var propertiesItems = new List<UIMenuListItem>();

            var onOffItems = new[] { "On", "Off" }.Cast<dynamic>().ToList();

            var isEnableEngineOnUnlockItem = new UIMenuListItem( "Enable Engine On Unlock", onOffItems, this.BoolToIndex( properties.IsEnableEngineOnUnlock ) );
            menu.AddItem( isEnableEngineOnUnlockItem );

            var isCloseAllDorsOnLockItem = new UIMenuListItem( "Close All Doors On Lock", onOffItems, this.BoolToIndex( properties.IsCloseAllDorsOnLock ) );
            menu.AddItem( isCloseAllDorsOnLockItem );

            var isCloseAllWindowsOnLockItem = new UIMenuListItem( "Close All Windows On Lock", onOffItems, this.BoolToIndex( properties.IsCloseAllWindowsOnLock ) );
            menu.AddItem( isCloseAllWindowsOnLockItem );

            var playSoundOnLockUnlockItem = new UIMenuListItem( "Play Sound On Lock/Unlock", onOffItems, this.BoolToIndex( properties.PlaySoundOnLockUnlock ) );
            menu.AddItem( playSoundOnLockUnlockItem );

            var applyChangesItem = new UIMenuItem( "Apply Changes" );
            applyChangesItem.Activated += ( UIMenu sender, UIMenuItem selectedItem ) =>
            {

                properties.IsCloseAllDorsOnLock = this.OnOffToBool( isCloseAllDorsOnLockItem.Index );
                properties.IsCloseAllWindowsOnLock = this.OnOffToBool( isCloseAllWindowsOnLockItem.Index );
                properties.IsEnableEngineOnUnlock = this.OnOffToBool( isEnableEngineOnUnlockItem.Index );
                properties.PlaySoundOnLockUnlock = this.OnOffToBool( playSoundOnLockUnlockItem.Index );

                this._menuPool.CloseAllMenus();
            };
            menu.AddItem( applyChangesItem );

            return menu;
        }

        private void GetNewCarMenuItems( UIMenu menu, Vehicle vehicle )
        {
            var installSmartLockBtn = new UIMenuItem( "Install SmartLock" );
            installSmartLockBtn.Activated += ( UIMenu sender, UIMenuItem selectedItem ) =>
            {
                this._locksCollection.AddVehicle( vehicle, new VehicleSmartLockProperties() );
                GTA.UI.Notification.Show( "SmartLock installed" );

                this._menuPool.CloseAllMenus();
            };

            menu.AddItem( installSmartLockBtn );
        }

        private void GetExistingCarMenuItems( UIMenu menu, Vehicle vehicle )
        {
            var uninstallSmartLockBtn = new UIMenuItem( "Uninstall SmartLock" );
            uninstallSmartLockBtn.Activated += ( UIMenu sender, UIMenuItem selectedItem ) =>
            {
                this._locksCollection.RemoveVehicle( vehicle );
                GTA.UI.Notification.Show( "SmartLock uninstalled" );

                this._menuPool.CloseAllMenus();
            };

            var smartLockPropertiesBtn = new UIMenuItem( "SmartLock Properties" );

            menu.AddItem( smartLockPropertiesBtn );
            menu.AddItem( uninstallSmartLockBtn );

            var propertiesMenu = this.GetPropertiesMenu( vehicle );

            menu.BindMenuToItem( propertiesMenu, smartLockPropertiesBtn );
            menu.RefreshIndex();

            this._menuPool.Add( propertiesMenu );
        }

        private void CreateMenu( Vehicle vehicle )
        {
            bool isSmartLockInstalled = this._locksCollection.IsInstalled( vehicle );

            var menu = new UIMenu( "SmartLock Menu", "" );
            if ( isSmartLockInstalled )
            {
                this.GetExistingCarMenuItems( menu, vehicle );
            }
            else
            {
                this.GetNewCarMenuItems( menu, vehicle );
            }
            menu.Visible = false;

            this._mainMenu = menu;
            this._menuPool.Add( menu );
        }

        public void ShowMenu( Vehicle vehicle )
        {
            this.CreateMenu( vehicle );
            this._mainMenu.Visible = true;
        }
    }
}
