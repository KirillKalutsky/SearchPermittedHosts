using test;
using FluentAssertions;

namespace TestSolution
{
    public class Tests
    {
        private string[] A = new[]
        {
            "unlock.microvirus.md",
            "visitwar.com",
            "visitwar.de",
            "fruonline.co.uk",
            "australia.open.com",
            "credit.card.us"
        };

        [Test]
        public void TestForbiddenAllHosts()
        {
            var B = new[] 
            { 
                "com",
                "microvirus.md",
                "visitwar.de",
                "co.uk",
                "credit.card.us"
            };
            var indexes = Solution.GetSolution(A, B);
            indexes.Should().BeEmpty();
        }

        [Test]
        public void TestForbiddenOnlyComHost()
        {
            var B = new[]
            {
                "com",
            };
            var indexes = Solution.GetSolution(A, B);
            indexes.Should().BeEquivalentTo(new[] { 0, 2, 3, 5});
        }

        [Test]
        public void TestForbiddenOnlyComOpenHost()
        {
            var B = new[]
            {
                "open.com",
            };
            var indexes = Solution.GetSolution(A, B);
            indexes.Should().BeEquivalentTo(new[] { 0, 1, 2, 3, 5 });
        }
    }
}