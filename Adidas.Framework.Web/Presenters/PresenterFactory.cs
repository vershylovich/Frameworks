namespace Adidas.Framework.Web.Presenters
{
    using System;

    using Adidas.Framework.Web.Views;

    using Microsoft.Practices.Unity;

    public class PresenterFactory : IPresenterFactory
    {
        private readonly IUnityContainer container;

        public PresenterFactory(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        public IPresenter<TView> Create<TView>(TView view) where TView : class, IView
        {
            this.container.RegisterInstance(view);

            var presenter = this.container.Resolve<IPresenter<TView>>();

            if (presenter == null)
            {
                throw new ArgumentException(string.Format("Presenter for '{0}' view is not registered", typeof(TView).Name));
            }

            return presenter;
        }
    }
}
