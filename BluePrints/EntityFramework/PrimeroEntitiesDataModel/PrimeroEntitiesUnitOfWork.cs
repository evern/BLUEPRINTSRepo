﻿using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using BluePrints.Common.Utils;
using BluePrints.Common.DataModel;
using BluePrints.Common.DataModel.DesignTime;
using BluePrints.Common.DataModel.EntityFramework;
using BluePrints.PrimeroData;

namespace BluePrints.PrimeroData.PrimeroEntitiesDataModel
{
    /// <summary>
    /// A PrimeroEntitiesUnitOfWork instance that represents the run-time implementation of the IPrimeroEntitiesUnitOfWork interface.
    /// </summary>
    public class PrimeroEntitiesUnitOfWork : DbUnitOfWork<PrimeroEntities>, IPrimeroEntitiesUnitOfWork
    {
        public PrimeroEntitiesUnitOfWork(Func<PrimeroEntities> contextFactory)
            : base(contextFactory)
        {
        }

        IRepository<ACCS_BALANCE, int> IPrimeroEntitiesUnitOfWork.ACCS_BALANCE
        {
            get { return GetRepository(x => x.Set<ACCS_BALANCE>(), (ACCS_BALANCE x) => x.ACCNO); }
        }

        IRepository<ADJUSTMENT_TYPES, int> IPrimeroEntitiesUnitOfWork.ADJUSTMENT_TYPES
        {
            get { return GetRepository(x => x.Set<ADJUSTMENT_TYPES>(), (ADJUSTMENT_TYPES x) => x.ATNO); }
        }

        IRepository<ADVERT_TYPES, int> IPrimeroEntitiesUnitOfWork.ADVERT_TYPES
        {
            get { return GetRepository(x => x.Set<ADVERT_TYPES>(), (ADVERT_TYPES x) => x.SEQNO); }
        }

        IRepository<ANALYSIS, int> IPrimeroEntitiesUnitOfWork.ANALYSIS
        {
            get { return GetRepository(x => x.Set<ANALYSIS>(), (ANALYSIS x) => x.SEQNO); }
        }

        IRepository<ANALYSIS_CODES, int> IPrimeroEntitiesUnitOfWork.ANALYSIS_CODES
        {
            get { return GetRepository(x => x.Set<ANALYSIS_CODES>(), (ANALYSIS_CODES x) => x.CODESEQNO); }
        }

        IRepository<ANALYSIS_MATRIX, Tuple<int, int, string>> IPrimeroEntitiesUnitOfWork.ANALYSIS_MATRIX
        {
            get { return GetRepository(x => x.Set<ANALYSIS_MATRIX>(), (ANALYSIS_MATRIX x) => Tuple.Create(x.CODE_SET_SEQNO, x.TRAN_SEQNO, x.TRAN_TYPE)); }
        }

        IRepository<ANALYSIS_TYPES, string> IPrimeroEntitiesUnitOfWork.ANALYSIS_TYPES
        {
            get { return GetRepository(x => x.Set<ANALYSIS_TYPES>(), (ANALYSIS_TYPES x) => x.TRAN_TYPE); }
        }

        IRepository<AUDIT_TRAIL, int> IPrimeroEntitiesUnitOfWork.AUDIT_TRAIL
        {
            get { return GetRepository(x => x.Set<AUDIT_TRAIL>(), (AUDIT_TRAIL x) => x.SEQNO); }
        }

        IRepository<AUDITOR_EVENTS, int> IPrimeroEntitiesUnitOfWork.AUDITOR_EVENTS
        {
            get { return GetRepository(x => x.Set<AUDITOR_EVENTS>(), (AUDITOR_EVENTS x) => x.EVENTID); }
        }

        IRepository<AUDITOR_SAVEPROCS, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.AUDITOR_SAVEPROCS
        {
            get { return GetRepository(x => x.Set<AUDITOR_SAVEPROCS>(), (AUDITOR_SAVEPROCS x) => Tuple.Create(x.BRANCHNO, x.SAVEID)); }
        }

        IRepository<AUDITOR_SETTINGS, Tuple<int, string, int, int, int, int>> IPrimeroEntitiesUnitOfWork.AUDITOR_SETTINGS
        {
            get { return GetRepository(x => x.Set<AUDITOR_SETTINGS>(), (AUDITOR_SETTINGS x) => Tuple.Create(x.BRANCHNO, x.TERMINALID, x.APPLICATIONID, x.STAFFNO, x.CLASSID, x.EVENTID)); }
        }

        IRepository<AUDITOR_TERMINALS, Tuple<int, string>> IPrimeroEntitiesUnitOfWork.AUDITOR_TERMINALS
        {
            get { return GetRepository(x => x.Set<AUDITOR_TERMINALS>(), (AUDITOR_TERMINALS x) => Tuple.Create(x.BRANCHNO, x.TERMINALID)); }
        }

        IRepository<AUTO_PURCH, Tuple<string, int, int>> IPrimeroEntitiesUnitOfWork.AUTO_PURCH
        {
            get { return GetRepository(x => x.Set<AUTO_PURCH>(), (AUTO_PURCH x) => Tuple.Create(x.STOCKCODE, x.COMPUTER_SEQNO, x.ACCNO)); }
        }

        IRepository<BANK_BATCH_PRINT, int> IPrimeroEntitiesUnitOfWork.BANK_BATCH_PRINT
        {
            get { return GetRepository(x => x.Set<BANK_BATCH_PRINT>(), (BANK_BATCH_PRINT x) => x.AccNo); }
        }

        IRepository<BANK_DATA_FORMAT, int> IPrimeroEntitiesUnitOfWork.BANK_DATA_FORMAT
        {
            get { return GetRepository(x => x.Set<BANK_DATA_FORMAT>(), (BANK_DATA_FORMAT x) => x.SEQNO); }
        }

        IRepository<BANK_DATA_TYPE, int> IPrimeroEntitiesUnitOfWork.BANK_DATA_TYPE
        {
            get { return GetRepository(x => x.Set<BANK_DATA_TYPE>(), (BANK_DATA_TYPE x) => x.DATA_TYPE); }
        }

        IRepository<BANK_REC_AUTO_MATCH, int> IPrimeroEntitiesUnitOfWork.BANK_REC_AUTO_MATCH
        {
            get { return GetRepository(x => x.Set<BANK_REC_AUTO_MATCH>(), (BANK_REC_AUTO_MATCH x) => x.GLSEQNO); }
        }

        IRepository<BANK_REC_LOG, int> IPrimeroEntitiesUnitOfWork.BANK_REC_LOG
        {
            get { return GetRepository(x => x.Set<BANK_REC_LOG>(), (BANK_REC_LOG x) => x.SEQNO); }
        }

        IRepository<BANK_REC_SETUP, int> IPrimeroEntitiesUnitOfWork.BANK_REC_SETUP
        {
            get { return GetRepository(x => x.Set<BANK_REC_SETUP>(), (BANK_REC_SETUP x) => x.Code); }
        }

        IRepository<BANK_REC_UPLOADS, int> IPrimeroEntitiesUnitOfWork.BANK_REC_UPLOADS
        {
            get { return GetRepository(x => x.Set<BANK_REC_UPLOADS>(), (BANK_REC_UPLOADS x) => x.SEQNO); }
        }

        IRepository<BANK_TEMPLATES, int> IPrimeroEntitiesUnitOfWork.BANK_TEMPLATES
        {
            get { return GetRepository(x => x.Set<BANK_TEMPLATES>(), (BANK_TEMPLATES x) => x.SEQNO); }
        }

        IRepository<BANKBATCH_HDR, int> IPrimeroEntitiesUnitOfWork.BANKBATCH_HDR
        {
            get { return GetRepository(x => x.Set<BANKBATCH_HDR>(), (BANKBATCH_HDR x) => x.BATCHNO); }
        }

        IRepository<BANKS, int> IPrimeroEntitiesUnitOfWork.BANKS
        {
            get { return GetRepository(x => x.Set<BANKS>(), (BANKS x) => x.SEQNO); }
        }

        IRepository<BATCH_QUANTITIES, int> IPrimeroEntitiesUnitOfWork.BATCH_QUANTITIES
        {
            get { return GetRepository(x => x.Set<BATCH_QUANTITIES>(), (BATCH_QUANTITIES x) => x.SEQNO); }
        }

        IRepository<BATCH_STOCKTAKE, int> IPrimeroEntitiesUnitOfWork.BATCH_STOCKTAKE
        {
            get { return GetRepository(x => x.Set<BATCH_STOCKTAKE>(), (BATCH_STOCKTAKE x) => x.SEQNO); }
        }

        IRepository<BILLOMAT_HDR, string> IPrimeroEntitiesUnitOfWork.BILLOMAT_HDR
        {
            get { return GetRepository(x => x.Set<BILLOMAT_HDR>(), (BILLOMAT_HDR x) => x.BILLCODE); }
        }

        IRepository<BILLOMAT_LINES, int> IPrimeroEntitiesUnitOfWork.BILLOMAT_LINES
        {
            get { return GetRepository(x => x.Set<BILLOMAT_LINES>(), (BILLOMAT_LINES x) => x.SEQNO); }
        }

        IRepository<BILLOMAT_TEMP, int> IPrimeroEntitiesUnitOfWork.BILLOMAT_TEMP
        {
            get { return GetRepository(x => x.Set<BILLOMAT_TEMP>(), (BILLOMAT_TEMP x) => x.SEQNO); }
        }

        IRepository<BPAY_TYPES, int> IPrimeroEntitiesUnitOfWork.BPAY_TYPES
        {
            get { return GetRepository(x => x.Set<BPAY_TYPES>(), (BPAY_TYPES x) => x.SEQNO); }
        }

        IRepository<BRANCHES, int> IPrimeroEntitiesUnitOfWork.BRANCHES
        {
            get { return GetRepository(x => x.Set<BRANCHES>(), (BRANCHES x) => x.BRANCHNO); }
        }

        IRepository<CAMPAIGN, int> IPrimeroEntitiesUnitOfWork.CAMPAIGN
        {
            get { return GetRepository(x => x.Set<CAMPAIGN>(), (CAMPAIGN x) => x.SEQNO); }
        }

        IReadOnlyRepository<CAMPAIGN_CONTACT_LISTS> IPrimeroEntitiesUnitOfWork.CAMPAIGN_CONTACT_LISTS
        {
            get { return GetReadOnlyRepository(x => x.Set<CAMPAIGN_CONTACT_LISTS>()); }
        }

        IRepository<CAMPAIGN_HIST, int> IPrimeroEntitiesUnitOfWork.CAMPAIGN_HIST
        {
            get { return GetRepository(x => x.Set<CAMPAIGN_HIST>(), (CAMPAIGN_HIST x) => x.SEQNO); }
        }

        IRepository<CAMPAIGN_STAGE, int> IPrimeroEntitiesUnitOfWork.CAMPAIGN_STAGE
        {
            get { return GetRepository(x => x.Set<CAMPAIGN_STAGE>(), (CAMPAIGN_STAGE x) => x.SEQNO); }
        }

        IRepository<CAMPAIGN_STAGE_CONSTRAINT, int> IPrimeroEntitiesUnitOfWork.CAMPAIGN_STAGE_CONSTRAINT
        {
            get { return GetRepository(x => x.Set<CAMPAIGN_STAGE_CONSTRAINT>(), (CAMPAIGN_STAGE_CONSTRAINT x) => x.SEQNO); }
        }

        IRepository<CAMPAIGN_TYPE, int> IPrimeroEntitiesUnitOfWork.CAMPAIGN_TYPE
        {
            get { return GetRepository(x => x.Set<CAMPAIGN_TYPE>(), (CAMPAIGN_TYPE x) => x.SEQNO); }
        }

        IRepository<CAMPAIGN_WAVE, int> IPrimeroEntitiesUnitOfWork.CAMPAIGN_WAVE
        {
            get { return GetRepository(x => x.Set<CAMPAIGN_WAVE>(), (CAMPAIGN_WAVE x) => x.SEQNO); }
        }

        IRepository<CAMPAIGN_WAVE_AUDIT, Tuple<int, int, int, DateTime>> IPrimeroEntitiesUnitOfWork.CAMPAIGN_WAVE_AUDIT
        {
            get { return GetRepository(x => x.Set<CAMPAIGN_WAVE_AUDIT>(), (CAMPAIGN_WAVE_AUDIT x) => Tuple.Create(x.SEQNO, x.CAMPAIGN_WAVE_SEQNO, x.CONTACT_SEQNO, x.LOGDATETIME)); }
        }

