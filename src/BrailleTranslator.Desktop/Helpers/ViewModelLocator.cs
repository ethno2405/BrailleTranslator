using System;
using System.Windows;
using System.Windows.Markup;
using BrailleTranslator.Desktop.Dialogs.ViewModels;
using BrailleTranslator.Desktop.Dialogs.Views;
using BrailleTranslator.Desktop.ViewModels;
using BrailleTranslator.Desktop.Views;
using GalaSoft.MvvmLight.Ioc;

namespace BrailleTranslator.Desktop.Helpers
{
    public class ViewModelLocator
    {
        private IMapper _mapper;

        public ViewModelLocator(IMapper mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            _mapper = mapper;

            _mapper.Map<ToolbarViewModel, ToolbarView>()
                   .Map<MainContentViewModel, MainContentView>()
                   .Map<ComponentTitleViewModel, ComponentTitleView>();

            Application.Current.Startup += ApplicationStartup;
        }

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            Application.Current.MainWindow.DataContext = SimpleIoc.Default.GetInstance<MainWindowViewModel>();

            foreach (var mapping in _mapper.GetMappings())
            {
                var template = CreateTemplate(mapping.Key, mapping.Value);

                Application.Current.Resources.Add(template.DataTemplateKey, template);
            }
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