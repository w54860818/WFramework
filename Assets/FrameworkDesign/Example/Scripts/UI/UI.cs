using System;
using System.Collections;
using System.Collections.Generic;
using FrameworkDesign;
using UnityEngine;

namespace WFramework.Example
{
    public class UI : AbstractPointGameController
    {
        // Start is called before the first frame update
        void Start()
        {
            this.RegisterEvent<GamePassEvent>(e =>
            {
                OnGamePass();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGamePass()
        {
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }

        void OnDestroy()
        {
        }
        
    }
}
