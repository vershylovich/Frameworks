namespace Adidas.Framework.Web.Presenters
{
    using Adidas.Framework.Web.Views;

    public interface IPresenterFactory
    {
        IPresenter<TView> Create<TView>(TView view) where TView : class, IView;
    }
}
