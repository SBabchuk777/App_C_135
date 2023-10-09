using UnityEngine;

namespace Models
{
    public class ControllerModel
    {
        private const string CoinsCountName = "CoinsCount";
        
        public int CoinsCount
        {
            get => PlayerPrefs.HasKey(CoinsCountName) ? PlayerPrefs.GetInt(CoinsCountName) : 0;
            set => PlayerPrefs.SetInt(CoinsCountName, value);
        }
    }
}
