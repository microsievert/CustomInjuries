using System;
using CustomInjuries.Events;
using Exiled.API.Features;

namespace CustomInjuries
{
    public class CustomInjuries : Plugin<Config>
    {
        public static CustomInjuries Instance;
        
        public override string Name => "CustomInjuries";
        public override string Author => "microsievert";

        public override Version Version { get; } = new Version(1, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

        private PlayerHandlers _playerHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            
            RegisterEvents();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            
            UnregisterEvents();
            
            base.OnDisabled();
        }
        
        // Events

        private void RegisterEvents()
        {
            _playerHandlers = new PlayerHandlers();

            Exiled.Events.Handlers.Player.Shot += _playerHandlers.OnShot;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.Shot -= _playerHandlers.OnShot;

            _playerHandlers = null;
        }
    }
}