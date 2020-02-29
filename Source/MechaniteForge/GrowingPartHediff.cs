using System;
using System.Text;
using MechaniteForge.Logic;
using RimWorld;
using Verse;

namespace MechaniteForge
{
	// Token: 0x02000004 RID: 4
	public class GrowingPartHediff : Hediff_AddedPart
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000023AF File Offset: 0x000005AF
		public override bool ShouldRemove
		{
			get
			{
				return this.Severity >= this.def.maxSeverity;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023C7 File Offset: 0x000005C7
		public override void ExposeData()
		{
			base.ExposeData();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000023D4 File Offset: 0x000005D4
		public override string TipStringExtra
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.TipStringExtra);
				stringBuilder.AppendLine(Translator.Translate("Efficiency") + ": " + this.def.addedPartProps.partEfficiency.ToStringPercent());
				stringBuilder.AppendLine("Growth: " + this.Severity.ToStringPercent());
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000244C File Offset: 0x0000064C
		public override void PostRemoved()
		{
			base.PostRemoved();
			bool flag = this.Severity >= 1f;
			if (flag)
			{
				this.pawn.ReplaceHediffFromBodypart(base.Part, HediffDefOf.MissingBodyPart, MFHediffDefOf.MFCuredBodypart);
			}
		}
	}
}
