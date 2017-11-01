/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    /// <summary>
    /// You should use linq as almost situation.
    /// But this class functions support other situation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayUtil<T>
    {
        /// <summary>
        /// Express no return function. with index int.
        /// </summary>
        /// <param name="t"> a list element </param>
        public delegate void VoidFuncI ( int index, T t );

        /// <summary>
        /// This wrapping a foreach in C#.With count index.
        /// </summary>
        /// <param name="list"> Target list. </param>
        /// <param name="func"> Apply function. </param>
        public static void ForEachI ( T[] list, VoidFuncI func )
        {
            for (int i = 0; i < list.Length; i++) { func( i, list[i] ); }
        }
    }
}
