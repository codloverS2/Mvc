// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// A type which is used to represent a property in a <see cref="ControllerModel"/>.
    /// </summary>
    [DebuggerDisplay("PropertyModel: Name={PropertyName}")]
    public abstract class PropertyModelBase : ICommonModel, IBindingModel
    {
        /// <summary>
        /// Gets any attributes which are annotated on the property.
        /// </summary>
        public IReadOnlyList<object> Attributes { get; protected set; }

        public IDictionary<object, object> Properties { get; protected set; }

        /// <summary>
        /// Gets or sets the <see cref="BindingInfo"/> associated with this model.
        /// </summary>
        public BindingInfo BindingInfo { get; set; }

        /// <summary>
        /// Gets the underlying <see cref="PropertyInfo"/>.
        /// </summary>
        public PropertyInfo PropertyInfo { get; protected set; }

        /// <summary>
        /// Gets or sets the name of the property represented by this model.
        /// </summary>
        public string PropertyName { get; set; }

        MemberInfo ICommonModel.MemberInfo => PropertyInfo;

        string ICommonModel.Name => PropertyName;
    }
}