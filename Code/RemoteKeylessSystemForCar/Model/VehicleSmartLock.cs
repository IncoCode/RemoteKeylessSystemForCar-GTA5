using GTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteKeylessSystemForCar.Model
{
    internal class VehicleSmartLock
    {
        private readonly Vehicle _vehicle;
        private bool _isLocked;
        private bool _isAutoLockHasBeenActivated; // ToDo: delete, move to controller

        public VehicleSmartLock(Vehicle vehicle)
        {
            this._vehicle = vehicle;
            this._isLocked = false;
            this._isAutoLockHasBeenActivated = false;
        }

        public void Lock()
        {
            if (this._isLocked)
            {
                return;
            }

            this._isLocked = true;

            this._vehicle.LockStatus = VehicleLockStatus.Locked;
            this._vehicle.SoundHorn(300);
            this._vehicle.LightsOn = true;
            Script.Wait(300);
            this._vehicle.LightsOn = false;
            this._vehicle.EngineRunning = false;
        }

        public void Unlock()
        {
            if (!this._isLocked)
            {
                return;
            }

            this._vehicle.LockStatus = VehicleLockStatus.Unlocked;
            for (int i = 0; i < 2; i++)
            {
                this._vehicle.SoundHorn(300);
                this._vehicle.LightsOn = true;
                Script.Wait(300);
                this._vehicle.LightsOn = false;
                Script.Wait(100);
            }

            this._isLocked = false;
            this._vehicle.EngineRunning = false;
        }
    }
}
