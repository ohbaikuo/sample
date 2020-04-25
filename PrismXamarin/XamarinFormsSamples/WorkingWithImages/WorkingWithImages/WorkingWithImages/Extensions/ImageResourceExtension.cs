using System;
using System.Reflection;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkingWithImages.Extensions
{
	[ContentProperty(nameof(Source))]
	public class ImageResourceExtension : IMarkupExtension<ImageSource>
	{
		public string Source { get; set; }

		public ImageSource ProvideValue(IServiceProvider serviceProvider)
		{
			if (String.IsNullOrEmpty(Source))
			{
				//IXmlLineInfoProvider:特定のエラーが検出されたことを示す行や文字の情報を提供するオブジェクト
				IXmlLineInfoProvider lineInfoProvider = serviceProvider.GetService(typeof(IXmlLineInfoProvider)) as IXmlLineInfoProvider;
				IXmlLineInfo lineInfo = (lineInfoProvider != null) ? lineInfoProvider.XmlLineInfo : new XmlLineInfo();
				throw new XamlParseException("ImageResourceExtensionはSourceプロパティを設定する必要があります。", lineInfo);
			}

			string assemblyName = GetType().GetTypeInfo().Assembly.GetName().Name;
			var imageSource = ImageSource.FromResource(assemblyName + "." + Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
			return imageSource;
		}

		object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
		{
			return (this as IMarkupExtension<ImageSource>).ProvideValue(serviceProvider);
		}
	}
}
