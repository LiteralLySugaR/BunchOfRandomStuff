using Exiled.API.Features;
using BunchOfRandomStuff.Events;
using Exiled.API.Enums;

namespace BunchOfRandomStuff
{
    public class BORSMain : Plugin<Config>
    {
        private static readonly BORSMain Singleton = new();
        private PlayerHandler PlayerHandler;
        private BORSMain()
        {
        }

        public static BORSMain Instance => Singleton;
        public override PluginPriority Priority { get; } = PluginPriority.Last;

        public override void OnEnabled()
        {
            RegisterEvents();

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnregisterEvents();

            base.OnDisabled();
        }
        private void RegisterEvents()
        {
            PlayerHandler = new PlayerHandler();

            Exiled.Events.Handlers.Player.Died += PlayerHandler.OnDied;
            Exiled.Events.Handlers.Player.Joined += PlayerHandler.OnJoined;
            Exiled.Events.Handlers.Player.Left += PlayerHandler.OnLeft;
            Exiled.Events.Handlers.Player.Spawned += PlayerHandler.OnSpawned;
            Exiled.Events.Handlers.Player.ChangingRole += PlayerHandler.OnChangingRole;
            Exiled.Events.Handlers.Player.Verified += PlayerHandler.OnVerified;
            Exiled.Events.Handlers.Player.FlippingCoin += PlayerHandler.OnFlippingCoin;
            Exiled.Events.Handlers.Player.TriggeringTesla += PlayerHandler.OnTriggeringTesla;
            Exiled.Events.Handlers.Player.EscapingPocketDimension += PlayerHandler.OnEscapingPocketDimension;

            Exiled.Events.Handlers.Scp079.GainingLevel += PlayerHandler.OnGainingLevel;
            Exiled.Events.Handlers.Scp079.GainingExperience += PlayerHandler.OnGainingExperience;

            Exiled.Events.Handlers.Server.RoundEnded += PlayerHandler.OnRoundEnded;
        }
        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.Died -= PlayerHandler.OnDied;
            Exiled.Events.Handlers.Player.Joined -= PlayerHandler.OnJoined;
            Exiled.Events.Handlers.Player.Left -= PlayerHandler.OnLeft;
            Exiled.Events.Handlers.Player.Spawned -= PlayerHandler.OnSpawned;
            Exiled.Events.Handlers.Player.ChangingRole -= PlayerHandler.OnChangingRole;
            Exiled.Events.Handlers.Player.Verified -= PlayerHandler.OnVerified;
            Exiled.Events.Handlers.Player.FlippingCoin -= PlayerHandler.OnFlippingCoin;
            Exiled.Events.Handlers.Player.TriggeringTesla -= PlayerHandler.OnTriggeringTesla;
            Exiled.Events.Handlers.Player.EscapingPocketDimension -= PlayerHandler.OnEscapingPocketDimension;

            Exiled.Events.Handlers.Scp079.GainingLevel -= PlayerHandler.OnGainingLevel;
            Exiled.Events.Handlers.Scp079.GainingExperience -= PlayerHandler.OnGainingExperience;

            Exiled.Events.Handlers.Server.RoundEnded -= PlayerHandler.OnRoundEnded;

            PlayerHandler = null;
        }
    }
}
