// Decompiled with JetBrains decompiler
// Type: MechaniteForge.Gizmo_ShieldHediff
// Assembly: MechaniteForge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B80C6535-D609-4359-9B0D-798144DD9EE2
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\mechaniteForge\Assemblies\MechaniteForge.dll

using System;
using UnityEngine;
using Verse;

namespace MechaniteForge
{
	// Token: 0x02000002 RID: 2
	[StaticConstructorOnStartup]
	public class Gizmo_ShieldHediff : Gizmo
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002080 File Offset: 0x00000280
		public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth)
		{
			Rect overRect = new Rect(topLeft.x, topLeft.y, this.GetWidth(maxWidth), 75f);
			Find.WindowStack.ImmediateWindow(942612547, overRect, 0, delegate()
			{
				Rect rect = GenUI.ContractedBy(GenUI.AtZero(overRect), 6f);
				Rect rect2 = rect;
				rect2.height = overRect.height / 2f;
				Text.Font = 0;
				Widgets.Label(rect2, this.shield.def.LabelCap);
				Rect rect3 = rect;
				rect3.yMin = overRect.height / 2f;
				float num = this.shield.Severity / this.shield.def.maxSeverity;
				Widgets.FillableBar(rect3, num, Gizmo_ShieldHediff.FullShieldBarTex, Gizmo_ShieldHediff.EmptyShieldBarTex, false);
				Text.Font = (GameFont)1;
				Text.Anchor = (TextAnchor)4;
				Widgets.Label(rect3, (int)this.shield.Severity + " \\ " + (int)this.shield.def.maxSeverity);
				Text.Anchor = 0;
			}, true, false, 1f);
			return new GizmoResult(0);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F4 File Offset: 0x000002F4
		public override float GetWidth(float maxWidth)
		{
			return 140f;
		}

		// Token: 0x04000001 RID: 1
		public ShieldHediff shield;

		// Token: 0x04000002 RID: 2
		private static readonly Texture2D FullShieldBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.6f, 0.29f, 0.13f));

		// Token: 0x04000003 RID: 3
		private static readonly Texture2D EmptyShieldBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);
	}
}
