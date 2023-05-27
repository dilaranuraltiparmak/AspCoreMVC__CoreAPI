using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasData(new Product
			{
				Id=1,
				CategoryId=1,
				Name="Kalem 1",
				Price=1300,
				Stock=20,
				CreatedDate=DateTime.Now,

			},
		new Product
		{
			Id=2,
			CategoryId=1,
			Name="Kalem 2",
				Price=10560,
			Stock=230,
			CreatedDate=DateTime.Now,

		}, new Product
		{
			Id=3,
			CategoryId=1,
			Name="Kitap 1",
			Price=10450,
			Stock=60,
			CreatedDate=DateTime.Now,

		},      new Product
		{
			Id=4,
			CategoryId=1,
			Name="Kitap 2",
			Price=1030,
			Stock=30,
			CreatedDate=DateTime.Now,

		});
		}
	}
}
