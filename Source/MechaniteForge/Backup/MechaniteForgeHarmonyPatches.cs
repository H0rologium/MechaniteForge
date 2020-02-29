// Decompiled with JetBrains decompiler
// Type: MechaniteForge.MechaniteForgeHarmonyPatches
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace MechaniteForge
{
  [StaticConstructorOnStartup]
  public static class MechaniteForgeHarmonyPatches
  {
    public static MethodInfo VerbTracker_InitVerbs;

    static MechaniteForgeHarmonyPatches()
    {
      HarmonyInstance harmony = HarmonyInstance.Create("chjees.mechaniteforge");
      MechaniteForgeHarmonyPatches.Patch_Pawn(harmony);
      harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    public static void Patch_Pawn(HarmonyInstance harmony)
    {
      Type type1 = typeof (Pawn);
      MethodBase method1 = (MethodBase) type1.GetMethod("GetGizmos");
      harmony.Patch(method1, (HarmonyMethod) null, new HarmonyMethod(typeof (MechaniteForgeHarmonyPatches).GetMethod("Patch_Pawn_GetGizmos_Postfix")), (HarmonyMethod) null);
      Type type2 = typeof (VerbTracker);
      MethodBase getMethod = (MethodBase) AccessTools.Property(type2, "PrimaryVerb").GetGetMethod();
      harmony.Patch(getMethod, new HarmonyMethod(typeof (MechaniteForgeHarmonyPatches).GetMethod("Patch_VerbTracker_PrimaryVerb_Prefix")), (HarmonyMethod) null, (HarmonyMethod) null);
      MechaniteForgeHarmonyPatches.VerbTracker_InitVerbs = type2.GetMethod("InitVerbs", BindingFlags.Instance | BindingFlags.NonPublic);
      MethodBase method2 = (MethodBase) typeof (VerbTracker).GetMethod("GetVerbsCommands");
      harmony.Patch(method2, (HarmonyMethod) null, new HarmonyMethod(typeof (MechaniteForgeHarmonyPatches).GetMethod("Patch_VerbTracker_GetVerbsCommands_Postfix")), (HarmonyMethod) null);
      MethodBase method3 = (MethodBase) type1.GetMethod("PreApplyDamage");
      harmony.Patch(method3, new HarmonyMethod(typeof (MechaniteForgeHarmonyPatches).GetMethod("Patch_Pawn_PreApplyDamage_Prefix")), (HarmonyMethod) null, (HarmonyMethod) null);
    }

    public static bool Patch_VerbTracker_PrimaryVerb_Prefix(
      ref VerbTracker __instance,
      ref Verb __result)
    {
      if (__instance.get_AllVerbs() == null && MechaniteForgeHarmonyPatches.VerbTracker_InitVerbs != null)
        MechaniteForgeHarmonyPatches.VerbTracker_InitVerbs.Invoke((object) __instance, new object[0]);
      CompEquippable directOwner = __instance.directOwner as CompEquippable;
      MultiVerbComp multiVerbComp = (MultiVerbComp) null;
      if (directOwner != null && (M0) (multiVerbComp = (MultiVerbComp) ThingCompUtility.TryGetComp<MultiVerbComp>((Thing) ((ThingComp) directOwner).parent)) != null)
      {
        __result = __instance.get_AllVerbs()[multiVerbComp.currentVerbIndex];
        if (__result != null)
          return false;
      }
      return true;
    }

    public static void Patch_VerbTracker_GetVerbsCommands_Postfix(
      ref VerbTracker __instance,
      ref IEnumerable<Command> __result,
      ref KeyCode hotKey)
    {
      CompEquippable directOwner = __instance.directOwner as CompEquippable;
      MultiVerbComp multiVerb = (MultiVerbComp) null;
      if (directOwner == null || (M0) (multiVerb = (MultiVerbComp) ThingCompUtility.TryGetComp<MultiVerbComp>((Thing) ((ThingComp) directOwner).parent)) == null)
        return;
      List<Command> commandList = new List<Command>();
      int index = 0;
      commandList.AddRange(__result);
      using (List<Verb>.Enumerator enumerator = __instance.get_AllVerbs().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MechaniteForgeHarmonyPatches.Command_MultiVerb commandMultiVerb = new MechaniteForgeHarmonyPatches.Command_MultiVerb(enumerator.Current, multiVerb, index);
          commandList.Add((Command) commandMultiVerb);
          ++index;
        }
      }
      __result = (IEnumerable<Command>) commandList;
    }

    public static void Patch_Pawn_GetGizmos_Postfix(
      ref Pawn __instance,
      ref IEnumerable<Gizmo> __result)
    {
      if (__instance.health == null || !((HediffSet) ((Pawn_HealthTracker) __instance.health).hediffSet).HasHediff(MFHediffDefOf.MFBastionHigh, false) || Find.get_Selector().get_NumSelected() != 1)
        return;
      ShieldHediff firstHediffOfDef = ((HediffSet) ((Pawn_HealthTracker) __instance.health).hediffSet).GetFirstHediffOfDef(MFHediffDefOf.MFBastionHigh, false) as ShieldHediff;
      __result = (IEnumerable<Gizmo>) CollectionExtensions.Add<Gizmo>((IEnumerable<M0>) __result, (M0) new Gizmo_ShieldHediff()
      {
        shield = firstHediffOfDef
      });
    }

    public static bool Patch_Pawn_PreApplyDamage_Prefix(
      ref Pawn __instance,
      ref DamageInfo dinfo,
      out bool absorbed)
    {
      if (((DamageInfo) ref dinfo).get_Def() != DamageDefOf.SurgicalCut && ((DamageInfo) ref dinfo).get_Def() != DamageDefOf.ExecutionCut && ((DamageInfo) ref dinfo).get_Def() != DamageDefOf.EMP && ((DamageInfo) ref dinfo).get_Def() != DamageDefOf.Stun && __instance.health != null && ((HediffSet) ((Pawn_HealthTracker) __instance.health).hediffSet).HasHediff(MFHediffDefOf.MFBastionHigh, false) && ((HediffSet) ((Pawn_HealthTracker) __instance.health).hediffSet).GetFirstHediffOfDef(MFHediffDefOf.MFBastionHigh, false) is ShieldHediff firstHediffOfDef)
      {
        if (firstHediffOfDef.broken)
          firstHediffOfDef.ResetBrokenCooldown();
        else if (firstHediffOfDef.AbsorbDamage(dinfo))
        {
          if (firstHediffOfDef.broken)
          {
            SoundStarter.PlayOneShot((SoundDef) SoundDefOf.EnergyShield_Broken, SoundInfo.op_Implicit(new TargetInfo(((Thing) __instance).get_Position(), ((Thing) __instance).get_Map(), false)));
            MoteMaker.MakeStaticMote(GenThing.TrueCenter((Thing) __instance), ((Thing) __instance).get_Map(), (ThingDef) ThingDefOf.Mote_ExplosionFlash, 12f);
          }
          else
          {
            SoundStarter.PlayOneShot((SoundDef) SoundDefOf.EnergyShield_AbsorbDamage, SoundInfo.op_Implicit(new TargetInfo(((Thing) __instance).get_Position(), ((Thing) __instance).get_Map(), false)));
            MoteMaker.MakeStaticMote(GenThing.TrueCenter((Thing) __instance), ((Thing) __instance).get_Map(), (ThingDef) ThingDefOf.Mote_ExplosionFlash, 6f);
          }
          if (!firstHediffOfDef.broken)
            firstHediffOfDef.ResetHitCooldown();
          absorbed = true;
          return false;
        }
      }
      absorbed = false;
      return true;
    }

    public class Command_MultiVerb : Command
    {
      private Verb verb;
      private MultiVerbComp multiVerb;
      private int index;

      public Command_MultiVerb(Verb verb, MultiVerbComp multiVerb, int index)
      {
        this.\u002Ector();
        this.defaultLabel = (__Null) ("Verb: " + (string) ((VerbProperties) verb.verbProps).label);
        this.verb = verb;
        this.multiVerb = multiVerb;
        this.index = index;
      }

      public virtual void ProcessInput(Event ev)
      {
        base.ProcessInput(ev);
        if (this.multiVerb == null)
          return;
        this.multiVerb.currentVerbIndex = this.index;
        Targeter targeter = Find.get_Targeter();
        if (targeter.get_IsTargeting())
        {
          if (this.verb.get_CasterIsPawn() && targeter.targetingVerb != null && ((Verb) targeter.targetingVerb).verbProps == this.verb.verbProps)
          {
            Pawn casterPawn = this.verb.get_CasterPawn();
            if (!targeter.IsPawnTargeting(casterPawn))
              ((List<Pawn>) targeter.targetingVerbAdditionalPawns).Add(casterPawn);
          }
          else
          {
            List<Pawn> pawnList = new List<Pawn>((IEnumerable<Pawn>) targeter.targetingVerbAdditionalPawns);
            targeter.BeginTargeting(this.verb);
            ((List<Pawn>) targeter.targetingVerbAdditionalPawns).AddRange((IEnumerable<Pawn>) pawnList);
          }
        }
        Log.Message(this.defaultLabel.ToString() + "; index=" + (object) this.index, false);
      }

      public virtual bool GroupsWith(Gizmo other)
      {
        return other is MechaniteForgeHarmonyPatches.Command_MultiVerb commandMultiVerb && commandMultiVerb.verb.verbProps == this.verb.verbProps && commandMultiVerb.index == this.index;
      }
    }
  }
}
