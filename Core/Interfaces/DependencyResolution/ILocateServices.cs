using System.Collections.Generic;

namespace Core.Interfaces.DependencyResolution
{
    public interface ILocateServices
    {
        /// <summary>
        /// This method returns the implementation of the given type. The 
        /// type should be an interface so that structure map can map it to 
        /// a concrete type.
        /// </summary>
        /// <returns>An implementation of the given type as registered in structure map.</returns>
        TType Get<TType>();

        /// <summary>
        /// This method returns the named implementation of the given type. The 
        /// type should be an interface so that structure map can map it to 
        /// a concrete type.
        /// </summary>
        /// <param name="name">The name of the dependency.</param>
        /// <returns>An implementation of the given type as registered in structure map.</returns>
        TType GetNamed<TType>(string name);

        /// <summary>
        /// Returns all instances of the given type.
        /// </summary>
        /// <typeparam name="TType">The target type.</typeparam>
        /// <returns>All registered instances of the given type.</returns>
        IEnumerable<TType> GetAllInstances<TType>();
    }
}
