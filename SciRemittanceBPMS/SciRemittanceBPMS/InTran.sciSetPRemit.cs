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

namespace Epicor.Customization.Bpm.DBD87649F7B9004657B9FBEFDE7ABBB7EC
{
    internal sealed class InTranDirective_sciSetPRemit_6D6E29F0433E4486BBF8286A093AB8A8 : CustCntTriggerDirective
    {
        public InTranDirective_sciSetPRemit_6D6E29F0433E4486BBF8286A093AB8A8(Epicor.Customization.Bpm.TriggerBase<Erp.ErpContext, Epicor.Hosting.Session, CustCntTempRow> owner, Erp.ErpContext ctx, Epicor.Hosting.Session session)
            : base(
                owner,
                ctx,
                session,
                new Epicor.Customization.Bpm.DirectiveDescription
                {
                    Id = new Guid("6d6e29f0-433e-4486-bbf8-286a093ab8a8"),
                    Name = "sciSetPRemit",
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

start: // Name = "Execute Custom Code 0", Id = "6d42dd5e-bb13-41f2-ae97-c150e6e2a809"
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
            if (ttCustCnt == null || ttCustCnt.Count <= 0)
            {
                return;
            }

            CustCntTempRow updatedContact = ttCustCnt.Where(r => r.Updated() == true || r.Added() == true).FirstOrDefault();

            if (updatedContact == null)
            {
                return;
            }

            CustCntTempRow unchangedContact = ttCustCnt.Where(r => r.Unchanged() == true && r.CustNum == updatedContact.CustNum && r.SysRowID == updatedContact.SysRowID).FirstOrDefault();

            if (updatedContact.Added() == true || (updatedContact.Updated() == true && updatedContact.UDField<bool>("Remittance_c") == true && updatedContact.UDField<bool>("Remittance_c") != unchangedContact.UDField<bool>("Remittance_c")))
            {
                using (var context = Ice.IceContext.CreateDefaultTransactionScope())
                {
                    List<CustCnt> contactList = Db.CustCnt.Where(r => r.CustNum == updatedContact.CustNum && r.SysRowID != updatedContact.SysRowID).ToList();

                    foreach (CustCnt currContact in contactList)
                    {
                        if (currContact.UDField<bool>("Remittance_c") == true)
                        {
                            currContact.SetUDField<bool>("Remittance_c", false);
                        }
                    }

                    Customer cust = Db.Customer.Where(c => c.CustNum == updatedContact.CustNum).FirstOrDefault();

                    if (cust != null)
                    {
                        cust.SetUDField<int>("PRemit_c", updatedContact.ConNum);
                    }

                    Db.Validate();
                    context.Complete();
                }
            }
        }
    }
}
