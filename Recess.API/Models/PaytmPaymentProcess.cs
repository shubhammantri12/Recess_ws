using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class PaytmPaymentProcess
    {
        public string MID { get; set; }
        public string ORDER_ID { get; set; }
        public string CUST_ID { get; set; }
        public string WEBSITE { get; set; }
        public string INDUSTRY_TYPE_ID { get; set; }
        public string CHANNEL_ID { get; set; }
        public string MOBILE_NO { get; set; }
        public string EMAIL { get; set; }
        public string TXN_AMOUNT { get; set; }
        public string CALLBACK_URL { get; set; }
        public string TXNID { get; set; }
        public string BANKTXNID { get; set; }
        public string TXNAMOUNT { get; set; }
        public string CURRENCY { get; set; }
        public string STATUS { get; set; }
        public string RESPCODE { get; set; }
        public string RESPMSG { get; set; }
        public DateTime TXNDATE { get; set; }
        public string GATEWAYNAME { get; set; }
        public string BANKNAME { get; set; }
        public string PAYMENTMODE { get; set; }

    }
}