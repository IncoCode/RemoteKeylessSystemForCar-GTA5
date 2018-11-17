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
        private readonly VehicleSmartLockProperties _properties;
        private bool _isLocked;

        #region Fields

        public VehicleSmartLockProperties Properties
        {
            get
            {
                return this._properties;
            }
        }

        public bool IsLocked
        {
            get
            {
                return this._isLocked;
            }
        }

        #endregion

        public VehicleSmartLock(Vehicle vehicle, VehicleSmartLockProperties properties)
        {
            this._vehicle = vehicle;
            this._properties = properties;
            this._isLocked = false;
        }

        private void CloseAllDoors()
        {
            foreach (VehicleDoor door in Enum.GetValues(typeof(VehicleDoor)))
            {
                this._vehicle.CloseDoor(door, false);
            }
        }

        private void CloseAllWindows()
        {
            foreach (VehicleWindow window in Enum.GetValues(typeof(VehicleWindow)))
            {
                this._vehicle.RollUpWindow(window);
            }
        }

        public void Lock()
        {
            if (this._isLocked)
            {
                return;
            }

            this._isLocked = true;

            this._vehicle.LockStatus = VehicleLockStatus.Locked;

            if (this._properties.IsCloseAllDorsOnLock)
            {
                this.CloseAllDoors();
            }
            if (this._properties.IsCloseAllWindowsOnLock)
            {
                this.CloseAllWindows();
            }

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

            if (!this._properties.IsEnableEngineOnUnlock)
            {
                this._vehicle.EngineRunning = false;
            }
        }
    }
}
