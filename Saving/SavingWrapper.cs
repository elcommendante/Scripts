using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameClient.Saving
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
            Load();
        }
        void Saving()
        {
            Save();
        }


        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}
