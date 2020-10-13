using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyPreUnity.Components
{
    interface IPropertyRentComponent
    {
        public int GetRent();
    }

    class RealEstateRentComponent : IPropertyRentComponent
    {
        private readonly PropertyDevelopmentComponent _propertyDevelopment;
        private readonly Func<int> _getRent;

        public int GetRent() =>
            _getRent();

        public RealEstateRentComponent(PropertyDevelopmentComponent propertyDevelopment, Func<int> getRent)
        {
            _propertyDevelopment = propertyDevelopment;
            _getRent = getRent;
        }
    }

    class UtilityRentComponent : IPropertyRentComponent
    {
        public int GetRent() =>
            throw new NotImplementedException();
    }
}