        IReadOnlyRepository<CAMPAIGN_WAVE_CONTACT_LISTS> IPrimeroEntitiesUnitOfWork.CAMPAIGN_WAVE_CONTACT_LISTS
        {
            get { return GetReadOnlyRepository(x => x.Set<CAMPAIGN_WAVE_CONTACT_LISTS>()); }
        }

        IRepository<CASHBOOKHEADER, int> IPrimeroEntitiesUnitOfWork.CASHBOOKHEADER
        {
            get { return GetRepository(x => x.Set<CASHBOOKHEADER>(), (CASHBOOKHEADER x) => x.SEQNO); }
        }

        IRepository<CASHBOOKLINE, int> IPrimeroEntitiesUnitOfWork.CASHBOOKLINE
        {
            get { return GetRepository(x => x.Set<CASHBOOKLINE>(), (CASHBOOKLINE x) => x.SEQNO); }
        }

        IRepository<CASHBOOKLINEALLOC, int> IPrimeroEntitiesUnitOfWork.CASHBOOKLINEALLOC
        {
            get { return GetRepository(x => x.Set<CASHBOOKLINEALLOC>(), (CASHBOOKLINEALLOC x) => x.SEQNO); }
        }

        IRepository<CHECKLIST_WIDGET_NODES, int> IPrimeroEntitiesUnitOfWork.CHECKLIST_WIDGET_NODES
        {
            get { return GetRepository(x => x.Set<CHECKLIST_WIDGET_NODES>(), (CHECKLIST_WIDGET_NODES x) => x.SEQNO); }
        }

        IRepository<CHEQUE_AUDIT, int> IPrimeroEntitiesUnitOfWork.CHEQUE_AUDIT
        {
            get { return GetRepository(x => x.Set<CHEQUE_AUDIT>(), (CHEQUE_AUDIT x) => x.CHQNO); }
        }

        IRepository<CHEQUE_GEN_INFO, Tuple<int, int, int, string>> IPrimeroEntitiesUnitOfWork.CHEQUE_GEN_INFO
        {
            get { return GetRepository(x => x.Set<CHEQUE_GEN_INFO>(), (CHEQUE_GEN_INFO x) => Tuple.Create(x.PPVERSION, x.CHQMIN, x.CHQMAX, x.USEMAILSHOT)); }
        }

        IRepository<CHEQUE_PRINT, Tuple<int, string, double, double>> IPrimeroEntitiesUnitOfWork.CHEQUE_PRINT
        {
            get { return GetRepository(x => x.Set<CHEQUE_PRINT>(), (CHEQUE_PRINT x) => Tuple.Create(x.ChequeNo, x.InvNo, x.WHTAXRATE, x.WHTAXAMOUNT)); }
        }

        IRepository<CHEQUE_PRINT_HDR, int> IPrimeroEntitiesUnitOfWork.CHEQUE_PRINT_HDR
        {
            get { return GetRepository(x => x.Set<CHEQUE_PRINT_HDR>(), (CHEQUE_PRINT_HDR x) => x.CHEQUENO); }
        }

        IRepository<CL_END_OF_PERIOD, int> IPrimeroEntitiesUnitOfWork.CL_END_OF_PERIOD
        {
            get { return GetRepository(x => x.Set<CL_END_OF_PERIOD>(), (CL_END_OF_PERIOD x) => x.SEQNO); }
        }

        IRepository<CL_END_OF_YEAR, int> IPrimeroEntitiesUnitOfWork.CL_END_OF_YEAR
        {
            get { return GetRepository(x => x.Set<CL_END_OF_YEAR>(), (CL_END_OF_YEAR x) => x.SEQNO); }
        }

        IRepository<CL_FINANCIAL_REVIEW, int> IPrimeroEntitiesUnitOfWork.CL_FINANCIAL_REVIEW
        {
            get { return GetRepository(x => x.Set<CL_FINANCIAL_REVIEW>(), (CL_FINANCIAL_REVIEW x) => x.SEQNO); }
        }

        IRepository<CL_SYSTEMS_INTEGRITY, int> IPrimeroEntitiesUnitOfWork.CL_SYSTEMS_INTEGRITY
        {
            get { return GetRepository(x => x.Set<CL_SYSTEMS_INTEGRITY>(), (CL_SYSTEMS_INTEGRITY x) => x.SEQNO); }
        }

        IRepository<CL_TAX_RETURN, int> IPrimeroEntitiesUnitOfWork.CL_TAX_RETURN
        {
            get { return GetRepository(x => x.Set<CL_TAX_RETURN>(), (CL_TAX_RETURN x) => x.SEQNO); }
        }

        IRepository<ClarityReport, int> IPrimeroEntitiesUnitOfWork.ClarityReport
        {
            get { return GetRepository(x => x.Set<ClarityReport>(), (ClarityReport x) => x.SEQNO); }
        }

        IRepository<COMMON_PHRASES, int> IPrimeroEntitiesUnitOfWork.COMMON_PHRASES
        {
            get { return GetRepository(x => x.Set<COMMON_PHRASES>(), (COMMON_PHRASES x) => x.SEQNO); }
        }

        IRepository<COMMUNICATION_PROCESSES, int> IPrimeroEntitiesUnitOfWork.COMMUNICATION_PROCESSES
        {
            get { return GetRepository(x => x.Set<COMMUNICATION_PROCESSES>(), (COMMUNICATION_PROCESSES x) => x.SEQNO); }
        }

        IRepository<COMPANY_TYPES, int> IPrimeroEntitiesUnitOfWork.COMPANY_TYPES
        {
            get { return GetRepository(x => x.Set<COMPANY_TYPES>(), (COMPANY_TYPES x) => x.SEQNO); }
        }

        IRepository<COMPUTER, int> IPrimeroEntitiesUnitOfWork.COMPUTER
        {
            get { return GetRepository(x => x.Set<COMPUTER>(), (COMPUTER x) => x.SEQNO); }
        }

        IRepository<CONTACT_HIST, int> IPrimeroEntitiesUnitOfWork.CONTACT_HIST
        {
            get { return GetRepository(x => x.Set<CONTACT_HIST>(), (CONTACT_HIST x) => x.SEQNO); }
        }

        IRepository<CONTACT_LIST, int> IPrimeroEntitiesUnitOfWork.CONTACT_LIST
        {
            get { return GetRepository(x => x.Set<CONTACT_LIST>(), (CONTACT_LIST x) => x.SEQNO); }
        }

        IRepository<CONTACT_LIST_ITEM, int> IPrimeroEntitiesUnitOfWork.CONTACT_LIST_ITEM
        {
            get { return GetRepository(x => x.Set<CONTACT_LIST_ITEM>(), (CONTACT_LIST_ITEM x) => x.SEQNO); }
        }

        IRepository<CONTACT_LIST_TYPE, int> IPrimeroEntitiesUnitOfWork.CONTACT_LIST_TYPE
        {
            get { return GetRepository(x => x.Set<CONTACT_LIST_TYPE>(), (CONTACT_LIST_TYPE x) => x.SEQNO); }
        }

        IRepository<CONTACT_MARKETING_CLASSES, int> IPrimeroEntitiesUnitOfWork.CONTACT_MARKETING_CLASSES
        {
            get { return GetRepository(x => x.Set<CONTACT_MARKETING_CLASSES>(), (CONTACT_MARKETING_CLASSES x) => x.CLASSNO); }
        }

        IRepository<CONTACTS, int> IPrimeroEntitiesUnitOfWork.CONTACTS
        {
            get { return GetRepository(x => x.Set<CONTACTS>(), (CONTACTS x) => x.SEQNO); }
        }

        IRepository<COUNTRY, string> IPrimeroEntitiesUnitOfWork.COUNTRY
        {
            get { return GetRepository(x => x.Set<COUNTRY>(), (COUNTRY x) => x.COUNTRY_CODE); }
        }

        IRepository<Courier_Location_Depot, int> IPrimeroEntitiesUnitOfWork.Courier_Location_Depot
        {
            get { return GetRepository(x => x.Set<Courier_Location_Depot>(), (Courier_Location_Depot x) => x.SeqNo); }
        }

        IRepository<COURIER_MANIFEST, int> IPrimeroEntitiesUnitOfWork.COURIER_MANIFEST
        {
            get { return GetRepository(x => x.Set<COURIER_MANIFEST>(), (COURIER_MANIFEST x) => x.SEQNO); }
        }

        IRepository<COURIERS, int> IPrimeroEntitiesUnitOfWork.COURIERS
        {
            get { return GetRepository(x => x.Set<COURIERS>(), (COURIERS x) => x.SEQNO); }
        }

        IRepository<CR_ACCGROUP2S, int> IPrimeroEntitiesUnitOfWork.CR_ACCGROUP2S
        {
            get { return GetRepository(x => x.Set<CR_ACCGROUP2S>(), (CR_ACCGROUP2S x) => x.ACCGROUP); }
        }

        IRepository<CR_ACCGROUPS, int> IPrimeroEntitiesUnitOfWork.CR_ACCGROUPS
        {
            get { return GetRepository(x => x.Set<CR_ACCGROUPS>(), (CR_ACCGROUPS x) => x.ACCGROUP); }
        }

        IRepository<CR_ACCS, int> IPrimeroEntitiesUnitOfWork.CR_ACCS
        {
            get { return GetRepository(x => x.Set<CR_ACCS>(), (CR_ACCS x) => x.ACCNO); }
        }

        IRepository<CR_ALLOCATIONS, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.CR_ALLOCATIONS
        {
            get { return GetRepository(x => x.Set<CR_ALLOCATIONS>(), (CR_ALLOCATIONS x) => Tuple.Create(x.SEQNO, x.ALLOCNO)); }
        }

        IRepository<CR_CONT_HIST, int> IPrimeroEntitiesUnitOfWork.CR_CONT_HIST
        {
            get { return GetRepository(x => x.Set<CR_CONT_HIST>(), (CR_CONT_HIST x) => x.SEQNO); }
        }

        IRepository<CR_CONTACTS, int> IPrimeroEntitiesUnitOfWork.CR_CONTACTS
        {
            get { return GetRepository(x => x.Set<CR_CONTACTS>(), (CR_CONTACTS x) => x.SEQNO); }
        }

        IRepository<CR_INVLINES, int> IPrimeroEntitiesUnitOfWork.CR_INVLINES
        {
            get { return GetRepository(x => x.Set<CR_INVLINES>(), (CR_INVLINES x) => x.SEQNO); }
        }

        IRepository<CR_INVLINES_SERIALS, int> IPrimeroEntitiesUnitOfWork.CR_INVLINES_SERIALS
        {
            get { return GetRepository(x => x.Set<CR_INVLINES_SERIALS>(), (CR_INVLINES_SERIALS x) => x.SEQNO); }
        }

        IRepository<CR_LIST_NAME, int> IPrimeroEntitiesUnitOfWork.CR_LIST_NAME
        {
            get { return GetRepository(x => x.Set<CR_LIST_NAME>(), (CR_LIST_NAME x) => x.LIST_NO); }
        }

        IRepository<CR_LISTS, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.CR_LISTS
        {
            get { return GetRepository(x => x.Set<CR_LISTS>(), (CR_LISTS x) => Tuple.Create(x.LIST_NO, x.ACCNO)); }
        }

        IRepository<CR_PRICES, int> IPrimeroEntitiesUnitOfWork.CR_PRICES
        {
            get { return GetRepository(x => x.Set<CR_PRICES>(), (CR_PRICES x) => x.SEQNO); }
        }

        IRepository<CR_TRANS, int> IPrimeroEntitiesUnitOfWork.CR_TRANS
        {
            get { return GetRepository(x => x.Set<CR_TRANS>(), (CR_TRANS x) => x.SEQNO); }
        }

        IRepository<CREDIT_STATUSES, int> IPrimeroEntitiesUnitOfWork.CREDIT_STATUSES
        {
            get { return GetRepository(x => x.Set<CREDIT_STATUSES>(), (CREDIT_STATUSES x) => x.STATUSNO); }
        }

        IRepository<CRM_BUDGET, int> IPrimeroEntitiesUnitOfWork.CRM_BUDGET
        {
            get { return GetRepository(x => x.Set<CRM_BUDGET>(), (CRM_BUDGET x) => x.SEQNO); }
        }

        IRepository<CRM_BUDGET_HDR, int> IPrimeroEntitiesUnitOfWork.CRM_BUDGET_HDR
        {
            get { return GetRepository(x => x.Set<CRM_BUDGET_HDR>(), (CRM_BUDGET_HDR x) => x.SEQNO); }
        }

