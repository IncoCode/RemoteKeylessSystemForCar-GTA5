using GTA;
using System.Windows.Forms;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteKeylessSystemForCar.Model;

namespace RemoteKeylessSystemForCar
{
    public class RemoteKeylessSystem : Script
    {
        private VehicleSmartLock vl;

        public RemoteKeylessSystem()
        {
            this.Tick += this.RemoteKeylessSystem_Tick;
            this.KeyDown += this.RemoteKeylessSystem_KeyDown;

            this.vl = new VehicleSmartLock(Game.Player.Character.LastVehicle);
        }

        private void RemoteKeylessSystem_Tick(object sender, EventArgs e)
        {
            //Vehicle lastVehicle = Game.Player.Character.LastVehicle;
            //if (!lastVehicle.IsInRangeOf(Game.Player.Character.Position, 6f))
            //{
            //    if (lastVehicle.LockStatus == VehicleLockStatus.Locked)
            //    {
            //        return;
            //    }
            //    lastVehicle.LockStatus = VehicleLockStatus.Locked;
            //    UI.ShowSubtitle("Vehicle auto locked");
            //}
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

                currentVehicle.HasAlarm = true;
                UI.ShowSubtitle("Alarm has been set up");
            }
            else if (e.KeyCode == Keys.K)
            {
                if (lastVehicle == null)
                {
                    return;
                }

                //lastVehicle.LockStatus = VehicleLockStatus.Locked;
                vl.Lock();
                UI.ShowSubtitle("Vehicle locked");
            }
            else if (e.KeyCode == Keys.O)
            {
                if (lastVehicle == null)
                {
                    return;
                }

                //lastVehicle.LockStatus = VehicleLockStatus.Unlocked;
                vl.Unlock();
                UI.ShowSubtitle("Vehicle unlocked");
            }
#endif
        }
    }
}
