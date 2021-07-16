using System;

namespace BackendTemplate.Infra.CrossCode
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid guid)
        {
            bool resultado = (guid == Guid.Empty);
            return resultado;
        }

        public static bool IsNullOrEmpty(this Guid? guid)
        {
            bool resultado = (!guid.HasValue || guid.GetValueOrDefault(Guid.Empty) == Guid.Empty);
            return resultado;
        }
    }
}
