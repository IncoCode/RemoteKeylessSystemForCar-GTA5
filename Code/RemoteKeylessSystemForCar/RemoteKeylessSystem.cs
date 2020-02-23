using GTA;
using RemoteKeylessSystemForCar.Collection;
using RemoteKeylessSystemForCar.Controller;
using System;
using System.Windows.Forms;

namespace RemoteKeylessSystemForCar
{
    public class RemoteKeylessSystem : Script
    {
        private readonly MenuController _menuController;
        private readonly SmartLocksCollection _smartLocks = new SmartLocksCollection();

        public RemoteKeylessSystem()
        {
            this.Tick += this.RemoteKeylessSystem_Tick;
            this.KeyDown += this.RemoteKeylessSystem_KeyDown;

            this._menuController = new MenuController( this._smartLocks );
        }

        private void RemoteKeylessSystem_Tick( object sender, EventArgs e )
        {
            this._menuController.Tick();
            this._smartLocks.Tick();
            GTA.UI.Notification.Show( "Is trying to enter a locked vehicle " + Game.Player.Character.IsTryingToEnterALockedVehicle + ", Vehicle Handle " + Game.Player.Character.VehicleTryingToEnter?.Handle );
        }

        private void RemoteKeylessSystem_KeyDown( object sender, KeyEventArgs e )
        {
#if DEBUG
            var currentVehicle = Game.Player.Character.CurrentVehicle;
            var lastVehicle = Game.Player.Character.LastVehicle;

            if ( e.KeyCode == Keys.I )
            {
                if ( currentVehicle == null )
                {
                    return;
                }

                //var properties = new VehicleSmartLockProperties();
                //this._smartLockController.AddVehicle(currentVehicle, properties);

                //UI.ShowSubtitle("Alarm has been set up");
                this._menuController.ShowMenu( currentVehicle );
            }
            //else if (e.KeyCode == Keys.K)
            //{
            //if (lastVehicle == null)
            //{
            //return;
            //}

            //UI.ShowSubtitle("Vehicle locked");
            //}
            //else if (e.KeyCode == Keys.O)
            //{
            //if (lastVehicle == null)
            //{
            //return;
            //}

            //UI.ShowSubtitle("Vehicle unlocked");
            //}
            else if ( e.KeyCode == Keys.K )
            {
                if ( currentVehicle == null )
                {
                    return;
                }

                this._smartLocks.AddVehicle( currentVehicle, new Model.VehicleSmartLockProperties() { IsEnableEngineOnUnlock = true } );
            }
#endif
        }
    }
}
