

using System.Collections.Generic;
using System.Web.Mvc;

namespace Unity.AspNet.Mvc
{
    /// <summary>
    /// Defines a filter provider for filter attributes that support injection of Unity dependencies.
    /// </summary>
    public class UnityFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityFilterAttributeFilterProvider"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IUnityContainer"/> that will be used to inject the filters.</param>
        public UnityFilterAttributeFilterProvider(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Gets a collection of custom action attributes, and injects them using a Unity container.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>A collection of custom action attributes.</returns>
        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var list = base.GetActionAttributes(controllerContext, actionDescriptor);

            foreach (var item in list)
            {
                _container.BuildUp(item.GetType(), item);
            }

            return list;
        }

        /// <summary>
        /// Gets a collection of controller attributes, and injects them using a Unity container.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>A collection of controller attributes.</returns>
        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var list = base.GetControllerAttributes(controllerContext, actionDescriptor);

            foreach (var item in list)
            {
                _container.BuildUp(item.GetType(), item);
            }

            return list;
        }
    }
}
