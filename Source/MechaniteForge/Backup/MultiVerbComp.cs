// Decompiled with JetBrains decompiler
// Type: MechaniteForge.MultiVerbComp
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using Verse;

namespace MechaniteForge
{
  public class MultiVerbComp : ThingComp
  {
    public int currentVerbIndex;

    public CompProperties_MultiVerb Props
    {
      get
      {
        return this.props as CompProperties_MultiVerb;
      }
    }

    public virtual void PostExposeData()
    {
      base.PostExposeData();
      // ISSUE: cast to a reference type
      Scribe_Values.Look<int>((M0&) ref this.currentVerbIndex, "currentVerbIndex", (M0) 0, false);
    }

    public MultiVerbComp()
    {
      base.\u002Ector();
    }
  }
}
