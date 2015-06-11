namespace Adidas.Framework.Security
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Security.Principal;

    [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
    public class Impersonation : IDisposable
    {
        WindowsImpersonationContext impersonationContext;
        public void ImpersonateUser(string user, string domain, string password)
        {
            MySafeTokenHandle token;
            if (NativeMethods.LogonUserA(
                user,
                domain,
                password,
                NativeMethods.Logon32LogonInteractive,
                NativeMethods.Logon32ProviderDefault,
                out token) != 0)
            {
                using (token)
                {
                    MySafeTokenHandle tokenDuplicate;
                    if (NativeMethods.DuplicateToken(token, 2, out tokenDuplicate) != 0)
                    {
                        using (tokenDuplicate)
                        {
                            if (!tokenDuplicate.IsInvalid)
                            {
                                var tempWindowsIdentity = new WindowsIdentity(tokenDuplicate.DangerousGetHandle());
                                this.impersonationContext = tempWindowsIdentity.Impersonate();
                            }
                        }
                    }
                }
            }
            else
            {
                throw new Exception(string.Format("LogonUser failed: {0}", Marshal.GetLastWin32Error()));
            }
        }

        public void Dispose()
        {
            this.impersonationContext.Undo();
            GC.SuppressFinalize(this);
        }
    }
}