        IRepository<CRM_BUDGET_LINE, int> IPrimeroEntitiesUnitOfWork.CRM_BUDGET_LINE
        {
            get { return GetRepository(x => x.Set<CRM_BUDGET_LINE>(), (CRM_BUDGET_LINE x) => x.SEQNO); }
        }

        IRepository<CURRENCIES, int> IPrimeroEntitiesUnitOfWork.CURRENCIES
        {
            get { return GetRepository(x => x.Set<CURRENCIES>(), (CURRENCIES x) => x.CURRENCYNO); }
        }

        IRepository<CURRENCY_CLOSING_RATES, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.CURRENCY_CLOSING_RATES
        {
            get { return GetRepository(x => x.Set<CURRENCY_CLOSING_RATES>(), (CURRENCY_CLOSING_RATES x) => Tuple.Create(x.CURRENCYNO, x.PERIOD_SEQNO)); }
        }

        IRepository<CURRENCY_RATECHANGES, int> IPrimeroEntitiesUnitOfWork.CURRENCY_RATECHANGES
        {
            get { return GetRepository(x => x.Set<CURRENCY_RATECHANGES>(), (CURRENCY_RATECHANGES x) => x.SEQNO); }
        }

        IRepository<CUSTCLARITYREPORT, int> IPrimeroEntitiesUnitOfWork.CUSTCLARITYREPORT
        {
            get { return GetRepository(x => x.Set<CUSTCLARITYREPORT>(), (CUSTCLARITYREPORT x) => x.SEQNO); }
        }

        IRepository<CUSTOM_REPORTS, string> IPrimeroEntitiesUnitOfWork.CUSTOM_REPORTS
        {
            get { return GetRepository(x => x.Set<CUSTOM_REPORTS>(), (CUSTOM_REPORTS x) => x.REPT_CODE); }
        }

        IRepository<CUSTOM_VIEWS, int> IPrimeroEntitiesUnitOfWork.CUSTOM_VIEWS
        {
            get { return GetRepository(x => x.Set<CUSTOM_VIEWS>(), (CUSTOM_VIEWS x) => x.SEQNO); }
        }

        IRepository<CUSTOMIZATION, int> IPrimeroEntitiesUnitOfWork.CUSTOMIZATION
        {
            get { return GetRepository(x => x.Set<CUSTOMIZATION>(), (CUSTOMIZATION x) => x.SEQNO); }
        }

        IRepository<D_DEBIT_HDR, int> IPrimeroEntitiesUnitOfWork.D_DEBIT_HDR
        {
            get { return GetRepository(x => x.Set<D_DEBIT_HDR>(), (D_DEBIT_HDR x) => x.SEQNO); }
        }

        IRepository<D_DEBIT_LINES, int> IPrimeroEntitiesUnitOfWork.D_DEBIT_LINES
        {
            get { return GetRepository(x => x.Set<D_DEBIT_LINES>(), (D_DEBIT_LINES x) => x.SEQNO); }
        }

        IRepository<DASHBOARDS, int> IPrimeroEntitiesUnitOfWork.DASHBOARDS
        {
            get { return GetRepository(x => x.Set<DASHBOARDS>(), (DASHBOARDS x) => x.SEQNO); }
        }

        IRepository<DASHBOARDS_STAFF, int> IPrimeroEntitiesUnitOfWork.DASHBOARDS_STAFF
        {
            get { return GetRepository(x => x.Set<DASHBOARDS_STAFF>(), (DASHBOARDS_STAFF x) => x.SEQNO); }
        }

        IRepository<DATAEXPORT_CTRL, int> IPrimeroEntitiesUnitOfWork.DATAEXPORT_CTRL
        {
            get { return GetRepository(x => x.Set<DATAEXPORT_CTRL>(), (DATAEXPORT_CTRL x) => x.DATAEXPORT_ID); }
        }

        IRepository<DATE_RANGES, int> IPrimeroEntitiesUnitOfWork.DATE_RANGES
        {
            get { return GetRepository(x => x.Set<DATE_RANGES>(), (DATE_RANGES x) => x.SEQNO); }
        }

        IRepository<DISCOUNTS, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.DISCOUNTS
        {
            get { return GetRepository(x => x.Set<DISCOUNTS>(), (DISCOUNTS x) => Tuple.Create(x.ACC_DISC_CODE, x.STK_DISC_CODE)); }
        }

        IRepository<DISPLAY_NAMES, string> IPrimeroEntitiesUnitOfWork.DISPLAY_NAMES
        {
            get { return GetRepository(x => x.Set<DISPLAY_NAMES>(), (DISPLAY_NAMES x) => x.NAMEID); }
        }

        IRepository<DISPMETHOD, int> IPrimeroEntitiesUnitOfWork.DISPMETHOD
        {
            get { return GetRepository(x => x.Set<DISPMETHOD>(), (DISPMETHOD x) => x.SEQ_NO); }
        }

        IRepository<DOC_BATCH_HDR, int> IPrimeroEntitiesUnitOfWork.DOC_BATCH_HDR
        {
            get { return GetRepository(x => x.Set<DOC_BATCH_HDR>(), (DOC_BATCH_HDR x) => x.SEQNO); }
        }

        IRepository<DOC_BATCH_LINES, int> IPrimeroEntitiesUnitOfWork.DOC_BATCH_LINES
        {
            get { return GetRepository(x => x.Set<DOC_BATCH_LINES>(), (DOC_BATCH_LINES x) => x.SEQNO); }
        }

        IRepository<DOCUMENTS, int> IPrimeroEntitiesUnitOfWork.DOCUMENTS
        {
            get { return GetRepository(x => x.Set<DOCUMENTS>(), (DOCUMENTS x) => x.SEQNO); }
        }

        IRepository<DR_ACCGROUP2S, int> IPrimeroEntitiesUnitOfWork.DR_ACCGROUP2S
        {
            get { return GetRepository(x => x.Set<DR_ACCGROUP2S>(), (DR_ACCGROUP2S x) => x.ACCGROUP); }
        }

        IRepository<DR_ACCGROUPS, int> IPrimeroEntitiesUnitOfWork.DR_ACCGROUPS
        {
            get { return GetRepository(x => x.Set<DR_ACCGROUPS>(), (DR_ACCGROUPS x) => x.ACCGROUP); }
        }

        IRepository<DR_ACCS, int> IPrimeroEntitiesUnitOfWork.DR_ACCS
        {
            get { return GetRepository(x => x.Set<DR_ACCS>(), (DR_ACCS x) => x.ACCNO); }
        }

        IRepository<DR_ADDRESSES, int> IPrimeroEntitiesUnitOfWork.DR_ADDRESSES
        {
            get { return GetRepository(x => x.Set<DR_ADDRESSES>(), (DR_ADDRESSES x) => x.SEQNO); }
        }

        IRepository<DR_ALLOCATIONS, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.DR_ALLOCATIONS
        {
            get { return GetRepository(x => x.Set<DR_ALLOCATIONS>(), (DR_ALLOCATIONS x) => Tuple.Create(x.SEQNO, x.ALLOCNO)); }
        }

        IRepository<DR_CONT_HIST, int> IPrimeroEntitiesUnitOfWork.DR_CONT_HIST
        {
            get { return GetRepository(x => x.Set<DR_CONT_HIST>(), (DR_CONT_HIST x) => x.SEQNO); }
        }

        IRepository<DR_CONTACTS, int> IPrimeroEntitiesUnitOfWork.DR_CONTACTS
        {
            get { return GetRepository(x => x.Set<DR_CONTACTS>(), (DR_CONTACTS x) => x.SEQNO); }
        }

        IRepository<DR_INVLINES, int> IPrimeroEntitiesUnitOfWork.DR_INVLINES
        {
            get { return GetRepository(x => x.Set<DR_INVLINES>(), (DR_INVLINES x) => x.SEQNO); }
        }

        IRepository<DR_INVLINES_SERIALS, int> IPrimeroEntitiesUnitOfWork.DR_INVLINES_SERIALS
        {
            get { return GetRepository(x => x.Set<DR_INVLINES_SERIALS>(), (DR_INVLINES_SERIALS x) => x.SEQNO); }
        }

        IRepository<DR_PRICE_POLICY, int> IPrimeroEntitiesUnitOfWork.DR_PRICE_POLICY
        {
            get { return GetRepository(x => x.Set<DR_PRICE_POLICY>(), (DR_PRICE_POLICY x) => x.POLICY_HDR); }
        }

        IRepository<DR_PRICE_POLICY_ACC, int> IPrimeroEntitiesUnitOfWork.DR_PRICE_POLICY_ACC
        {
            get { return GetRepository(x => x.Set<DR_PRICE_POLICY_ACC>(), (DR_PRICE_POLICY_ACC x) => x.SEQNO); }
        }

        IRepository<DR_PRICEGROUPS, int> IPrimeroEntitiesUnitOfWork.DR_PRICEGROUPS
        {
            get { return GetRepository(x => x.Set<DR_PRICEGROUPS>(), (DR_PRICEGROUPS x) => x.GROUPNO); }
        }

        IRepository<DR_PRICES, int> IPrimeroEntitiesUnitOfWork.DR_PRICES
        {
            get { return GetRepository(x => x.Set<DR_PRICES>(), (DR_PRICES x) => x.SEQNO); }
        }

        IRepository<DR_TRANS, int> IPrimeroEntitiesUnitOfWork.DR_TRANS
        {
            get { return GetRepository(x => x.Set<DR_TRANS>(), (DR_TRANS x) => x.SEQNO); }
        }

        IRepository<dtproperties, Tuple<int, string>> IPrimeroEntitiesUnitOfWork.dtproperties
        {
            get { return GetRepository(x => x.Set<dtproperties>(), (dtproperties x) => Tuple.Create(x.id, x.property)); }
        }

        IRepository<EFT_AUDIT, int> IPrimeroEntitiesUnitOfWork.EFT_AUDIT
        {
            get { return GetRepository(x => x.Set<EFT_AUDIT>(), (EFT_AUDIT x) => x.SEQNO); }
        }

        IRepository<EXONET_CACHE_TABLES, Tuple<int, string>> IPrimeroEntitiesUnitOfWork.EXONET_CACHE_TABLES
        {
            get { return GetRepository(x => x.Set<EXONET_CACHE_TABLES>(), (EXONET_CACHE_TABLES x) => Tuple.Create(x.APPID, x.TABLENAME)); }
        }

        IRepository<EXTRA_FIELDS, int> IPrimeroEntitiesUnitOfWork.EXTRA_FIELDS
        {
            get { return GetRepository(x => x.Set<EXTRA_FIELDS>(), (EXTRA_FIELDS x) => x.SEQNO); }
        }

        IRepository<FACT_GLOSSARY, string> IPrimeroEntitiesUnitOfWork.FACT_GLOSSARY
        {
            get { return GetRepository(x => x.Set<FACT_GLOSSARY>(), (FACT_GLOSSARY x) => x.FIELDNAME); }
        }

        IRepository<FORMULAHDR, int> IPrimeroEntitiesUnitOfWork.FORMULAHDR
        {
            get { return GetRepository(x => x.Set<FORMULAHDR>(), (FORMULAHDR x) => x.SEQNO); }
        }

        IRepository<FORMULALINES, int> IPrimeroEntitiesUnitOfWork.FORMULALINES
        {
            get { return GetRepository(x => x.Set<FORMULALINES>(), (FORMULALINES x) => x.SEQNO); }
        }

        IRepository<FREIGHT_COST_TYPES, string> IPrimeroEntitiesUnitOfWork.FREIGHT_COST_TYPES
        {
            get { return GetRepository(x => x.Set<FREIGHT_COST_TYPES>(), (FREIGHT_COST_TYPES x) => x.COSTCODE); }
        }

        IRepository<FREIGHTCOST_SPREAD_TYPE, int> IPrimeroEntitiesUnitOfWork.FREIGHTCOST_SPREAD_TYPE
        {
            get { return GetRepository(x => x.Set<FREIGHTCOST_SPREAD_TYPE>(), (FREIGHTCOST_SPREAD_TYPE x) => x.SEQNO); }
        }

        IRepository<GENERAL_INFO, int> IPrimeroEntitiesUnitOfWork.GENERAL_INFO
        {
            get { return GetRepository(x => x.Set<GENERAL_INFO>(), (GENERAL_INFO x) => x.SEQNO); }
        }

        IRepository<GL_ACCGROUPS, int> IPrimeroEntitiesUnitOfWork.GL_ACCGROUPS
        {
            get { return GetRepository(x => x.Set<GL_ACCGROUPS>(), (GL_ACCGROUPS x) => x.ACCGROUP); }
        }

