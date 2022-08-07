using BTD_Mod_Helper;
using MelonLoader;
using Round_Changer;

[assembly: MelonInfo(typeof(Round_Changer.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Round_Changer
{
    using Assets.Scripts.Unity;
    using Assets.Scripts.Unity.UI_New;
    using Assets.Scripts.Unity.UI_New.Popups;
    using HarmonyLib;
    using Assets.Scripts.Simulation;
    using Assets.Scripts.Unity.UI_New.Settings;
    using TMPro;
    using System;
    using Assets.Scripts.Unity.UI_New.InGame;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Drawing;
    using System.IO;
    using Assets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;
    using BTD_Mod_Helper.Extensions;

    public class Main : BloonsTD6Mod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            MelonLogger.Msg("Round_Changer has loaded, click on the round button to set the round");
        }
        
        public static void SetRound(int round)
        {
            if (round > 1)
                InGame.instance.bridge.SetRound(round-1);
            if (round == 1)
                InGame.instance.bridge.SetRound(1);
        }
        [HarmonyPatch(typeof(MainHudRightAlign), nameof(MainHudRightAlign.OnRoundButtonClicked))]
        public class MainHudRightAlign_Initialise
        {

            [HarmonyPostfix]
            private static void Postfix()
            {
                PopupScreen.instance.ShowSetValuePopup("Set Round to ", "",
                    new System.Action<int>(round => 
                    {
                        SetRound(round);
                    })
                    , 100);
            }
        }
    }
}