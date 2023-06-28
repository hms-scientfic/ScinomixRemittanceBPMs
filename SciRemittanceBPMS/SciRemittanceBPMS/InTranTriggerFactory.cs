using System;
using System.Collections.Generic;

namespace Epicor.Customization.Bpm.DBD87649F7B9004657B9FBEFDE7ABBB7EC
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
