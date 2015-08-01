using System;

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

        internal static void DomainUnhandledExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // TODO: Implemento gestione delle eccezioni a livello globale
            throw new NotImplementedException();
        }
    }
}
