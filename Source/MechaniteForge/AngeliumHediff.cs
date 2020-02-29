// Decompiled with JetBrains decompiler
// Type: MechaniteForge.AngeliumHediff
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using MechaniteForge.Logic;
using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace MechaniteForge
{
	public class AngeliumHediff : HediffWithComps
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		public override void PostMake()
		{
			base.PostMake();
			this.SetNextTick();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002125 File Offset: 0x00000325
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.ticksUntilNextHeal, "ticksUntilNextHeal", 0, false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		public override void Tick()
		{
			base.Tick();
			bool flag = Current.Game.tickManager.TicksGame >= this.ticksUntilNextHeal;
			if (flag)
			{
				this.TrySealWounds();
				this.TryRegrowBodyparts();
				this.SetNextTick();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002190 File Offset: 0x00000390
		public void TrySealWounds()
		{
			IEnumerable<Hediff> enumerable = from hd in this.pawn.health.hediffSet.hediffs
											 where hd.Bleeding
											 select hd;
			bool flag = enumerable != null;
			if (flag)
			{
				foreach (Hediff hediff in enumerable)
				{
					HediffWithComps hediffWithComps = hediff as HediffWithComps;
					bool flag2 = hediffWithComps != null;
					if (flag2)
					{
						HediffComp_TendDuration hediffComp_TendDuration = HediffUtility.TryGetComp<HediffComp_TendDuration>(hediffWithComps);
						hediffComp_TendDuration.tendQuality = 2f;
						hediffComp_TendDuration.tendTicksLeft = Find.TickManager.TicksGame;
						this.pawn.health.Notify_HediffChanged(hediff);
					}
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002268 File Offset: 0x00000468
		public void TryRegrowBodyparts()
		{
			using (IEnumerator<BodyPartRecord> enumerator = this.pawn.GetFirstMatchingBodyparts(this.pawn.RaceProps.body.corePart, HediffDefOf.MissingBodyPart, MFHediffDefOf.MFProtoBodypart, (Hediff hediff) => hediff is Hediff_AddedPart).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BodyPartRecord part = enumerator.Current;
					Hediff hediff2 = this.pawn.health.hediffSet.hediffs.First((Hediff hediff) => hediff.Part == part && hediff.def == HediffDefOf.MissingBodyPart);
					bool flag = hediff2 != null;
					if (flag)
					{
						this.pawn.health.RemoveHediff(hediff2);
						this.pawn.health.AddHediff(MFHediffDefOf.MFProtoBodypart, part, null, null);
						this.pawn.health.hediffSet.DirtyCache();
					}
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002388 File Offset: 0x00000588
		public void SetNextTick()
		{
			this.ticksUntilNextHeal = Current.Game.tickManager.TicksGame + 2000;
		}

		// Token: 0x04000004 RID: 4
		public int ticksUntilNextHeal;
	}
}
