// Decompiled with JetBrains decompiler
// Type: MechaniteForge.ShieldHediff
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using System.Text;
using Verse;

namespace MechaniteForge
{
  public class ShieldHediff : HediffWithComps
  {
    public int cooldownTicks;
    public bool broken;

    public virtual bool ShouldRemove
    {
      get
      {
        return false;
      }
    }

    public virtual void ExposeData()
    {
      base.ExposeData();
      // ISSUE: cast to a reference type
      Scribe_Values.Look<int>((M0&) ref this.cooldownTicks, "cooldownTicks", (M0) 0, false);
      // ISSUE: cast to a reference type
      Scribe_Values.Look<bool>((M0&) ref this.broken, "broken", (M0) 0, false);
    }

    public virtual void Tick()
    {
      ((Hediff) this).Tick();
      if (this.cooldownTicks > 0)
      {
        --this.cooldownTicks;
      }
      else
      {
        ((Hediff) this).set_Severity(((Hediff) this).get_Severity() + 0.06666667f);
        this.broken = false;
      }
    }

    public bool AbsorbDamage(DamageInfo dinfo)
    {
      if (this.broken)
        return false;
      ((Hediff) this).set_Severity(((Hediff) this).get_Severity() - ((DamageInfo) ref dinfo).get_Amount());
      if ((double) ((Hediff) this).get_Severity() <= 0.0)
      {
        this.broken = true;
        this.ResetBrokenCooldown();
      }
      return true;
    }

    public void ResetBrokenCooldown()
    {
      this.cooldownTicks = 750;
    }

    public void ResetHitCooldown()
    {
      this.cooldownTicks = 200;
    }

    public virtual string TipStringExtra
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(base.get_TipStringExtra());
        stringBuilder.AppendLine("Strength: " + (object) (int) ((Hediff) this).get_Severity() + "\\" + (object) (int) ((HediffDef) ((Hediff) this).def).maxSeverity + " (" + GenText.ToStringPercent(((Hediff) this).get_Severity() / (float) ((HediffDef) ((Hediff) this).def).maxSeverity) + ")");
        return stringBuilder.ToString();
      }
    }

    public virtual string DebugString()
    {
      StringBuilder stringBuilder = new StringBuilder(base.DebugString());
      stringBuilder.AppendLine("cooldownTicks: " + (object) this.cooldownTicks);
      return stringBuilder.ToString();
    }

    public ShieldHediff()
    {
      base.\u002Ector();
    }
  }
}