        IRepository<GL_ACCGRP_REPCODE, int> IPrimeroEntitiesUnitOfWork.GL_ACCGRP_REPCODE
        {
            get { return GetRepository(x => x.Set<GL_ACCGRP_REPCODE>(), (GL_ACCGRP_REPCODE x) => x.REPCODE); }
        }

        IRepository<GL_CLOSING_STOCK, Tuple<int, int, int, int>> IPrimeroEntitiesUnitOfWork.GL_CLOSING_STOCK
        {
            get { return GetRepository(x => x.Set<GL_CLOSING_STOCK>(), (GL_CLOSING_STOCK x) => Tuple.Create(x.BS_ACCNO, x.BS_SUBACCNO, x.BRANCHNO, x.PERIOD_SEQNO)); }
        }

        IRepository<GL_CONT_HIST, int> IPrimeroEntitiesUnitOfWork.GL_CONT_HIST
        {
            get { return GetRepository(x => x.Set<GL_CONT_HIST>(), (GL_CONT_HIST x) => x.SEQNO); }
        }

        IRepository<GL_CONTROL, int> IPrimeroEntitiesUnitOfWork.GL_CONTROL
        {
            get { return GetRepository(x => x.Set<GL_CONTROL>(), (GL_CONTROL x) => x.SEQNO); }
        }

        IRepository<GL_POSTBATCH, int> IPrimeroEntitiesUnitOfWork.GL_POSTBATCH
        {
            get { return GetRepository(x => x.Set<GL_POSTBATCH>(), (GL_POSTBATCH x) => x.SEQNO); }
        }

        IRepository<GL_REPORTCOLS, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.GL_REPORTCOLS
        {
            get { return GetRepository(x => x.Set<GL_REPORTCOLS>(), (GL_REPORTCOLS x) => Tuple.Create(x.SEQNO, x.REPORTNO)); }
        }

        IRepository<GL_REPORTROWS, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.GL_REPORTROWS
        {
            get { return GetRepository(x => x.Set<GL_REPORTROWS>(), (GL_REPORTROWS x) => Tuple.Create(x.SEQNO, x.REPORTNO)); }
        }

        IRepository<GL_REPORTS, int> IPrimeroEntitiesUnitOfWork.GL_REPORTS
        {
            get { return GetRepository(x => x.Set<GL_REPORTS>(), (GL_REPORTS x) => x.SEQNO); }
        }

        IRepository<GL_SJHDR, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.GL_SJHDR
        {
            get { return GetRepository(x => x.Set<GL_SJHDR>(), (GL_SJHDR x) => Tuple.Create(x.SEQNO, x.LEDGERID)); }
        }

        IRepository<GL_SJLINES, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.GL_SJLINES
        {
            get { return GetRepository(x => x.Set<GL_SJLINES>(), (GL_SJLINES x) => Tuple.Create(x.SEQNO, x.HDR_SEQNO)); }
        }

        IRepository<GL_SJPOSTRUN, int> IPrimeroEntitiesUnitOfWork.GL_SJPOSTRUN
        {
            get { return GetRepository(x => x.Set<GL_SJPOSTRUN>(), (GL_SJPOSTRUN x) => x.SEQNO); }
        }

        IRepository<GLACCS, int> IPrimeroEntitiesUnitOfWork.GLACCS
        {
            get { return GetRepository(x => x.Set<GLACCS>(), (GLACCS x) => x.ACCNO); }
        }

        IRepository<GLBATCH, int> IPrimeroEntitiesUnitOfWork.GLBATCH
        {
            get { return GetRepository(x => x.Set<GLBATCH>(), (GLBATCH x) => x.BATCHNO); }
        }

        IReadOnlyRepository<GLBATCH_NUMBERS> IPrimeroEntitiesUnitOfWork.GLBATCH_NUMBERS
        {
            get { return GetReadOnlyRepository(x => x.Set<GLBATCH_NUMBERS>()); }
        }

        IRepository<GLBUDGETS, int> IPrimeroEntitiesUnitOfWork.GLBUDGETS
        {
            get { return GetRepository(x => x.Set<GLBUDGETS>(), (GLBUDGETS x) => x.SEQNO); }
        }

        IRepository<GLBUDGETS_HDR, int> IPrimeroEntitiesUnitOfWork.GLBUDGETS_HDR
        {
            get { return GetRepository(x => x.Set<GLBUDGETS_HDR>(), (GLBUDGETS_HDR x) => x.SEQNO); }
        }

        IRepository<GLCHART_BUSINESS, int> IPrimeroEntitiesUnitOfWork.GLCHART_BUSINESS
        {
            get { return GetRepository(x => x.Set<GLCHART_BUSINESS>(), (GLCHART_BUSINESS x) => x.BUSINESSNO); }
        }

        IRepository<GLCHART_INDUSTRY, int> IPrimeroEntitiesUnitOfWork.GLCHART_INDUSTRY
        {
            get { return GetRepository(x => x.Set<GLCHART_INDUSTRY>(), (GLCHART_INDUSTRY x) => x.INDUSTRYNO); }
        }

        IRepository<GLMOVEMENTS, int> IPrimeroEntitiesUnitOfWork.GLMOVEMENTS
        {
            get { return GetRepository(x => x.Set<GLMOVEMENTS>(), (GLMOVEMENTS x) => x.SEQNO); }
        }

        IRepository<GLREPORT_BATCH, int> IPrimeroEntitiesUnitOfWork.GLREPORT_BATCH
        {
            get { return GetRepository(x => x.Set<GLREPORT_BATCH>(), (GLREPORT_BATCH x) => x.BATCHNO); }
        }

        IRepository<GLREPORT_BATCH_LINE, int> IPrimeroEntitiesUnitOfWork.GLREPORT_BATCH_LINE
        {
            get { return GetRepository(x => x.Set<GLREPORT_BATCH_LINE>(), (GLREPORT_BATCH_LINE x) => x.SEQNO); }
        }

        IRepository<GLREPORTHEADER, int> IPrimeroEntitiesUnitOfWork.GLREPORTHEADER
        {
            get { return GetRepository(x => x.Set<GLREPORTHEADER>(), (GLREPORTHEADER x) => x.REPORT_SEQNO); }
        }

        IRepository<GLSUBACCS, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.GLSUBACCS
        {
            get { return GetRepository(x => x.Set<GLSUBACCS>(), (GLSUBACCS x) => Tuple.Create(x.ACCNO, x.SUBACCNO)); }
        }

        IRepository<GLTRANS, int> IPrimeroEntitiesUnitOfWork.GLTRANS
        {
            get { return GetRepository(x => x.Set<GLTRANS>(), (GLTRANS x) => x.SEQNO); }
        }

        IRepository<GLTRANS_ARCHIVE, int> IPrimeroEntitiesUnitOfWork.GLTRANS_ARCHIVE
        {
            get { return GetRepository(x => x.Set<GLTRANS_ARCHIVE>(), (GLTRANS_ARCHIVE x) => x.SEQNO); }
        }

        IRepository<GRAPH_SERIES, int> IPrimeroEntitiesUnitOfWork.GRAPH_SERIES
        {
            get { return GetRepository(x => x.Set<GRAPH_SERIES>(), (GRAPH_SERIES x) => x.SEQNO); }
        }

        IRepository<IDENTIFIERS, Tuple<string, string>> IPrimeroEntitiesUnitOfWork.IDENTIFIERS
        {
            get { return GetRepository(x => x.Set<IDENTIFIERS>(), (IDENTIFIERS x) => Tuple.Create(x.TABLENAME, x.FIELDNAME)); }
        }

        IRepository<INV_FIELD_NAMES, int> IPrimeroEntitiesUnitOfWork.INV_FIELD_NAMES
        {
            get { return GetRepository(x => x.Set<INV_FIELD_NAMES>(), (INV_FIELD_NAMES x) => x.INVTYPE); }
        }

        IRepository<INV_NUMBERS, int> IPrimeroEntitiesUnitOfWork.INV_NUMBERS
        {
            get { return GetRepository(x => x.Set<INV_NUMBERS>(), (INV_NUMBERS x) => x.INVNO); }
        }

        IRepository<INWARDS_GOODS, int> IPrimeroEntitiesUnitOfWork.INWARDS_GOODS
        {
            get { return GetRepository(x => x.Set<INWARDS_GOODS>(), (INWARDS_GOODS x) => x.SEQNO); }
        }

        IRepository<INWARDS_GOODS_COSTS, int> IPrimeroEntitiesUnitOfWork.INWARDS_GOODS_COSTS
        {
            get { return GetRepository(x => x.Set<INWARDS_GOODS_COSTS>(), (INWARDS_GOODS_COSTS x) => x.SEQNO); }
        }

        IRepository<INWARDS_GOODS_LINES, int> IPrimeroEntitiesUnitOfWork.INWARDS_GOODS_LINES
        {
            get { return GetRepository(x => x.Set<INWARDS_GOODS_LINES>(), (INWARDS_GOODS_LINES x) => x.SEQNO); }
        }

        IRepository<JOB_CATEGORIES, int> IPrimeroEntitiesUnitOfWork.JOB_CATEGORIES
        {
            get { return GetRepository(x => x.Set<JOB_CATEGORIES>(), (JOB_CATEGORIES x) => x.CATNO); }
        }

        IRepository<JOB_CONTRACT_BILLINGS, int> IPrimeroEntitiesUnitOfWork.JOB_CONTRACT_BILLINGS
        {
            get { return GetRepository(x => x.Set<JOB_CONTRACT_BILLINGS>(), (JOB_CONTRACT_BILLINGS x) => x.SEQNO); }
        }

        IRepository<JOB_COSTGROUPS, int> IPrimeroEntitiesUnitOfWork.JOB_COSTGROUPS
        {
            get { return GetRepository(x => x.Set<JOB_COSTGROUPS>(), (JOB_COSTGROUPS x) => x.SEQNO); }
        }

        IRepository<JOB_COSTTYPES, int> IPrimeroEntitiesUnitOfWork.JOB_COSTTYPES
        {
            get { return GetRepository(x => x.Set<JOB_COSTTYPES>(), (JOB_COSTTYPES x) => x.SEQNO); }
        }

        IRepository<JOB_HIST, int> IPrimeroEntitiesUnitOfWork.JOB_HIST
        {
            get { return GetRepository(x => x.Set<JOB_HIST>(), (JOB_HIST x) => x.SEQNO); }
        }

        IRepository<JOB_OTHER_REPORTS, int> IPrimeroEntitiesUnitOfWork.JOB_OTHER_REPORTS
        {
            get { return GetRepository(x => x.Set<JOB_OTHER_REPORTS>(), (JOB_OTHER_REPORTS x) => x.REPORTNO); }
        }

        IRepository<JOB_OUTPUT_ITEMS, int> IPrimeroEntitiesUnitOfWork.JOB_OUTPUT_ITEMS
        {
            get { return GetRepository(x => x.Set<JOB_OUTPUT_ITEMS>(), (JOB_OUTPUT_ITEMS x) => x.SEQNO); }
        }

        IRepository<JOB_QUOTE_OPTIONS, int> IPrimeroEntitiesUnitOfWork.JOB_QUOTE_OPTIONS
        {
            get { return GetRepository(x => x.Set<JOB_QUOTE_OPTIONS>(), (JOB_QUOTE_OPTIONS x) => x.SEQNO); }
        }

        IRepository<JOB_RESOURCE_ALLOCATION, int> IPrimeroEntitiesUnitOfWork.JOB_RESOURCE_ALLOCATION
        {
            get { return GetRepository(x => x.Set<JOB_RESOURCE_ALLOCATION>(), (JOB_RESOURCE_ALLOCATION x) => x.SEQNO); }
        }

        IRepository<JOB_RETENTION_LEVELS, int> IPrimeroEntitiesUnitOfWork.JOB_RETENTION_LEVELS
        {
            get { return GetRepository(x => x.Set<JOB_RETENTION_LEVELS>(), (JOB_RETENTION_LEVELS x) => x.SEQNO); }
        }

        IRepository<JOB_STATUS, string> IPrimeroEntitiesUnitOfWork.JOB_STATUS
        {
            get { return GetRepository(x => x.Set<JOB_STATUS>(), (JOB_STATUS x) => x.STATUSKEY); }
        }

        IRepository<JOB_STATUS_CONSTRAINT, int> IPrimeroEntitiesUnitOfWork.JOB_STATUS_CONSTRAINT
        {
            get { return GetRepository(x => x.Set<JOB_STATUS_CONSTRAINT>(), (JOB_STATUS_CONSTRAINT x) => x.SEQNO); }
        }

