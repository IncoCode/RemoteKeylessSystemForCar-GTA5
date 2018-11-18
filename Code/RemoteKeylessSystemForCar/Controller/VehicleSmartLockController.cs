using GTA;
using RemoteKeylessSystemForCar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteKeylessSystemForCar.Controller
{
    internal class VehicleSmartLockController
    {
        private readonly Dictionary<int, VehicleSmartLock> _smartLocks; // key - vehicle handle
        private readonly Dictionary<int, bool> _isAutolocked; // ToDo: delete?

        public VehicleSmartLockController()
        {
            this._smartLocks = new Dictionary<int, VehicleSmartLock>();
            this._isAutolocked = new Dictionary<int, bool>();
        }

        public void AddVehicle(Vehicle vehicle, VehicleSmartLockProperties properties)
        {
            var vl = new VehicleSmartLock(vehicle, properties);
            this._smartLocks.Add(vehicle.Handle, vl);
            vehicle.HasAlarm = true;
            this._isAutolocked.Add(vehicle.Handle, false);
        }

        public void Tick()
        {
            foreach (KeyValuePair<int, VehicleSmartLock> kvp in this._smartLocks.ToList())
            {
                var vehicle = new Vehicle(kvp.Key);
                VehicleSmartLock smartLock = kvp.Value;

                if (!vehicle.Exists())
                {
                    this._smartLocks.Remove(vehicle.Handle);
                    continue;
                }

                bool isPlayerInRange = vehicle.IsInRangeOf(Game.Player.Character.Position, 6f);

                if (smartLock.IsLocked && isPlayerInRange)
                {
                    smartLock.Unlock();
                }
                else if (!smartLock.IsLocked && !isPlayerInRange)
                {
                    smartLock.Lock();
                }
            }
        }

        public bool IsSmartLockInstalled(Vehicle vehicle)
        {
            return this._smartLocks.ContainsKey(vehicle.Handle);
        }
    }
}
