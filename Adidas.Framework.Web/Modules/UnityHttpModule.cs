namespace Adidas.Framework.Web.Modules
{
    using System;
    using System.Web;
    using System.Web.SessionState;

    using Adidas.Framework.Web.Extensions;

    using Microsoft.Practices.Unity;

    public class UnityHttpModule : IHttpModule, IRequiresSessionState
    {
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += this.OnPreRequestHandlerExecute;
        }

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            var handler = HttpContext.Current.Handler as System.Web.UI.Page;
            if (handler != null)
            {
                var container = HttpContext.Current.Application.GetContainer();
                if (container != null)
                {
                    container.BuildUp(handler.GetType(), handler);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
