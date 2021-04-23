using System;
using System.Windows.Forms;

namespace Spoon.SIDC
{
    public class WaitCursor : IDisposable
    {
        public WaitCursor()
        {
            IsWaitCursor = true;
        }

        public void Dispose()
        {
            IsWaitCursor = false;
        }

        public bool IsWaitCursor
        {
            get
            {
                return Application.UseWaitCursor;
            }
            set
            {
                if (Application.UseWaitCursor != value)
                {
                    Application.UseWaitCursor = value;
                    Cursor.Current = value ? Cursors.WaitCursor : Cursors.Default;
                }
            }
        }
    }
}
