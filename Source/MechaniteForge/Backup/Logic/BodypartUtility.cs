// Decompiled with JetBrains decompiler
// Type: MechaniteForge.Logic.BodypartUtility
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using System;
using System.Collections.Generic;
using Verse;

namespace MechaniteForge.Logic
{
  public static class BodypartUtility
  {
    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(
      this Pawn pawn,
      BodyPartRecord startingPart,
      HediffDef hediffDef)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerable<BodyPartRecord>) new BodypartUtility.\u003CGetFirstMatchingBodyparts\u003Ed__0(-2)
      {
        \u003C\u003E3__pawn = pawn,
        \u003C\u003E3__startingPart = startingPart,
        \u003C\u003E3__hediffDef = hediffDef
      };
    }

    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(
      this Pawn pawn,
      BodyPartRecord startingPart,
      HediffDef hediffDef,
      HediffDef hediffExceptionDef)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerable<BodyPartRecord>) new BodypartUtility.\u003CGetFirstMatchingBodyparts\u003Ed__1(-2)
      {
        \u003C\u003E3__pawn = pawn,
        \u003C\u003E3__startingPart = startingPart,
        \u003C\u003E3__hediffDef = hediffDef,
        \u003C\u003E3__hediffExceptionDef = hediffExceptionDef
      };
    }

    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(
      this Pawn pawn,
      BodyPartRecord startingPart,
      HediffDef hediffDef,
      HediffDef hediffExceptionDef,
      Predicate<Hediff> extraExceptionPredicate)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerable<BodyPartRecord>) new BodypartUtility.\u003CGetFirstMatchingBodyparts\u003Ed__2(-2)
      {
        \u003C\u003E3__pawn = pawn,
        \u003C\u003E3__startingPart = startingPart,
        \u003C\u003E3__hediffDef = hediffDef,
        \u003C\u003E3__hediffExceptionDef = hediffExceptionDef,
        \u003C\u003E3__extraExceptionPredicate = extraExceptionPredicate
      };
    }

    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(
      this Pawn pawn,
      BodyPartRecord startingPart,
      HediffDef hediffDef,
      HediffDef[] hediffExceptionDefs)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerable<BodyPartRecord>) new BodypartUtility.\u003CGetFirstMatchingBodyparts\u003Ed__3(-2)
      {
        \u003C\u003E3__pawn = pawn,
        \u003C\u003E3__startingPart = startingPart,
        \u003C\u003E3__hediffDef = hediffDef,
        \u003C\u003E3__hediffExceptionDefs = hediffExceptionDefs
      };
    }

    public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(
      this Pawn pawn,
      BodyPartRecord startingPart,
      HediffDef[] hediffDefs)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerable<BodyPartRecord>) new BodypartUtility.\u003CGetFirstMatchingBodyparts\u003Ed__4(-2)
      {
        \u003C\u003E3__pawn = pawn,
        \u003C\u003E3__startingPart = startingPart,
        \u003C\u003E3__hediffDefs = hediffDefs
      };
    }

    public static void ReplaceHediffFromBodypart(
      this Pawn pawn,
      BodyPartRecord startingPart,
      HediffDef hediffDef,
      HediffDef replaceWithDef)
    {
      List<Hediff> hediffs = (List<Hediff>) ((HediffSet) ((Pawn_HealthTracker) pawn.health).hediffSet).hediffs;
      List<BodyPartRecord> bodyPartRecordList1 = new List<BodyPartRecord>();
      List<BodyPartRecord> bodyPartRecordList2 = new List<BodyPartRecord>();
      bodyPartRecordList2.Add(startingPart);
      do
      {
        bodyPartRecordList1.AddRange((IEnumerable<BodyPartRecord>) bodyPartRecordList2);
        bodyPartRecordList2.Clear();
        using (List<BodyPartRecord>.Enumerator enumerator = bodyPartRecordList1.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            BodyPartRecord current = enumerator.Current;
            for (int index = hediffs.Count - 1; index >= 0; --index)
            {
              Hediff hediff1 = hediffs[index];
              if (hediff1.get_Part() == current && hediff1.def == hediffDef)
              {
                Hediff hediff2 = hediffs[index];
                hediffs.RemoveAt(index);
                hediff2.PostRemoved();
                Hediff hediff3 = HediffMaker.MakeHediff(replaceWithDef, pawn, current);
                hediffs.Insert(index, hediff3);
              }
            }
            for (int index = 0; index < ((List<BodyPartRecord>) current.parts).Count; ++index)
              bodyPartRecordList2.Add(((List<BodyPartRecord>) current.parts)[index]);
          }
        }
        bodyPartRecordList1.Clear();
      }
      while (bodyPartRecordList2.Count > 0);
    }
  }
}
