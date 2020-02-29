// Decompiled with JetBrains decompiler
using System;
using System.Collections.Generic;
using Verse;

namespace MechaniteForge
{
	// Token: 0x0200000C RID: 12
	public class CompProperties_MultiVerb : CompProperties
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002CCA File Offset: 0x00000ECA
		public CompProperties_MultiVerb()
		{
			this.compClass = typeof(MultiVerbComp);
		}

		// Token: 0x0400000D RID: 13
		public List<VerbProperties> verbs;
	}
}
