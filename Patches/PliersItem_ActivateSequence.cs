using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;

namespace Censorship.Patches;

[HarmonyPatch(typeof(PliersItem), nameof(PliersItem.ActivateSequence))]
public class PliersItem_ActivateSequence
{
    public static IEnumerator Postfix(IEnumerator result, PliersItem __instance)
    {
        AscensionStatsData.TryIncrementStat(AscensionStat.Type.TeethPulled);

        __instance.PlayExitAnimation();
        yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, false, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));
        if (!ProgressionData.LearnedMechanic(MechanicsConcept.PliersItem))
        {
            ProgressionData.SetMechanicLearned(MechanicsConcept.PliersItem);
            yield return new WaitForSeconds(1f);
            yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("LearnedPliersItem", TextDisplayer.MessageAdvanceMode.Input);
        }
    }
}