﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MazeRunner.TestbedUI.Helpers;

namespace MazeRunner.TestbedUI.Controls;

[Obfuscation(ApplyToMembers = false, Exclude = true, StripAfterObfuscation = true)]
public sealed class CCLabelWithBottomBorderOnly : Label
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        set => base.Text = Ux.GenerateEllipsisInMiddle(value, maxLength: 50);
    }

    protected override void OnPaint(PaintEventArgs ea)
    {
        ArgumentNullException.ThrowIfNull(ea);

        base.OnPaint(ea);
        ControlPaint.DrawBorder(
            ea.Graphics,
            ClientRectangle,
            Color.Black, 0, ButtonBorderStyle.Inset,
            Color.Black, 0, ButtonBorderStyle.Inset,
            Color.Black, 0, ButtonBorderStyle.Inset,
            bottomColor: Color.Black,
            bottomWidth: 1,
            bottomStyle: ButtonBorderStyle.Inset
        );
    }
}