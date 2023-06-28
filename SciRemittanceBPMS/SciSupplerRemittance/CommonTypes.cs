using System;
using System.Linq;
using System.Collections.Generic;

using Ice;

using Erp.Bpm.TempTables;
using Erp.Tables;

namespace Epicor.Customization.Bpm.DB77A5A4B4EE6C4ABAB9A5A5D1A93265CF
{
    // ---------------------------------------------------------------------------------------------------------------
    //                                              Directive base type
    // ---------------------------------------------------------------------------------------------------------------

    internal abstract partial class VendCntTriggerDirective : Epicor.Customization.Bpm.TriggerDirectiveBase<Erp.ErpContext, Epicor.Hosting.Session, VendCntTempRow>
    {
        #region Data members

        protected readonly Epicor.Customization.Bpm.EFTableWithFilter<VendCntTempRow> ttVendCntHolder = new Epicor.Customization.Bpm.EFTableWithFilter<VendCntTempRow>();

        #endregion // Data members

        #region Table accessors

        protected List<VendCntTempRow> ttVendCnt
        {
            get { return this.ttVendCntHolder.Get(this.UseDataFilter); }
        }

        #endregion // Table accessors

        protected VendCntTriggerDirective(
            Epicor.Customization.Bpm.TriggerBase<Erp.ErpContext, Epicor.Hosting.Session, VendCntTempRow> owner,
            Erp.ErpContext context,
            Epicor.Hosting.Session session,
            Epicor.Customization.Bpm.DirectiveDescription description)
            : base(owner, context, session, description)
        {
            this.RegisterFilters(this.ttVendCntHolder);
        }

        protected override void Initialize(Epicor.Customization.Bpm.TriggerParameters<VendCntTempRow> parameters, bool preparation)
        {
            this.ttVendCntHolder.Attach(parameters.table, asCurrent: false);
        }

        protected override void Teardown(Epicor.Customization.Bpm.TriggerParameters<VendCntTempRow> parameters)
        {
            parameters.table = this.ttVendCntHolder.Original;
            this.Db.Validate();
        }

        protected override void StoreArguments(Epicor.Customization.Bpm.ISerializationBufferBuilder builder, bool bpmFormMode, bool cca = true)
        {
            if (bpmFormMode)
            {
                throw new Ice.Common.BusinessObjectException("BPM Forms are not supported in data directives.");
            }

            builder
                .AddArgument("ttVendCnt", this.ttVendCnt, byRef: false);
        }
    }
}
