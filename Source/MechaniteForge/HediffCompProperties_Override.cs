using System;
using Verse;

namespace MechaniteForge
{
	// Token: 0x02000006 RID: 6
	public class HediffCompProperties_Override : HediffCompProperties
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000025CC File Offset: 0x000007CC
		public HediffCompProperties_Override()
		{
			this.compClass = typeof(HediffComp_Override);
		}

		// Token: 0x04000006 RID: 6
		public HediffDef overridesHediff;
	}
}
