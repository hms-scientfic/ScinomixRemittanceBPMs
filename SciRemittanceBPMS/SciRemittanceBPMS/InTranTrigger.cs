using System;

using Ice;

using Erp.Bpm.TempTables;
using Erp.Tables;

namespace Epicor.Customization.Bpm.DBD87649F7B9004657B9FBEFDE7ABBB7EC
{
    internal sealed class InTranTrigger : Epicor.Customization.Bpm.InTranTriggerBase2<Erp.ErpContext, Epicor.Hosting.Session, CustCntTempRow>
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

            this.directives = new Epicor.Customization.Bpm.TriggerDirectiveBase<Erp.ErpContext, Epicor.Hosting.Session, CustCntTempRow>[] {
                new InTranDirective_sciSetPRemit_6D6E29F0433E4486BBF8286A093AB8A8(this, this.Db, this.Session),
            };

            #endregion // Directives
        }

        protected override string AttachedTo
        {
            get { return "Erp.CustCnt"; }
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
