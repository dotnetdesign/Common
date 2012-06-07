using System;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Defines the method signature of a method to handle the unregister callback.
    /// </summary>
    /// <typeparam name="TEventHandler"></typeparam>
    /// <param name="eventHandler">The event handler.</param>
    public delegate void UnregisterCallback<TEventHandler>(EventHandler<TEventHandler> eventHandler) where TEventHandler : EventArgs;
}