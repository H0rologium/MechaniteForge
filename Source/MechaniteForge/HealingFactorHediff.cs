using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace MechaniteForge
{
	// Token: 0x02000005 RID: 5
	public class HealingFactorHediff : HediffWithComps
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000249C File Offset: 0x0000069C
		public override void PostMake()
		{
			base.PostMake();
			this.SetNextTick();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024B0 File Offset: 0x000006B0
		public override void Tick()
		{
			base.Tick();
			bool flag = Current.Game.tickManager.TicksGame >= this.ticksUntilNextHeal;
			if (flag)
			{
				this.TryHealWounds();
				this.SetNextTick();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024F4 File Offset: 0x000006F4
		public void TryHealWounds()
		{
			IEnumerable<Hediff> enumerable = from hd in this.pawn.health.hediffSet.hediffs
											 where hd is Hediff_Injury
											 select hd;
			bool flag = enumerable != null;
			if (flag)
			{
				foreach (Hediff hediff in enumerable)
				{
					hediff.Severity -= 0.1f;
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002594 File Offset: 0x00000794
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.ticksUntilNextHeal, "ticksUntilNextHeal", 0, false);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025B1 File Offset: 0x000007B1
		public void SetNextTick()
		{
			this.ticksUntilNextHeal = Current.Game.tickManager.TicksGame + 50;
		}

		// Token: 0x04000005 RID: 5
		public int ticksUntilNextHeal;
	}
}
