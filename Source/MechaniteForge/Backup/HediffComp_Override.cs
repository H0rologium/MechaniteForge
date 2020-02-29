// Decompiled with JetBrains decompiler
// Type: MechaniteForge.HediffComp_Override
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using Verse;

namespace MechaniteForge
{
  public class HediffComp_Override : HediffComp
  {
    public HediffCompProperties_Override Properties
    {
      get
      {
        return this.props as HediffCompProperties_Override;
      }
    }

    public virtual void CompPostPostAdd(DamageInfo? dinfo)
    {
      Hediff firstHediffOfDef = ((HediffSet) ((Pawn_HealthTracker) this.get_Pawn().health).hediffSet).GetFirstHediffOfDef(this.Properties.overridesHediff, false);
      if (firstHediffOfDef == null)
        return;
      ((Pawn_HealthTracker) this.get_Pawn().health).RemoveHediff(firstHediffOfDef);
    }

    public HediffComp_Override()
    {
      base.\u002Ector();
    }
  }
}
