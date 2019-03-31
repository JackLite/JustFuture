using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Companions
{
    public class CompanionsManager : MonoBehaviour
    {
        public float companionBlockWidth = 194;

        private List<Companion> companions = new List<Companion>();
        private GameObject companionsWrapper;
        GameObject companionInfo;
        // Use this for initialization
        void Start()
        {
            companionsWrapper = GameObject.Find("CompanionsWrapper");
            var companionsWrapperRect = companionsWrapper.GetComponent<RectTransform>();
            var childCount = companionsWrapperRect.childCount;
            companionsWrapperRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, childCount * companionBlockWidth);
            companionInfo = GameObject.Find("CompanionInfo");
            companionInfo.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddCompanion(Companion companion)
        {
            companions.Add(companion);
            if(companions.Count == 2)
            {
                DeactivateCompanionsChoose();
            }
        }

        public void RemoveCompanion(Companion companion)
        {
            companions.Remove(companion);
            if (companions.Count < 2)
            {
                ActivateCompanionsChoose();
            }
        }

        public void DeactivateCompanionsChoose()
        {
            var companionsWrapperTransform = companionsWrapper.GetComponent<Transform>();
            for (int i = 0; i < companionsWrapperTransform.childCount; i++)
            {
                var companion = companionsWrapperTransform.GetChild(i).GetComponent<Companion>();
                if(!companions.Contains(companion))
                {
                    companionsWrapperTransform.GetChild(i).GetComponent<Transform>().Find("ChooseButton").GetComponent<Button>().interactable = false;
                }
            }
        }

        public void ActivateCompanionsChoose()
        {
            var companionsWrapperTransform = companionsWrapper.GetComponent<Transform>();
            for (int i = 0; i < companionsWrapperTransform.childCount; i++)
            {
                companionsWrapperTransform.GetChild(i).GetComponent<Transform>().Find("ChooseButton").GetComponent<Button>().interactable = true;
            }
        }

        public void ShowCompanionInfo(Companion companion)
        {
            companionInfo.transform.Find("Name").GetComponent<Text>().text = companion.GetGameCompanion().GetInfo().name;
            companionInfo.transform.Find("Description").GetComponent<Text>().text = companion.GetGameCompanion().GetInfo().description;
            companionInfo.SetActive(true);
        }
    }
}
