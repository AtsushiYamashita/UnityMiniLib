/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    using System.Collections.Generic;

    /// <summary>
    /// You should use linq as almost situation.
    /// But this class functions support other situation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListUtil<T>
    {
        /// <summary>
        /// express filter function. when this function return false then remove.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public delegate bool FilterFunc ( T t );

        /// <summary>
        /// Express no return function.
        /// </summary>
        /// <param name="t"> a list element </param>
        public delegate void VoidFunc ( T t );

        /// <summary>
        /// this function is little heavy because every call use new.
        /// this function will filter the list under the function.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns>filtered list.</returns>
        public static List<T> Filter ( List<T> list, FilterFunc func )
        {
            List<T> ret = new List<T>();
            foreach (var t in list)
            {
                var match = func( t );
                if (match) { ret.Add( t ); }
            }
            return ret;
        }

        /// <summary>
        /// This wrapping a foreach in C#.
        /// </summary>
        /// <param name="list"> Target list. </param>
        /// <param name="func"> Apply function. </param>
        public static void ForEach ( List<T> list, VoidFunc func )
        {
            foreach (T t in list) func( t );
        }
    }
}
