﻿using GTA;
using System;
using System.Linq;

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

        public VehicleSmartLock( Vehicle vehicle, VehicleSmartLockProperties properties )
        {
            this._vehicle = vehicle;
            this._properties = properties;
            this._isLocked = false;
        }

        private void CloseAllDoors()
        {
            foreach ( VehicleDoor door in this._vehicle.Doors )
            {
                door.Close();
            }
        }

        private void CloseAllWindows()
        {
            foreach ( var window in Enum.GetValues( typeof( VehicleWindowIndex ) ).Cast<VehicleWindowIndex>() )
            {
                this._vehicle.Windows[ window ].RollUp();
            }
        }

        private void PlaySound()
        {
            if ( this.Properties.PlaySoundOnLockUnlock )
            {
                this._vehicle.SoundHorn( 300 );
            }
        }

        private void BlinkLights()
        {
            this._vehicle.AreLightsOn = true;
            Script.Wait( 300 );
            this._vehicle.AreLightsOn = false;
        }

        public void Lock()
        {
            if ( this._isLocked )
            {
                return;
            }

            this._isLocked = true;

            this._vehicle.LockStatus = VehicleLockStatus.Locked;

            if ( this._properties.IsCloseAllDorsOnLock )
            {
                this.CloseAllDoors();
            }
            if ( this._properties.IsCloseAllWindowsOnLock )
            {
                this.CloseAllWindows();
            }
            if ( this._properties.IsCloseRoofOnLock )
            {
                this._vehicle.RoofState = VehicleRoofState.Closing;
            }

            this.PlaySound();
            this.BlinkLights();
            this._vehicle.IsEngineRunning = false;
        }

        public void Unlock( bool force = false )
        {
            if ( !this._isLocked )
            {
                return;
            }

            this._vehicle.LockStatus = VehicleLockStatus.Unlocked;
            if ( force )
            {
                return;
            }

            for ( int i = 0; i < 2; i++ )
            {
                this.PlaySound();
                this.BlinkLights();
                Script.Wait( 100 );
            }

            this._isLocked = false;
            bool isLightsOn = World.CurrentTimeOfDay.Hours >= 20 || World.CurrentTimeOfDay.Hours < 8;

            this._vehicle.AreLightsOn = isLightsOn;
            if ( !this._properties.IsEnableEngineOnUnlock )
            {
                this._vehicle.IsEngineRunning = false;
            }
        }
    }
}
