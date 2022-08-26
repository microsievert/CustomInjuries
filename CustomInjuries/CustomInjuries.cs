using System;
using CustomInjuries.Events;
using Exiled.API.Features;
using CustomInjuries.API.Classes;

namespace CustomInjuries
{
    public class CustomInjuries : Plugin<Config>
    {
        public static CustomInjuries Instance;
        
        public override string Name => "CustomInjuries";
        public override string Author => "microsievert";

        public override Version Version { get; } = new Version(1, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

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