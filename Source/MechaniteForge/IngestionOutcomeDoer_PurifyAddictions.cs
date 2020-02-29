using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MechaniteForge
{
	// Token: 0x0200000E RID: 14
	public class IngestionOutcomeDoer_PurifyAddictions : IngestionOutcomeDoer
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002D28 File Offset: 0x00000F28
		public IEnumerable<HediffDef> BeneficialAddictiveDrugHediffs
		{
			get
			{
				bool flag = !this.beneficialDrugsSearched;
				if (flag)
				{
					foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
					{
						bool flag2 = thingDef.IsIngestible && thingDef.IsAddictiveDrug;
						if (flag2)
						{
							foreach (IngestionOutcomeDoer ingestionOutcomeDoer in thingDef.ingestible.outcomeDoers)
							{
								IngestionOutcomeDoer_GiveHediff ingestionOutcomeDoer_GiveHediff;
								bool flag3 = (ingestionOutcomeDoer_GiveHediff = (ingestionOutcomeDoer as IngestionOutcomeDoer_GiveHediff)) != null;
								if (flag3)
								{
									IngestionOutcomeDoer_PurifyAddictions.beneficialDrugDefs.Add(ingestionOutcomeDoer_GiveHediff.hediffDef);
								}
							}
						}
					}
					this.beneficialDrugsSearched = true;
				}
				return IngestionOutcomeDoer_PurifyAddictions.beneficialDrugDefs;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E20 File Offset: 0x00001020
		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
		{
			bool flag = AddictionUtility.AddictedToAnything(pawn);
			if (flag)
			{
				foreach (ChemicalDef chemical in DefDatabase<ChemicalDef>.AllDefs)
				{
					bool flag2 = AddictionUtility.IsAddicted(pawn, chemical);
					if (flag2)
					{
						Hediff hediff = AddictionUtility.FindAddictionHediff(pawn, chemical);
						Hediff hediff2 = AddictionUtility.FindToleranceHediff(pawn, chemical);
						pawn.health.RemoveHediff(hediff);
						bool flag3 = hediff2 != null;
						if (flag3)
						{
							pawn.health.RemoveHediff(hediff2);
						}
					}
				}
				foreach (HediffDef def in this.BeneficialAddictiveDrugHediffs)
				{
					Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(def, false);
					bool flag4 = firstHediffOfDef != null;
					if (flag4)
					{
						pawn.health.RemoveHediff(firstHediffOfDef);
					}
				}
			}
		}

		// Token: 0x0400000F RID: 15
		private static List<HediffDef> beneficialDrugDefs = new List<HediffDef>();

		// Token: 0x04000010 RID: 16
		private bool beneficialDrugsSearched = false;
	}
}
