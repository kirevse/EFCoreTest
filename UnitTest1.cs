using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            using var efCoreDbContext = new EfCoreTestDbContext();

            var dependent = new Dependent();
            dependent.DependentAttributes.Add(new DependentAttribute());
            var parent = new Parent
            {
                Name = "Test 1"
            };
            parent.Dependents.Add(dependent);
            await efCoreDbContext.AddAsync(parent);
            await efCoreDbContext.SaveChangesAsync();

            (await efCoreDbContext.Parents.ToListAsync())
                .Should()
                .HaveCount(1)
                .And
                .BeEquivalentTo(parent);

            efCoreDbContext.Parents.Remove(await efCoreDbContext.Parents
                .Include(p => p.Dependents)
                .ThenInclude(d => d.DependentAttributes)
                .FirstOrDefaultAsync(p => p.Name == "Test 1"));
            await efCoreDbContext.SaveChangesAsync();

            (await efCoreDbContext.Parents.AnyAsync())
                .Should()
                .BeFalse();
        }
    }
}
