using System;

namespace Transfer.WebApi.CQRS
{
    //Marker
    [AttributeUsage(AttributeTargets.Class)]
    public class PublicContractAttribute : Attribute
    {
    }
}