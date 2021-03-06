/// <summary>
/// Utility library.
/// <authority> ATSUSHI YAMASHITA. </authority>
/// </summary>
namespace AY_Util
{
    using System;
    using System.Linq;

    /// <summary>
    /// 列挙型を扱う場合の汎用的なライブラリ。
    /// </summary>
    /// <typeparam name="T">操作対象とする列挙型</typeparam>
    public static class EnumUtil<T>
    {
        /// <summary>
        /// 列挙型の配列上のインデックスを指定して取得する。
        /// 列挙型で要素の値が重複していると、ソートの関係で一意でない値が返る。
        /// </summary>
        /// <param name="index">配列のインデックス。</param>
        /// <returns>列挙型の要素</returns>
        public static T GetElement(int index)
        {
            return (T)ToArray().GetValue(index);
        }

        /// <summary>
        /// 文字列としてインデックスで指定された要素を返す。
        /// 列挙型で要素の値が重複していると、ソートの関係で一意でない値が返る。
        /// </summary>
        /// <param name="index">要素を指定するインデックス</param>
        /// <returns>文字列化した要素</returns>
        public static string GetString(int index)
        {
            return GetString(index);
        }

        /// <summary>
        /// 列挙型の要素を配列として取得する。これは数値でソートしたものとする。
        /// </summary>
        /// <returns>配列のインスタンス。</returns>
        public static T[] ToArray()
        {
            T[] arr = (T[])Enum.GetValues(typeof(T));
            Array.Sort(arr);
            return arr;
        }

        /// <summary>
        /// 列挙型の要素に文字列と同じ名前があればそれを返す
        /// </summary>
        /// <param name="str">列挙型からとりたい文字列</param>
        /// <returns>一致した列挙型の要素</returns>
        public static T StringTo(string str)
        {
            var arr = ToArray();
            var ret = arr.Where((elm) =>
            {
                return elm.ToString().Equals(str);
            });
            if (ret == null) { throw new Exception("This string is not match = " + str); }
            if (ret.ToArray().Length != 1) { throw new Exception("This string is not match = " + str); }
            return ret.ToArray()[0];
        }
    }
}
