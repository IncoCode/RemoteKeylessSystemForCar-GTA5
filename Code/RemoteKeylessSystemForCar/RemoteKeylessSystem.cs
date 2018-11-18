using GTA;
using System.Windows.Forms;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteKeylessSystemForCar.Model;
using RemoteKeylessSystemForCar.Controller;

namespace RemoteKeylessSystemForCar
{
    public class RemoteKeylessSystem : Script
    {
        private readonly VehicleSmartLockController _smartLockController;
        private readonly MenuController _menuController;

        public RemoteKeylessSystem()
        {
            this.Tick += this.RemoteKeylessSystem_Tick;
            this.KeyDown += this.RemoteKeylessSystem_KeyDown;

            this._smartLockController = new VehicleSmartLockController();
            this._menuController = new MenuController(this._smartLockController);
        }

        private void RemoteKeylessSystem_Tick(object sender, EventArgs e)
        {
            this._smartLockController.Tick();
            this._menuController.Tick();
        }

        private void RemoteKeylessSystem_KeyDown(object sender, KeyEventArgs e)
        {
#if DEBUG
            var currentVehicle = Game.Player.Character.CurrentVehicle;
            var lastVehicle = Game.Player.Character.LastVehicle;

            if (e.KeyCode == Keys.I)
            {
                if (currentVehicle == null)
                {
                    return;
                }

                //var properties = new VehicleSmartLockProperties();
                //this._smartLockController.AddVehicle(currentVehicle, properties);

                //UI.ShowSubtitle("Alarm has been set up");
                this._menuController.ShowMenu(currentVehicle);
            }
            else if (e.KeyCode == Keys.K)
            {
                if (lastVehicle == null)
                {
                    return;
                }

                UI.ShowSubtitle("Vehicle locked");
            }
            else if (e.KeyCode == Keys.O)
            {
                if (lastVehicle == null)
                {
                    return;
                }

                UI.ShowSubtitle("Vehicle unlocked");
            }
#endif
        }
    }
}
