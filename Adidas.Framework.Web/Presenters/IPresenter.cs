namespace Adidas.Framework.Web.Presenters
{
    using Adidas.Framework.Web.Views;

    public interface IPresenter<out TView> where TView : class, IView
    {
        TView View { get; }

        void OnViewLoad();
    }
}
