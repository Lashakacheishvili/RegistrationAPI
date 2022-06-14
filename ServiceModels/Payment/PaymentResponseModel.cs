using Common.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceModels.Payment
{
    public class PaymentRequestModel : BaseRequestModel
    {
        [NotMappedAttribute]
        public int Id { get; set; }
        [BindNever]
        public List<int> ChildId { get; set; }
    }
    public class PaymentResponseModel
    {
        public IEnumerable<PaymentItemModel> Payments { get; set; }
        public int TotalCount { get; set; }
    }
    public class PaymentItemModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        [JoinTableAttribute(JoinType = "LEFT", PropertyName = "ParrentId", TableName = "Users", TargetPropertyName = "Id", ColumnName = "Name")]
        public string ParrentName { get; set; }
        [JoinTableAttribute(JoinType = "LEFT", PropertyName = "ChildId", TableName = "Users", TargetPropertyName = "Id", ColumnName = "Name")]
        public string ChildName { get; set; }
        [JoinTableAttribute(JoinType = "LEFT", PropertyName = "ReasonId", TableName = "Reasons", TargetPropertyName = "Id", ColumnName = "Name")]
        public string ReasonName { get; set; }
    }
}
