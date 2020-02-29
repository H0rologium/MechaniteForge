using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace MechaniteForge.Logic
{
	// Token: 0x0200000F RID: 15
	public static class BodypartUtility
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002F48 File Offset: 0x00001148
		public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef)
		{
			List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
			List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
			List<BodyPartRecord> nextSet = new List<BodyPartRecord>();
			nextSet.Add(startingPart);
			do
			{
				currentSet.AddRange(nextSet);
				nextSet.Clear();
				foreach (BodyPartRecord part in currentSet)
				{
					bool matchingPart = false;
					int num;
					for (int i = hediffs.Count - 1; i >= 0; i = num - 1)
					{
						Hediff hediff = hediffs[i];
						bool flag = hediff.Part == part && hediff.def == hediffDef;
						if (flag)
						{
							matchingPart = true;
							yield return part;
						}
						hediff = null;
						num = i;
					}
					bool flag2 = !matchingPart;
					if (flag2)
					{
						for (int j = 0; j < part.parts.Count; j = num + 1)
						{
							nextSet.Add(part.parts[j]);
							num = j;
						}
					}
					//part = null;
				}
				List<BodyPartRecord>.Enumerator enumerator = default(List<BodyPartRecord>.Enumerator);
				currentSet.Clear();
			}
			while (nextSet.Count > 0);
			yield break;
			yield break;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002F66 File Offset: 0x00001166
		public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef, HediffDef hediffExceptionDef)
		{
			List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
			List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
			List<BodyPartRecord> nextSet = new List<BodyPartRecord>();
			nextSet.Add(startingPart);
			do
			{
				currentSet.AddRange(nextSet);
				nextSet.Clear();
				foreach (BodyPartRecord part in currentSet)
				{
					bool matchingPart = false;
					int num;
					for (int i = hediffs.Count - 1; i >= 0; i = num - 1)
					{
						Hediff hediff = hediffs[i];
						bool flag = hediff.Part == part;
						if (flag)
						{
							bool flag2 = hediff.def == hediffExceptionDef;
							if (flag2)
							{
								matchingPart = true;
								break;
							}
							bool flag3 = hediff.def == hediffDef;
							if (flag3)
							{
								matchingPart = true;
								yield return part;
								break;
							}
						}
						hediff = null;
						num = i;
					}
					bool flag4 = !matchingPart;
					if (flag4)
					{
						for (int j = 0; j < part.parts.Count; j = num + 1)
						{
							nextSet.Add(part.parts[j]);
							num = j;
						}
					}
					//part = null;
				}
				List<BodyPartRecord>.Enumerator enumerator = default(List<BodyPartRecord>.Enumerator);
				currentSet.Clear();
			}
			while (nextSet.Count > 0);
			yield break;
			yield break;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002F8B File Offset: 0x0000118B
		public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef, HediffDef hediffExceptionDef, Predicate<Hediff> extraExceptionPredicate)
		{
			List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
			List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
			List<BodyPartRecord> nextSet = new List<BodyPartRecord>();
			nextSet.Add(startingPart);
			do
			{
				currentSet.AddRange(nextSet);
				nextSet.Clear();
				foreach (BodyPartRecord part in currentSet)
				{
					bool matchingPart = false;
					int num;
					for (int i = hediffs.Count - 1; i >= 0; i = num - 1)
					{
						Hediff hediff = hediffs[i];
						bool flag = hediff.Part == part;
						if (flag)
						{
							bool flag2 = hediff.def == hediffExceptionDef || extraExceptionPredicate(hediff);
							if (flag2)
							{
								matchingPart = true;
								break;
							}
							bool flag3 = hediff.def == hediffDef;
							if (flag3)
							{
								matchingPart = true;
								yield return part;
								break;
							}
						}
						hediff = null;
						num = i;
					}
					bool flag4 = !matchingPart;
					if (flag4)
					{
						for (int j = 0; j < part.parts.Count; j = num + 1)
						{
							nextSet.Add(part.parts[j]);
							num = j;
						}
					}
					//part = null;
				}
				List<BodyPartRecord>.Enumerator enumerator = default(List<BodyPartRecord>.Enumerator);
				currentSet.Clear();
			}
			while (nextSet.Count > 0);
			yield break;
			yield break;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002FB8 File Offset: 0x000011B8
		public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef, HediffDef[] hediffExceptionDefs)
		{
			List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
			List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
			List<BodyPartRecord> nextSet = new List<BodyPartRecord>();
			nextSet.Add(startingPart);
			do
			{
				currentSet.AddRange(nextSet);
				nextSet.Clear();
				foreach (BodyPartRecord part in currentSet)
				{
					bool matchingPart = false;
					int num;
					for (int i = hediffs.Count - 1; i >= 0; i = num - 1)
					{
						Hediff hediff = hediffs[i];
						bool flag = hediff.Part == part;
						if (flag)
						{
							bool flag2 = hediffExceptionDefs.Contains(hediff.def);
							if (flag2)
							{
								matchingPart = true;
								break;
							}
							bool flag3 = hediff.def == hediffDef;
							if (flag3)
							{
								matchingPart = true;
								yield return part;
								break;
							}
						}
						hediff = null;
						num = i;
					}
					bool flag4 = !matchingPart;
					if (flag4)
					{
						for (int j = 0; j < part.parts.Count; j = num + 1)
						{
							nextSet.Add(part.parts[j]);
							num = j;
						}
					}
					//part = null;
				}
				List<BodyPartRecord>.Enumerator enumerator = default(List<BodyPartRecord>.Enumerator);
				currentSet.Clear();
			}
			while (nextSet.Count > 0);
			yield break;
			yield break;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002FDD File Offset: 0x000011DD
		public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef[] hediffDefs)
		{
			List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
			List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
			List<BodyPartRecord> nextSet = new List<BodyPartRecord>();
			nextSet.Add(startingPart);
			do
			{
				currentSet.AddRange(nextSet);
				nextSet.Clear();
				foreach (BodyPartRecord part in currentSet)
				{
					bool matchingPart = false;
					int num;
					for (int i = hediffs.Count - 1; i >= 0; i = num - 1)
					{
						Hediff hediff = hediffs[i];
						bool flag = hediff.Part == part && hediffDefs.Contains(hediff.def);
						if (flag)
						{
							matchingPart = true;
							yield return part;
							break;
						}
						hediff = null;
						num = i;
					}
					bool flag2 = !matchingPart;
					if (flag2)
					{
						for (int j = 0; j < part.parts.Count; j = num + 1)
						{
							nextSet.Add(part.parts[j]);
							num = j;
						}
					}
					//part = null;
				}
				List<BodyPartRecord>.Enumerator enumerator = default(List<BodyPartRecord>.Enumerator);
				currentSet.Clear();
			}
			while (nextSet.Count > 0);
			yield break;
			yield break;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002FFC File Offset: 0x000011FC
		public static void ReplaceHediffFromBodypart(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef, HediffDef replaceWithDef)
		{
			List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
			List<BodyPartRecord> list = new List<BodyPartRecord>();
			List<BodyPartRecord> list2 = new List<BodyPartRecord>();
			list2.Add(startingPart);
			do
			{
				list.AddRange(list2);
				list2.Clear();
				foreach (BodyPartRecord bodyPartRecord in list)
				{
					for (int i = hediffs.Count - 1; i >= 0; i--)
					{
						Hediff hediff = hediffs[i];
						bool flag = hediff.Part == bodyPartRecord && hediff.def == hediffDef;
						if (flag)
						{
							Hediff hediff2 = hediffs[i];
							hediffs.RemoveAt(i);
							hediff2.PostRemoved();
							Hediff item = HediffMaker.MakeHediff(replaceWithDef, pawn, bodyPartRecord);
							hediffs.Insert(i, item);
						}
					}
					for (int j = 0; j < bodyPartRecord.parts.Count; j++)
					{
						list2.Add(bodyPartRecord.parts[j]);
					}
				}
				list.Clear();
			}
			while (list2.Count > 0);
		}
	}
}
