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
  [StaticConstructorOnStartup]
  public class Gizmo_ShieldHediff : Gizmo
  {
    private static readonly Texture2D FullShieldBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.6f, 0.29f, 0.13f));
    private static readonly Texture2D EmptyShieldBarTex = SolidColorMaterials.NewSolidColorTexture(Color.get_clear());
    public ShieldHediff shield;

    public virtual GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth)
    {
      Rect overRect = new Rect((float) topLeft.x, (float) topLeft.y, base.GetWidth(maxWidth), 75f);
      Find.get_WindowStack().ImmediateWindow(942612547, overRect, (WindowLayer) 0, (Action) (() =>
      {
        Rect rect1 = GenUI.ContractedBy(GenUI.AtZero(overRect), 6f);
        Rect rect2 = rect1;
        ((Rect) ref rect2).set_height(((Rect) ref overRect).get_height() / 2f);
        Text.set_Font((GameFont) 0);
        Widgets.Label(rect2, ((Def) ((Hediff) this.shield).def).get_LabelCap());
        Rect rect3 = rect1;
        ((Rect) ref rect3).set_yMin(((Rect) ref overRect).get_height() / 2f);
        float num = ((Hediff) this.shield).get_Severity() / (float) ((HediffDef) ((Hediff) this.shield).def).maxSeverity;
        Widgets.FillableBar(rect3, num, Gizmo_ShieldHediff.FullShieldBarTex, Gizmo_ShieldHediff.EmptyShieldBarTex, false);
        Text.set_Font((GameFont) 1);
        Text.set_Anchor((TextAnchor) 4);
        Widgets.Label(rect3, ((int) ((Hediff) this.shield).get_Severity()).ToString() + " \\ " + (object) (int) ((HediffDef) ((Hediff) this.shield).def).maxSeverity);
        Text.set_Anchor((TextAnchor) 0);
      }), true, false, 1f);
      return new GizmoResult((GizmoState) 0);
    }

    public virtual float GetWidth(float maxWidth)
    {
      return 140f;
    }

    public Gizmo_ShieldHediff()
    {
      base.\u002Ector();
    }
  }
}
