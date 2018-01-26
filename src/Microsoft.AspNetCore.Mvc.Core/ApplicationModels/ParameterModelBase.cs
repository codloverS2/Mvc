// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc.ApplicationModels
{
    [DebuggerDisplay("ParameterModel: Name={ParameterName}")]
    public class ParameterModelBase : ICommonModel, IBindingModel
    {
        public IReadOnlyList<object> Attributes { get; protected set; }

        public IDictionary<object, object> Properties { get; protected set; }

        MemberInfo ICommonModel.MemberInfo => ParameterInfo.Member;

        string ICommonModel.Name => ParameterName;

        public ParameterInfo ParameterInfo { get; protected set; }

        public string ParameterName { get; set; }

        public BindingInfo BindingInfo { get; set; }
    }
}