namespace Adidas.Framework.Web.Views
{
    using System.Web.UI;

    using Adidas.Framework.Web.Presenters;

    using Microsoft.Practices.Unity;

    public abstract class BaseView : Page, IView
    {
        public abstract string Name { get; }

        [Dependency]
        public IPresenterFactory PresenterFactory { get; set; }
    }
}
