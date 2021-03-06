﻿using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;
using BrailleTranslator.Desktop.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace BrailleTranslator.Desktop.Model
{
    public class VolumeComponent : BlockComponent
    {
        public VolumeComponent()
        {
            SubscribeForMessages();
        }

        public VolumeComponent(string title) : base(title)
        {
            SubscribeForMessages();
        }

        public VolumeComponent(string title, Volume volume) : base(title, volume)
        {
            SubscribeForMessages();
        }

        public VolumeComponent(Volume volume) : base(volume)
        {
            SubscribeForMessages();
        }

        public override string CreateChildText
        {
            get
            {
                return "New section";
            }
        }

        public override string CreateChildGestureText
        {
            get
            {
                return "Ctrl + Shift + Enter";
            }
        }

        protected Volume Volume
        {
            get
            {
                return (Volume)Block;
            }
            set
            {
                Block = value;
            }
        }

        protected override void RemoveChild(Component component)
        {
            var blockComponent = component as BlockComponent;

            if (blockComponent == null) throw new InvalidOperationException(string.Format("Component is of type {0}, but type {1} is expected.", component.GetType(), typeof(BlockComponent)));

            (Block as Volume).Blocks.Remove(blockComponent.Block);

            Children.Remove(component);
        }

        protected override void PopulateChildren(TextElement textElement)
        {
            var volume = textElement as Volume;

            if (volume == null) throw new ArgumentException(string.Concat("Text element is not of type ", GetType().FullName), nameof(textElement));

            PopulateChildren(volume.Blocks);
        }

        protected override void MoveUp()
        {
            if (!CanMoveUp()) return;

            var document = Volume.Parent as FlowDocument;
            var previous = Volume.PreviousBlock;

            Freeze = true;

            document.Blocks.Remove(Block);
            document.Blocks.InsertBefore(previous, Volume);

            base.MoveUp();
            Freeze = false;
        }

        protected override void MoveDown()
        {
            if (!CanMoveDown()) return;

            var document = Volume.Parent as FlowDocument;
            var next = Volume.NextBlock;

            Freeze = true;

            document.Blocks.Remove(next);
            document.Blocks.InsertBefore(Volume, next);

            base.MoveDown();
            Freeze = false;
        }

        protected override TextElement CreateChildElement()
        {
            var section = new Section(new Paragraph(new Run(string.Empty)));

            Volume.Blocks.Add(section);

            return section;
        }

        protected override void CombineComponents(IEnumerable<Component> components)
        {
            throw new NotImplementedException();
        }

        private void SubscribeForMessages()
        {
            Messenger.Default.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.Enter, ModifierKeys.Control | ModifierKeys.Shift),
                m =>
                {
                    if (IsLast())
                    {
                        CreateChildCommand.Execute(null);
                    }
                });
        }
    }
}