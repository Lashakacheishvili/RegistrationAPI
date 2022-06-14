using Common.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServiceModels.Reason
{
    public class ReasonRequestModel: BaseRequestModel
    {
        [NotMappedAttribute]
        public int Id { get; set; }
        [BindNever]
        public List<int> ChildId { get; set; }
    }
    public class ReasonResponseModel
    {
        public IEnumerable<ReasonItemModel>  Reasons { get; set; }
        public int TotalCount { get; set; }
    }
    public class ReasonItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string DeleteDescription { get; set; }
        public bool Required { get; set; }
        public bool IsSuccessed { get; set; }
        [JoinTableAttribute(JoinType = "LEFT", PropertyName = "ParrentId", TableName = "Users", TargetPropertyName = "Id", ColumnName = "Name")]
        public string ParrentName { get; set; }
        [JoinTableAttribute(JoinType = "LEFT", PropertyName = "ChildId", TableName = "Users", TargetPropertyName = "Id", ColumnName = "Name")]
        public string ChildName { get; set; }
    }
}
