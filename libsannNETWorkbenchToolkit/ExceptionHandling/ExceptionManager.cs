using System;
using System.Windows.Forms;
using log4net;

namespace libsannNETWorkbenchToolkit.ExceptionHandling
{
    class ExceptionManager
    {
        internal static string Parse(Exception e)
        {
            if (e.InnerException != null)
            {
                return e.Message + Environment.NewLine + Parse(e.InnerException);
            }
            return (e.Message);
        }

        internal static void LogAndShowException(Exception e, string caption, ILog logger)
        {
            var msg = Parse(e);
            logger.ErrorFormat(msg + Environment.NewLine + e.StackTrace);
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static void DomainUnhandledExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(Parse(e.Exception), "Unhandled exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
