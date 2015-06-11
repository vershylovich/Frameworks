namespace Adidas.Framework.Web.Extensions
{
    using System.Web;

    using Microsoft.Practices.Unity;

    public static class HttpApplicationStateExtensions
    {
        private const string Key = "UnityContainer";

        public static IUnityContainer GetContainer(this HttpApplicationState appState)
        {
            appState.Lock();

            try
            {
                return appState[Key] as IUnityContainer;
            }
            finally
            {
                appState.UnLock();
            }
        }

        public static void SetContainer(this HttpApplicationState appState, IUnityContainer container)
        {
            appState.Lock();

            try
            {
                appState[Key] = container;
            }
            finally
            {
                appState.UnLock();
            }
        }
    }
}
