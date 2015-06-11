namespace Adidas.Framework.Security
{
    using System.Runtime.ConstrainedExecution;
    using System.Security.Permissions;

    using Microsoft.Win32.SafeHandles;

    [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class MySafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private MySafeTokenHandle()
            : base(true)
        {
        }
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        override protected bool ReleaseHandle()
        {
            return NativeMethods.CloseHandle(this.handle);
        }
    }
}