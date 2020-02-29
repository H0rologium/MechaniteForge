// Decompiled with JetBrains decompiler
// Type: MechaniteForge.RemovableHediff
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using Verse;

namespace MechaniteForge
{
  public class RemovableHediff : Hediff
  {
    public virtual bool ShouldRemove
    {
      get
      {
        return true;
      }
    }

    public RemovableHediff()
    {
      base.\u002Ector();
    }
  }
}
