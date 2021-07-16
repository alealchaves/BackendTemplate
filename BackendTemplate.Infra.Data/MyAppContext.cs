using BackendTemplate.Domain.Core.Entities;
using BackendTemplate.Infra.Data.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace BackendTemplate.Infra.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Owned<EntityControl>();

            MapEntities(modelBuilder);
            DisableDeleteCascade(modelBuilder);

            SetDefaultDatabaseTypes(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SetDefaultDatabaseTypes(ModelBuilder modelBuilder)
        {

            //DateTime = datetime (no lugar de datetime2)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties().Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))))
                property.SetColumnType("datetime");

            //string = varchar (no lugar de nvarchar)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                var maxLength = property.GetMaxLength().HasValue ? property.GetMaxLength().ToString() : "max";
                property.SetColumnType($"varchar({maxLength})");
            }

            //Int64 = bigint
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties().Where(p => p.ClrType == typeof(long) || p.ClrType == typeof(long?))))
                property.SetColumnType("bigint");
        }

        private void DisableDeleteCascade(ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private void MapEntities(ModelBuilder modelBuilder)
        {
            Assembly modelInAssembly = typeof(Entity).Assembly;
            var exportedTypes = modelInAssembly.ExportedTypes;

            foreach (Type type in exportedTypes)
            {
                var attributes = type.GetCustomAttributes();
                if (!type.IsAbstract &&
                    !type.AssemblyQualifiedName.Contains("Base") &&
                    type.AssemblyQualifiedName.Contains("Entities") &&
                    IsInherance(type, typeof(Entity)))
                {
                    if (attributes.Any(a => a.GetType().Name.Equals("KeylessAttribute")))
                        modelBuilder.Entity(type).HasNoKey();
                    else
                        modelBuilder.Entity(type);
                }
            }
        }

        private bool IsInherance(Type classe, Type tipo)
        {
            if (classe.BaseType == null) return false;
            if (classe.BaseType.GetInterfaces().Contains(tipo) ||
                classe.BaseType == tipo)
            {
                return true;
            }
            else
            {
                return IsInherance(classe.BaseType, tipo);
            }
        }
    }
}
