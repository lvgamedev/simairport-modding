using Needs;
using System;

namespace SimAirport.Modding.Data {
    public class NeedManager {

        public static NeedManager Instance { get; private set; }


        /// <summary>
        /// Creates singleton of this manager.
        /// </summary>
        /// <param name="internalRegisterNeed">NeedManagerInternal.InternalRegisterNeed</param>
        /// <param name="internalUnregisterNeed">NeedManagerInternal.InternalUnregisterNeed</param>
        public NeedManager(Action<Type> internalRegisterNeed, Action<Type> internalUnregisterNeed) {
            if( Instance != null )
                throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

            Instance = this;

            InternalRegisterNeed = internalRegisterNeed;
            InternalUnregisterNeed = internalUnregisterNeed;
        }


        /// <summary>
        /// Register a new need to the game.
        /// </summary>
        /// <typeparam name="T">The class defining the new need.</typeparam>
        public void RegisterNeed<T>() where T : Need, new() {
            InternalRegisterNeed(typeof(T));
        }
        private readonly Action<Type> InternalRegisterNeed;


        /// <summary>
        /// Unregisters the given need.
        /// </summary>
        /// <typeparam name="T">The class defining the new need.</typeparam>
        public void UnregisterNeed<T>() where T : Need {
            InternalUnregisterNeed(typeof(T));
        }
        private readonly Action<Type> InternalUnregisterNeed;
    }
}