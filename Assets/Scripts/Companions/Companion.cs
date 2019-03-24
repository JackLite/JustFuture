using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

namespace Companions
{
    public class Companion : MonoBehaviour
    {
        public Game.Companions.Companion gameCompanion;
        public string description;

        bool chosen;
        Text nameText;
        CompanionsManager companionsManager;
        // Use this for initialization
        void Start()
        {
            nameText = transform.Find("CompanionNameText").GetComponent<Text>();
            companionsManager = GameObject.Find("CompanionsManager").GetComponent<CompanionsManager>();
            nameText.text = gameCompanion.getInfo().name;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Choose()
        {
            chosen = !chosen;
            if (chosen)
            {
                companionsManager.addCompanion(this);
                nameText.fontStyle = FontStyle.Bold;
            }
            else
            {
                companionsManager.removeCompanion(this);
                nameText.fontStyle = FontStyle.Normal;
            }
        }

        public Game.Companions.Companion getGameCompanion()
        {
            return gameCompanion;
        }
    }
}