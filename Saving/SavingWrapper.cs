using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F8))
            {
                Saving();
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                Loading();
            }
        }

        void Loading()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        void Saving()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }


    }
}
