using System;
using Verse;

namespace MechaniteForge
{
	// Token: 0x02000008 RID: 8
	public class RemovableHediff : Hediff
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000265B File Offset: 0x0000085B
		public override bool ShouldRemove
		{
			get
			{
				return true;
			}
		}
	}
}
