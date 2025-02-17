﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Config
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            builder.Property(P => P.Name).HasMaxLength(100);
            
          
            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(P => P.ProductBrandId);

            builder.HasOne(P => P.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
        }
    }
}
