﻿using System;
using System.Windows;
using System.Windows.Markup;

namespace BrailleTranslator.Desktop.Helpers
{
    public class Mapper : IMapper
    {
        public IMapper Map<TViewModel, TView>() where TView : FrameworkElement
        {
            return Map(typeof(TViewModel), typeof(TView));
        }

        public IMapper Map(Type viewModelType, Type viewType)
        {
            var template = CreateTemplate(viewModelType, viewType);
            var key = template.DataTemplateKey;
            Application.Current.Resources.Add(key, template);

            return this;
        }

        private DataTemplate CreateTemplate(Type viewModelType, Type viewType)
        {
            const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><v:{1} /></DataTemplate>";
            var xaml = string.Format(xamlTemplate, viewModelType.Name, viewType.Name);

            var context = new ParserContext();

            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace, viewModelType.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction("v", viewType.Namespace, viewType.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("v", "v");

            return (DataTemplate)XamlReader.Parse(xaml, context);
        }
    }
}