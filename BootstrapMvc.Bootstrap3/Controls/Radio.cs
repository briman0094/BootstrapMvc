﻿using System;
using BootstrapMvc.Core;
using BootstrapMvc.Forms;

namespace BootstrapMvc.Controls
{
    public class Radio : Element, IFormControl
    {
        private IControlContext controlContext;

        private string text;

        private bool inline;

        private string value;

        public Radio(IBootstrapContext context)
            : base(context)
        {
            // Nothing
        }

        public void SetControlContext(IControlContext context)
        {
            controlContext = context;
        }

        #region Fluent

        public Radio Value(string value)
        {
            this.value = value;
            return this;
        }

        public Radio Text(string text)
        {
            this.text = text;
            return this;
        }

        public Radio Inline(bool inline = true)
        {
            this.inline = inline;
            return this;
        }

        #endregion

        protected override void WriteSelf(System.IO.TextWriter writer)
        {
            var groupContext = FormGroup.GetCurrentContext(Context);
            if (controlContext == null)
            {
                controlContext = groupContext.ControlContext;
            }

            var lbl = Context.CreateTagBuilder("label");

            if (inline && groupContext.WithStackedCheckbox)
            {
                throw new InvalidOperationException("Can't generate 'inline' checkbox in 'WithStackedCheckbox' group");
            }
            if (!inline && !groupContext.WithStackedCheckbox)
            {
                throw new InvalidOperationException("Can't generate 'stacked' checkbox without 'WithStackedCheckbox' in group");
            }
            lbl.AddCssClass(inline ? "radio-inline" : "radio");

            writer.Write(lbl.GetStartTag());

            var input = Context.CreateTagBuilder("input");
            input.MergeAttribute("type", "radio");
            if (controlContext != null)
            {
                input.MergeAttribute("id", controlContext.Name);
                input.MergeAttribute("name", controlContext.Name);
                input.MergeAttribute("value", value);
                var controlValue = controlContext.Value;
                if (controlValue != null && value.Equals(controlValue.ToString()))
                {
                    input.MergeAttribute("checked", "checked");
                }
            }

            ApplyCss(input);
            ApplyAttributes(input);

            ////input.MergeAttributes(helper.HtmlHelper.GetUnobtrusiveValidationAttributes(context.ExpressionText, context.Metadata));

            writer.Write(input.GetFullTag());

            writer.Write(Context.HtmlEncode(text));

            writer.Write(lbl.GetEndTag());
        }
    }
}
