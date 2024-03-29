﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BackendTemplate.Infra.CrossCode
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetNoAbstractTypes(this Assembly assemblyFile)
        {
            var types = assemblyFile.GetTypes();
            var assemblyTypes = types.Where(t => !t.GetTypeInfo().IsAbstract);

            return assemblyTypes;
        }
    }
}
