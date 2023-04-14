using System.Collections;
using System.Collections.Generic;
using FrameworkDesign.Example.Scripts;
using FrameworkDesign.Example.Scripts.Command;
using FrameworkDesign;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace WFramework.Example
{
    public class GameStartPanel : AbstractPointGameController
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.Find("StartBtn").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    gameObject.SetActive(false);
                    this.SendCommand<StartGameCommand>();
                });
        }
    }
}
