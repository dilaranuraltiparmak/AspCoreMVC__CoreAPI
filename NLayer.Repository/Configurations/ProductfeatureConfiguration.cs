using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class ProductfeatureConfiguration : IEntityTypeConfiguration<Productfeature>
	{
		public void Configure(EntityTypeBuilder<Productfeature> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x=>x.Id).UseIdentityColumn();
			builder.HasOne(x => x.Product).WithOne(x => x.Productfeature).HasForeignKey<Productfeature>(x => x.ProductId);
		}
	}
}
