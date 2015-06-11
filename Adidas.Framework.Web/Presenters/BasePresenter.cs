namespace Adidas.Framework.Web.Presenters
{
    using System;

    using Adidas.Framework.Web.Views;

    public abstract class BasePresenter<TView> : IPresenter<TView> where TView : class, IView
    {
        public TView View { get; private set; }

        protected BasePresenter(TView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            this.View = view;
        }

        public virtual void OnViewLoad()
        {
        }
    }
}
