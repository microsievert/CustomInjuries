using System;

using Exiled.API.Features;

using CustomInjuries.API.Classes;
using CustomInjuries.Events;

namespace CustomInjuries
{
    public class CustomInjuries : Plugin<Config>
    {
        public static CustomInjuries Instance;
        
        public override string Name => "CustomInjuries";
        public override string Author => "microsievert";

        public override Version Version { get; } = new (1, 1, 1);
        public override Version RequiredExiledVersion { get; } = new (6, 0, 0);

        public Data Data;

        private PlayerHandlers _playerHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            Data = new Data();
            
            RegisterEvents();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            Data = null;
            
            UnregisterEvents();
            
            base.OnDisabled();
        }
        
        // Events

        private void RegisterEvents()
        {
            _playerHandlers = new PlayerHandlers();

            Exiled.Events.Handlers.Player.Shot += _playerHandlers.OnShot;
            Exiled.Events.Handlers.Player.Hurting += _playerHandlers.OnHurting;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.Shot -= _playerHandlers.OnShot;
            Exiled.Events.Handlers.Player.Hurting -= _playerHandlers.OnHurting;

            _playerHandlers = null;
        }
    }
}