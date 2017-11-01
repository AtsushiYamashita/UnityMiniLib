/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    using UnityEngine;
    using UnityEngine.Assertions;
    using UnityEngine.UI;

    /// <summary>
    /// This component draw number sprite to component of image in myself.
    /// <author>Atsushi. Yamashita.</author>
    /// </summary>
    public class NumberSpriteDraw : MonoBehaviour
    {
        /// <summary>
        /// number sprite array.
        /// </summary>
        [SerializeField]
        private Sprite[] _numberSprites = new Sprite[12];

        /// <summary>
        /// Draw image component
        /// </summary>
        private Image _self;

        /// <summary>
        /// Draw number sprites
        /// </summary>
        /// <param name="num">draw number.</param>
        public void Draw ( int num )
        {
            Assert.IsTrue( num < _numberSprites.Length );
            _self.sprite = _numberSprites[num];
        }

        /// <summary>
        /// Initialize image component.
        /// </summary>
        private void Start ( )
        {
            _self = GetComponent<Image>();
            _self.sprite = _numberSprites[0];
        }
    }
}
