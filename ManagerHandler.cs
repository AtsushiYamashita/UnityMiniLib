/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Assertions;

    /// <summary>
    /// ManagerHandler is manager of manager for singleton like operation.
    /// </summary>
    public class ManagerHandler : MonoBehaviour
    {
        /// <summary>
        /// This instance for use singleton.
        /// </summary>
        private static ManagerHandler sInstance;

        /// <summary>
        /// This dictionary include managers.
        /// </summary>
        private Dictionary<Type, IManager> mManagersDic = new Dictionary<Type, IManager>();

        /// <summary>
        /// This interface expresses manager class.
        /// </summary>
        public interface IManager
        {
            /// <summary>
            /// this process expresses initialize the instance.
            /// </summary>
            void Initialize ( );

            /// <summary>
            /// this process expresses initialize the instance.
            /// </summary>
            void Updating ( );
        }

        /// <summary>
        /// Get the type instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T> ( )
        {
            if (sInstance == null) { sInstance = new ManagerHandler(); }
            var name = GetName<T>();
            var dic = sInstance.mManagersDic;
            var type = typeof( T );
            var contain = dic.ContainsKey( type );
            Assert.IsTrue( contain, "You should make new instance of " + name );
            return ( T )dic[type];
        }

        /// <summary>
        /// this process check the type instance included.
        /// </summary>
        /// <typeparam name="T">checking type.</typeparam>
        /// <returns></returns>
        public static bool IsContain<T> ( )
        {
            if (sInstance == null) { sInstance = new ManagerHandler(); }
            var name = GetName<T>();
            var dic = sInstance.mManagersDic;
            var type = typeof( T );
            return dic.ContainsKey( type );
        }

        /// <summary>
        /// Assign the manager instance like a singleton.
        /// </summary>
        /// <typeparam name="T">Target manager type.</typeparam>
        /// <param name="obj">setting instance.</param>
        /// <returns></returns>
        public static ManagerHandler Set<T> ( T obj )
        {
            if (sInstance == null) { sInstance = new ManagerHandler(); }
            var name = GetName<T>();
            var dic = sInstance.mManagersDic;
            var type = typeof( T );
            var contain = dic.ContainsKey( type );
            var im = obj as IManager;
            Assert.IsNotNull( im, "This instance is not implemented IManager interface." );
            if (contain)
            {
                Assert.IsFalse( true, "This manager is already constructed." );
                dic[type] = im;
                return sInstance;
            }
            var value = new Pair<string, object>().Set( name, obj );
            sInstance.mManagersDic.Add( typeof( T ), im );
            return sInstance;
        }

        /// <summary>
        /// This function return type name.
        /// </summary>
        /// <typeparam name="T">Your wanted type.</typeparam>
        /// <returns>type name.</returns>
        private static string GetName<T> ( )
        {
            return ( typeof( T ) ).GetType().Name;
        }

        /// <summary>
        /// All added manager initialize in awaken.
        /// </summary>
        private void Awake ( )
        {
            sInstance = this;
            foreach (var page in mManagersDic) { page.Value.Initialize(); }
        }

        /// <summary>
        /// All added manager update.
        /// </summary>
        private void Update ( )
        {
            foreach (var page in mManagersDic) { page.Value.Updating(); }
        }
    }
}
