/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    /// <summary>
    /// Pair value.
    /// </summary>
    /// <typeparam name="F">First instance type.</typeparam>
    /// <typeparam name="S">Second instance type.</typeparam>
    public class Pair<F, S>
    {
        /// <summary>
        /// first instance.
        /// </summary>
        protected F mFirst;

        /// <summary>
        /// second instance.
        /// </summary>
        protected S mSecond;

        /// <summary>
        /// this delegate express converter.
        /// </summary>
        /// <typeparam name="F2">convert to</typeparam>
        /// <typeparam name="S2">convert to</typeparam>
        /// <param name="self">instance of myself</param>
        /// <returns>process instance</returns>
        public delegate Pair<F2, S2> Converte<F2, S2> ( Pair<F, S> self );

        /// <summary>
        /// This delegate express process.
        /// This work in collection.
        /// </summary>
        /// <param name="self">instance of myself</param>
        /// <returns>processed instance</returns>
        public delegate Pair<F, S> Process ( Pair<F, S> self );

        /// <summary>
        /// This is converter.
        /// </summary>
        /// <typeparam name="F2">converted class.</typeparam>
        /// <typeparam name="S2">converted class.</typeparam>
        /// <param name="conv">convert function.</param>
        /// <returns>converted instance,</returns>
        public virtual Pair<F2, S2> Converter<F2, S2> ( Converte<F2, S2> conv )
        {
            return conv( this );
        }

        /// <summary>
        /// getter for first.
        /// </summary>
        /// <returns>first instance.</returns>
        public F GetFirst ( )
        {
            return mFirst;
        }

        /// <summary>
        /// getter for second.
        /// </summary>
        /// <returns>second instance.</returns>
        public S GetSecond ( )
        {
            return mSecond;
        }

        /// <summary>
        /// This process myself by the argument
        /// </summary>
        /// <param name="proc">process function</param>
        /// <returns>processed instance,</returns>
        public virtual Pair<F, S> Processer ( Process proc )
        {
            return proc( this );
        }

        /// <summary>
        /// setter for all.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public virtual Pair<F, S> Set ( F obj1, S obj2 )
        {
            mFirst = obj1; mSecond = obj2; return this;
        }

        /// <summary>
        /// setter for first,
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Pair<F, S> SetFirst ( F obj )
        {
            mFirst = obj; return this;
        }

        /// <summary>
        /// setter for second.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual Pair<F, S> SetSecond ( S obj )
        {
            mSecond = obj; return this;
        }
    }
}
