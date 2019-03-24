using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Companions
{
    public class CompanionInfo : MonoBehaviour
    {

        GameObject infoWindow;

        void Start()
        {
            infoWindow = GameObject.Find("CompanionInfo");
        }

        public void CloseInfoWindow()
        {
            infoWindow.SetActive(false);
        }
    }
}