        IRepository<JOB_TIMESHEET_ALLOWANCE, int> IPrimeroEntitiesUnitOfWork.JOB_TIMESHEET_ALLOWANCE
        {
            get { return GetRepository(x => x.Set<JOB_TIMESHEET_ALLOWANCE>(), (JOB_TIMESHEET_ALLOWANCE x) => x.SEQNO); }
        }

        IRepository<JOB_TIMESHEET_ALLOWANCE_TYPES, int> IPrimeroEntitiesUnitOfWork.JOB_TIMESHEET_ALLOWANCE_TYPES
        {
            get { return GetRepository(x => x.Set<JOB_TIMESHEET_ALLOWANCE_TYPES>(), (JOB_TIMESHEET_ALLOWANCE_TYPES x) => x.SEQNO); }
        }

        IRepository<JOB_TIMESHEETS, int> IPrimeroEntitiesUnitOfWork.JOB_TIMESHEETS
        {
            get { return GetRepository(x => x.Set<JOB_TIMESHEETS>(), (JOB_TIMESHEETS x) => x.SEQNO); }
        }

        IRepository<JOB_TIMESHEETS_RATES, int> IPrimeroEntitiesUnitOfWork.JOB_TIMESHEETS_RATES
        {
            get { return GetRepository(x => x.Set<JOB_TIMESHEETS_RATES>(), (JOB_TIMESHEETS_RATES x) => x.SEQNO); }
        }

        IRepository<JOB_TRANSACTIONS, int> IPrimeroEntitiesUnitOfWork.JOB_TRANSACTIONS
        {
            get { return GetRepository(x => x.Set<JOB_TRANSACTIONS>(), (JOB_TRANSACTIONS x) => x.SEQNO); }
        }

        IRepository<JOB_TYPES, int> IPrimeroEntitiesUnitOfWork.JOB_TYPES
        {
            get { return GetRepository(x => x.Set<JOB_TYPES>(), (JOB_TYPES x) => x.TYPENO); }
        }

        IRepository<JOBCOST_FLAGS, int> IPrimeroEntitiesUnitOfWork.JOBCOST_FLAGS
        {
            get { return GetRepository(x => x.Set<JOBCOST_FLAGS>(), (JOBCOST_FLAGS x) => x.JOBNO); }
        }

        IRepository<JOBCOST_FLAGSDESC, string> IPrimeroEntitiesUnitOfWork.JOBCOST_FLAGSDESC
        {
            get { return GetRepository(x => x.Set<JOBCOST_FLAGSDESC>(), (JOBCOST_FLAGSDESC x) => x.FLAGCODE); }
        }

        IRepository<JOBCOST_GENERAL_INFO, int> IPrimeroEntitiesUnitOfWork.JOBCOST_GENERAL_INFO
        {
            get { return GetRepository(x => x.Set<JOBCOST_GENERAL_INFO>(), (JOBCOST_GENERAL_INFO x) => x.SEQNO); }
        }

        IRepository<JOBCOST_HDR, int> IPrimeroEntitiesUnitOfWork.JOBCOST_HDR
        {
            get { return GetRepository(x => x.Set<JOBCOST_HDR>(), (JOBCOST_HDR x) => x.JOBNO); }
        }

        IRepository<JOBCOST_LINES, int> IPrimeroEntitiesUnitOfWork.JOBCOST_LINES
        {
            get { return GetRepository(x => x.Set<JOBCOST_LINES>(), (JOBCOST_LINES x) => x.SEQNO); }
        }

        IRepository<JOBCOST_PROJ, int> IPrimeroEntitiesUnitOfWork.JOBCOST_PROJ
        {
            get { return GetRepository(x => x.Set<JOBCOST_PROJ>(), (JOBCOST_PROJ x) => x.SEQNO); }
        }

        IRepository<JOBCOST_RESOURCE, int> IPrimeroEntitiesUnitOfWork.JOBCOST_RESOURCE
        {
            get { return GetRepository(x => x.Set<JOBCOST_RESOURCE>(), (JOBCOST_RESOURCE x) => x.SEQNO); }
        }

        IRepository<KIT_SERIAL_INFO, int> IPrimeroEntitiesUnitOfWork.KIT_SERIAL_INFO
        {
            get { return GetRepository(x => x.Set<KIT_SERIAL_INFO>(), (KIT_SERIAL_INFO x) => x.KITSEQNO); }
        }

        IRepository<LEDGER_PERIODS, int> IPrimeroEntitiesUnitOfWork.LEDGER_PERIODS
        {
            get { return GetRepository(x => x.Set<LEDGER_PERIODS>(), (LEDGER_PERIODS x) => x.SEQNO); }
        }

        IRepository<LEGENDS, int> IPrimeroEntitiesUnitOfWork.LEGENDS
        {
            get { return GetRepository(x => x.Set<LEGENDS>(), (LEGENDS x) => x.SEQNO); }
        }

        IRepository<MANIFESTS, int> IPrimeroEntitiesUnitOfWork.MANIFESTS
        {
            get { return GetRepository(x => x.Set<MANIFESTS>(), (MANIFESTS x) => x.MANIFESTNO); }
        }

        IRepository<MANREP, int> IPrimeroEntitiesUnitOfWork.MANREP
        {
            get { return GetRepository(x => x.Set<MANREP>(), (MANREP x) => x.SEQNO); }
        }

        IRepository<MANREP_BRANCH, int> IPrimeroEntitiesUnitOfWork.MANREP_BRANCH
        {
            get { return GetRepository(x => x.Set<MANREP_BRANCH>(), (MANREP_BRANCH x) => x.SEQNO); }
        }

        IRepository<MANREP_BRANCH_BUDGET, int> IPrimeroEntitiesUnitOfWork.MANREP_BRANCH_BUDGET
        {
            get { return GetRepository(x => x.Set<MANREP_BRANCH_BUDGET>(), (MANREP_BRANCH_BUDGET x) => x.SEQNO); }
        }

        IRepository<MANREP_BUDGET, int> IPrimeroEntitiesUnitOfWork.MANREP_BUDGET
        {
            get { return GetRepository(x => x.Set<MANREP_BUDGET>(), (MANREP_BUDGET x) => x.BUDGET_SEQNO); }
        }

        IRepository<MANREP_CLOSING_STOCK, Tuple<int, string>> IPrimeroEntitiesUnitOfWork.MANREP_CLOSING_STOCK
        {
            get { return GetRepository(x => x.Set<MANREP_CLOSING_STOCK>(), (MANREP_CLOSING_STOCK x) => Tuple.Create(x.MANREP_SEQNO, x.STOCKCODE)); }
        }

        IRepository<MANREP_DAYPLAN, Tuple<DateTime, int>> IPrimeroEntitiesUnitOfWork.MANREP_DAYPLAN
        {
            get { return GetRepository(x => x.Set<MANREP_DAYPLAN>(), (MANREP_DAYPLAN x) => Tuple.Create(x.THEDATE, x.PERIOD_SEQNO)); }
        }

        IRepository<MANREP_PERIOD, int> IPrimeroEntitiesUnitOfWork.MANREP_PERIOD
        {
            get { return GetRepository(x => x.Set<MANREP_PERIOD>(), (MANREP_PERIOD x) => x.PERIOD_SEQNO); }
        }

        IRepository<MANREP_SALESPERSON, int> IPrimeroEntitiesUnitOfWork.MANREP_SALESPERSON
        {
            get { return GetRepository(x => x.Set<MANREP_SALESPERSON>(), (MANREP_SALESPERSON x) => x.SEQNO); }
        }

        IRepository<MANREP_SETUP, int> IPrimeroEntitiesUnitOfWork.MANREP_SETUP
        {
            get { return GetRepository(x => x.Set<MANREP_SETUP>(), (MANREP_SETUP x) => x.SEQNO); }
        }

        IRepository<MANREP_STOCK, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.MANREP_STOCK
        {
            get { return GetRepository(x => x.Set<MANREP_STOCK>(), (MANREP_STOCK x) => Tuple.Create(x.MANREP_SEQNO, x.Location)); }
        }

        IRepository<MENU_ASSIGNMENTS, string> IPrimeroEntitiesUnitOfWork.MENU_ASSIGNMENTS
        {
            get { return GetRepository(x => x.Set<MENU_ASSIGNMENTS>(), (MENU_ASSIGNMENTS x) => x.USERNAME); }
        }

        IRepository<MENU_COLLECTION, int> IPrimeroEntitiesUnitOfWork.MENU_COLLECTION
        {
            get { return GetRepository(x => x.Set<MENU_COLLECTION>(), (MENU_COLLECTION x) => x.SEQNO); }
        }

        IRepository<MENU_HDR, int> IPrimeroEntitiesUnitOfWork.MENU_HDR
        {
            get { return GetRepository(x => x.Set<MENU_HDR>(), (MENU_HDR x) => x.SEQNO); }
        }

        IRepository<MENU_LINES, int> IPrimeroEntitiesUnitOfWork.MENU_LINES
        {
            get { return GetRepository(x => x.Set<MENU_LINES>(), (MENU_LINES x) => x.SEQNO); }
        }

        IRepository<MENU_MASTER, int> IPrimeroEntitiesUnitOfWork.MENU_MASTER
        {
            get { return GetRepository(x => x.Set<MENU_MASTER>(), (MENU_MASTER x) => x.MENU_NO); }
        }

        IRepository<MENU_OPTIONS, int> IPrimeroEntitiesUnitOfWork.MENU_OPTIONS
        {
            get { return GetRepository(x => x.Set<MENU_OPTIONS>(), (MENU_OPTIONS x) => x.SEQNO); }
        }

        IRepository<MENU_PROCEDURES, int> IPrimeroEntitiesUnitOfWork.MENU_PROCEDURES
        {
            get { return GetRepository(x => x.Set<MENU_PROCEDURES>(), (MENU_PROCEDURES x) => x.PROCNO); }
        }

        IRepository<MODULE_SECURITY, int> IPrimeroEntitiesUnitOfWork.MODULE_SECURITY
        {
            get { return GetRepository(x => x.Set<MODULE_SECURITY>(), (MODULE_SECURITY x) => x.APP_ID); }
        }

        IRepository<MONTHSTABLE, int> IPrimeroEntitiesUnitOfWork.MONTHSTABLE
        {
            get { return GetRepository(x => x.Set<MONTHSTABLE>(), (MONTHSTABLE x) => x.MONTH); }
        }

        IRepository<NARRATIVES, int> IPrimeroEntitiesUnitOfWork.NARRATIVES
        {
            get { return GetRepository(x => x.Set<NARRATIVES>(), (NARRATIVES x) => x.SEQNO); }
        }

        IRepository<OBJECT_DATA, string> IPrimeroEntitiesUnitOfWork.OBJECT_DATA
        {
            get { return GetRepository(x => x.Set<OBJECT_DATA>(), (OBJECT_DATA x) => x.ID); }
        }

        IRepository<OBJECT_LOCK, int> IPrimeroEntitiesUnitOfWork.OBJECT_LOCK
        {
            get { return GetRepository(x => x.Set<OBJECT_LOCK>(), (OBJECT_LOCK x) => x.SEQNO); }
        }

        IRepository<ONLINE_USERS, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.ONLINE_USERS
        {
            get { return GetRepository(x => x.Set<ONLINE_USERS>(), (ONLINE_USERS x) => Tuple.Create(x.COMPUTERSEQNO, x.APPLICATION)); }
        }

        IRepository<OPPORTUNITY, int> IPrimeroEntitiesUnitOfWork.OPPORTUNITY
        {
            get { return GetRepository(x => x.Set<OPPORTUNITY>(), (OPPORTUNITY x) => x.SEQNO); }
        }

        IRepository<OPPORTUNITY_HIST, int> IPrimeroEntitiesUnitOfWork.OPPORTUNITY_HIST
        {
            get { return GetRepository(x => x.Set<OPPORTUNITY_HIST>(), (OPPORTUNITY_HIST x) => x.SEQNO); }
        }

        IRepository<OPPORTUNITY_LEAD, int> IPrimeroEntitiesUnitOfWork.OPPORTUNITY_LEAD
        {
            get { return GetRepository(x => x.Set<OPPORTUNITY_LEAD>(), (OPPORTUNITY_LEAD x) => x.SEQNO); }
        }

        IRepository<OPPORTUNITY_LINES, int> IPrimeroEntitiesUnitOfWork.OPPORTUNITY_LINES
        {
            get { return GetRepository(x => x.Set<OPPORTUNITY_LINES>(), (OPPORTUNITY_LINES x) => x.SEQNO); }
        }

        IRepository<OPPORTUNITY_QUOTE, int> IPrimeroEntitiesUnitOfWork.OPPORTUNITY_QUOTE
        {
            get { return GetRepository(x => x.Set<OPPORTUNITY_QUOTE>(), (OPPORTUNITY_QUOTE x) => x.SEQNO); }
        }

