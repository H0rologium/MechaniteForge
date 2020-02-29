using System;
using System.Text;
using Verse;

namespace MechaniteForge
{
	// Token: 0x02000009 RID: 9
	public class ShieldHediff : HediffWithComps
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002667 File Offset: 0x00000867
		public override bool ShouldRemove
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000266A File Offset: 0x0000086A
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.cooldownTicks, "cooldownTicks", 0, false);
			Scribe_Values.Look<bool>(ref this.broken, "broken", false, false);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000269C File Offset: 0x0000089C
		public override void Tick()
		{
			base.Tick();
			bool flag = this.cooldownTicks > 0;
			if (flag)
			{
				this.cooldownTicks--;
			}
			else
			{
				this.Severity += 0.06666667f;
				this.broken = false;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026EC File Offset: 0x000008EC
		public bool AbsorbDamage(DamageInfo dinfo)
		{
			bool flag = this.broken;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.Severity -= dinfo.Amount;
				bool flag2 = this.Severity <= 0f;
				if (flag2)
				{
					this.broken = true;
					this.ResetBrokenCooldown();
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002746 File Offset: 0x00000946
		public void ResetBrokenCooldown()
		{
			this.cooldownTicks = 750;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002754 File Offset: 0x00000954
		public void ResetHitCooldown()
		{
			this.cooldownTicks = 200;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002764 File Offset: 0x00000964
		public override string TipStringExtra
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.TipStringExtra);
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"Strength: ",
					(int)this.Severity,
					"\\",
					(int)this.def.maxSeverity,
					" (",
					(this.Severity / this.def.maxSeverity).ToStringPercent(),
					")"
				}));
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002800 File Offset: 0x00000A00
		public override string DebugString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.DebugString());
			stringBuilder.AppendLine("cooldownTicks: " + this.cooldownTicks);
			return stringBuilder.ToString();
		}

		// Token: 0x04000007 RID: 7
		public int cooldownTicks;

		// Token: 0x04000008 RID: 8
		public bool broken;
	}
}
