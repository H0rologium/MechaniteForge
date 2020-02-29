// Decompiled with JetBrains decompiler
// Type: MechaniteForge.AngeliumHediff
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using MechaniteForge.Logic;
using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace MechaniteForge
{
  public class AngeliumHediff : HediffWithComps
  {
    public int ticksUntilNextHeal;

    public virtual void PostMake()
    {
      base.PostMake();
      this.SetNextTick();
    }

    public virtual void ExposeData()
    {
      base.ExposeData();
      // ISSUE: cast to a reference type
      Scribe_Values.Look<int>((M0&) ref this.ticksUntilNextHeal, "ticksUntilNextHeal", (M0) 0, false);
    }

    public virtual void Tick()
    {
      ((Hediff) this).Tick();
      if (((TickManager) Current.get_Game().tickManager).get_TicksGame() < this.ticksUntilNextHeal)
        return;
      this.TrySealWounds();
      this.TryRegrowBodyparts();
      this.SetNextTick();
    }

    public void TrySealWounds()
    {
      IEnumerable<Hediff> hediffs = ((IEnumerable<Hediff>) ((HediffSet) ((Pawn_HealthTracker) ((Pawn) ((Hediff) this).pawn).health).hediffSet).hediffs).Where<Hediff>((Func<Hediff, bool>) (hd => hd.get_Bleeding()));
      if (hediffs == null)
        return;
      using (IEnumerator<Hediff> enumerator = hediffs.GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          Hediff current = enumerator.Current;
          if (current is HediffWithComps hediffWithComps)
          {
            HediffComp_TendDuration comp = (HediffComp_TendDuration) HediffUtility.TryGetComp<HediffComp_TendDuration>((Hediff) hediffWithComps);
            comp.tendQuality = (__Null) 2.0;
            comp.tendTicksLeft = (__Null) Find.get_TickManager().get_TicksGame();
            ((Pawn_HealthTracker) ((Pawn) ((Hediff) this).pawn).health).Notify_HediffChanged(current);
          }
        }
      }
    }

    public void TryRegrowBodyparts()
    {
      using (IEnumerator<BodyPartRecord> enumerator = ((Pawn) ((Hediff) this).pawn).GetFirstMatchingBodyparts((BodyPartRecord) ((BodyDef) ((Pawn) ((Hediff) this).pawn).get_RaceProps().body).corePart, (HediffDef) HediffDefOf.MissingBodyPart, MFHediffDefOf.MFProtoBodypart, (Predicate<Hediff>) (hediff => hediff is Hediff_AddedPart)).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          BodyPartRecord part = enumerator.Current;
          Hediff hediff1 = ((IEnumerable<Hediff>) ((HediffSet) ((Pawn_HealthTracker) ((Pawn) ((Hediff) this).pawn).health).hediffSet).hediffs).First<Hediff>((Func<Hediff, bool>) (hediff => hediff.get_Part() == part && hediff.def == HediffDefOf.MissingBodyPart));
          if (hediff1 != null)
          {
            ((Pawn_HealthTracker) ((Pawn) ((Hediff) this).pawn).health).RemoveHediff(hediff1);
            ((Pawn_HealthTracker) ((Pawn) ((Hediff) this).pawn).health).AddHediff(MFHediffDefOf.MFProtoBodypart, part, new DamageInfo?(), (DamageWorker.DamageResult) null);
            ((HediffSet) ((Pawn_HealthTracker) ((Pawn) ((Hediff) this).pawn).health).hediffSet).DirtyCache();
          }
        }
      }
    }

    public void SetNextTick()
    {
      this.ticksUntilNextHeal = ((TickManager) Current.get_Game().tickManager).get_TicksGame() + 2000;
    }

    public AngeliumHediff()
    {
      base.\u002Ector();
    }
  }
}
