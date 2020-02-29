// Decompiled with JetBrains decompiler
// Type: MechaniteForge.IngestionOutcomeDoer_PurifyAddictions
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using RimWorld;
using System.Collections;
using System.Collections.Generic;
using Verse;

namespace MechaniteForge
{
  public class IngestionOutcomeDoer_PurifyAddictions : IngestionOutcomeDoer
  {
    private static List<HediffDef> beneficialDrugDefs = new List<HediffDef>();
    private bool beneficialDrugsSearched;

    public IEnumerable<HediffDef> BeneficialAddictiveDrugHediffs
    {
      get
      {
        if (!this.beneficialDrugsSearched)
        {
          using (IEnumerator<ThingDef> enumerator1 = DefDatabase<ThingDef>.get_AllDefs().GetEnumerator())
          {
            while (((IEnumerator) enumerator1).MoveNext())
            {
              ThingDef current1 = enumerator1.Current;
              if (current1.get_IsIngestible() && current1.get_IsAddictiveDrug())
              {
                using (List<IngestionOutcomeDoer>.Enumerator enumerator2 = ((List<IngestionOutcomeDoer>) ((IngestibleProperties) current1.ingestible).outcomeDoers).GetEnumerator())
                {
                  while (enumerator2.MoveNext())
                  {
                    if (enumerator2.Current is IngestionOutcomeDoer_GiveHediff current)
                      IngestionOutcomeDoer_PurifyAddictions.beneficialDrugDefs.Add((HediffDef) current.hediffDef);
                  }
                }
              }
            }
          }
          this.beneficialDrugsSearched = true;
        }
        return (IEnumerable<HediffDef>) IngestionOutcomeDoer_PurifyAddictions.beneficialDrugDefs;
      }
    }

    protected virtual void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
    {
      if (!AddictionUtility.AddictedToAnything(pawn))
        return;
      using (IEnumerator<ChemicalDef> enumerator = DefDatabase<ChemicalDef>.get_AllDefs().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          ChemicalDef current = enumerator.Current;
          if (AddictionUtility.IsAddicted(pawn, current))
          {
            Hediff addictionHediff = (Hediff) AddictionUtility.FindAddictionHediff(pawn, current);
            Hediff toleranceHediff = AddictionUtility.FindToleranceHediff(pawn, current);
            ((Pawn_HealthTracker) pawn.health).RemoveHediff(addictionHediff);
            if (toleranceHediff != null)
              ((Pawn_HealthTracker) pawn.health).RemoveHediff(toleranceHediff);
          }
        }
      }
      using (IEnumerator<HediffDef> enumerator = this.BeneficialAddictiveDrugHediffs.GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          HediffDef current = enumerator.Current;
          Hediff firstHediffOfDef = ((HediffSet) ((Pawn_HealthTracker) pawn.health).hediffSet).GetFirstHediffOfDef(current, false);
          if (firstHediffOfDef != null)
            ((Pawn_HealthTracker) pawn.health).RemoveHediff(firstHediffOfDef);
        }
      }
    }

    public IngestionOutcomeDoer_PurifyAddictions()
    {
      base.\u002Ector();
    }
  }
}
