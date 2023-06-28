using System;

using Ice;

using Erp.Bpm.TempTables;
using Erp.Tables;

namespace Epicor.Customization.Bpm.DB77A5A4B4EE6C4ABAB9A5A5D1A93265CF
{
    internal sealed class InTranTrigger : Epicor.Customization.Bpm.InTranTriggerBase2<Erp.ErpContext, Epicor.Hosting.Session, VendCntTempRow>
    {
        #region Data members

        private readonly bool isContextShouldBeDisposed;

        #endregion // Data members

        public InTranTrigger(
            Erp.ErpContext context,
            Epicor.Hosting.Session session,
            Epicor.Functions.Runtime.IFunctionsCatalog functionsCatalog,
            bool isContextShouldBeDisposed = false)
            : base(context, session, functionsCatalog)
        {
            this.isContextShouldBeDisposed = isContextShouldBeDisposed;

            #region Directives

            this.directives = new Epicor.Customization.Bpm.TriggerDirectiveBase<Erp.ErpContext, Epicor.Hosting.Session, VendCntTempRow>[] {
                new InTranDirective_sciSetVendRemittance_399F00713B2941828F8AEA77E6F534CB(this, this.Db, this.Session),
            };

            #endregion // Directives
        }

        protected override string AttachedTo
        {
            get { return "Erp.VendCnt"; }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) 
            {
                return;
            }

            if (this.isContextShouldBeDisposed)
            {
                this.Db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