        IRepository<OPPORTUNITY_QUOTE_OPTIONS, int> IPrimeroEntitiesUnitOfWork.OPPORTUNITY_QUOTE_OPTIONS
        {
            get { return GetRepository(x => x.Set<OPPORTUNITY_QUOTE_OPTIONS>(), (OPPORTUNITY_QUOTE_OPTIONS x) => x.SEQNO); }
        }

        IRepository<OPPORTUNITY_STAGE, int> IPrimeroEntitiesUnitOfWork.OPPORTUNITY_STAGE
        {
            get { return GetRepository(x => x.Set<OPPORTUNITY_STAGE>(), (OPPORTUNITY_STAGE x) => x.SEQNO); }
        }

        IRepository<OPPORTUNITY_STAGE_CONSTRAINT, int> IPrimeroEntitiesUnitOfWork.OPPORTUNITY_STAGE_CONSTRAINT
        {
            get { return GetRepository(x => x.Set<OPPORTUNITY_STAGE_CONSTRAINT>(), (OPPORTUNITY_STAGE_CONSTRAINT x) => x.SEQNO); }
        }

        IRepository<OPPORTUNITY_TYPE, int> IPrimeroEntitiesUnitOfWork.OPPORTUNITY_TYPE
        {
            get { return GetRepository(x => x.Set<OPPORTUNITY_TYPE>(), (OPPORTUNITY_TYPE x) => x.SEQNO); }
        }

        IRepository<PAYMENT_DENOMINATIONS, int> IPrimeroEntitiesUnitOfWork.PAYMENT_DENOMINATIONS
        {
            get { return GetRepository(x => x.Set<PAYMENT_DENOMINATIONS>(), (PAYMENT_DENOMINATIONS x) => x.SEQNO); }
        }

        IRepository<PAYMENT_GROUP, int> IPrimeroEntitiesUnitOfWork.PAYMENT_GROUP
        {
            get { return GetRepository(x => x.Set<PAYMENT_GROUP>(), (PAYMENT_GROUP x) => x.PGNO); }
        }

        IRepository<PAYMENT_TYPES, int> IPrimeroEntitiesUnitOfWork.PAYMENT_TYPES
        {
            get { return GetRepository(x => x.Set<PAYMENT_TYPES>(), (PAYMENT_TYPES x) => x.PTNO); }
        }

        IRepository<PAYROLL_COSTCENTRE, Tuple<int, long>> IPrimeroEntitiesUnitOfWork.PAYROLL_COSTCENTRE
        {
            get { return GetRepository(x => x.Set<PAYROLL_COSTCENTRE>(), (PAYROLL_COSTCENTRE x) => Tuple.Create(x.COCODE, x.CODE)); }
        }

        IRepository<PAYROLL_EMPLOYEES, Tuple<int, string>> IPrimeroEntitiesUnitOfWork.PAYROLL_EMPLOYEES
        {
            get { return GetRepository(x => x.Set<PAYROLL_EMPLOYEES>(), (PAYROLL_EMPLOYEES x) => Tuple.Create(x.EMPLOYEE_CODE, x.LAST_NAME)); }
        }

        IReadOnlyRepository<PAYROLL_INFO> IPrimeroEntitiesUnitOfWork.PAYROLL_INFO
        {
            get { return GetReadOnlyRepository(x => x.Set<PAYROLL_INFO>()); }
        }

        IRepository<PAYROLL_PAYRATES, Tuple<int, double>> IPrimeroEntitiesUnitOfWork.PAYROLL_PAYRATES
        {
            get { return GetRepository(x => x.Set<PAYROLL_PAYRATES>(), (PAYROLL_PAYRATES x) => Tuple.Create(x.PAYRATE_NO, x.PAYRATE)); }
        }

        IRepository<PAYRUN_HDR, int> IPrimeroEntitiesUnitOfWork.PAYRUN_HDR
        {
            get { return GetRepository(x => x.Set<PAYRUN_HDR>(), (PAYRUN_HDR x) => x.SEQNO); }
        }

        IRepository<PAYRUN_LINES, int> IPrimeroEntitiesUnitOfWork.PAYRUN_LINES
        {
            get { return GetRepository(x => x.Set<PAYRUN_LINES>(), (PAYRUN_LINES x) => x.SEQNO); }
        }

        IRepository<PERIOD_STATUS, int> IPrimeroEntitiesUnitOfWork.PERIOD_STATUS
        {
            get { return GetRepository(x => x.Set<PERIOD_STATUS>(), (PERIOD_STATUS x) => x.SEQNO); }
        }

        IRepository<PERIODS_DEFN, int> IPrimeroEntitiesUnitOfWork.PERIODS_DEFN
        {
            get { return GetRepository(x => x.Set<PERIODS_DEFN>(), (PERIODS_DEFN x) => x.SEQNO); }
        }

        IRepository<PERIODS_DEFN_NEXTFINYR, int> IPrimeroEntitiesUnitOfWork.PERIODS_DEFN_NEXTFINYR
        {
            get { return GetRepository(x => x.Set<PERIODS_DEFN_NEXTFINYR>(), (PERIODS_DEFN_NEXTFINYR x) => x.SEQNO); }
        }

        IRepository<POS_COUNT, Tuple<int, int>> IPrimeroEntitiesUnitOfWork.POS_COUNT
        {
            get { return GetRepository(x => x.Set<POS_COUNT>(), (POS_COUNT x) => Tuple.Create(x.SHIFTNO, x.PTNO)); }
        }

        IRepository<POS_COUNT_DENOM, int> IPrimeroEntitiesUnitOfWork.POS_COUNT_DENOM
        {
            get { return GetRepository(x => x.Set<POS_COUNT_DENOM>(), (POS_COUNT_DENOM x) => x.SEQNO); }
        }

        IRepository<POS_SETTLEMENT, int> IPrimeroEntitiesUnitOfWork.POS_SETTLEMENT
        {
            get { return GetRepository(x => x.Set<POS_SETTLEMENT>(), (POS_SETTLEMENT x) => x.SEQNO); }
        }

        IRepository<POS_SHIFTS, int> IPrimeroEntitiesUnitOfWork.POS_SHIFTS
        {
            get { return GetRepository(x => x.Set<POS_SHIFTS>(), (POS_SHIFTS x) => x.SHIFTNO); }
        }

        IRepository<POSTCODES, int> IPrimeroEntitiesUnitOfWork.POSTCODES
        {
            get { return GetRepository(x => x.Set<POSTCODES>(), (POSTCODES x) => x.SEQNO); }
        }

        IReadOnlyRepository<PREVIOUS_ITEMS> IPrimeroEntitiesUnitOfWork.PREVIOUS_ITEMS
        {
            get { return GetReadOnlyRepository(x => x.Set<PREVIOUS_ITEMS>()); }
        }

        IRepository<PRICE_NAMES, int> IPrimeroEntitiesUnitOfWork.PRICE_NAMES
        {
            get { return GetRepository(x => x.Set<PRICE_NAMES>(), (PRICE_NAMES x) => x.PRICENO); }
        }

        IRepository<PRICE_SCHEDULE, int> IPrimeroEntitiesUnitOfWork.PRICE_SCHEDULE
        {
            get { return GetRepository(x => x.Set<PRICE_SCHEDULE>(), (PRICE_SCHEDULE x) => x.SEQNO); }
        }

        IRepository<PRINT_LOG, int> IPrimeroEntitiesUnitOfWork.PRINT_LOG
        {
            get { return GetRepository(x => x.Set<PRINT_LOG>(), (PRINT_LOG x) => x.SEQNO); }
        }

        IRepository<PROFILE, int> IPrimeroEntitiesUnitOfWork.PROFILE
        {
            get { return GetRepository(x => x.Set<PROFILE>(), (PROFILE x) => x.ID); }
        }

        IRepository<PROFILE_CONFLICT_SET, int> IPrimeroEntitiesUnitOfWork.PROFILE_CONFLICT_SET
        {
            get { return GetRepository(x => x.Set<PROFILE_CONFLICT_SET>(), (PROFILE_CONFLICT_SET x) => x.ID); }
        }

        IRepository<PROFILE_CONFLICT_SET_MEMBERS, Tuple<int, string>> IPrimeroEntitiesUnitOfWork.PROFILE_CONFLICT_SET_MEMBERS
        {
            get { return GetRepository(x => x.Set<PROFILE_CONFLICT_SET_MEMBERS>(), (PROFILE_CONFLICT_SET_MEMBERS x) => Tuple.Create(x.CONFLICTSETID, x.FIELDNAME)); }
        }

        IRepository<PROFILE_CONSTRAINT_VALUES, Tuple<int, string, string>> IPrimeroEntitiesUnitOfWork.PROFILE_CONSTRAINT_VALUES
        {
            get { return GetRepository(x => x.Set<PROFILE_CONSTRAINT_VALUES>(), (PROFILE_CONSTRAINT_VALUES x) => Tuple.Create(x.CONSTRAINTID, x.VALUETYPE, x.CONSTRAINTVALUE)); }
        }

        IRepository<PROFILE_CONSTRAINTS, int> IPrimeroEntitiesUnitOfWork.PROFILE_CONSTRAINTS
        {
            get { return GetRepository(x => x.Set<PROFILE_CONSTRAINTS>(), (PROFILE_CONSTRAINTS x) => x.ID); }
        }

        IRepository<PROFILE_FIELDS, string> IPrimeroEntitiesUnitOfWork.PROFILE_FIELDS
        {
            get { return GetRepository(x => x.Set<PROFILE_FIELDS>(), (PROFILE_FIELDS x) => x.FIELDNAME); }
        }

        IRepository<PROFILE_FORMS, int> IPrimeroEntitiesUnitOfWork.PROFILE_FORMS
        {
            get { return GetRepository(x => x.Set<PROFILE_FORMS>(), (PROFILE_FORMS x) => x.PROFILEID); }
        }

        IRepository<PROFILE_GRIDS, Tuple<int, int, string>> IPrimeroEntitiesUnitOfWork.PROFILE_GRIDS
        {
            get { return GetRepository(x => x.Set<PROFILE_GRIDS>(), (PROFILE_GRIDS x) => Tuple.Create(x.PROFILEID, x.COMPUTERSEQNO, x.REGKEY)); }
        }

        IRepository<PROFILE_TYPE_INFO, int> IPrimeroEntitiesUnitOfWork.PROFILE_TYPE_INFO
        {
            get { return GetRepository(x => x.Set<PROFILE_TYPE_INFO>(), (PROFILE_TYPE_INFO x) => x.ID); }
        }

        IRepository<PROFILE_TYPES, int> IPrimeroEntitiesUnitOfWork.PROFILE_TYPES
        {
            get { return GetRepository(x => x.Set<PROFILE_TYPES>(), (PROFILE_TYPES x) => x.ID); }
        }

        IRepository<PROFILE_VALUES, Tuple<int, string>> IPrimeroEntitiesUnitOfWork.PROFILE_VALUES
        {
            get { return GetRepository(x => x.Set<PROFILE_VALUES>(), (PROFILE_VALUES x) => Tuple.Create(x.PROFILEID, x.FIELDNAME)); }
        }

        IRepository<PROSPECT_CONTACTS, int> IPrimeroEntitiesUnitOfWork.PROSPECT_CONTACTS
        {
            get { return GetRepository(x => x.Set<PROSPECT_CONTACTS>(), (PROSPECT_CONTACTS x) => x.SEQNO); }
        }

        IRepository<PROSPECT_HIST, int> IPrimeroEntitiesUnitOfWork.PROSPECT_HIST
        {
            get { return GetRepository(x => x.Set<PROSPECT_HIST>(), (PROSPECT_HIST x) => x.SEQNO); }
        }

        IRepository<PROSPECTS, int> IPrimeroEntitiesUnitOfWork.PROSPECTS
        {
            get { return GetRepository(x => x.Set<PROSPECTS>(), (PROSPECTS x) => x.SEQNO); }
        }

        IRepository<PURCHORD_HDR, int> IPrimeroEntitiesUnitOfWork.PURCHORD_HDR
        {
            get { return GetRepository(x => x.Set<PURCHORD_HDR>(), (PURCHORD_HDR x) => x.SEQNO); }
        }

        IRepository<PURCHORD_HDR_ARCHIVE, int> IPrimeroEntitiesUnitOfWork.PURCHORD_HDR_ARCHIVE
        {
            get { return GetRepository(x => x.Set<PURCHORD_HDR_ARCHIVE>(), (PURCHORD_HDR_ARCHIVE x) => x.SEQNO); }
        }

