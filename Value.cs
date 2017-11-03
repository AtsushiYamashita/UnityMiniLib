/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    using System.Collections.Generic;

    /// <summary>
    /// This class express call back class.
    /// If you set call backs,
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReflectiveValue<T> : Value<T>
    {
        /// <summary>
        /// call back process list.
        /// </summary>
        protected List<Notice> mNoticeCall;

        /// <summary>
        /// value name
        /// </summary>
        protected string mValueName;

        /// <summary>
        /// call back process delegate.
        /// </summary>
        /// <param name="name">value name</param>
        /// <param name="old">old value</param>
        /// <param name="now">new value</param>
        public delegate void Notice ( string name, T old, T now );

        /// <summary>
        /// getter.
        /// </summary>
        /// <returns></returns>
        public T Get ( )
        {
            return mValue;
        }

        /// <summary>
        /// getter value name.
        /// </summary>
        /// <returns></returns>
        public string GetName ( )
        {
            return mValueName;
        }

        /// <summary>
        /// call back function setting.
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public ReflectiveValue<T> SetCallBack ( Notice notice )
        {
            mNoticeCall.Add( notice );
            return this;
        }

        /// <summary>
        /// value name setter
        /// </summary>
        /// <param name="name">value name.</param>
        /// <returns></returns>
        public ReflectiveValue<T> SetName ( string name )
        {
            mValueName = name;
            return this;
        }

        /// <summary>
        /// value over write and notice.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public ReflectiveValue<T> Write ( T instance )
        {
            foreach (var cb in mNoticeCall) { cb( mValueName, mValue, instance ); }
            mValue = instance;
            return this;
        }
    }

    /// <summary>
    /// Use this class for primitive value wrapping.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class Value<T>
    {
        public T mValue;
    }
}
