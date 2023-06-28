using System;
using System.Linq;
using System.Collections.Generic;

using Ice;

using Erp.Bpm.TempTables;
using Erp.Tables;

namespace Epicor.Customization.Bpm.DBD87649F7B9004657B9FBEFDE7ABBB7EC
{
    // ---------------------------------------------------------------------------------------------------------------
    //                                              Directive base type
    // ---------------------------------------------------------------------------------------------------------------

    internal abstract partial class CustCntTriggerDirective : Epicor.Customization.Bpm.TriggerDirectiveBase<Erp.ErpContext, Epicor.Hosting.Session, CustCntTempRow>
    {
        #region Data members

        protected readonly Epicor.Customization.Bpm.EFTableWithFilter<CustCntTempRow> ttCustCntHolder = new Epicor.Customization.Bpm.EFTableWithFilter<CustCntTempRow>();

        #endregion // Data members

        #region Table accessors

        protected List<CustCntTempRow> ttCustCnt
        {
            get { return this.ttCustCntHolder.Get(this.UseDataFilter); }
        }

        #endregion // Table accessors

        protected CustCntTriggerDirective(
            Epicor.Customization.Bpm.TriggerBase<Erp.ErpContext, Epicor.Hosting.Session, CustCntTempRow> owner,
            Erp.ErpContext context,
            Epicor.Hosting.Session session,
            Epicor.Customization.Bpm.DirectiveDescription description)
            : base(owner, context, session, description)
        {
            this.RegisterFilters(this.ttCustCntHolder);
        }

        protected override void Initialize(Epicor.Customization.Bpm.TriggerParameters<CustCntTempRow> parameters, bool preparation)
        {
            this.ttCustCntHolder.Attach(parameters.table, asCurrent: false);
        }

        protected override void Teardown(Epicor.Customization.Bpm.TriggerParameters<CustCntTempRow> parameters)
        {
            parameters.table = this.ttCustCntHolder.Original;
            this.Db.Validate();
        }

        protected override void StoreArguments(Epicor.Customization.Bpm.ISerializationBufferBuilder builder, bool bpmFormMode, bool cca = true)
        {
            if (bpmFormMode)
            {
                throw new Ice.Common.BusinessObjectException("BPM Forms are not supported in data directives.");
            }

            builder
                .AddArgument("ttCustCnt", this.ttCustCnt, byRef: false);
        }
    }
}
