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
        /// Express no return function.
        /// </summary>
        /// <param name="t"> a list element </param>
        public delegate void VoidFunc ( T t );

        /// <summary>
        /// This wrapping a foreach in C#.
        /// </summary>
        /// <param name="list"> Target list. </param>
        /// <param name="func"> Apply function. </param>
        public static void ForEach ( List<T> list, VoidFunc func )
        {
            foreach (T t in list) { func( t ) };
        }
    }
}
