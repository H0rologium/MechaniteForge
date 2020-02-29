// Decompiled with JetBrains decompiler
// Type: MechaniteForge.HealingFactorHediff
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace MechaniteForge
{
  public class HealingFactorHediff : HediffWithComps
  {
    public int ticksUntilNextHeal;

    public virtual void PostMake()
    {
      base.PostMake();
      this.SetNextTick();
    }

    public virtual void Tick()
    {
      ((Hediff) this).Tick();
      if (((TickManager) Current.get_Game().tickManager).get_TicksGame() < this.ticksUntilNextHeal)
        return;
      this.TryHealWounds();
      this.SetNextTick();
    }

    public void TryHealWounds()
    {
      IEnumerable<Hediff> hediffs = ((IEnumerable<Hediff>) ((HediffSet) ((Pawn_HealthTracker) ((Pawn) ((Hediff) this).pawn).health).hediffSet).hediffs).Where<Hediff>((Func<Hediff, bool>) (hd => hd is Hediff_Injury));
      if (hediffs == null)
        return;
      using (IEnumerator<Hediff> enumerator = hediffs.GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          Hediff current = enumerator.Current;
          current.set_Severity(current.get_Severity() - 0.1f);
        }
      }
    }

    public virtual void ExposeData()
    {
      base.ExposeData();
      // ISSUE: cast to a reference type
      Scribe_Values.Look<int>((M0&) ref this.ticksUntilNextHeal, "ticksUntilNextHeal", (M0) 0, false);
    }

    public void SetNextTick()
    {
      this.ticksUntilNextHeal = ((TickManager) Current.get_Game().tickManager).get_TicksGame() + 50;
    }

    public HealingFactorHediff()
    {
      base.\u002Ector();
    }
  }
}
