namespace Adidas.Framework.Security
{
    using System;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;
    using System.Security;

    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        #region P/Invoke

        internal const int Logon32LogonInteractive = unchecked(2);
        internal const int Logon32ProviderDefault = unchecked(0);

        [DllImport("advapi32.dll")]
        internal static extern int LogonUserA(string lpszUserName,
                                              string lpszDomain,
                                              string lpszPassword,
                                              int dwLogonType,
                                              int dwLogonProvider,
                                              out MySafeTokenHandle phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int DuplicateToken(MySafeTokenHandle hToken,
                                                  int impersonationLevel,
                                                  out MySafeTokenHandle hNewToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool CloseHandle(IntPtr handle);

        #endregion
    }
}