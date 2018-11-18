using GTA;
using NativeUI;
using RemoteKeylessSystemForCar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteKeylessSystemForCar.Controller
{
    internal class MenuController
    {
        private readonly MenuPool _menuPool;
        private readonly VehicleSmartLockController _smartLockController;
        private UIMenu _mainMenu;

        public MenuController(VehicleSmartLockController smartLockController)
        {
            this._menuPool = new MenuPool();
            this._smartLockController = smartLockController;
        }

        public void Tick()
        {
            this._menuPool.ProcessMenus();
        }

        private void CreateMenu(Vehicle vehicle)
        {
            bool isSmartLockInstalled = this._smartLockController.IsSmartLockInstalled(vehicle);

            var installSmartLockBtn = new UIMenuItem("Install SmartLock");
            installSmartLockBtn.Activated += (UIMenu sender, UIMenuItem selectedItem) =>
            {
                this._smartLockController.AddVehicle(vehicle, new VehicleSmartLockProperties());
                UI.Notify("SmartLock installed");
                // ToDo: refresh menu
                // Decrease money
            };

            var uninstallSmartLockBtn = new UIMenuItem("Uninstall SmartLock");
            uninstallSmartLockBtn.Activated += (UIMenu sender, UIMenuItem selectedItem) =>
            {
                UI.Notify("SmartLock uninstalled (DUMMY)");
            };

            var smartLockPropertiesBtn = new UIMenuItem("Lock Properties");
            smartLockPropertiesBtn.Activated += (UIMenu sender, UIMenuItem selectedItem) =>
            {
                UI.Notify("Open Properties");
            };

            var menu = new UIMenu("SmartLock Menu", "");
            menu.Visible = false;


            menu.AddItem(!isSmartLockInstalled ? installSmartLockBtn : uninstallSmartLockBtn);
            if (isSmartLockInstalled)
            {
                menu.AddItem(smartLockPropertiesBtn);
            }

            this._mainMenu = menu;
            this._menuPool.Add(menu);
        }

        public void ShowMenu(Vehicle vehicle)
        {
            this.CreateMenu(vehicle);
            this._mainMenu.Visible = true;
        }
    }
}
