// Decompiled with JetBrains decompiler
// Type: MechaniteForge.GrowingPartHediff
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using MechaniteForge.Logic;
using RimWorld;
using System.Text;
using Verse;

namespace MechaniteForge
{
  public class GrowingPartHediff : Hediff_AddedPart
  {
    public virtual bool ShouldRemove
    {
      get
      {
        return (double) ((Hediff) this).get_Severity() >= ((HediffDef) ((Hediff) this).def).maxSeverity;
      }
    }

    public virtual void ExposeData()
    {
      base.ExposeData();
    }

    public virtual string TipStringExtra
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(base.get_TipStringExtra());
        stringBuilder.AppendLine(Translator.Translate("Efficiency") + ": " + GenText.ToStringPercent((float) ((AddedBodyPartProps) ((HediffDef) ((Hediff) this).def).addedPartProps).partEfficiency));
        stringBuilder.AppendLine("Growth: " + GenText.ToStringPercent(((Hediff) this).get_Severity()));
        return stringBuilder.ToString();
      }
    }

    public virtual void PostRemoved()
    {
      ((HediffWithComps) this).PostRemoved();
      if ((double) ((Hediff) this).get_Severity() < 1.0)
        return;
      ((Pawn) ((Hediff) this).pawn).ReplaceHediffFromBodypart(((Hediff) this).get_Part(), (HediffDef) HediffDefOf.MissingBodyPart, MFHediffDefOf.MFCuredBodypart);
    }

    public GrowingPartHediff()
    {
      base.\u002Ector();
    }
  }
}