        IRepository<PURCHORD_HIST, int> IPrimeroEntitiesUnitOfWork.PURCHORD_HIST
        {
            get { return GetRepository(x => x.Set<PURCHORD_HIST>(), (PURCHORD_HIST x) => x.SEQNO); }
        }

        IRepository<PURCHORD_LINES, int> IPrimeroEntitiesUnitOfWork.PURCHORD_LINES
        {
            get { return GetRepository(x => x.Set<PURCHORD_LINES>(), (PURCHORD_LINES x) => x.SEQNO); }
        }

        IRepository<PURCHORD_LINES_ARCHIVE, int> IPrimeroEntitiesUnitOfWork.PURCHORD_LINES_ARCHIVE
        {
            get { return GetRepository(x => x.Set<PURCHORD_LINES_ARCHIVE>(), (PURCHORD_LINES_ARCHIVE x) => x.SEQNO); }
        }

        IRepository<QUICK_INSERT_TABSHEET, int> IPrimeroEntitiesUnitOfWork.QUICK_INSERT_TABSHEET
        {
            get { return GetRepository(x => x.Set<QUICK_INSERT_TABSHEET>(), (QUICK_INSERT_TABSHEET x) => x.SEQNO); }
        }

        IRepository<REASON_CLASS, int> IPrimeroEntitiesUnitOfWork.REASON_CLASS
        {
            get { return GetRepository(x => x.Set<REASON_CLASS>(), (REASON_CLASS x) => x.CLASSNO); }
        }

        IRepository<REASON_EVENTS, int> IPrimeroEntitiesUnitOfWork.REASON_EVENTS
        {
            get { return GetRepository(x => x.Set<REASON_EVENTS>(), (REASON_EVENTS x) => x.SEQNO); }
        }

        IRepository<REASONS, int> IPrimeroEntitiesUnitOfWork.REASONS
        {
            get { return GetRepository(x => x.Set<REASONS>(), (REASONS x) => x.SEQNO); }
        }

        IRepository<RELATIONSHIPS, int> IPrimeroEntitiesUnitOfWork.RELATIONSHIPS
        {
            get { return GetRepository(x => x.Set<RELATIONSHIPS>(), (RELATIONSHIPS x) => x.SEQNO); }
        }

        IRepository<RELATIONSHIPS_DEFN, int> IPrimeroEntitiesUnitOfWork.RELATIONSHIPS_DEFN
        {
            get { return GetRepository(x => x.Set<RELATIONSHIPS_DEFN>(), (RELATIONSHIPS_DEFN x) => x.SEQNO); }
        }

        IRepository<RELATIONSHIPS_SET, int> IPrimeroEntitiesUnitOfWork.RELATIONSHIPS_SET
        {
            get { return GetRepository(x => x.Set<RELATIONSHIPS_SET>(), (RELATIONSHIPS_SET x) => x.SEQNO); }
        }

        IRepository<SALES_MESSAGES, int> IPrimeroEntitiesUnitOfWork.SALES_MESSAGES
        {
            get { return GetRepository(x => x.Set<SALES_MESSAGES>(), (SALES_MESSAGES x) => x.SEQNO); }
        }

        IRepository<SALESORD_HDR, int> IPrimeroEntitiesUnitOfWork.SALESORD_HDR
        {
            get { return GetRepository(x => x.Set<SALESORD_HDR>(), (SALESORD_HDR x) => x.SEQNO); }
        }

        IRepository<SALESORD_HDR_ARCHIVE, int> IPrimeroEntitiesUnitOfWork.SALESORD_HDR_ARCHIVE
        {
            get { return GetRepository(x => x.Set<SALESORD_HDR_ARCHIVE>(), (SALESORD_HDR_ARCHIVE x) => x.SEQNO); }
        }

        IRepository<SALESORD_LINES, int> IPrimeroEntitiesUnitOfWork.SALESORD_LINES
        {
            get { return GetRepository(x => x.Set<SALESORD_LINES>(), (SALESORD_LINES x) => x.SEQNO); }
        }

        IRepository<SALESORD_LINES_ARCHIVE, int> IPrimeroEntitiesUnitOfWork.SALESORD_LINES_ARCHIVE
        {
            get { return GetRepository(x => x.Set<SALESORD_LINES_ARCHIVE>(), (SALESORD_LINES_ARCHIVE x) => x.SEQNO); }
        }

        IRepository<SALESORD_STATUS, int> IPrimeroEntitiesUnitOfWork.SALESORD_STATUS
        {
            get { return GetRepository(x => x.Set<SALESORD_STATUS>(), (SALESORD_STATUS x) => x.SEQNO); }
        }

        IRepository<SALESORDHIST, int> IPrimeroEntitiesUnitOfWork.SALESORDHIST
        {
            get { return GetRepository(x => x.Set<SALESORDHIST>(), (SALESORDHIST x) => x.SEQNO); }
        }

        IRepository<SHIPMENT_COST_DETAILS, int> IPrimeroEntitiesUnitOfWork.SHIPMENT_COST_DETAILS
        {
            get { return GetRepository(x => x.Set<SHIPMENT_COST_DETAILS>(), (SHIPMENT_COST_DETAILS x) => x.SEQNO); }
        }

        IRepository<SHIPMENT_HDR, int> IPrimeroEntitiesUnitOfWork.SHIPMENT_HDR
        {
            get { return GetRepository(x => x.Set<SHIPMENT_HDR>(), (SHIPMENT_HDR x) => x.SEQNO); }
        }

        IRepository<SHIPMENT_METHODS, int> IPrimeroEntitiesUnitOfWork.SHIPMENT_METHODS
        {
            get { return GetRepository(x => x.Set<SHIPMENT_METHODS>(), (SHIPMENT_METHODS x) => x.SEQNO); }
        }

        IRepository<SHIPMENT_STATUS, int> IPrimeroEntitiesUnitOfWork.SHIPMENT_STATUS
        {
            get { return GetRepository(x => x.Set<SHIPMENT_STATUS>(), (SHIPMENT_STATUS x) => x.SEQNO); }
        }

        IRepository<SMTP_ACCOUNTS, int> IPrimeroEntitiesUnitOfWork.SMTP_ACCOUNTS
        {
            get { return GetRepository(x => x.Set<SMTP_ACCOUNTS>(), (SMTP_ACCOUNTS x) => x.SEQNO); }
        }

        IRepository<STAFF, int> IPrimeroEntitiesUnitOfWork.STAFF
        {
            get { return GetRepository(x => x.Set<STAFF>(), (STAFF x) => x.STAFFNO); }
        }

        IRepository<STOCK_CLASSIFICATIONS, int> IPrimeroEntitiesUnitOfWork.STOCK_CLASSIFICATIONS
        {
            get { return GetRepository(x => x.Set<STOCK_CLASSIFICATIONS>(), (STOCK_CLASSIFICATIONS x) => x.CLASSNO); }
        }

        IRepository<STOCK_COLOUR, int> IPrimeroEntitiesUnitOfWork.STOCK_COLOUR
        {
            get { return GetRepository(x => x.Set<STOCK_COLOUR>(), (STOCK_COLOUR x) => x.COLOURID); }
        }

        IRepository<STOCK_CONT_HIST, int> IPrimeroEntitiesUnitOfWork.STOCK_CONT_HIST
        {
            get { return GetRepository(x => x.Set<STOCK_CONT_HIST>(), (STOCK_CONT_HIST x) => x.SEQNO); }
        }

        IRepository<STOCK_GROUP2S, int> IPrimeroEntitiesUnitOfWork.STOCK_GROUP2S
        {
            get { return GetRepository(x => x.Set<STOCK_GROUP2S>(), (STOCK_GROUP2S x) => x.GROUPNO); }
        }

        IRepository<STOCK_GROUPS, int> IPrimeroEntitiesUnitOfWork.STOCK_GROUPS
        {
            get { return GetRepository(x => x.Set<STOCK_GROUPS>(), (STOCK_GROUPS x) => x.GROUPNO); }
        }

        IRepository<STOCK_ITEMS, string> IPrimeroEntitiesUnitOfWork.STOCK_ITEMS
        {
            get { return GetRepository(x => x.Set<STOCK_ITEMS>(), (STOCK_ITEMS x) => x.STOCKCODE); }
        }

        IRepository<STOCK_LOC_INFO, Tuple<string, int>> IPrimeroEntitiesUnitOfWork.STOCK_LOC_INFO
        {
            get { return GetRepository(x => x.Set<STOCK_LOC_INFO>(), (STOCK_LOC_INFO x) => Tuple.Create(x.STOCKCODE, x.LOCATION)); }
        }

        IRepository<STOCK_LOCATIONS, int> IPrimeroEntitiesUnitOfWork.STOCK_LOCATIONS
        {
            get { return GetRepository(x => x.Set<STOCK_LOCATIONS>(), (STOCK_LOCATIONS x) => x.LOCNO); }
        }

        IRepository<STOCK_PRICEGROUPS, int> IPrimeroEntitiesUnitOfWork.STOCK_PRICEGROUPS
        {
            get { return GetRepository(x => x.Set<STOCK_PRICEGROUPS>(), (STOCK_PRICEGROUPS x) => x.GROUPNO); }
        }

        IRepository<STOCK_RECEIPTS, int> IPrimeroEntitiesUnitOfWork.STOCK_RECEIPTS
        {
            get { return GetRepository(x => x.Set<STOCK_RECEIPTS>(), (STOCK_RECEIPTS x) => x.RECEIPTNO); }
        }

        IRepository<STOCK_REQUESTLINES, int> IPrimeroEntitiesUnitOfWork.STOCK_REQUESTLINES
        {
            get { return GetRepository(x => x.Set<STOCK_REQUESTLINES>(), (STOCK_REQUESTLINES x) => x.SEQNO); }
        }

        IRepository<STOCK_REQUESTS, int> IPrimeroEntitiesUnitOfWork.STOCK_REQUESTS
        {
            get { return GetRepository(x => x.Set<STOCK_REQUESTS>(), (STOCK_REQUESTS x) => x.SEQNO); }
        }

        IRepository<STOCK_REQUESTTYPES, int> IPrimeroEntitiesUnitOfWork.STOCK_REQUESTTYPES
        {
            get { return GetRepository(x => x.Set<STOCK_REQUESTTYPES>(), (STOCK_REQUESTTYPES x) => x.SEQNO); }
        }

        IRepository<STOCK_SERIALNOS, int> IPrimeroEntitiesUnitOfWork.STOCK_SERIALNOS
        {
            get { return GetRepository(x => x.Set<STOCK_SERIALNOS>(), (STOCK_SERIALNOS x) => x.SEQNO); }
        }

        IRepository<STOCK_SIZE, int> IPrimeroEntitiesUnitOfWork.STOCK_SIZE
        {
            get { return GetRepository(x => x.Set<STOCK_SIZE>(), (STOCK_SIZE x) => x.SIZEID); }
        }

        IRepository<STOCK_TRANS, int> IPrimeroEntitiesUnitOfWork.STOCK_TRANS
        {
            get { return GetRepository(x => x.Set<STOCK_TRANS>(), (STOCK_TRANS x) => x.SEQNO); }
        }

        IRepository<STOCK_TRANS_ARCHIVE, int> IPrimeroEntitiesUnitOfWork.STOCK_TRANS_ARCHIVE
        {
            get { return GetRepository(x => x.Set<STOCK_TRANS_ARCHIVE>(), (STOCK_TRANS_ARCHIVE x) => x.SEQNO); }
        }

        IRepository<STOCK_TRANS_HDR, int> IPrimeroEntitiesUnitOfWork.STOCK_TRANS_HDR
        {
            get { return GetRepository(x => x.Set<STOCK_TRANS_HDR>(), (STOCK_TRANS_HDR x) => x.SEQNO); }
        }

        IRepository<STOCK_TRANS_SERIALS, int> IPrimeroEntitiesUnitOfWork.STOCK_TRANS_SERIALS
        {
            get { return GetRepository(x => x.Set<STOCK_TRANS_SERIALS>(), (STOCK_TRANS_SERIALS x) => x.SEQNO); }
        }

        IRepository<STOCK_UNITDEFINITION, string> IPrimeroEntitiesUnitOfWork.STOCK_UNITDEFINITION
        {
            get { return GetRepository(x => x.Set<STOCK_UNITDEFINITION>(), (STOCK_UNITDEFINITION x) => x.UNITCODE); }
        }

        IRepository<STOCK_WEB, string> IPrimeroEntitiesUnitOfWork.STOCK_WEB
        {
            get { return GetRepository(x => x.Set<STOCK_WEB>(), (STOCK_WEB x) => x.STOCKCODE); }
        }

