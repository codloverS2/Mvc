﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// An <see cref="IModelBinderProvider"/> for binding header values.
    /// </summary>
    public class HeaderModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc />
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var bindingInfo = context.BindingInfo;
            if (bindingInfo.BindingSource == null
                || !bindingInfo.BindingSource.CanAcceptDataFrom(BindingSource.Header))
            {
                return null;
            }

            var modelMetadata = context.Metadata;
            var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();

            if (!IsSimpleType(modelMetadata))
            {
                var logger = loggerFactory.CreateLogger<HeaderModelBinderProvider>();
                logger.CannotCreateHeaderModelBinder(context.Metadata.ModelType);
                return null;
            }

            // Since we are delegating the binding of the current model type to other binders, modify the
            // binding source of the current model type to a non-FromHeader binding source in order to avoid an
            // infinite recursion into this binder provider.
            var nonFromHeaderBindingInfo = new BindingInfo(bindingInfo);
            nonFromHeaderBindingInfo.BindingSource = BindingSource.ModelBinding;

            var metadata = modelMetadata.GetMetadataForType(modelMetadata.ModelType);
            var innerModelBinder = context.CreateBinder(modelMetadata, nonFromHeaderBindingInfo);
            if (innerModelBinder == null)
            {
                return null;
            }

            return new HeaderModelBinder(loggerFactory, innerModelBinder);
        }

        // Support binding only to simple types or collection of simple types.
        private bool IsSimpleType(ModelMetadata modelMetadata)
        {
            var metadata = modelMetadata.ElementMetadata ?? modelMetadata;
            return !metadata.IsComplexType;
        }
    }
}
