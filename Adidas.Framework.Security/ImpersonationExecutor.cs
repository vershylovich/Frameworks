namespace Adidas.Framework.Security
{
    using System;

    public class ImpersonationExecutor
    {
        private readonly string user;

        private readonly string domain;

        private readonly string password;

        public ImpersonationExecutor(string user, string domain, string password)
        {
            this.user = user;
            this.domain = domain;
            this.password = password;
        }

        public void ExecuteCode(Action action)
        {
            using (var impersonation = new Impersonation())
            {
                impersonation.ImpersonateUser(this.user, this.domain, this.password);
                action();
            }
        }
    }
}