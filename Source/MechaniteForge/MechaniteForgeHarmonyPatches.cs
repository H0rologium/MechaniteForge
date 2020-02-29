using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace MechaniteForge
{
	// Token: 0x0200000A RID: 10
	[StaticConstructorOnStartup]
	public static class MechaniteForgeHarmonyPatches
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002840 File Offset: 0x00000A40
		static MechaniteForgeHarmonyPatches()
		{
			Harmony harmonyInstance = new Harmony("horologium.mechaniteforge");
			MechaniteForgeHarmonyPatches.Patch_Pawn(harmonyInstance);
			harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000286C File Offset: 0x00000A6C
		public static void Patch_Pawn(Harmony harmony)
		{
			Type typeFromHandle = typeof(Pawn);
			MethodBase method = typeFromHandle.GetMethod("GetGizmos");
			harmony.Patch(method, null, new HarmonyMethod(typeof(MechaniteForgeHarmonyPatches).GetMethod("Patch_Pawn_GetGizmos_Postfix")), null);
			Type typeFromHandle2 = typeof(VerbTracker);
			MethodBase getMethod = AccessTools.Property(typeFromHandle2, "PrimaryVerb").GetGetMethod();
			harmony.Patch(getMethod, new HarmonyMethod(typeof(MechaniteForgeHarmonyPatches).GetMethod("Patch_VerbTracker_PrimaryVerb_Prefix")), null, null);
			MechaniteForgeHarmonyPatches.VerbTracker_InitVerbs = typeFromHandle2.GetMethod("InitVerbs", BindingFlags.Instance | BindingFlags.NonPublic);
			MethodBase method2 = typeof(VerbTracker).GetMethod("GetVerbsCommands");
			harmony.Patch(method2, null, new HarmonyMethod(typeof(MechaniteForgeHarmonyPatches).GetMethod("Patch_VerbTracker_GetVerbsCommands_Postfix")), null);
			MethodBase method3 = typeFromHandle.GetMethod("PreApplyDamage");
			harmony.Patch(method3, new HarmonyMethod(typeof(MechaniteForgeHarmonyPatches).GetMethod("Patch_Pawn_PreApplyDamage_Prefix")), null, null);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002978 File Offset: 0x00000B78
		public static bool Patch_VerbTracker_PrimaryVerb_Prefix(ref VerbTracker __instance, ref Verb __result)
		{
			bool flag = __instance.AllVerbs == null && MechaniteForgeHarmonyPatches.VerbTracker_InitVerbs != null;
			if (flag)
			{
				MechaniteForgeHarmonyPatches.VerbTracker_InitVerbs.Invoke(__instance, new object[0]);
			}
			CompEquippable compEquippable = __instance.directOwner as CompEquippable;
			MultiVerbComp multiVerbComp = null;
			bool flag2 = compEquippable != null && (multiVerbComp = compEquippable.parent.TryGetComp<MultiVerbComp>()) != null;
			if (flag2)
			{
				__result = __instance.AllVerbs[multiVerbComp.currentVerbIndex];
				bool flag3 = __result != null;
				if (flag3)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A08 File Offset: 0x00000C08
		public static void Patch_VerbTracker_GetVerbsCommands_Postfix(ref VerbTracker __instance, ref IEnumerable<Command> __result, ref KeyCode hotKey)
		{
			CompEquippable compEquippable = __instance.directOwner as CompEquippable;
			MultiVerbComp multiVerb = null;
			bool flag = compEquippable != null && (multiVerb = compEquippable.parent.TryGetComp<MultiVerbComp>()) != null;
			if (flag)
			{
				List<Command> list = new List<Command>();
				int num = 0;
				list.AddRange(__result);
				foreach (Verb verb in __instance.AllVerbs)
				{
					MechaniteForgeHarmonyPatches.Command_MultiVerb item = new MechaniteForgeHarmonyPatches.Command_MultiVerb(verb, multiVerb, num);
					list.Add(item);
					num++;
				}
				__result = list;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public static void Patch_Pawn_GetGizmos_Postfix(ref Pawn __instance, ref IEnumerable<Gizmo> __result)
		{
			bool flag = __instance.health != null && __instance.health.hediffSet.HasHediff(MFHediffDefOf.MFBastionHigh, false);
			if (flag)
			{
				bool flag2 = Find.Selector.NumSelected == 1;
				if (flag2)
				{
					ShieldHediff shield = __instance.health.hediffSet.GetFirstHediffOfDef(MFHediffDefOf.MFBastionHigh, false) as ShieldHediff;
					__result = CollectionExtensions.AddItem<Gizmo>(__result, new Gizmo_ShieldHediff
					{
						shield = shield
					});
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B34 File Offset: 0x00000D34
		public static bool Patch_Pawn_PreApplyDamage_Prefix(ref Pawn __instance, ref DamageInfo dinfo, out bool absorbed)
		{
			bool flag = dinfo.Def != DamageDefOf.SurgicalCut && dinfo.Def != DamageDefOf.ExecutionCut && dinfo.Def != DamageDefOf.EMP && dinfo.Def != DamageDefOf.Stun;
			bool flag2 = flag && __instance.health != null && __instance.health.hediffSet.HasHediff(MFHediffDefOf.MFBastionHigh, false);
			if (flag2)
			{
				ShieldHediff shieldHediff = __instance.health.hediffSet.GetFirstHediffOfDef(MFHediffDefOf.MFBastionHigh, false) as ShieldHediff;
				bool flag3 = shieldHediff != null;
				if (flag3)
				{
					bool broken = shieldHediff.broken;
					if (broken)
					{
						shieldHediff.ResetBrokenCooldown();
					}
					else
					{
						bool flag4 = shieldHediff.AbsorbDamage(dinfo);
						bool flag5 = flag4;
						if (flag5)
						{
							bool broken2 = shieldHediff.broken;
							if (broken2)
							{
								SoundDefOf.EnergyShield_Broken.PlayOneShot(new TargetInfo(__instance.Position, __instance.Map, false));
								MoteMaker.MakeStaticMote(GenThing.TrueCenter(__instance), __instance.Map, ThingDefOf.Mote_ExplosionFlash, 12f);
							}
							else
							{
								SoundDefOf.EnergyShield_AbsorbDamage.PlayOneShot(new TargetInfo(__instance.Position, __instance.Map, false));
								MoteMaker.MakeStaticMote(GenThing.TrueCenter(__instance), __instance.Map, ThingDefOf.Mote_ExplosionFlash, 6f);
							}
							bool flag6 = !shieldHediff.broken;
							if (flag6)
							{
								shieldHediff.ResetHitCooldown();
							}
							absorbed = true;
							return false;
						}
					}
				}
			}
			absorbed = false;
			return true;
		}

		// Token: 0x04000009 RID: 9
		public static MethodInfo VerbTracker_InitVerbs;

		// Token: 0x02000014 RID: 20
		public class Command_MultiVerb : Command
		{
			// Token: 0x06000045 RID: 69 RVA: 0x000032C4 File Offset: 0x000014C4
			public Command_MultiVerb(Verb verb, MultiVerbComp multiVerb, int index)
			{
				this.defaultLabel = "Verb: " + verb.verbProps.label;
				this.verb = verb;
				this.multiVerb = multiVerb;
				this.index = index;
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00003300 File Offset: 0x00001500
			public override void ProcessInput(Event ev)
			{
				base.ProcessInput(ev);
				bool flag = this.multiVerb != null;
				if (flag)
				{
					this.multiVerb.currentVerbIndex = this.index;
					Targeter targeter = Find.Targeter;
					bool isTargeting = targeter.IsTargeting;
					if (isTargeting)
					{
						bool flag2 = this.verb.CasterIsPawn && targeter.targetingSource != null && targeter.targetingSource == this.verb.verbProps;
						if (flag2)
						{
							Pawn casterPawn = this.verb.CasterPawn;
							bool flag3 = !targeter.IsPawnTargeting(casterPawn);
							if (flag3)
							{
								targeter.targetingSourceAdditionalPawns.Add(casterPawn);
							}
						}
						else
						{
							List<Pawn> collection = new List<Pawn>(targeter.targetingSourceAdditionalPawns);
							targeter.BeginTargeting(this.verb);
							targeter.targetingSourceAdditionalPawns.AddRange(collection);
						}
					}
					Log.Message(this.defaultLabel + "; index=" + this.index, false);
				}
			}

			// Token: 0x06000047 RID: 71 RVA: 0x00003400 File Offset: 0x00001600
			public override bool GroupsWith(Gizmo other)
			{
				MechaniteForgeHarmonyPatches.Command_MultiVerb command_MultiVerb = other as MechaniteForgeHarmonyPatches.Command_MultiVerb;
				return command_MultiVerb != null && command_MultiVerb.verb.verbProps == this.verb.verbProps && command_MultiVerb.index == this.index;
			}

			// Token: 0x04000019 RID: 25
			private Verb verb;

			// Token: 0x0400001A RID: 26
			private MultiVerbComp multiVerb;

			// Token: 0x0400001B RID: 27
			private int index;
		}
	}
}
