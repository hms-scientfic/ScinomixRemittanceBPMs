using System;
using System.Collections.Generic;

namespace Epicor.Customization.Bpm.DB77A5A4B4EE6C4ABAB9A5A5D1A93265CF
{
    public sealed class InTranTriggerFactory : Epicor.Customization.Bpm.BpmCustomizationFactoryBase2<Erp.ErpContext, Epicor.Hosting.Session>
    {
        #region BpmCustomizationFactoryBase2 Members

        protected override object CreateImplementationCore(
            Erp.ErpContext context,
            Epicor.Functions.Runtime.IFunctionsCatalog functionsCatalog,
            object input)
        {
            var childCtx = Ice.Services.ContextFactory.CreateNewConnectedContext<Erp.ErpContext>(context);
            return new InTranTrigger(childCtx, this.Session, functionsCatalog, true);
        }

        protected override Epicor.Customization.Bpm.IAsyncActionsHost2 CreateAsyncActionsHostCore(
            Erp.ErpContext context,
            Epicor.Functions.Runtime.IFunctionsCatalog functionsCatalog,
            Guid directiveId)
        {
            return null;
        }

        #endregion // BpmCustomizationFactoryBase2 Members
    }
}
