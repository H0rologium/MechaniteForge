using System;
using Verse;

namespace MechaniteForge
{
	// Token: 0x02000007 RID: 7
	public class HediffComp_Override : HediffComp
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000025E8 File Offset: 0x000007E8
		public HediffCompProperties_Override Properties
		{
			get
			{
				return this.props as HediffCompProperties_Override;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002608 File Offset: 0x00000808
		public override void CompPostPostAdd(DamageInfo? dinfo)
		{
			Hediff firstHediffOfDef = base.Pawn.health.hediffSet.GetFirstHediffOfDef(this.Properties.overridesHediff, false);
			bool flag = firstHediffOfDef != null;
			if (flag)
			{
				base.Pawn.health.RemoveHediff(firstHediffOfDef);
			}
		}
	}
}
