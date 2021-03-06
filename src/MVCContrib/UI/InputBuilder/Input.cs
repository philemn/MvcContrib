using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace MvcContrib.UI.InputBuilder
{
	public class Input<T> where T : class
	{
		private readonly HtmlHelper<T> _htmlHelper;

		public Input(HtmlHelper<T> htmlHelper)
		{
			_htmlHelper = htmlHelper;
		}

		public IInputSpecification RenderInput(Expression<Func<T, object>> expression)
		{
			PropertyInfo propertyInfo = ReflectionHelper.FindPropertyFromExpression(expression);

			return new InputPropertySpecification
			       	{
			       		Model = new InputModelPropertyFactory<T>(_htmlHelper, InputBuilder.Conventions).Create(propertyInfo),
			       		HtmlHelper = _htmlHelper,
			       	};
		}

		public IInputSpecification RenderForm(string controller, string action)
		{
			return new InputTypeSpecification<T>
			       	{
			       		ModelType = typeof (T),
			       		HtmlHelper = _htmlHelper,
			       		Controller = controller,
			       		Action = action,
			       	};
		}
	}
}