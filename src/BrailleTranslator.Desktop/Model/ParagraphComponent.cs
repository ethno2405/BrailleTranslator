﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace BrailleTranslator.Desktop.Model
{
    public class ParagraphComponent : BlockComponent
    {
        public ParagraphComponent()
        {
        }

        public ParagraphComponent(string title) : base(title)
        {
        }

        public ParagraphComponent(string title, Paragraph paragraph) : base(title, paragraph)
        {
        }

        public ParagraphComponent(Paragraph paragraph) : base(paragraph)
        {
        }

        public InlineComponent InlineComponent { get; set; }

        public override ObservableCollection<Component> Children
        {
            get
            {
                return new ObservableCollection<Component>();
            }
        }

        public override string CreateChildGestureText
        {
            get
            {
                return string.Empty;
            }
        }

        public override bool CanCreateChildComponent
        {
            get
            {
                return false;
            }
        }

        protected Paragraph Paragraph
        {
            get
            {
                return Block as Paragraph;
            }
            set
            {
                Block = value;
            }
        }

        public void InitializeInlineComponent(Paragraph paragraph)
        {
            var inline = paragraph.Inlines.FirstInline;

            if (inline == null)
            {
                InlineComponent = null;
                return;
            }

            if (InlineComponent == null)
            {
                InlineComponent = ComponentFactory.CreateComponent(inline) as InlineComponent;
                InlineComponent.Parent = this;
                return;
            }

            InlineComponent.Inline = inline;
        }

        protected override void MoveUp()
        {
            if (!CanMoveUp()) return;

            Freeze = true;

            var section = Paragraph.Parent as Section;
            var previous = Paragraph.PreviousBlock;

            section.Blocks.Remove(Block);
            section.Blocks.InsertBefore(previous, Paragraph);

            base.MoveUp();
            Freeze = false;
        }

        protected override void MoveDown()
        {
            if (!CanMoveDown()) return;

            Freeze = true;

            var section = Paragraph.Parent as Section;
            var next = Paragraph.NextBlock;

            section.Blocks.Remove(Block);
            section.Blocks.InsertAfter(next, Paragraph);

            base.MoveDown();
            Freeze = false;
        }

        protected override void RemoveChild(Component component)
        {
            var inlineComponent = component as InlineComponent;

            if (inlineComponent == null) throw new InvalidOperationException(string.Format("Component is of type {0}, but {1} is expected.", component.GetType(), typeof(InlineComponent)));

            Paragraph.Inlines.Remove(inlineComponent.Inline);

            InlineComponent = null;
        }

        protected override void PopulateChildren(TextElement textElement)
        {
            var paragraph = textElement as Paragraph;

            if (paragraph == null) throw new ArgumentException(string.Concat("Text element is not of type ", GetType().FullName), nameof(textElement));

            InitializeInlineComponent(paragraph);
        }

        protected override TextElement CreateChildElement()
        {
            throw new NotSupportedException();
        }

        protected override void CombineComponents(IEnumerable<Component> components)
        {
            throw new NotImplementedException();
        }
    }
}