        IRepository<STOCKREQUIREMENT, string> IPrimeroEntitiesUnitOfWork.STOCKREQUIREMENT
        {
            get { return GetRepository(x => x.Set<STOCKREQUIREMENT>(), (STOCKREQUIREMENT x) => x.STOCKCODE); }
        }

        IRepository<STOCKTAKE_CTRL, int> IPrimeroEntitiesUnitOfWork.STOCKTAKE_CTRL
        {
            get { return GetRepository(x => x.Set<STOCKTAKE_CTRL>(), (STOCKTAKE_CTRL x) => x.LOCNO); }
        }

        IRepository<STOCKTAKE_EVENTS, int> IPrimeroEntitiesUnitOfWork.STOCKTAKE_EVENTS
        {
            get { return GetRepository(x => x.Set<STOCKTAKE_EVENTS>(), (STOCKTAKE_EVENTS x) => x.SEQNO); }
        }

        IRepository<STOCKTAKE_TOTALS, int> IPrimeroEntitiesUnitOfWork.STOCKTAKE_TOTALS
        {
            get { return GetRepository(x => x.Set<STOCKTAKE_TOTALS>(), (STOCKTAKE_TOTALS x) => x.SEQNO); }
        }

        IRepository<SU_WARRANTY, int> IPrimeroEntitiesUnitOfWork.SU_WARRANTY
        {
            get { return GetRepository(x => x.Set<SU_WARRANTY>(), (SU_WARRANTY x) => x.SEQNO); }
        }

        IRepository<SU_WARRANTYTYPE, int> IPrimeroEntitiesUnitOfWork.SU_WARRANTYTYPE
        {
            get { return GetRepository(x => x.Set<SU_WARRANTYTYPE>(), (SU_WARRANTYTYPE x) => x.SEQNO); }
        }

        IRepository<SUPPLIER_STOCK_ITEMS, Tuple<string, string, int>> IPrimeroEntitiesUnitOfWork.SUPPLIER_STOCK_ITEMS
        {
            get { return GetRepository(x => x.Set<SUPPLIER_STOCK_ITEMS>(), (SUPPLIER_STOCK_ITEMS x) => Tuple.Create(x.STOCKCODE, x.SUPPLIERCODE, x.ACCNO)); }
        }

        IRepository<TASK_STATUSES, int> IPrimeroEntitiesUnitOfWork.TASK_STATUSES
        {
            get { return GetRepository(x => x.Set<TASK_STATUSES>(), (TASK_STATUSES x) => x.SEQNO); }
        }

        IRepository<TASK_TYPES, int> IPrimeroEntitiesUnitOfWork.TASK_TYPES
        {
            get { return GetRepository(x => x.Set<TASK_TYPES>(), (TASK_TYPES x) => x.SEQNO); }
        }

        IRepository<TASKS, int> IPrimeroEntitiesUnitOfWork.TASKS
        {
            get { return GetRepository(x => x.Set<TASKS>(), (TASKS x) => x.SEQNO); }
        }

        IRepository<TAX_KEY_POINT, int> IPrimeroEntitiesUnitOfWork.TAX_KEY_POINT
        {
            get { return GetRepository(x => x.Set<TAX_KEY_POINT>(), (TAX_KEY_POINT x) => x.SEQNO); }
        }

        IRepository<TAX_RATES, int> IPrimeroEntitiesUnitOfWork.TAX_RATES
        {
            get { return GetRepository(x => x.Set<TAX_RATES>(), (TAX_RATES x) => x.SEQNO); }
        }

        IRepository<TAX_RETURN, string> IPrimeroEntitiesUnitOfWork.TAX_RETURN
        {
            get { return GetRepository(x => x.Set<TAX_RETURN>(), (TAX_RETURN x) => x.TAXRETURNCODE); }
        }

        IReadOnlyRepository<TAX_RETURN_ALLOC> IPrimeroEntitiesUnitOfWork.TAX_RETURN_ALLOC
        {
            get { return GetReadOnlyRepository(x => x.Set<TAX_RETURN_ALLOC>()); }
        }

        IRepository<TAX_RETURN_CALC, Tuple<double, int>> IPrimeroEntitiesUnitOfWork.TAX_RETURN_CALC
        {
            get { return GetRepository(x => x.Set<TAX_RETURN_CALC>(), (TAX_RETURN_CALC x) => Tuple.Create(x.INV_ALLOCATED, x.PERIOD_SEQNO)); }
        }

        IRepository<TAX_RETURN_POINT, Tuple<string, string>> IPrimeroEntitiesUnitOfWork.TAX_RETURN_POINT
        {
            get { return GetRepository(x => x.Set<TAX_RETURN_POINT>(), (TAX_RETURN_POINT x) => Tuple.Create(x.TAXRETURNCODE, x.KEY_POINT)); }
        }

        IRepository<TAX_RETURN_TRANS, Tuple<string, string, int, string>> IPrimeroEntitiesUnitOfWork.TAX_RETURN_TRANS
        {
            get { return GetRepository(x => x.Set<TAX_RETURN_TRANS>(), (TAX_RETURN_TRANS x) => Tuple.Create(x.TAXRETURNCODE, x.DRCR, x.TRANSTYPE, x.ALLOCATED)); }
        }

        IRepository<TERRITORYBUDGET, int> IPrimeroEntitiesUnitOfWork.TERRITORYBUDGET
        {
            get { return GetRepository(x => x.Set<TERRITORYBUDGET>(), (TERRITORYBUDGET x) => x.SEQNO); }
        }

        IRepository<TERRITORYELEMENTS, int> IPrimeroEntitiesUnitOfWork.TERRITORYELEMENTS
        {
            get { return GetRepository(x => x.Set<TERRITORYELEMENTS>(), (TERRITORYELEMENTS x) => x.SEQNO); }
        }

        IRepository<TERRITORYHDR, int> IPrimeroEntitiesUnitOfWork.TERRITORYHDR
        {
            get { return GetRepository(x => x.Set<TERRITORYHDR>(), (TERRITORYHDR x) => x.SEQNO); }
        }

        IRepository<TIME_BILLING, int> IPrimeroEntitiesUnitOfWork.TIME_BILLING
        {
            get { return GetRepository(x => x.Set<TIME_BILLING>(), (TIME_BILLING x) => x.SEQNO); }
        }

        IRepository<USER_DEF_BANK_COLS, int> IPrimeroEntitiesUnitOfWork.USER_DEF_BANK_COLS
        {
            get { return GetRepository(x => x.Set<USER_DEF_BANK_COLS>(), (USER_DEF_BANK_COLS x) => x.SEQNO); }
        }

        IRepository<VERIFICATION_CLASS, string> IPrimeroEntitiesUnitOfWork.VERIFICATION_CLASS
        {
            get { return GetRepository(x => x.Set<VERIFICATION_CLASS>(), (VERIFICATION_CLASS x) => x.V_CLASS); }
        }

        IRepository<VERIFICATION_HDR, int> IPrimeroEntitiesUnitOfWork.VERIFICATION_HDR
        {
            get { return GetRepository(x => x.Set<VERIFICATION_HDR>(), (VERIFICATION_HDR x) => x.SEQNO); }
        }

        IRepository<VERIFICATION_LINES, int> IPrimeroEntitiesUnitOfWork.VERIFICATION_LINES
        {
            get { return GetRepository(x => x.Set<VERIFICATION_LINES>(), (VERIFICATION_LINES x) => x.SEQNO); }
        }

        IRepository<WEEK_DATES, int> IPrimeroEntitiesUnitOfWork.WEEK_DATES
        {
            get { return GetRepository(x => x.Set<WEEK_DATES>(), (WEEK_DATES x) => x.WEEK_NO); }
        }

        IRepository<WORKSORD_HDR, int> IPrimeroEntitiesUnitOfWork.WORKSORD_HDR
        {
            get { return GetRepository(x => x.Set<WORKSORD_HDR>(), (WORKSORD_HDR x) => x.SEQNO); }
        }

        IRepository<WORKSORD_LINES, int> IPrimeroEntitiesUnitOfWork.WORKSORD_LINES
        {
            get { return GetRepository(x => x.Set<WORKSORD_LINES>(), (WORKSORD_LINES x) => x.SEQNO); }
        }

        IRepository<WORKSORD_STATUS, int> IPrimeroEntitiesUnitOfWork.WORKSORD_STATUS
        {
            get { return GetRepository(x => x.Set<WORKSORD_STATUS>(), (WORKSORD_STATUS x) => x.STATUSNO); }
        }

        IRepository<X_ER_REPORT, int> IPrimeroEntitiesUnitOfWork.X_ER_REPORT
        {
            get { return GetRepository(x => x.Set<X_ER_REPORT>(), (X_ER_REPORT x) => x.SEQNO); }
        }

        IRepository<X_ER_SETUP, int> IPrimeroEntitiesUnitOfWork.X_ER_SETUP
        {
            get { return GetRepository(x => x.Set<X_ER_SETUP>(), (X_ER_SETUP x) => x.SEQNO); }
        }

        IRepository<X_HBIZ_NOTIFICATIONS, int> IPrimeroEntitiesUnitOfWork.X_HBIZ_NOTIFICATIONS
        {
            get { return GetRepository(x => x.Set<X_HBIZ_NOTIFICATIONS>(), (X_HBIZ_NOTIFICATIONS x) => x.ID); }
        }

        IRepository<X_HBIZ_PURCH_REQ_COMMENTS, int> IPrimeroEntitiesUnitOfWork.X_HBIZ_PURCH_REQ_COMMENTS
        {
            get { return GetRepository(x => x.Set<X_HBIZ_PURCH_REQ_COMMENTS>(), (X_HBIZ_PURCH_REQ_COMMENTS x) => x.SEQNO); }
        }

        IRepository<X_HBIZ_PURCH_REQ_HDR, int> IPrimeroEntitiesUnitOfWork.X_HBIZ_PURCH_REQ_HDR
        {
            get { return GetRepository(x => x.Set<X_HBIZ_PURCH_REQ_HDR>(), (X_HBIZ_PURCH_REQ_HDR x) => x.SEQNO); }
        }

        IRepository<X_HBIZ_PURCH_REQ_HDR_HISTORY, int> IPrimeroEntitiesUnitOfWork.X_HBIZ_PURCH_REQ_HDR_HISTORY
        {
            get { return GetRepository(x => x.Set<X_HBIZ_PURCH_REQ_HDR_HISTORY>(), (X_HBIZ_PURCH_REQ_HDR_HISTORY x) => x.SEQNO); }
        }

        IRepository<X_HBIZ_PURCH_REQ_LINES, int> IPrimeroEntitiesUnitOfWork.X_HBIZ_PURCH_REQ_LINES
        {
            get { return GetRepository(x => x.Set<X_HBIZ_PURCH_REQ_LINES>(), (X_HBIZ_PURCH_REQ_LINES x) => x.SEQNO); }
        }

        IRepository<X_HBIZ_PURCH_REQ_STATUS_VALUES, int> IPrimeroEntitiesUnitOfWork.X_HBIZ_PURCH_REQ_STATUS_VALUES
        {
            get { return GetRepository(x => x.Set<X_HBIZ_PURCH_REQ_STATUS_VALUES>(), (X_HBIZ_PURCH_REQ_STATUS_VALUES x) => x.SEQNO); }
        }

        IRepository<X_HBIZ_PURCH_REQ_SUBSCRIPTIONS, int> IPrimeroEntitiesUnitOfWork.X_HBIZ_PURCH_REQ_SUBSCRIPTIONS
        {
            get { return GetRepository(x => x.Set<X_HBIZ_PURCH_REQ_SUBSCRIPTIONS>(), (X_HBIZ_PURCH_REQ_SUBSCRIPTIONS x) => x.SEQNO); }
        }

        IRepository<X_HBIZ_REPORTS_TO_STAFF, int> IPrimeroEntitiesUnitOfWork.X_HBIZ_REPORTS_TO_STAFF
        {
            get { return GetRepository(x => x.Set<X_HBIZ_REPORTS_TO_STAFF>(), (X_HBIZ_REPORTS_TO_STAFF x) => x.SEQNO); }
        }

        IRepository<X_PURCH_REQ_COMMENTS_FILES, int> IPrimeroEntitiesUnitOfWork.X_PURCH_REQ_COMMENTS_FILES
        {
            get { return GetRepository(x => x.Set<X_PURCH_REQ_COMMENTS_FILES>(), (X_PURCH_REQ_COMMENTS_FILES x) => x.SEQNO); }
        }
    }
}
