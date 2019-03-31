using UnityEngine;
using UnityEngine.UI;

namespace Companions
{
    public class Companion : MonoBehaviour
    {
        public Game.Companions.Companion gameCompanion;
        public string description;

        private bool chosen;
        private Text nameText;

        private CompanionsManager companionsManager;
        // Use this for initialization
        void Start()
        {
            nameText = transform.Find("CompanionNameText").GetComponent<Text>();
            companionsManager = GameObject.Find("CompanionsManager").GetComponent<CompanionsManager>();
            nameText.text = gameCompanion.GetInfo().name;
        }

        public void Choose()
        {
            chosen = !chosen;
            if (chosen)
            {
                companionsManager.AddCompanion(this);
                nameText.fontStyle = FontStyle.Bold;
            }
            else
            {
                companionsManager.RemoveCompanion(this);
                nameText.fontStyle = FontStyle.Normal;
            }
        }

        public Game.Companions.Companion GetGameCompanion()
        {
            return gameCompanion;
        }
    }
}