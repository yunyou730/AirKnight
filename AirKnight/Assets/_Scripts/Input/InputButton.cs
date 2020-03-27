using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ak
{
    public class InputButton
    {
        string careButton;

        public bool isPress = false;
        public bool isHold = false;
        public bool isRelease = false;

        public InputButton(string careButton)
        {
            SetCareButton(careButton);
        }

        void SetCareButton(string careButton)
        {
            this.careButton = careButton;
        }

        public void Collect()
        {
            isPress = Input.GetButtonDown(careButton);
            isHold = Input.GetButton(careButton);
            isRelease = Input.GetButtonUp(careButton);
        }
    }
}