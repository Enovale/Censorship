using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;

namespace Censorship.Patches;

[HarmonyPatch(typeof(SpecialDaggerItem), nameof(SpecialDaggerItem.ActivateSequence))]
public class SpecialDaggerItem_ActivateSequence
{
    public static IEnumerator Postfix(IEnumerator result, SpecialDaggerItem __instance)
    {
        __instance.PlayExitAnimation();
        yield return Singleton<LifeManager>.Instance.ShowDamageSequence(4, 1, false, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_Eyeball"));
        RunState.Run.eyeState = EyeballState.Missing;
        StoryEventsData.SetEventCompleted(StoryEvent.SpecialDaggerUsed);
        Singleton<TurnManager>.Instance.PostBattleSpecialNode = new ChooseEyeballNodeData();
    }
}