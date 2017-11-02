/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    using System;
    using System.Collections.Generic;
    using UnityEngine.Assertions;

    public class Managers
    {
        /// <summary>
        /// This instance for use singleton.
        /// </summary>
        private static Managers sInstance;

        /// <summary>
        /// This dictionary include managers.
        /// </summary>
        private Dictionary<Type, Pair<string, object>> mManagersDic = new Dictionary<Type, Pair<string, object>>();

        /// <summary>
        /// This is seal for static use.
        /// </summary>
        private Managers ( )
        {
        }

        /// <summary>
        /// Get the type instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T> ( )
        {
            if (sInstance == null) { sInstance = new Managers(); }
            var name = GetName<T>();
            var dic = sInstance.mManagersDic;
            var type = typeof( T );
            var contain = dic.ContainsKey( type );
            Assert.IsTrue( contain, "You should make new instance of " + name );
            return ( T )dic[type].GetSecond();
        }

        /// <summary>
        /// this process check the type instance included.
        /// </summary>
        /// <typeparam name="T">checking type.</typeparam>
        /// <returns></returns>
        public static bool IsContain<T> ( )
        {
            if (sInstance == null) { sInstance = new Managers(); }
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
        public static Managers Set<T> ( T obj )
        {
            if (sInstance == null) { sInstance = new Managers(); }
            var name = GetName<T>();
            var dic = sInstance.mManagersDic;
            var type = typeof( T );
            var contain = dic.ContainsKey( type );
            if (contain)
            {
                Assert.IsFalse( true, "This manager is second constructed instance." );
                dic[type].Set( name, obj );
                return sInstance;
            }
            var value = new Pair<string, object>().Set( name, obj );
            sInstance.mManagersDic.Add( typeof( T ), value );
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
    }
}
