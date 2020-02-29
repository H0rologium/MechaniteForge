using System;
using Verse;

namespace MechaniteForge
{
	// Token: 0x0200000D RID: 13
	public class MultiVerbComp : ThingComp
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public CompProperties_MultiVerb Props
		{
			get
			{
				return this.props as CompProperties_MultiVerb;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D01 File Offset: 0x00000F01
		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<int>(ref this.currentVerbIndex, "currentVerbIndex", 0, false);
		}

		// Token: 0x0400000E RID: 14
		public int currentVerbIndex;
	}
}
