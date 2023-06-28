using Epicor.Customization.Bpm;
using Epicor.Data;
using Epicor.Hosting;
using Epicor.Utilities;
using Erp.Bpm.TempTables;
using Erp.Tables;
using Ice;
using Ice.Bpm.TempTables;
using Ice.ExtendedData;
using Ice.Tables;
using Ice.Tablesets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;

namespace Epicor.Customization.Bpm.DB77A5A4B4EE6C4ABAB9A5A5D1A93265CF
{
    internal sealed class InTranDirective_sciSetVendRemittance_399F00713B2941828F8AEA77E6F534CB : VendCntTriggerDirective
    {
        public InTranDirective_sciSetVendRemittance_399F00713B2941828F8AEA77E6F534CB(Epicor.Customization.Bpm.TriggerBase<Erp.ErpContext, Epicor.Hosting.Session, VendCntTempRow> owner, Erp.ErpContext ctx, Epicor.Hosting.Session session)
            : base(
                owner,
                ctx,
                session,
                new Epicor.Customization.Bpm.DirectiveDescription
                {
                    Id = new Guid("399f0071-3b29-4182-8f8a-ea77e6f534cb"),
                    Name = "sciSetVendRemittance",
                    Type = Ice.BO.BpMethod.DataDirectiveType.InTrans,
                    TypeName = "InTrans",
                    VisibilityScope = Ice.BO.BpMethod.DirectiveScope.CompanySpecific,
                    Company = "SCI01",
                    TenantId = null,
                })
        {
        }

        protected override bool ExecuteCore()
        {
            var conditionBlockValue = false;

start: // Name = "Execute Custom Code 0", Id = "b907c7a3-8201-4390-9971-f5bf05d314bb"
            this.UseDataFilter = true;
            try
            {
                this.A001_CustomCodeAction();
            }
            catch (Ice.Common.BusinessObjectException ex)
            {
                this.RememberException(ex);
            }
            this.RefreshData(matched: false);

finish:
            return true;
        }

        private void A001_CustomCodeAction()
        {
            // CUSTOM CODE START
            if (ttVendCnt == null || ttVendCnt.Count <= 0)
            {
                return;
            }

            VendCntTempRow updatedContact = ttVendCnt.Where(r => r.Updated() == true || r.Added() == true).FirstOrDefault();

            if (updatedContact == null)
            {
                return;
            }

            VendCntTempRow unchangedContact = ttVendCnt.Where(r => r.Unchanged() == true && r.VendorNum == updatedContact.VendorNum && r.SysRowID == updatedContact.SysRowID).FirstOrDefault();

            if (updatedContact.Added() == true || (updatedContact.Updated() == true && updatedContact.UDField<bool>("Remittance_c") == true && updatedContact.UDField<bool>("Remittance_c") != unchangedContact.UDField<bool>("Remittance_c")))
            {
                using (var context = Ice.IceContext.CreateDefaultTransactionScope())
                {
                    List<VendCnt> contactList = Db.VendCnt.Where(r => r.VendorNum == updatedContact.VendorNum && r.SysRowID != updatedContact.SysRowID).ToList();

                    foreach (VendCnt currContact in contactList)
                    {
                        if (currContact.UDField<bool>("Remittance_c") == true)
                        {
                            currContact.SetUDField<bool>("Remittance_c", false);
                        }
                    }

                    Vendor vend = Db.Vendor.Where(c => c.VendorNum == updatedContact.VendorNum).FirstOrDefault();

                    if (vend != null)
                    {
                        vend.SetUDField<int>("PRemit_c", updatedContact.ConNum);
                    }

                    Db.Validate();
                    context.Complete();
                }
            }


        }
    }
}
