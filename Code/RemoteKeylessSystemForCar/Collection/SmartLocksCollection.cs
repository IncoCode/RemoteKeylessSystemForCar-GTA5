using GTA;
using RemoteKeylessSystemForCar.Model;
using System.Collections.Generic;
using System.Linq;

namespace RemoteKeylessSystemForCar.Collection
{
    internal class SmartLocksCollection
    {
        private readonly Dictionary<int, VehicleSmartLock> _smartLocks;

        public SmartLocksCollection()
        {
            this._smartLocks = new Dictionary<int, VehicleSmartLock>();
        }

        public void Tick()
        {
            foreach ( KeyValuePair<int, VehicleSmartLock> kvp in this._smartLocks.ToList() )
            {
                var vehicle = Entity.FromHandle( kvp.Key ) as Vehicle;
                int vehicleHandle = kvp.Key;
                VehicleSmartLock smartLock = kvp.Value;

                if ( !vehicle.Exists() )
                {
                    this._smartLocks.Remove( vehicle.Handle );
                    continue;
                }

                bool isPlayerInRange = vehicle.IsInRange( Game.Player.Character.Position, 6f );
                bool isPlayerTryingToEnter = Game.Player.Character.VehicleTryingToEnter?.Handle == vehicleHandle;

                if ( smartLock.IsLocked && isPlayerTryingToEnter )
                {
                    smartLock.Unlock();
                }
                else if ( !smartLock.IsLocked && !isPlayerInRange && !isPlayerTryingToEnter )
                {
                    smartLock.Lock();
                }
            }
        }

        public void AddVehicle(Vehicle vehicle, VehicleSmartLockProperties properties)
        {
            this._smartLocks.Add( vehicle.Handle, new VehicleSmartLock( vehicle, properties ) );
        }

        public bool IsInstalled(Vehicle vehicle)
        {
            return this._smartLocks.ContainsKey( vehicle.Handle );
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            this._smartLocks.Remove( vehicle.Handle );
        }

        public VehicleSmartLock GetVehicleSmartLock(Vehicle vehicle)
        {
            return this._smartLocks[ vehicle.Handle ];
        }
    }
}
